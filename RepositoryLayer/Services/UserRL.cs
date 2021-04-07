using CommanLayer.ResponseModel;
using CommonLayer.ResponseModel;
using CommonLayer.UserAccountException;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        
        readonly FundooApiContext userContext;
       
        

        private IConfiguration Configuration { get; }
        public UserRL(FundooApiContext context, IConfiguration configuration)
        {
            userContext = context;
            Configuration = configuration;
        
            
        }




        public bool Registration(ResponseUserAccount user)
        {
            if (!userContext.Accounts.Any(u => u.Email == user.Email))
            {
                UserAccount register = new UserAccount
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateOfBirth = user.DateOfBirth,
                    PhoneNumber = user.PhoneNumber,
                    Password = Password.ConvertToEncrypt(user.Password),
                    Creationtime = user.Creationtime = DateTime.Now,
                    Modificationtime = user.Modificationtime,
                  
                };

                userContext.Accounts.Add(register);
                userContext.SaveChanges();
                return true;
            }

            else
            {
               throw new UserAccountException(UserAccountException.ExceptionType.EMAIL_ALREADY_EXIST, "Email Id alredy exist");
            }
           
        }
        public UserAccount Login(LoginModule login)
        {
            try
            {

                if (userContext.Accounts.Any(U => U.Email.Equals(login.Email)))
                {
                    LoginModule login1 = new LoginModule
                    {
                        Email = login.Email,
                        Password = Password.ConvertToEncrypt(login.Password),
                    };

                    if (userContext.Accounts.Any(U => U.Email.Equals(login.Email)))
                    {
                        UserAccount searchLogin = userContext.Accounts
                          .Where(e => e.Email.Equals(login1.Email) && e.Password.Equals(login1.Password)).FirstOrDefault(e => e.Email == login.Email);
                        searchLogin = new UserAccount
                        {
                            UserId = searchLogin.UserId,
                            FirstName = searchLogin.FirstName,
                            LastName = searchLogin.LastName,
                            Email = searchLogin.Email,
                            DateOfBirth = searchLogin.DateOfBirth,
                            PhoneNumber = searchLogin.PhoneNumber,
                            Password = Password.ConvertToEncrypt(searchLogin.Password),
                            Creationtime = searchLogin.Creationtime,
                            Modificationtime = searchLogin.Modificationtime,
                        };
                        return searchLogin;
                    }
                    else
                        throw new UserAccountException(UserAccountException.ExceptionType.WRONG_PASSWORD, "wrong password");
                }
                else
                {
                    throw new UserAccountException(UserAccountException.ExceptionType.EMAIL_DONT_EXIST, "email address is not registered");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

            public string GenerateToken(UserAccount login)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[] {

                  new Claim("UserId", login.UserId.ToString()),
                  new Claim("Email", login.Email),

              };
                var token = new JwtSecurityToken(Configuration["Jwt:Issuer"],
                   Configuration["Jwt:Issuer"],
                   claims,
                   expires: DateTime.Now.AddMinutes(120),
                   signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        
            public forgetclass ForgetPassword(ForgetPasswordModel forgetPasswordModel)
            {

                ForgetModel forget = new ForgetModel();
                UserAccount login = new UserAccount();
                login.Email = forgetPasswordModel.Email;
                UserAccount validateEmail = userContext.Accounts
                          .Where(e => e.Email.Equals(login.Email)).FirstOrDefault(e => e.Email == login.Email);
                var jwt = GenerateToken(validateEmail);
                forget.JwtToken = jwt;


                if (validateEmail != null)
                {
                    var model1 = new ForgetPasswordModel { Email = forgetPasswordModel.Email };
                    var model2 = new ForgetModel { JwtToken = forget.JwtToken };
                    var model = new forgetclass { Email = model1.Email, JwtToken = model2.JwtToken };
                    return model;
                }
                else
                {
                    throw new UserAccountException(UserAccountException.ExceptionType.EMAIL_DONT_EXIST, "email address is not registered");
                }


            }
        


        public bool ResetAccountPassword(ResetPasswordModel reset, string Email)
        {
            try
            {

                ResetPasswordModel password = new ResetPasswordModel

                {
                    NewPassword = reset.NewPassword,
                    ConfirmPassword = reset.ConfirmPassword,
                };

                if (password.NewPassword == password.ConfirmPassword)
                {
                    var dbLogin = userContext.Accounts.FirstOrDefault(u => u.Email.Equals(Email));
                    var newPassword = Password.ConvertToEncrypt(reset.ConfirmPassword);
                    dbLogin.Password = newPassword;
                    userContext.Entry(dbLogin).Property(x => x.Password).IsModified = true;
                    userContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception )
            {
                throw;
            }

        }
        
    }

}

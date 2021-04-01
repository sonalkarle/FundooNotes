using CommanLayer.ResponseModel;
using CommonLayer.ResponseModel;
using CommonLayer.UserAccountException;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.ContextFile;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using UserContext = RepositoryLayer.ContextFile.UserContext;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        readonly UserContext userContext;
        private IConfiguration Configuration { get; }
        public UserRL(UserContext context, IConfiguration configuration)
        {
            userContext = context;
            Configuration = configuration;
        }


        

        public bool Registration(User user)
        {

            User register = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                Password = Password.ConvertToEncrypt(user.Password),
                Creationtime = user.Creationtime,
                Modificationtime = user.Modificationtime,
            };




            userContext.Account.Add(register);
            userContext.SaveChanges();

            if (userContext.Account
                  .FirstOrDefault(e => e.UserId == register.UserId) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User Login(LoginModule login)
        {

            LoginModule login1 = new LoginModule
            {
                Email = login.Email,
                Password = Password.ConvertToEncrypt(login.Password),
            };


            User searchLogin = userContext.Account
                  .Where(e => e.Email.Equals(login1.Email) && e.Password.Equals(login1.Password)).FirstOrDefault(e => e.Email == login.Email);


            if (searchLogin != null)
            {
                searchLogin = new User { UserId = searchLogin.UserId, Email = searchLogin.Email };
                return searchLogin;
            }
            else
            {
                return searchLogin;
            }

        }
        public string GenerateToken(User login)
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
            User login = new User();
            login.Email = forgetPasswordModel.Email;
            User validateEmail = userContext.Account
                  .Where(e => e.Email.Equals(login.Email)).FirstOrDefault(e => e.Email == login.Email);
            var jwt = GenerateToken(validateEmail);
            forget.JwtToken = jwt;


            if (validateEmail != null)
            {
                var model1 = new ForgetPasswordModel { Email = forgetPasswordModel.Email};
                var model2 = new ForgetModel { JwtToken = forget.JwtToken };
                var model = new forgetclass { Email = model1.Email, JwtToken = model2.JwtToken };
                return model ;
            }
            else
            {
                return null;
            }
        }





        public bool ResetAccountPassword(ResetPasswordModel reset,string Email)
        {
            
            ResetPasswordModel password = new ResetPasswordModel
           
            {
                NewPassword = reset.NewPassword,
                ConfirmPassword = reset.ConfirmPassword,
            };

            if (password.NewPassword == password.ConfirmPassword)
            {
                var dbLogin = userContext.Account.FirstOrDefault(u => u.Email.Equals(Email));
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








    }

}

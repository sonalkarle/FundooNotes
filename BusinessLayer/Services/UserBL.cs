using BusinessLayer.Interface;

using CommanLayer.ResponseModel;
using CommonLayer.ResponseModel;
using CommonLayer.UserAccountException;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL

    {
        readonly UserDetailValidation userDetailValidation;

        IUserRL userRL;
       

        public UserBL(IUserRL userRL, IConfiguration config)
        {
            userDetailValidation = new UserDetailValidation();
      

            this.userRL = userRL;
        }


      

        public bool Registration(ResponseUserAccount user)
        {
            try
            {

                if (userDetailValidation.ValidateFirstName(user.FirstName) &&
              userDetailValidation.ValidateLastName(user.LastName) &&
              userDetailValidation.ValidateEmailAddress(user.Email) &&
              userDetailValidation.ValidatePassword(user.Password))
                {

                    return this.userRL.Registration(user);
                }
                else
                {
                    throw new UserDetailException(UserDetailException.ExceptionType.ENTERED_INVALID_USER_DETAILS, "user details are invalid");
                }
            }
            catch (Exception)
            {
                throw;
            }            

        
        }

        public string GenerateToken(UserAccount login)
        {
            try
            {

                return this.userRL.GenerateToken(login);                 //throw exceptions
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public forgetclass ForgetPassword(ForgetPasswordModel forgetPasswordModel)
        {
            try
            {

                return this.userRL.ForgetPassword(forgetPasswordModel);                 //throw exceptions
            }

            catch (Exception)
            {
                throw;
            }
        }
        public UserAccount Login(LoginModule user)
        {
            try
            {
                if (userDetailValidation.ValidateEmailAddress(user.Email) &&
                userDetailValidation.ValidatePassword(user.Password))
                {
                    return userRL.Login(user);
                }
                else
                {
                    throw new UserDetailException(UserDetailException.ExceptionType.ENTERED_INVALID_USER_DETAILS, "user details are invalid");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Resets the account password when password is known
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="UserDetailException">New and comfirm password do not match</exception>
        public bool ResetAccountPassword(ResetPasswordModel user,string Email)
        {

            try
            {
                if (userDetailValidation.ValidatePassword(user.NewPassword))

                {
                    return userRL.ResetAccountPassword(user, Email);
                }
                else
                {
                    throw new UserDetailException(UserDetailException.ExceptionType.CONFIRM_PASSWORD_DO_NO_MATCH, "New and comfirm password do not match");
                }
                }
            catch (Exception)
            {
                throw;
            }
        }

    }



    
}

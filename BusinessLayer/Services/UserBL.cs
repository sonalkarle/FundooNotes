using BusinessLayer.Interface;
using CommanLayer.ResponseModel;
using CommonLayer.ResponseModel;
using CommonLayer.UserAccountException;
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
        IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }


       

        public bool Registration(UserAccount user)
        {
            try
            {

                return this.userRL.Registration(user);                 //throw exceptions
            }

            catch (Exception e)
            {
                throw e;
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

            catch (UserAccountException)
            {
                throw new UserAccountException(UserAccountException.ExceptionType.EMAIL_DONT_EXIST, "Email Doesnt exist");
            }
        }
        public UserAccount Login(LoginModule login)
        {
            try
            {

                return this.userRL.Login(login);                 //throw exceptions
            }

            catch (Exception ex)
            {
                throw ex;
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

            {
                return userRL.ResetAccountPassword(user,Email);
            }

        }



    }
}

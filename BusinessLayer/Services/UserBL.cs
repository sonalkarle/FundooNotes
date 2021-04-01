using BusinessLayer.Interface;
using CommanLayer.ResponseModel;
using CommonLayer.ResponseModel;
using Microsoft.IdentityModel.Tokens;
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


       

        public bool Registration(User user)
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

        public string GenerateToken(User login)
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

            catch (Exception e)
            {
                throw e;
            }
        }
        public User Login(LoginModule login)
        {
            try
            {

                return this.userRL.Login(login);                 //throw exceptions
            }

            catch (Exception e)
            {
                throw e;
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

using CommanLayer.ResponseModel;
using CommonLayer.ResponseModel;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {


        public bool Registration(UserAccount user);
        public UserAccount Login(LoginModule user);
        public string GenerateToken(UserAccount login);
        public bool ResetAccountPassword(ResetPasswordModel user,string Email);
        public forgetclass ForgetPassword(ForgetPasswordModel forgetPasswordModel);



    }
}

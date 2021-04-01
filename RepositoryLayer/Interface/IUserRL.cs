using CommanLayer.ResponseModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {

     

        public bool Registration(User user);

        public User Login(LoginModule user);

        public string GenerateToken(User login);

        public bool ResetAccountPassword(ResetPasswordModel user,string Email);


        public forgetclass ForgetPassword(ForgetPasswordModel forgetPasswordModel);











    }
}

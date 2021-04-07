using CommanLayer.ResponseModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {

  
        public bool Registration(ResponseUserAccount user);

        public UserAccount Login(LoginModule user);

        public string GenerateToken(UserAccount login);

        public bool ResetAccountPassword(ResetPasswordModel user,string Email);


        public forgetclass ForgetPassword(ForgetPasswordModel forgetPasswordModel);





    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Metadata;
using CommonLayer.ResponseModel;
using RepositoryLayer.Services;
using CommanLayer.ResponseModel;

namespace RepositoryLayer.ContextFile
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions option) : base(option)
        {


        }

         public virtual DbSet<User> Account { get; set; }
       public virtual DbSet<Note> Note { get; set; }


    }
    

}




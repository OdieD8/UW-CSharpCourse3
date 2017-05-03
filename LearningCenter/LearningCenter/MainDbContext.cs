using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LearningCenter.Models;

namespace LearningCenter
{
    public class MainDbContext : DbContext
    {
        public MainDbContext() : base("name=Entities")
        {
        }

        public DbSet<User> User { get; set; }
    }
}
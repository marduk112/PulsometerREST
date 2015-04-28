using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using REST.Models;

namespace REST.Migrations
{
    public class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}
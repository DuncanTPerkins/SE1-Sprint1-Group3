using FormBuilderApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FormBuilderApp.DataContexts
{
    public class StudentCourseDb : DbContext
    {
        public StudentCourseDb()
            : base("DefaultConnection")
        {

        }

        public DbSet<Form> form { get; set; }
        public DbSet<Workflow> flow { get; set; }
        public DbSet<Positions> position { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FormBuilderApp.Models
{
    public class User
    {
        [Key]
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Email; 

        public int AuthLevel { get; set; }
    }
}
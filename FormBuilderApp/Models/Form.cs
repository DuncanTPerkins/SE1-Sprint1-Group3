using System;
using System.Web;
using System.Collections.Generic;
namespace FormBuilderApp.Models
{
    public class Form
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean IsCompleted { get; set; }
        public Boolean IsAccepted { get; set; }

        public ICollection<InputField> InputFields { get; set; } 
    }
}

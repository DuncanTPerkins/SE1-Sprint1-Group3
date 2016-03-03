using System;
using System.Web;
using System.Collections.Generic;
namespace FormBuilderApp.Models
{
    public class Form
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public enum FormStatus { Draft = 1, Completed, Accepted }
        public FormStatus Status { get; set; }

        public ICollection<InputField> InputFields { get; set; } 
    }
}

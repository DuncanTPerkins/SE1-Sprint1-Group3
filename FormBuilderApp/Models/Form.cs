using System;
using System.Web;

namespace FormBuilderApp.Models
{
    public class Form
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Checkbox { get; set; }
    }
}

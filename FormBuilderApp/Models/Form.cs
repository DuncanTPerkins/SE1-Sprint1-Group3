using System;
using System.Web;
using System.Collections.Generic;
namespace FormBuilderApp.Models
{
    public class Form
    {
        public int Id { get; set; }
        public String UserId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public enum FormStatus { Template = 1, Draft, Completed, Accepted }
        public FormStatus Status { get; set; }
        public string FormData { get; set; }
    }
}

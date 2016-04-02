using System;
using System.Web;
using System.Collections.Generic;
namespace FormBuilderApp.Models
{
    public class Form
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public enum FormStatus { Template = 1, Draft = 2, Completed = 4, Accepted = 8 }
        public FormStatus Status { get; set; }
        public string FormData { get; set; }
        public ICollection<Positions> Workflow { get; set; }
        public virtual Positions flow { get; set; }
    }
}

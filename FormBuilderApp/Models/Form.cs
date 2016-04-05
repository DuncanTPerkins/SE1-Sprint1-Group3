using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormBuilderApp.Models
{
    public class Form
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public enum FormStatus { Template = 1, Draft = 2, Completed = 4, Accepted = 8, Denied = 16 }
        public FormStatus Status { get; set; }
        public string FormData { get; set; }

        public int WorkflowId { get; set; }
        public virtual Workflow flow { get; set; }
    }
}

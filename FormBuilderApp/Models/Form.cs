using System;
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
        public String UserId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public enum FormStatus { Template = 1, Draft = 2, Completed = 4, Accepted = 8, Denied = 16 }
        public FormStatus Status { get; set; }
        public string FormData { get; set; }
<<<<<<< HEAD

        public int? flowId { get; set; }
=======
        public String FormObjectRepresentation { get; set; }
        public int WorkflowId { get; set; }
>>>>>>> 0f4982b57b7253696dc3e1732ffccb28e127927a
        public virtual Workflow flow { get; set; }
    }
}

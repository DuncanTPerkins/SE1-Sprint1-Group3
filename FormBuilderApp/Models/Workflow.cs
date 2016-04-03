using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FormBuilderApp.Models
{
    public class Workflow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
<<<<<<< HEAD
        public int FlowId { get; set; }
=======
        public string FormId { get; set; }

>>>>>>> 07d90c30dd4851bf41c2900a1864b52812ea1382
        public ICollection<Positions> Positions { get; set; }

        public virtual Positions Input { get; set; }
    }
}
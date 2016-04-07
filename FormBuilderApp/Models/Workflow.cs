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
        public int FlowId { get; set; }

        public int FormId { get; set; }
        public ICollection<Positions> Positions { get; set; }
        public virtual Positions Input { get; set; }
    }
}
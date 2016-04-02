using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormBuilderApp.Models
{
    public class Workflow
    {
        public string FormId { get; set; }
        public ICollection<Positions> Positions { get; set; }
        public virtual Positions Input { get; set; }
    }
}
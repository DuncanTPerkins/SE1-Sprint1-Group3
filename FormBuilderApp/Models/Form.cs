using System;
using System.Web;
using System.Collections.Generic;
using System.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.Owin;

namespace FormBuilderApp.Models
{
    public class Form
    {
        [Key]
        public int Id { get; set; }
        public String UserId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public enum FormStatus { Template = 1, Draft, Completed, Accepted }
        public FormStatus Status { get; set; }
        public string FormData { get; set; }
        public List<String> Users { get; set; }
        public List<String> FormObjectRepresentation { get; set; }
    }
}

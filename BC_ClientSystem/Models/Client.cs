using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BC_ClientSystem.Models
{
    public class Client
    {
        public int ClientId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Client Code")]
        public string ClientCode { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
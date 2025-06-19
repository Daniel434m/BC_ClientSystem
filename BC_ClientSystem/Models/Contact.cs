using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BC_ClientSystem.Models
{
    public class Contact
    {
        public int ContactId { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MSA2021.Model
{
    public class User
    {

        [Key]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Address { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

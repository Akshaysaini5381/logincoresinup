using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace logincoresinup.Models
{
    public class Emplogin
    {
        [Key]
        public  int UserId { get; set; }
        [Required]
        public  string UserName { get; set; }
        [Required]
        public  string UserPassword { get; set; }
    }
}

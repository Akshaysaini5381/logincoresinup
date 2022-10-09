using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace logincoresinup.Models
{
    public class Employee
    {
        [Key]
        
        public int EmpId { get; set; }
        [Required]
        public string EmpName { get; set; }
        [Required]
        public string EmpDepartment { get; set; }
        [Required]
        public int Empsalary { get; set; }
        [Required]
        public string TL { get; set; }
        [Required]
        public string FM { get; set; }
    }
}

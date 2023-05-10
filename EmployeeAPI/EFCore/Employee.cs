using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeAPI.EFCore
{
    [Table("employee")]
    public class Employee
    {
        [Key, Required]
        public int id { get; set; }
        public string name { get; set; }
        public string jabatan { get; set; }
    }
}

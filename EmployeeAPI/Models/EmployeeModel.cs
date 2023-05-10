using System.ComponentModel;

namespace EmployeeAPI.Models
{
    public class EmployeeModel
    {
        [ReadOnly(true)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Jabatan { get; set; }
    }
}

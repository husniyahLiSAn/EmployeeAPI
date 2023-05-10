using EmployeeAPI.EFCore;
using EmployeeAPI.Models;

namespace EmployeeAPI.Repositories
{
    public class EmployeeRepository
    {
        private DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }
        /// <summary>
        /// GET
        /// </summary>
        /// <returns></returns>
        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> response = new List<EmployeeModel>();
            var dataList = _context.Employees.ToList();
            dataList.ForEach(row => response.Add(new EmployeeModel()
            {
                Id = row.id,
                Name = row.name,
                Jabatan = row.jabatan
            }));
            return response;
        }

        public EmployeeModel GetEmployeeById(int id)
        {
            EmployeeModel response = new EmployeeModel();
            var row = _context.Employees.Where(d => d.id.Equals(id)).FirstOrDefault();
            return new EmployeeModel()
            {
                Id = row.id,
                Name = row.name,
                Jabatan = row.jabatan
            };
        }
        /// <summary>
        /// It serves the POST/PUT/PATCH
        /// </summary>
        public void SaveEmployee(EmployeeModel model)
        {
            Employee dbTable = new Employee();
            if (model.Id > 0)
            {
                //PUT
                dbTable = _context.Employees.Where(d => d.id.Equals(model.Id)).FirstOrDefault();
                if (dbTable != null)
                {
                    dbTable.name = model.Name;
                    dbTable.jabatan = model.Jabatan;
                }
            }
            else
            {
                //POST
                dbTable.name = model.Name;
                dbTable.jabatan = model.Jabatan.ToString();
                _context.Employees.Add(dbTable);
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="id"></param>
        public void DeleteEmployee(int id)
        {
            var employee = _context.Employees.Where(d => d.id.Equals(id)).FirstOrDefault();
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
        }
    }
}

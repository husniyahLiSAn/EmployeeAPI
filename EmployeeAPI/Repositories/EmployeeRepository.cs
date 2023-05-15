using EmployeeAPI.EFCore;
using EmployeeAPI.Models;
using EmployeeAPI.Repositories.Interface;

namespace EmployeeAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
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
        public int SaveEmployee(EmployeeModel model)
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
                    _context.SaveChanges();

                    return dbTable.id;
                }
                return 0;
            }
            else
            {
                //POST
                dbTable.name = model.Name;
                dbTable.jabatan = model.Jabatan.ToString();
                _context.Employees.Add(dbTable);
                _context.SaveChanges();

                return dbTable.id;
            }
        }
        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="id"></param>
        public int DeleteEmployee(int id)
        {
            var employee = _context.Employees.Where(d => d.id.Equals(id)).FirstOrDefault();
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();

                return id;
            }
            return 0;
        }
    }
}

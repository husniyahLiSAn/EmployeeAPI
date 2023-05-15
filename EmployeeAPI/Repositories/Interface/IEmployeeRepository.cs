using EmployeeAPI.Models;

namespace EmployeeAPI.Repositories.Interface
{
    public interface IEmployeeRepository
    {
        List<EmployeeModel> GetEmployees();
        EmployeeModel GetEmployeeById(int id);
        int SaveEmployee(EmployeeModel model);
        int DeleteEmployee(int id);
    }
}

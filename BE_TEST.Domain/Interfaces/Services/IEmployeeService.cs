using BE_TEST.Domain.Base;
using BE_TEST.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BE_TEST.Domain.Interfaces.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        Task<Employee> GetById(int id);
        Task AddEmployee(Employee employee);
        Task EditEmployee(Employee employee);
        Task Delete(int id);
    }
}

using AutoMapper;
using BE_TEST.Domain.Entities;
using BE_TEST.Domain.Interfaces;
using BE_TEST.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_TEST.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }   

        public async Task AddEmployee(Employee employee)
        {
            _unitOfWork.CommandEmployeeRepository.Add(employee);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = _unitOfWork.QueryEmployeeRepository.GetById(id);
            _unitOfWork.CommandEmployeeRepository.TemporaryDelete(entity);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EditEmployee(Employee employee)
        {
            employee.UpdatedAt = DateTime.Now;

            _unitOfWork.CommandEmployeeRepository.Update(employee);
            await _unitOfWork.SaveChangesAsync();
        }

        public IEnumerable<Employee> GetAll()
        {
            return _unitOfWork.QueryEmployeeRepository.GetAll();
        }

        public async Task<Employee> GetById(int id)
        {
            return await _unitOfWork.QueryEmployeeRepository.GetByIdAsync(id);
        }
    }
}

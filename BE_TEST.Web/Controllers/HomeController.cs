using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using BE_TEST.Domain.Entities;
using BE_TEST.Domain.Interfaces;
using BE_TEST.Domain.Interfaces.Repositories;
using BE_TEST.Domain.Interfaces.Services;
using BE_TEST.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BE_TEST.Web.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IEmployeeService _employeeService;
        private readonly INotyfService _notyf;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration,
            IUnitOfWork unitOfWork, IEmployeeService employeeService,
            INotyfService notyf, IMapper mapper)
        {
            _logger = logger;
            _configuration = configuration;
            _employeeService = employeeService;
            _notyf = notyf;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            ViewBag.Employees = _employeeService.GetAll();
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var newEmployee = _mapper.Map<Employee>(employee);
                await _employeeService.AddEmployee(newEmployee);
                _notyf.Success("Successfully added employee");
            }
            else
            {
                _notyf.Warning("Employee already");
            }
            
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.Delete(id);
            _notyf.Success("Successfully deleted employee");
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public async Task<IActionResult> FindEmployee(int id)
        {
            var employee = await _employeeService.GetById(id);

            return new JsonResult(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                var oldEmployee = await _employeeService.GetById(employee.Id);
                _mapper.Map(employee, oldEmployee);
                await _employeeService.EditEmployee(oldEmployee);
                _notyf.Success("Successfully edited employee");
            }
            else
            {
                _notyf.Warning("Employee already");
            }
            
            return Redirect("/Home/Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
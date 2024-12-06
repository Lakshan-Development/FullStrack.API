using FullStrack.API.Database;
using FullStrack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStrack.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly FullStrackDbContext _fullStrackDbContext;
        public EmployeesController(FullStrackDbContext fullStrackDbContext)
        {
            _fullStrackDbContext = fullStrackDbContext;
        }

        [HttpGet]

        public async Task <IActionResult> GetAllEmployees()
        {
            var employees = await _fullStrackDbContext.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await _fullStrackDbContext.Employees.AddAsync(employeeRequest);
            await _fullStrackDbContext.SaveChangesAsync();

            return Ok(employeeRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEmployee([FromRoute]Guid id)
        {
          var employee =  await _fullStrackDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee updateEmployeeRequest)
        {
            var employee = await _fullStrackDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = updateEmployeeRequest.Name;
            employee.Email = updateEmployeeRequest.Email;
            employee.Salary = updateEmployeeRequest.Salary;
            employee.Mobile = updateEmployeeRequest.Mobile;
            employee.Department = updateEmployeeRequest.Department;

            await _fullStrackDbContext.SaveChangesAsync();
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStrackDbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _fullStrackDbContext.Employees.Remove(employee);
            await _fullStrackDbContext.SaveChangesAsync();

            return Ok(employee);
        }
    }
}

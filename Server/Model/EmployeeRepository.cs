using EmployeeManagementBlazor.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementBlazor.Server.Model
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext cxt;

        public EmployeeRepository(AppDbContext cxt)
        {
            this.cxt = cxt;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            if (employee.Department != null)
            {
                cxt.Entry(employee.Department).State = EntityState.Unchanged;
            }
            var result = await cxt.Employees.AddAsync(employee);
            await cxt.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteEmployee(int employeeId)
        {
            var employee = await cxt.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
            if (employee != null)
            {
                cxt.Employees.Remove(employee);
                await cxt.SaveChangesAsync();
            }
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            return await cxt.Employees
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.EmployeeId == employeeId);
        }

        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            return await cxt.Employees
                .Include(x => x.Department)
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await cxt.Employees.Include(x => x.Department).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> employees = cxt.Employees;
            if (!string.IsNullOrEmpty(name))
            {
                employees = employees.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name));
            }

            if (gender != null)
            {
                employees = employees.Where(x => x.Gender == gender);
            }

            return await employees.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var emp = await cxt.Employees.SingleOrDefaultAsync(x => x.EmployeeId == employee.EmployeeId);
            if (emp != null)
            {
                emp.DateOfBrith = employee.DateOfBrith;
                if (employee.DepartmentId != 0)
                {
                    emp.DepartmentId = employee.DepartmentId;
                }
                else if (employee.Department != null)
                {
                    emp.DepartmentId = employee.Department.DepartmentId;
                }
                
                emp.Email = employee.Email;
                emp.FirstName = employee.FirstName;
                emp.Gender = employee.Gender;
                emp.LastName = employee.LastName;
                emp.PhotoPath = employee.PhotoPath;

                await cxt.SaveChangesAsync();
            }
            return emp;
        }


    }
}

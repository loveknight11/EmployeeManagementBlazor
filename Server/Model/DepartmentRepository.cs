using EmployeeManagementBlazor.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementBlazor.Server.Model
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext cxt;

        public DepartmentRepository(AppDbContext cxt)
        {
            this.cxt = cxt;
        }
        public async Task<Department> GetDepartment(int departmentId)
        {
            return await cxt.Departments.FirstOrDefaultAsync(x => x.DepartmentId == departmentId);
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await cxt.Departments.ToListAsync();
        }
    }
}

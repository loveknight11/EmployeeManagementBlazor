using EmployeeManagementBlazor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EmployeeManagementBlazor.Client.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public Task<Department> GetDepartment(int departmentId)
        {
            return httpClient.GetFromJsonAsync<Department>($"api/departments/{departmentId}");
        }

        public Task<IEnumerable<Department>> GetDepartments()
        {
            return httpClient.GetFromJsonAsync<IEnumerable<Department>>($"api/departments");
        }
    }
}

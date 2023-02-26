using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace EmployeeManagementBlazor.Client.Services
{
    public class EmployeeAdapter : DataAdaptor
    {
        private readonly IEmployeeService employeeService;

        public EmployeeAdapter(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
        {
            var data = await employeeService.GetEmployees(dataManagerRequest.Skip, dataManagerRequest.Take);
            
            DataResult dataResult = new DataResult() { 
            Count = data.Count,
            Result = data.Employees
            };
            return dataResult;
        }
    }
}

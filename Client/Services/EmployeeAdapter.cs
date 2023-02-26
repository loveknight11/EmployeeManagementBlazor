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
            string Sort = null;
            if (dataManagerRequest.Sorted != null)
            {
                dataManagerRequest.Sorted.Reverse();
                Sort = string.Join(",", dataManagerRequest.Sorted.Select(x => $"{x.Name} {x.Direction}"));
            }
            
            var data = await employeeService.GetEmployees(dataManagerRequest.Skip, dataManagerRequest.Take, Sort);
            
            DataResult dataResult = new DataResult() { 
            Count = data.Count,
            Result = data.Employees
            };
            return dataResult;
        }
    }
}

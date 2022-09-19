using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public interface IEmployee : IRepository<Employee>
    {
        void Insert(Employee employee);
        void Update(Employee employee);
        List<Employee> GetAllEmployees();
        Employee GetByName(string name);
    }
}

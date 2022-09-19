using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataService
{
    public class EmployeeRepo : Repository<Employee>, IEmployee
    {
        HRContext _context;
        public EmployeeRepo(HRContext context) : base(context)
        {
            _context = context;
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }
     
        public Employee GetByName(string name)
        {
            return _context.Employees.Where(em => em.Name == name).FirstOrDefault();
        }

        public Employee GetEmployee(int employeeId)
        {
            return _context.Employees.Find(employeeId);
        }

        public void Insert(Employee employee)
        {
            _context.Employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}

using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public class UnitOfWork : IUnitOfWork
    {
        HRContext _context;
        public IEmployee Employee { get; private set; }
        public IAttendence Attendence { get; private set; }
        public UnitOfWork(HRContext context)
        {
            Employee = new EmployeeRepo(context);
            Attendence = new AttendenceRepo(context);
            _context = context;
        }

        public int Complete()
        {
           return _context.SaveChanges();
        }
    }
}

using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService
{
    public class AttendenceRepo : Repository<Attendence>, IAttendence
    {
        HRContext _context;
        public AttendenceRepo(HRContext context) : base(context)
        {
            _context = context;
        }

        public List<Attendence> GetAttendencesByEmpID(int empId)
        {
            return _context.Attendences.Where(a => a.Employee.Id == empId).ToList();
        }

        public void Insert(Attendence attendence)
        {
            _context.Attendences.Add(attendence);
        }

        public void Update(Attendence attendence)
        {
            _context.Attendences.Update(attendence);
        }
    }
}

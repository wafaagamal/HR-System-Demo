using DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public interface IAttendence
    {
        List<Attendence> GetAttendencesByEmpID(int empId);
        void Insert(Attendence attendence);
        void Update(Attendence attendence);

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public interface IUnitOfWork
    {
        IEmployee Employee { get; }
        IAttendence Attendence { get; }

        int Complete();
    }
}

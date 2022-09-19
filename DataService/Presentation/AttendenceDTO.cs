using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public class AttendenceDTO
    {
        public int Id { get; set; }
        public DateTime SignIn { get; set; }
        public DateTime SignOut { get; set; }
        public int WorkingHours { get; set; }
        public DateTime Date { get; set; }
        public EmployeeDTO Employee { get; set; }

    }
}

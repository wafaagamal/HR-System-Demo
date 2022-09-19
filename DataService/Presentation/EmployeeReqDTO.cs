using System;
using System.Collections.Generic;
using System.Text;

namespace DataService
{
    public class EmployeeReqDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public DateTime BirthDate { get; set; }
        public int MangerId { get; set; }
    }
}

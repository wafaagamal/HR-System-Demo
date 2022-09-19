using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess
{
    public class Attendence
    {
        [Key]
        public int Id { get; set; }
        public DateTime SignIn { get; set; }
        public DateTime SignOut { get; set; }
        public int WorkingHours { get; set; }
        public DateTime Date { get; set; }
        public virtual Employee Employee { get; set; }
    }
}

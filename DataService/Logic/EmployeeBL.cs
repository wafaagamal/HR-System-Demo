using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataService
{
    public class EmployeeBL
    {
        IUnitOfWork _uow;
        public EmployeeBL(IUnitOfWork uow)
        {
            _uow = uow;
        }
        private int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Now;
            int age = today.Year - birthDate.Year;
            if (today.Month < birthDate.Month || (today.Month == birthDate.Month && today.Day < birthDate.Day)) { age--; }
            return age;
        }
        private EmployeeDTO ConvertToEmployeeDTO(Employee emp)
        {
            var empDTO = new EmployeeDTO()
            {
                Address = emp.Address,
                Email = emp.Email,
                BirthDate = emp.BirthDate,
                Name = emp.Name,
                MobileNo = emp.MobileNo,
                Id = emp.Id
            };
            if (emp.Manger != null)
            {
                empDTO.Manger = ConvertToEmployeeDTO(emp.Manger);
            }
            return empDTO;
        }
        private List<EmployeeDTO> ConvertToEmployeeDTOs(List<Employee> employees)
        {
            List<EmployeeDTO> EmployeeDTOs = new List<EmployeeDTO>();
            foreach (var emp in employees)
            {
                EmployeeDTOs.Add(ConvertToEmployeeDTO(emp));
            }
            return EmployeeDTOs;
        }
        private AttendenceDTO ConvertToAttendenceDTO(Attendence att)
        {
            return new AttendenceDTO
            {
                Date = att.Date,
                SignIn = att.SignIn,
                SignOut = att.SignOut,
                WorkingHours = att.WorkingHours
            };
        }
        private List<AttendenceDTO> ConvertToAttendenceDTOs(List<Attendence> Attendences)
        {
            List<AttendenceDTO> attendenceDTOs = new List<AttendenceDTO>();
            foreach (var att in Attendences)
            {
                attendenceDTOs.Add(ConvertToAttendenceDTO(att));
            }
            return attendenceDTOs;
        }
        private List<EmployeeDTO> filterEmployee(int page, int pageSize, string mangerName)
        {
            using (SqlConnection conn = new SqlConnection("Server=DESKTOP-MCEIK97\\SQLEXPRESS;Trusted_Connection=True;Database=HR_Company"))
            {
                using (SqlCommand cmd = new SqlCommand("GetEmployees", conn))
                {
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapt.SelectCommand.Parameters.AddWithValue("@page", page);
                    adapt.SelectCommand.Parameters.AddWithValue("@size", pageSize);
                    adapt.SelectCommand.Parameters.AddWithValue("@mangerName", mangerName);
                    List<EmployeeDTO> employeeDTOs = new List<EmployeeDTO>();
                    DataTable dt = new DataTable();
                    conn.Open();
                    adapt.Fill(dt);
                    conn.Close();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            EmployeeDTO vM = new EmployeeDTO();
                            vM.Name = dt.Rows[i][0].ToString();
                            vM.Email = dt.Rows[i][1].ToString();
                            vM.Manger = new EmployeeDTO
                            {
                                Name = dt.Rows[i][2].ToString()
                            };
                            vM.Id = int.Parse(dt.Rows[i][3].ToString());
                            employeeDTOs.Add(vM);
                        }
                    }

                    adapt.Dispose();
                    return employeeDTOs;
                }
            }

        }
        public void CreateEmployee(EmployeeReqDTO employeeDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(employeeDTO.Name))
                    throw new Exception("Invalid Name");
                if (string.IsNullOrEmpty(employeeDTO.Email) || !Regex.IsMatch(employeeDTO.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                    throw new Exception("Invalid Email Address");
                if (string.IsNullOrEmpty(employeeDTO.MobileNo) || !Regex.IsMatch(employeeDTO.MobileNo, @"^(01)[0-9]{9}$"))
                    throw new Exception("Invalid MobileNo");

                if (string.IsNullOrEmpty(employeeDTO.Address))
                    throw new Exception("Invalid Address");

                int age = GetAge(employeeDTO.BirthDate);
                if (employeeDTO.BirthDate == DateTime.MinValue || age < 0 || age > 150)
                    throw new Exception("Invalid Birth Of Date");

                if (employeeDTO.MangerId <= 0)
                    throw new Exception("Invalid Manger Id");

                Employee emp = _uow.Employee.GetByName(employeeDTO.Name);
                if (emp != null)
                    throw new Exception("Employee Already Exists");

                Employee manger = _uow.Employee.Get(employeeDTO.MangerId);
                if (manger == null)
                    throw new Exception("Manger Doesn't Exist");

                Employee newEmployee = new Employee
                {
                    Name = employeeDTO.Name,
                    Address = employeeDTO.Address,
                    Email = employeeDTO.Email,
                    MobileNo = employeeDTO.MobileNo,
                    BirthDate = employeeDTO.BirthDate,
                    CreatedAt = DateTime.Now,
                    Manger = manger
                };
                _uow.Employee.Insert(newEmployee);
                _uow.Complete();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<EmployeeDTO> GetEmployees(int page, int pageSize)
        {
            return filterEmployee(page, pageSize, null);
        }
        public List<EmployeeDTO> GetEmployeesByMangerName(int page, int pageSize, string mangerName)
        {
            return filterEmployee(page, pageSize, mangerName);
        }

        public void SignIn(int empId)
        {
            if (empId <= 0)
                throw new Exception("Invalid Employee Id");
            Employee employee = _uow.Employee.Get(empId);
            if (employee == null)
                throw new Exception("Employee Doesn't Exist");
            Attendence attendence = new Attendence
            {
                Date = DateTime.Now,
                Employee = employee,
                SignIn = DateTime.Now,
            };
            _uow.Attendence.Insert(attendence);
            _uow.Complete();

        }
        public void SignOut(int empId)
        {
            if (empId <= 0)
                throw new Exception("Invalid Employee Id");
            Employee employee = _uow.Employee.Get(empId);
            if (employee == null)
                throw new Exception("Employee Doesn't Exist");

            List<Attendence> attendences = _uow.Attendence.GetAttendencesByEmpID(empId).OrderByDescending(y => y.SignIn).ToList();

            if (attendences.Count == 0 || attendences[0].SignIn == DateTime.MinValue)
                throw new Exception("Employee is Absent Today");
         
            attendences[0].SignOut = DateTime.Now;
            attendences[0].WorkingHours = (attendences[0].SignOut.Hour - attendences[0].SignIn.Hour);
            _uow.Attendence.Update(attendences[0]);
            _uow.Complete();

        }
        public List<AttendenceDTO> EmployeeAttendence(int empId)
        {
            if (empId <= 0)
                throw new Exception("Invalid Employee Id");
            Employee employee = _uow.Employee.Get(empId);
            if (employee == null)
                throw new Exception("Employee Doesn't Exist");
            List<Attendence> attendences = _uow.Attendence.GetAttendencesByEmpID(empId);
            return ConvertToAttendenceDTOs(attendences);
        }
        public List<EmployeeDTO> GetMangers()
        {
            return ConvertToEmployeeDTOs(_uow.Employee.GetAllEmployees());
        }

    }
}

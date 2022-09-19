using DataService;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRSystemDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IUnitOfWork _UOW;
        EmployeeBL _EmployeeLogic;
        public EmployeeController(IUnitOfWork UOW)
        {
            _UOW = UOW;
            _EmployeeLogic = new EmployeeBL(_UOW);
        }

        [HttpGet("{page}/{pageSize}/{name}")]
        public ActionResult GetEmployeesByMangerName(int page, int pageSize, string name)
        {
            try
            {
                var result = _EmployeeLogic.GetEmployeesByMangerName(page, pageSize, name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{page:int}/{pageSize:int}")]
        public ActionResult GetEmployees(int page, int pageSize)
        {
            try
            {
                var result = _EmployeeLogic.GetEmployees(page, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] EmployeeReqDTO employee)
        {
            try
            {
                _EmployeeLogic.CreateEmployee(employee);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        //[SwaggerOperation(Tags = new[] { "Employee" })]
        [HttpGet("in/{Id}", Name = "SignIn")]
        public IActionResult SignIn(int Id)
        {
            try
            {
                _EmployeeLogic.SignIn(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("out/{Id}", Name = "SignOut")]
        public void SignOut(int Id)
        {
            try
            {
                _EmployeeLogic.SignOut(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{empId}")]
        public IActionResult EmployeeAttendence(int empId)
       {
            try
            {
                var results = _EmployeeLogic.EmployeeAttendence(empId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("manger")]
        public IActionResult GetMangers()
        {
            try
            {
                var results = _EmployeeLogic.GetMangers();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

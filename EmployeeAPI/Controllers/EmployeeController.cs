using EmployeeAPI.EFCore;
using EmployeeAPI.Helper;
using EmployeeAPI.Models;
using EmployeeAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _db;
        public EmployeeController(DataContext context)
        {
            _db = new EmployeeRepository(context);
        }
        // GET: api/<EmployeeController>
        [HttpGet]
        [Route("GetEmployee")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                IEnumerable<EmployeeModel> data = _db.GetEmployees();

                if (!data.Any())
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // GET api/<EmployeeController>/5
        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                EmployeeModel data = _db.GetEmployeeById(id);

                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // POST api/<EmployeeController>
        [HttpPost]
        [Route("SaveEmployee")]
        public IActionResult Post([FromBody] EmployeeModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                int response = _db.SaveEmployee(model);
                model.Id = response;
                
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/<EmployeeController>/5
        [HttpPut]
        [Route("UpdateEmployee")]
        public IActionResult Put([FromBody] EmployeeModel model)
        {

            try
            {
                ResponseType type = ResponseType.Success;
                int response = _db.SaveEmployee(model);

                if (response == 0)
                {
                    type = ResponseType.NotFound;
                    model = null;
                } else
                {
                    model.Id = response;
                }

                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                string message = null;
                int response = _db.DeleteEmployee(id);

                if (response == 0)
                {
                    type = ResponseType.NotFound;
                } else
                {
                    message = "Delete Successfully";
                }
                return Ok(ResponseHandler.GetAppResponse(type, message));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}

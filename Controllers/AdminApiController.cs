using Microsoft.AspNetCore.Mvc;
using Hospital_Management.Models;
using Hospital_Management.Repositories;
using System.Diagnostics;


namespace Hospital_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        AdminRepository repository;
        public AdminApiController(HospitalDbContext context) //used the feature dependency injection
        {
            this.repository = new AdminRepository(context);
        }

        // GET: api/<AdminApiController>/all/{uid}
        [HttpGet("all/uid")]
        public IActionResult GetAll(int uid)
        {
            try
            {
                List<Admin> admins = repository.GetAll(uid);
                return Ok(admins);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<AdminApiController>/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id,int uid)
        {
            try
            {
                Admin admin = repository.GetById(id, uid);
                if (admin == null)
                {
                    return BadRequest();
                }
                else
                    return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        // POST api/<AdminApiController>
        [HttpPost]
        public IActionResult Post([FromBody] Admin admin, int uid)    //Body is Json Data
        {
            try
            {
                Debug.WriteLine("Got Request! " + admin.AdminId);
                repository.Add(admin, uid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }




        // PUT api/<AdminApiController>/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Admin admin , int uid)
        {
            try
            {
                Admin a = repository.GetById(id, uid);
                if (a == null)
                {
                    return BadRequest();
                }
                else
                {
                    repository.Update(admin, uid);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // DELETE api/<AdminApiController>/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, int uid)
        {
            try
            {
                repository.Delete(id, uid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}

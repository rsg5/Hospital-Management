using Microsoft.AspNetCore.Mvc;
using Hospital_Management.Models;
using Hospital_Management.Repositories;
using System.Diagnostics;


namespace Hospital_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorApiController : ControllerBase
    {
        DoctorRepository repository;
        public DoctorApiController(HospitalDbContext context) //used the feature dependency injection
        {
            this.repository = new DoctorRepository(context);
        }

        // GET: api/<DoctorApiController>/all/{uid}
        [HttpGet("all/{uid}")]
        public IActionResult GetAll(int uid)
        {
            try
            {
                List<Doctor> doctors = repository.GetAll(uid);
                return Ok(doctors);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<DoctorApiController>/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id, int uid)
        {
            try
            {
                Doctor doctor = repository.GetById(id, uid);
                if (doctor == null)
                {
                    return BadRequest();
                }
                else
                    return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<GetallSpecificAppointmentsByDoctorId>/{id}
        [HttpGet("GetallSpecificAppointmentsByDoctorId/{id}")]
        public IActionResult GetallSpecificAppointmentsByDoctorId(int id, int uid)
        {
            try
            {
                List<Appointment> doctor = repository.GetallSpecificAppointmentsByDoctorId(id, uid);
                if (doctor == null)
                {
                    return BadRequest();
                }
                else
                    return Ok(doctor);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        // POST api/<DoctorApiController>
        [HttpPost]
        public IActionResult Post([FromBody] Doctor doctor, int uid)    //Body is Json Data
        {
            try
            {

                Debug.WriteLine("Got Request! " + doctor.DoctorId);
                repository.Add(doctor, uid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }




        // PUT api/<DoctorApiController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, int uid, [FromBody] Doctor doctor)
        {
            try
            {
                Doctor doom = repository.GetById(id, uid);
                if (doom == null)
                {
                    return BadRequest();
                }
                else
                {
                    repository.Update(doctor, uid);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // DELETE api/<DoctorApiController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, int uid)
        {
            try
            {
                repository.Delete(id, uid);
                return Ok();
            }
            catch (Exception Ex)
            {
                return BadRequest();
            }
        }

    }
}

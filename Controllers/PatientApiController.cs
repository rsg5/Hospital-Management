using Microsoft.AspNetCore.Mvc;
using Hospital_Management.Models;
using Hospital_Management.Repositories;
using System.Diagnostics;


namespace Hospital_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientApiController:ControllerBase
    {
        PatientRepository repository;
        public PatientApiController(HospitalDbContext context) //used the feature dependency injection
        {
            this.repository = new PatientRepository(context);
        }

        // GET: api/<PatientApiController>/all/{uid}
        [HttpGet("all/{uid}")]
        public IActionResult GetAll(int uid)
        {
            try
            {
                List<Patient> patients = repository.GetAll(uid);
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GET api/<PatientApiController>/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id, int uid)
        {
            try
            {
                Patient patient = repository.GetById(id, uid);
                if (patient == null)
                {
                    return BadRequest();
                }
                else
                    return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        // POST api/<PatientApiController>
        [HttpPost]
        public IActionResult Post([FromBody] Patient patient,int uid)    //Body is Json Data
        {
            try
            {
                Debug.WriteLine("Got Request! " + patient.PatientId);
                repository.Add(patient, uid);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }




        // PUT api/<PatientApiController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, int uid, [FromBody] Patient patient)
        {
            try
            {
                Patient p = repository.GetById(id, uid);
                if (p == null)
                {
                    return BadRequest();
                }
                else
                {
                    repository.Update(patient, uid);
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // DELETE api/<PatientApiController>/{id}
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

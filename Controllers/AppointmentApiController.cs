using Microsoft.AspNetCore.Mvc;
using Hospital_Management.Models;
using Hospital_Management.Repositories;
using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Text;



namespace Hospital_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentApiController : ControllerBase
    {
        AppointmentRepository repository;
        public AppointmentApiController(HospitalDbContext context) //used the feature dependency injection
        {
            this.repository = new AppointmentRepository(context);
        }

        // GETALL: api/<AppointmentApiController>/all/{uid}
        [HttpGet("all/{uid}")]
        public IActionResult GetAll(int uid)
        {
            try
            {
                List<Appointment> appointments = repository.GetAll(uid);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // GETBYID: api/<AppointmentApiController>/{id}
        [HttpGet("{id}/{uid}")]
        public IActionResult GetById(int id, int uid)
        {
            try
            {
                Appointment appointment = repository.GetById(id, uid);
                if (appointment == null)
                {
                    return BadRequest();
                }
                else
                    return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        // POST: api/<AppointmentApiController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Appointment appointment, int uid)    //Body is Json Data
        {
            try
            {
                Debug.WriteLine("Got Request! " + appointment.AppointmentId);
                repository.Add(appointment, uid);

                using (var client = new HttpClient())
                {
                    var logicAppUrl = "https://prod-15.southindia.logic.azure.com:443/workflows/78fa6e4d4ca9492392bef3a56385183c/triggers/When_a_HTTP_request_is_received/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2FWhen_a_HTTP_request_is_received%2Frun&sv=1.0&sig=BiYYy93ycwEo-JP9QovoKdA3_JJNdWEwrnffuWhnI40";
                    var response = await client.PostAsync(logicAppUrl, new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok();
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        return StatusCode((int)response.StatusCode, new { message = response.ReasonPhrase, details = errorContent });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }





        // PUT: api/<AppointmentApiController>/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,int uid, [FromBody] Appointment appointment)
        {
            try
            {
                Appointment ap = repository.GetById(id, uid);
                if (ap == null)
                {
                    return BadRequest();
                }
                else
                {
                    repository.Update(appointment, uid);
                    using (var client = new HttpClient())
                    {
                        var logicAppUrl = "https://prod-15.southindia.logic.azure.com:443/workflows/78fa6e4d4ca9492392bef3a56385183c/triggers/When_a_HTTP_request_is_received/paths/invoke?api-version=2016-10-01&sp=%2Ftriggers%2FWhen_a_HTTP_request_is_received%2Frun&sv=1.0&sig=BiYYy93ycwEo-JP9QovoKdA3_JJNdWEwrnffuWhnI40";
                        var response = await client.PostAsync(logicAppUrl, new StringContent(JsonConvert.SerializeObject(appointment), Encoding.UTF8, "application/json"));

                        if (response.IsSuccessStatusCode)
                        {
                            return Ok();
                        }
                        else
                        {
                            var errorContent = await response.Content.ReadAsStringAsync();
                            return StatusCode((int)response.StatusCode, new { message = response.ReasonPhrase, details = errorContent });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // DELETE: api/<AppointmentApiController>/{id}
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


using Hospital_Management.InterFace;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Web.Http;


namespace Hospital_Management.Repositories
{
    public class DoctorRepository : DoctorInterface<Doctor>
    {
        HospitalDbContext _context;

        public DoctorRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public void Add(Doctor obj,int uid)
        {
            bool result1 = _context.Doctors.Any(l => l.DoctorId == uid);
            bool result2 = _context.Admins.Any(l => l.AdminId == uid);
            if (!result1 && !result2)
            {
                throw new Exception();
            }
            Debug.WriteLine(obj.DoctorId);
            _context.Doctors.Add(obj);
            _context.SaveChanges();
        }

        public void Delete(object id, int uid)
        {
            bool result1 = _context.Doctors.Any(l => l.DoctorId == uid);
            bool result2 = _context.Admins.Any(l => l.AdminId == uid);
            if (!result1 && !result2)
            {
                throw new Exception();
            }
            int doctorId = (int)id;
            if(uid!=doctorId && result2==false)
            {
                throw new Exception();
            }
            var doctor = _context.Doctors.Find(doctorId);
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
        }

        public List<Doctor> GetAll(int uid)
        {
            
            bool result2 = _context.Admins.Any(l => l.AdminId == uid);
            if (!result2)
            {
                throw new Exception();
            }
            return _context.Doctors.ToList();
        }

        public Doctor GetById(object id, int uid)
        {
            bool result1 = _context.Doctors.Any(l => l.DoctorId == uid);
            bool result2 = _context.Admins.Any(l => l.AdminId == uid);
            bool result3 = _context.Patients.Any(p=> p.PatientId == uid);
            if (!result1 && !result2 && !result3)
            {
                throw new Exception();
            }
            int doctorId = (int)id;
            var doctor = _context.Doctors.Find(doctorId);
            return doctor;
        }


        public List<Appointment> GetallSpecificAppointmentsByDoctorId(object id, int uid)
        {
            bool result1 = _context.Appointments.Any(l => l.DoctorId == uid);
            if (!result1)
            {
                throw new Exception();
            }
            int doctorId = (int)id;
            if (uid != doctorId)
            {
                throw new Exception();
            }
            var appointment = _context.Appointments.Where(l=>l.DoctorId==doctorId).ToList();

            return appointment;
        }
        public void Update(Doctor obj, int uid)
        {
            bool result1 = _context.Doctors.Any(l => l.DoctorId == uid);
            bool result2 = _context.Admins.Any(l => l.AdminId == uid);
            if (!result1 && !result2)
            {
                throw new Exception();
            }
            Doctor doctor = _context.Doctors.Find(obj.DoctorId);
            if (doctor == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            doctor.DoctorName = obj.DoctorName;
            doctor.Sex = obj.Sex;
            doctor.Specialization = obj.Specialization;
            doctor.PhoneNumber = obj.PhoneNumber;
            doctor.Email = obj.Email;
            _context.SaveChanges();
        }
    }
}

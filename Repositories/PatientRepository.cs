using Hospital_Management.InterFace;
using Hospital_Management.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Web.Http;


namespace Hospital_Management.Repositories
{
    public class PatientRepository : PatientInterface<Patient>
    {
        HospitalDbContext _context;

        public PatientRepository(HospitalDbContext context)
        { 
            _context = context;
        }
        public void Add(Patient obj, int uid)
        {
            bool result1 = _context.Patients.Any(l => l.PatientId == uid);
            bool result2 = _context.Admins.Any(l => l.AdminId == uid);
            if (!result1 && !result2)
            {
                throw new Exception();
            }
            Debug.WriteLine(obj.PatientId);
            _context.Patients.Add(obj);
            _context.SaveChanges();
        }

        public void Delete(object id, int uid)
        {
            bool result1 = _context.Patients.Any(l => l.PatientId == uid);
            bool result2 = _context.Admins.Any(l => l.AdminId == uid);
            if (!result1 && !result2)
            {
                throw new Exception();
            }
            int patientId = (int)id;
            var patient = _context.Patients.Find(patientId);
            _context.Patients.Remove(patient);
            _context.SaveChanges();
        }

        public List<Patient> GetAll(int uid)
        {
            bool result1 = _context.Doctors.Any(p=>p.DoctorId==uid);
            bool result2 = _context.Admins.Any(l => l.AdminId == uid);
            if (!result1 && !result2)
            {
                throw new Exception();
            }
            return _context.Patients.ToList();
        }

        public Patient GetById(object id, int uid)
        {
            bool result1 = _context.Patients.Any(l => l.PatientId == uid);
            bool result2=_context.Admins.Any(l=>l.AdminId==uid);
            bool result3= _context.Doctors.Any(d=>d.DoctorId==uid);
            if (!result1 && !result2 && !result3)
            {
                throw new Exception();
            }
            int patientId = (int)id;
            var patient = _context.Patients.Find(patientId);
            return patient;
        }

        public void Update(Patient obj,int uid)
        {
            bool result1 = _context.Patients.Any(l => l.PatientId == uid);
            bool result2 = _context.Admins.Any(l => l.AdminId == uid);
            if (!result1 && !result2)
            {
                throw new Exception();
            }
            Patient patient = _context.Patients.Find(obj.PatientId);
            if (patient == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            patient.PatientName = obj.PatientName;
            patient.Sex = obj.Sex;
            patient.EmergencyContact = obj.EmergencyContact;
            patient.PhoneNumber = obj.PhoneNumber;
            patient.Email = obj.Email;
            patient.ReportPreference = obj.ReportPreference;
            patient.DoctorId = obj.DoctorId;
            _context.SaveChanges();
        }
    }
}



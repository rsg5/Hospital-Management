using Hospital_Management.InterFace;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Web.Http;

namespace Hospital_Management.Repositories
{
    public class AppointmentRepository: AppointmentInterface<Appointment>
    {
        HospitalDbContext _context;

        public AppointmentRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public void Add(Appointment obj,int uid)
        {
            bool result = _context.Admins.Any(l => l.AdminId == uid);
            if (!result)
            {
                throw new Exception();
            }
            Debug.WriteLine(obj.AppointmentId);
            _context.Appointments.Add(obj);
            _context.SaveChanges();
        }

        public void Delete(object id, int uid)
        {
            bool result = _context.Admins.Any(l => l.AdminId == uid);
            if (!result)
            {
                throw new Exception();
            }
            int appointmentId = (int)id;
            Appointment appointment = _context.Appointments.Find(appointmentId);
             _context.Appointments.Remove(appointment);
            _context.SaveChanges();
        }

        public List<Appointment> GetAll(int uid)
        {
            bool result = _context.Admins.Any(l => l.AdminId == uid);
            if (!result)
            {
                throw new Exception();
            }
            var appointments = _context.Appointments
                                .Include(a => a.patient)  // Include the patient navigation property
                                .Include(a => a.doctor)   // Include the doctor navigation property
                                .ToList();  // Execute the query and retrieve the results as a list

            // Return the list of appointments
            return appointments;
        }

        public Appointment GetById(object id,int uid)
        {
            bool result = _context.Admins.Any(l => l.AdminId == uid);
            bool result2= _context.Patients.Any(r => r.PatientId == uid);
            bool result3= _context.Doctors.Any(d => d.DoctorId == uid);
            if (!result && !result2 && !result3)
            {
                throw new Exception();
            }
            int appointmentId = (int)id;
            Appointment appointment = _context.Appointments.Include(a => a.patient).Include(a => a.doctor).FirstOrDefault(a => a.AppointmentId == appointmentId);
            return appointment;
        }

        public void Update(Appointment obj, int uid)
        {
            bool result = _context.Admins.Any(l => l.AdminId == uid);
            if (!result)
            {
                throw new Exception();

            }
            Appointment appointment = _context.Appointments.Where(a => a.AppointmentId == obj.AppointmentId).FirstOrDefault();
            if (appointment == null)
            {
                throw new Exception();
            }
            appointment.PatientId = obj.PatientId;
            appointment.ConsultationStatus = obj.ConsultationStatus;
            appointment.NextAppointmentDate = obj.NextAppointmentDate;
            appointment.ReportPreference = obj.ReportPreference;
            appointment.DoctorId = obj.DoctorId;
            _context.SaveChanges();
        }
    }
}

using Hospital_Management.InterFace;
using Hospital_Management.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Web.Http;

namespace Hospital_Management.Repositories
{
    public class AdminRepository: AdminInterface<Admin>
    {
        HospitalDbContext _context;
        public AdminRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public void Add(Admin obj, int uid)
        {
            bool result = _context.Admins.Any(l => l.AdminId == uid);
            if (!result)
            {
                throw new Exception(); 
            }
            Debug.WriteLine(obj.AdminId);
            _context.Admins.Add(obj);
            _context.SaveChanges();
        }

        public void Delete(object id, int uid)
        {
            bool result = _context.Admins.Any(l => l.AdminId == uid);
            if (!result)
            {
                throw new Exception(); 
            }
            int adminId = (int)id;
            var admin = _context.Admins.Find(adminId);
            _context.Admins.Remove(admin);
            _context.SaveChanges();
        }

        public List<Admin> GetAll(int uid)
        {
            bool result = _context.Admins.Any(l => l.AdminId == uid);
            if (!result)
            {
                throw new Exception();
            }
            var adminList=_context.Admins.ToList();
            return adminList;
        }

        public Admin GetById(object id, int uid)
        {
            bool result = _context.Admins.Any(l => l.AdminId == uid);
            if (!result)
            {
                throw new Exception();
            }
            int adminId = (int)id;
            var admin = _context.Admins.Find(adminId);
            return admin;
        }

        public void Update(Admin obj, int uid)
        {
            bool result = _context.Admins.Any(l => l.AdminId == uid);
            if (!result)
            {
                throw new Exception();
            }
            Admin admin = _context.Admins.Find(obj.AdminId);
            if (admin == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            admin.AdminName = obj.AdminName;
            admin.PhoneNumber = obj.PhoneNumber;
            admin.Email = obj.Email;
            _context.SaveChanges();
        }
    }
}

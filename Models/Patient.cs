using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Management.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }        
        public string PatientName { get; set; }
        public string Sex { get; set; }

        public string EmergencyContact { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string ReportPreference { get; set; }


        //Navigation Properties
        internal Doctor? Doctor { get;}
        internal Appointment? appointment { get;}
        
    }
}

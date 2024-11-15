using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Hospital_Management.Models
{
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentId { get; set; }
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        public string ReportPreference { get; set; }
        public string ConsultationStatus { get; set; }
        public DateTime CurrentAppointmentDate { get; set; }
        public DateTime NextAppointmentDate { get; set; }


        //Navigation Properties
        [JsonIgnore]
        internal Doctor? doctor { get; set;}
        [JsonIgnore]
        internal Patient? patient { get; set;}
    }
}

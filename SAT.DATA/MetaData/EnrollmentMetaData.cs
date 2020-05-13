using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.DATA
{
    public class EnrollmentMetaData
    {
        [Required]
        public int EnrollmentID { get; set; }
        [Required]
        public int ScheduleClassID { get; set; }
        [Required]
        public int StudentID { get; set; }
        [Required]
        public Nullable<System.DateTime> EnrollmentDate { get; set; }
    }
    [MetadataType(typeof(EnrollmentMetaData))]
    public partial class Enrollment
    {

    }
}

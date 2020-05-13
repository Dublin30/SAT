using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.DATA
{
    public class ScheduleClassMetaData
    {
        [Required]
        public int ScheduleClassesID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public Nullable<System.DateTime> StarteDate { get; set; }
        [Required]
        public Nullable<System.DateTime> EndDate { get; set; }
        [Required]
        [Display(Name ="Instructor")]
        public string InstructorName { get; set; }
        [Required]
        public int SCSID { get; set; }
        public string Location { get; set; }
    }

    [MetadataType(typeof(ScheduleClassMetaData))]
    public partial class ScheduledClassStatus
    {

    }
}

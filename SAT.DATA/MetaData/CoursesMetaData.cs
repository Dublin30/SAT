using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAT.DATA
{
    public class CoursesMetaData
    {
        [Required]
        public int CourseID { get; set; }
        [Required]
        [Display(Name ="Course Name")]
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        [Required]
        [Display(Name ="Credit Hours")]
        public string CreditHours { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
        public string Curriculum { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
    [MetadataType(typeof(CoursesMetaData))]
    public partial class Cours
    {

    }
}

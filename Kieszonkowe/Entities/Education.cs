using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.Entities
{
    public class Education
    {
        [Key]
        public Guid Id { get; set; }
        public string EducationDegree { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ASPNETCoreWebApi6.Models
{
    [ModelMetadataType(typeof(CourseMetadata))]
    public partial class Course
    {
    }

    internal class CourseMetadata
    {
        public Int32 CourseId { get; set; }
        [Required(ErrorMessage = "�п�J�ҵ{���D")]
        public String Title { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "�ҵ{�����u�श�� 1 ~ 5 ����")]
        public Int32 Credits { get; set; }
        public Int32 DepartmentId { get; set; }
    }
}

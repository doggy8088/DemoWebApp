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
        [Required(ErrorMessage = "請輸入課程標題")]
        public String Title { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "課程評價只能介於 1 ~ 5 之間")]
        public Int32 Credits { get; set; }
        public Int32 DepartmentId { get; set; }
    }
}

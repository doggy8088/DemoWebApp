using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ASPNETCoreWebApi6.Models
{
    [ModelMetadataType(typeof(DepartmentMetadata))]
    public partial class Department : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Name == "�M�׳�" && this.Budget < 1000)
            {
                yield return new ValidationResult("�M�׳����w�⤣��ӧC", new string[] { "Budget" });
            }
        }
    }

    internal class DepartmentMetadata
    {
        // [Required]
        public Int32 DepartmentId { get; set; }
        // [Required]
        public String Name { get; set; }
        // [Required]
        public Decimal Budget { get; set; }
        // [Required]
        public DateTime StartDate { get; set; }
        // [Required]
        public Nullable<Int32> InstructorId { get; set; }
        // [Required]
        public Byte[] RowVersion { get; set; }
    }
}

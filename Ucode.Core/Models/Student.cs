﻿using Ucode.Core.Emuns;

namespace Ucode.Core.Models
{
    namespace AcademicControl.API.Models
    {
        public class Student
        {
            public long Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public DateTime BirthDate { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public DateTime? UpdatedAt { get; set; }

            public EGender Gender { get; set; }


            public long CourseId { get; set; }
            public Course Course { get; set; } = null!;

            public string UserId { get; set; } = string.Empty;
        }
    }

}

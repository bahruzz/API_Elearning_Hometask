﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Api_Elearning.Models
{
    public class CourseStudent
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Course))] public int CourseId { get; set; }
        public Course Course { get; set; }
        [ForeignKey(nameof(Student))] public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}

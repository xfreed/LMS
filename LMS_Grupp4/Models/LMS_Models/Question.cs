using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LMS_Grupp4.Models.LMS_Models
{
    [Bind(Exclude = "ID")]
    public class Question
    {
        [Key] public int ID { get; set; }

        [MaxLength(100)]
        [Display(Name = "Question Name")]
        [Required]
        public string QuestionName { get; set; }

        [MaxLength(100)]
        [Display(Name = "Answer")]
        [Required]
        public string Answer { get; set; }

        [MaxLength(100)]
        [Display(Name = "Incorrect answer 1")]
        [Required]
        public string Fake1 { get; set; }

        [MaxLength(100)]
        [Display(Name = "Incorrect answer 2")]
        [Required]
        public string Fake2 { get; set; }

        [MaxLength(100)]
        [Display(Name = "Incorrect answer 3")]
        [Required]
        public string Fake3 { get; set; }
    }
}
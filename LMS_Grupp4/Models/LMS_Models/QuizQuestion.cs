using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LMS_Grupp4.Models.LMS_Models
{
    [Bind(Exclude = "ID")]
    public class QuizQuestion
    {
        [Key] public int ID { get; set; }

        public QuizInformation QuizInformation { get; set; }
        public Question Question { get; set; }
    }
}
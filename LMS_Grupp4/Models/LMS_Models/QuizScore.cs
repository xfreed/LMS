using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LMS_Grupp4.Models.LMS_Models
{
    [Bind(Exclude = "ID")]
    public class QuizScore
    {
        [Key] public int ID { get; set; }

        public QuizInformation QuizInformation { get; set; }
        public ApplicationUser QuizUser { get; set; }
        public int Score { get; set; }
    }
}
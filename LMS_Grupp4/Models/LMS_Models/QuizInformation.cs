using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LMS_Grupp4.Models.LMS_Models
{
	[Bind(Exclude = "ID")]
    public class QuizInformation
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(100)]
        [Display(Name = "Quiz name")]
        [Required]
        public string QuizName { get; set; }
    }
}
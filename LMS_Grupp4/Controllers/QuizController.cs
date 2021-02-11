using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS_Grupp4.Models.LMS_Models;
using LMS_Grupp4.Repositories;

namespace LMS_Grupp4.Controllers
{
    [Authorize(Roles = "student,teacher")]
    public class QuizController : Controller
    {
        LMSRepository LMSRepo = new LMSRepository();

        // GET: Quiz
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Play()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Delete()
        {
            SelectList quizs = new SelectList(LMSRepo.GetAllQuizInformation(), "Id", "QuizName");
            ViewBag.QuizInformation = quizs;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            return View();
        }

        public JsonResult CreateNewQuiz(string quizName, string[] questionNames, string[] questionAnswers,
            string[] questionFake1, string[] questionFake2, string[] questionFake3)
        {
            var quizInformation = new QuizInformation {QuizName = quizName};
            var quizQuestions = new List<QuizQuestion>();
            var questions = questionNames.Select((t, i) => new Question()
                {
                    QuestionName = t,
                    Answer = questionAnswers[i],
                    Fake1 = questionFake1[i],
                    Fake2 = questionFake2[i],
                    Fake3 = questionFake3[i]
                })
                .ToList();

            LMSRepo.AddQuestions(questions);
            LMSRepo.AddQuizInformation(quizInformation);
            for (int i = 0; i < questionNames.Length; i++)
            {
                quizQuestions.Add(new QuizQuestion()
                {
                    QuizInformation = quizInformation,
                    Question = questions[i]
                });
            }

            LMSRepo.AddQuizQuestions(quizQuestions);
            return Json(new {response = true});
        }
    }
}
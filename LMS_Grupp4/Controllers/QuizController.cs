using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMS_Grupp4.Models;
using LMS_Grupp4.Models.LMS_Models;
using LMS_Grupp4.Repositories;
using Microsoft.AspNet.Identity;

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

        [HttpGet]
        public ActionResult Play()
        {
            SelectList quiz = new SelectList(LMSRepo.GetAllQuizInformation(), "Id", "QuizName");
            ViewBag.QuizInformation = quiz;
            return View();
        }

        [HttpPost]
        public JsonResult Play(int id)
        {
            var questions = LMSRepo.GetQuestionById(id).ToList();
            UserManager<ApplicationUser> userManager = LMSRepo.GetUserManager();
            bool played = LMSRepo.GetPlayedQuiz(userManager.FindById(User.Identity.GetUserId()).Id, id);
            return Json(new {response = questions, played });
        }

        [HttpGet]
        public ActionResult Delete()
        {
            SelectList quiz = new SelectList(LMSRepo.GetAllQuizInformation(), "Id", "QuizName");
            ViewBag.QuizInformation = quiz;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            LMSRepo.DeleteQuiz(id);
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

        public JsonResult SaveQuizTry(string[] answerStrings,List<Question> questions,int quizId )
        {
            var answers = answerStrings.Select(x => x.Substring(x.IndexOf(' ')+1)).ToList();
            int score = questions.Where((t, i) => t.Answer.Equals(answers[i])).Count();
            UserManager<ApplicationUser> userManager = LMSRepo.GetUserManager();

            QuizScore scrQuizScore = new QuizScore
            {
                Score = score,
                QuizUser = userManager.FindById(User.Identity.GetUserId()),
                QuizInformation = LMSRepo.GetQuizInformationById(quizId)
            };
            LMSRepo.AddNewQuizScore(scrQuizScore);
            return Json(new {response = score});
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveyApp.Models;
using WebMatrix.WebData;

namespace SurveyApp.Controllers
{
    [Authorize]
    public class SurveyController : Controller
    {
        //
        // GET: /Survey/
        public SurveyController()
        {
            Database.SetInitializer<SurveyContext>(null);            
        }

        public ActionResult Index()
        {            
            return View();
        }
        public ActionResult Survey(int? surveyID)
        {
            return View(surveyID);
        }

        public ActionResult SurveyList()
        {
            return View();
        }

        public ActionResult SurveyAddEdit(int? id)
        {            
            Survey objSurvey = new Survey();
            if (id.HasValue)
            {
                if (Models.Survey.IsUserAuthorizedForSurvey(WebSecurity.CurrentUserId, id.Value) == false)
                {
                    return RedirectToAction("Index", "Survey");
                }
                var sContext = new SurveyContext();
                objSurvey = sContext.Surveys.Find(id.Value);
            }

            return View(objSurvey);
        }

        [HttpPost]
        public ActionResult SurveyAddEdit(Survey model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Id <= 0 && doesSurveyExist(model.Title))
            {
                ModelState.AddModelError("", "This survey already exists, please provide different details.");
                return View(model);
            }

            using (var db = new SurveyContext())
            {
                Survey survey = null;
                if (model.Id > 0)
                {
                    var result = db.Surveys.SingleOrDefault(s => s.Id == model.Id);
                    if (result != null)
                    {
                        result.Title = model.Title;
                        result.Style = model.Style;
                        result.Tagline = model.Tagline;
                        result.Title_Abbr = model.Title_Abbr;
                    }                    
                }
                else
                {
                    survey = new Survey { Title = model.Title, Tagline = model.Tagline, Style = model.Style, Title_Abbr = model.Title_Abbr };
                    db.Surveys.Add(survey);
                }
                                           
                db.SaveChanges();
            }


            return RedirectToAction("Index", "Survey");

        }

        public bool doesSurveyExist(string title)
        {
            bool result = false;
            using (var sContext = new SurveyContext())
            {
                Survey objSurvey = sContext.Surveys.Where(s => s.Title == title).FirstOrDefault();
                if (objSurvey != null && objSurvey.Id > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        #region SurveyQuestion
        public ActionResult SurveyQuestionList(int surveyId)
        {
            return View(surveyId);
        }

        public ActionResult SurveyQuestionAddEdit(int? id)
        {
            SurveyQuestion objSurveyQuestion = new SurveyQuestion();
            if (id.HasValue)
            {
                if (Models.Survey.IsUserAuthorizedForSurvey(WebSecurity.CurrentUserId, id.Value) == false)
                {
                    return RedirectToAction("SurveyQuestionList", "Survey");
                }
                var sqContext = new SurveyQuestionContext();
                objSurveyQuestion = sqContext.SurveyQuestions.Find(id.Value);
            }

            return View(objSurveyQuestion);
        }

        [HttpPost]
        public ActionResult SurveyQuestionAddEdit(SurveyQuestion model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int surveyId = 0;
            using (var db = new SurveyQuestionContext())
            {                
                if (model.ID > 0)
                {
                    var result = db.SurveyQuestions.SingleOrDefault(s => s.ID == model.ID);
                    if (result != null)
                    {
                        result.Question = model.Question;
                        surveyId = result.SurveyID;
                    }
                }
                
                db.SaveChanges();
            }


            return RedirectToAction("SurveyQuestionList", "Survey", new { surveyId = surveyId });

        }
        #endregion

    }
}

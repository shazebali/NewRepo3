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

        #region Survey
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

            string title = string.Empty, tagline = string.Empty, abbreviation = string.Empty, style = string.Empty;
            int surveyId = 0;

            using (var db = new SurveyContext())
            {
                Survey survey = null;
                if (model.Id > 0)
                {
                    var result = db.Surveys.SingleOrDefault(s => s.Id == model.Id);
                    if (result != null)
                    {
                        surveyId = result.Id;
                        title = result.Title;
                        tagline = result.Tagline;
                        abbreviation = result.Title_Abbr;
                        style = result.Style;

                        result.Title = model.Title;
                        result.Style = model.Style;
                        result.Tagline = model.Tagline;
                        result.Title_Abbr = model.Title_Abbr;
                    }
                }
                else
                {
                    surveyId = model.Id;
                    title = model.Title;
                    tagline = model.Tagline;
                    abbreviation = model.Title_Abbr;
                    style = model.Style;

                    survey = new Survey { Title = model.Title, Tagline = model.Tagline, Style = model.Style, Title_Abbr = model.Title_Abbr };
                    db.Surveys.Add(survey);
                }

                db.SaveChanges();

            }

            using (var acContext = new ActivityLogContext())
            {
                ActivityLog objALog = new ActivityLog();
                objALog.Activity = "SurveyAddUpdate";
                objALog.Date = DateTime.Now;
                objALog.Information = "SurveyId: " + surveyId + " Title : " + title + " Tagline: " + tagline + " Abbreviation: " + abbreviation + " Style: " + style;
                objALog.UserId = WebSecurity.CurrentUserId;

                acContext.Activities.Add(objALog);
                acContext.SaveChanges();
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

        #endregion

        #region SurveyQuestion
        public ActionResult SurveyQuestionList(int? surveyId)
        {
            if(surveyId.HasValue == false || surveyId.Value <= 0)
            {
                return RedirectToAction("Index", "Survey");
            }
            
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

            if (string.IsNullOrEmpty(model.InputType) == true)
            {
                //ModelState.AddModelError("", "This survey already exists, please provide different details.");
                ModelState.AddModelError("", "Please Select Answer Option Type.");
                return View(model);
            }
            if (model.Seq <= 0)
            {
                //ModelState.AddModelError("", "This survey already exists, please provide different details.");
                ModelState.AddModelError("", "Please provide order in which Question should appear in the Questionnaire.");
                return View(model);
            }

            int surveyId = 0, questionId = 0;
            string question = string.Empty, possibleAnswers = string.Empty, aClass = string.Empty, 
                score = string.Empty, seq = string.Empty, style = string.Empty, inputType = string.Empty,
                section = string.Empty, parentAnswerlike = string.Empty, nClass = string.Empty,
                questionGroup = string.Empty;

            using (var db = new SurveyQuestionContext())
            {                
                if (model.ID > 0)
                {
                    var result = db.SurveyQuestions.SingleOrDefault(s => s.ID == model.ID);
                    if (result != null)
                    {
                        questionId = result.ID;
                        question = result.Question;
                        possibleAnswers = result.PossibleAnswers;
                        aClass = result.Aclass;
                        score = result.Score;
                        seq = result.Seq.ToString();
                        style = result.Style;
                        inputType = result.InputType;
                        section = result.Section;
                        parentAnswerlike = result.PAnswerLike;
                        nClass = result.Nclass;
                        questionGroup = result.QuestionGroup;


                        //update question
                        result.Question = model.Question;
                        result.PossibleAnswers = string.IsNullOrEmpty(model.PossibleAnswers) == true ? string.Empty : model.PossibleAnswers;
                        result.Aclass = model.Aclass;
                        result.Qclass = model.Qclass;
                        result.Score = model.Score;
                        result.InputType = model.InputType;
                        result.Seq = model.Seq;
                        result.Section = model.Section;
                        result.PAnswerLike = string.IsNullOrEmpty(model.PAnswerLike) == true ? string.Empty : model.PAnswerLike;
                        result.QuestionGroup = string.IsNullOrEmpty(model.QuestionGroup) == true ? string.Empty : model.QuestionGroup;
                        result.Style = string.IsNullOrEmpty(model.Style) == true ? string.Empty : model.Style;
                        result.Nclass = string.IsNullOrEmpty(model.Nclass) == true ? "col-sm-12" : model.Nclass;
                        result.QuestionGroup = string.IsNullOrEmpty(model.QuestionGroup) == true ? string.Empty : model.QuestionGroup;

                        surveyId = result.SurveyID;
                    }
                }
                else
                {
                    SurveyQuestion surveyQuestion = new SurveyQuestion {
                        Question = model.Question,
                        InputType = model.InputType,
                        PossibleAnswers = string.IsNullOrEmpty(model.PossibleAnswers) == true ? string.Empty : model.PossibleAnswers,
                        Section = model.Section,
                        Qclass = model.Qclass,
                        Aclass = model.Aclass,
                        Score = model.Score,
                        Seq = model.Seq,
                        SurveyID = model.SurveyID,
                        PAnswerLike = string.IsNullOrEmpty(model.PAnswerLike) == true ? string.Empty : model.PAnswerLike,
                        QuestionGroup = string.IsNullOrEmpty(model.QuestionGroup) == true ? string.Empty : model.QuestionGroup,
                        Style = string.IsNullOrEmpty(model.Style) == true ? string.Empty : model.Style,
                        Nclass = string.IsNullOrEmpty(model.Nclass) == true ? string.Empty : model.Nclass

                    };

                    surveyId = model.SurveyID;

                    db.SurveyQuestions.Add(surveyQuestion);

                }
                
                db.SaveChanges();
            }

            using (var acContext = new ActivityLogContext())
            {
                ActivityLog objALog = new ActivityLog();
                objALog.Activity = "SurveyQuestionAddEdit";
                objALog.Date = DateTime.Now;
                objALog.Information = "SurveyId: " + surveyId + " QuestionId: " + questionId + 
                    " Question: " + question + " PossibleAnswers: " + possibleAnswers + 
                    " Qclass: " + aClass + " Score: " + score + " InoutType: " + inputType + 
                    " Section: " + section + " ParentAnswerlike: " + parentAnswerlike + 
                    " NClass: " + nClass + " QuestionGroup: " + questionGroup;
                objALog.UserId = WebSecurity.CurrentUserId;

                acContext.Activities.Add(objALog);
                acContext.SaveChanges();
            }


            return RedirectToAction("SurveyQuestionList", "Survey", new { surveyId = surveyId });

        }
        #endregion

    }
}

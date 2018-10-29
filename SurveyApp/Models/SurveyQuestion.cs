using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace SurveyApp.Models
{
    [Table("Question")]
    public class SurveyQuestion
    {
        
    
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }        
        public int? PID { get; set; }        
        public double Seq { get; set; }
        public string PAnswerLike { get; set; }        
        public int SurveyID { get; set; }
        public string QuestionGroup { get; set; }
        [Required(ErrorMessage = "Please provide question", AllowEmptyStrings = false)]
        public string Question { get; set; }        
        public string PossibleAnswers { get; set; }        
        public string InputType { get; set; }        
        public string Style { get; set; }        
        public string Nclass { get; set; }
        public string Qclass { get; set; }
        public string Aclass { get; set; }
        public string Score { get; set; }
        public string ScoreCategory { get; set; }
        public string Area { get; set; }
        public int? DeletedSurveyID { get; set; }
        public string Section { get; set; }



        public static List<SurveyQuestion> SurveyQuestionGetAllForSurvey(int surveyId)
        {
            List<SurveyQuestion> lst = new List<SurveyQuestion>();

            DataSet ds = DataHelper.SurveyQuestionGetAllForSurvey(surveyId);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SurveyQuestion objSurveyQuestion = new SurveyQuestion();
                    objSurveyQuestion.ID = dr["ID"] != DBNull.Value ? Convert.ToInt32(dr["ID"]) : 0;
                    objSurveyQuestion.PID = dr["PID"] != DBNull.Value ? Convert.ToInt32(dr["PID"]) : 0;
                    objSurveyQuestion.Seq = dr["Seq"] != DBNull.Value ? Convert.ToDouble(dr["Seq"]) : 0.0;
                    objSurveyQuestion.PAnswerLike = dr["PAnswerLike"] != DBNull.Value ? Convert.ToString(dr["PAnswerLike"]) : string.Empty;
                    objSurveyQuestion.SurveyID = dr["SurveyID"] != DBNull.Value ? Convert.ToInt32(dr["SurveyID"]) : 0;
                    objSurveyQuestion.QuestionGroup = dr["QuestionGroup"] != DBNull.Value ? Convert.ToString(dr["QuestionGroup"]) : string.Empty;
                    objSurveyQuestion.Question = dr["Question"] != DBNull.Value ? Convert.ToString(dr["Question"]) : string.Empty;
                    objSurveyQuestion.PossibleAnswers = dr["PossibleAnswers"] != DBNull.Value ? Convert.ToString(dr["PossibleAnswers"]) : string.Empty;
                    objSurveyQuestion.InputType = dr["InputType"] != DBNull.Value ? Convert.ToString(dr["InputType"]) : string.Empty;
                    objSurveyQuestion.Style = dr["Style"] != DBNull.Value ? Convert.ToString(dr["Style"]) : string.Empty;
                    objSurveyQuestion.Nclass = dr["Nclass"] != DBNull.Value ? Convert.ToString(dr["Nclass"]) : string.Empty;
                    objSurveyQuestion.Qclass = dr["Qclass"] != DBNull.Value ? Convert.ToString(dr["Qclass"]) : string.Empty;
                    objSurveyQuestion.Aclass = dr["Aclass"] != DBNull.Value ? Convert.ToString(dr["Aclass"]) : string.Empty;
                    objSurveyQuestion.Score = dr["Score"] != DBNull.Value ? Convert.ToString(dr["Score"]) : string.Empty;
                    objSurveyQuestion.ScoreCategory = dr["ScoreCategory"] != DBNull.Value ? Convert.ToString(dr["ScoreCategory"]) : string.Empty;
                    objSurveyQuestion.Area = dr["Area"] != DBNull.Value ? Convert.ToString(dr["Area"]) : string.Empty;
                    objSurveyQuestion.DeletedSurveyID = dr["DeletedSurveyID"] != DBNull.Value ? Convert.ToInt32(dr["DeletedSurveyID"]) : 0;
                    objSurveyQuestion.Section = dr["Section"] != DBNull.Value ? Convert.ToString(dr["Section"]) : string.Empty;

                    lst.Add(objSurveyQuestion);
                }
            }

            return lst;
        }

        public static bool IsUserAuthorizedForQuestion(int userId, int surveyId)
        {
            return Roles.IsUserInRole(WebSecurity.CurrentUserName, "Administrator");
        }

    }
}
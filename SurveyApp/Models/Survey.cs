using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Data.Entity;
using System.Web.Security;
using WebMatrix.WebData;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SurveyApp.Models
{
    [Table("Survey")]
    public class Survey
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide title for the survey", AllowEmptyStrings = false)]
        public string Title { get; set; }
        public string Tagline { get; set; }
        [Required(ErrorMessage = "Please select style for the survey", AllowEmptyStrings = false)]
        public string Style { get; set; }
        [Required(ErrorMessage = "Please provide abbreviation for the survey", AllowEmptyStrings = false)]
        public string Title_Abbr { get; set; }
        public bool IsDeleted { get; set; }


        public static List<Survey> SurveyGetAll()
        {
            List<Survey> lst = new List<Survey>();

            DataSet ds = DataHelper.SurveyGetAll();
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Survey objSurvey = new Survey();
                    objSurvey.Id = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"]) : 0;
                    objSurvey.Title = dr["Title"] != DBNull.Value ? Convert.ToString(dr["Title"]) : string.Empty;
                    objSurvey.Tagline = dr["Tagline"] != DBNull.Value ? Convert.ToString(dr["Tagline"]) : string.Empty;
                    objSurvey.Style = dr["Style"] != DBNull.Value ? Convert.ToString(dr["Style"]) : string.Empty;
                    objSurvey.Title_Abbr = dr["Title_Abbr"] != DBNull.Value ? Convert.ToString(dr["Title_Abbr"]) : string.Empty;
                    objSurvey.IsDeleted = dr["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(dr["IsDeleted"]) : true;

                    lst.Add(objSurvey);
                }
            }

            return lst;
        }

        public static bool IsUserAuthorizedForSurvey(int userId, int surveyId)
        {            
            return Roles.IsUserInRole(WebSecurity.CurrentUserName, "Administrator");            
        }

    }

    public class SurveyStyle
    {
        public int Id { get; set; }
        public string Style { get; set; }

        public static List<SurveyStyle> SurveyStyle_GetAll()
        {
            List<SurveyStyle> lstStyle = new List<SurveyStyle>();

            lstStyle.Add(new SurveyStyle() { Id = 1, Style = "Column" });
            lstStyle.Add(new SurveyStyle() { Id = 2, Style = "Row" });
            
            return lstStyle;
        }
    }

    public class SurveyContext : DbContext
    {
        public SurveyContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Survey> Surveys { get; set; }
    }

    public class SurveyQuestionContext : DbContext
    {
        public SurveyQuestionContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
    }

    public enum Questionnaire
    {
        Abbreviated_sensory_profile = 1,
        Caregiver_Strain_Questionnaire = 2,
        GI_Severity_Index = 3,
        Aberrant_Behavior_Checklist = 4,
        Social_Responsiveness_Scale = 5,
        Peds_quality_of_life_6 = 6,
        Peds_quality_of_life_7 = 7,
        Peds_quality_of_life_8 = 8,
        Peds_quality_of_life_9 = 9,
        Short_language_profile = 10,
        Social_communication_Questionnaire_life_time = 11,
        Insomnia_Severity_Index = 12,
        Connor_Davidson_Resilience_Scale_10 = 13,
        Family_Empowerment_Scale = 14,
        Feedback_Questionnaire = 15,
        Parent_Intake_Form_OHS = 16,
        Parent_Follow_Up_Form_OHS = 21,
        Weekly_Teacher = 24,
        Academic_Progress_Rating_Scales = 27,
        Adverse_Reaction = 28,
        Life_Events = 29,
        Vanderbilt_ADHD_Diagnostic_Teacher_Rating_Scale = 34,
        Anxiety_Depression_and_Mood_Scale = 35,
        Monthly_Teacher = 37,
        CAS_Grit_and_Resilience_Scale = 42,
        Intake_Form_CAS = 43,
        Classroom_Intervention = 44,
        Peds_quality_of_life = 45,
        Vineland_3_Parent = 46,
        Vineland_3_Teacher = 47,
        Parent_Assessment_Summary = 48,
        CAS_Academic_Progress_Reading_and_Writing = 49,
        School_Anxiety_Scale_Teacher_Report = 50,
        Strengths_and_Difficulties_Questionnaire_51 = 51,
        Strengths_and_Difficulties_Questionnaire_52 = 52,
        CAS_Academic_Progress_Math_and_Science = 53,
        CAS_Academic_Progress_Other_Skills = 54,
        CAS_Grit_and_Resilience_Parent = 55,
        Vanderbilt_ADHD_Scale_Parent = 56,
        CGI_I_Caregiver_Rating = 57,
        CGI_I_Teacher_Rating = 58,
        CGI_I_Self_Rating = 59,
        Life_Update = 60,
        Interventions_Update = 61,
        Social_Skills_Improvement_System_62 = 62,
        Social_Skills_Improvement_System_63 = 63,
        Social_Skills_Improvement_System_Parent = 64,
        Social_Skills_Improvement_System_Teacher = 65,
        Intake_Form_Athena_Academy = 66,
        Academic_Progress_Scale_For_Athena_Academy = 67,
        Revised_Childrens_Anxiety_and_Depression_Scale_RCADS_25 = 68,
        Mood_And_Feelings_Qestionnaire_Short_Version = 69,
        SCAS_Parent = 71,
        SCAS_Teacher = 72
    }
}
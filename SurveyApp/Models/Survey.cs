using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;

namespace SurveyApp.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Tagline { get; set; }
        public string Style { get; set; }
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

    }
}
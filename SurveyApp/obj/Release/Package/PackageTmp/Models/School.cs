using SurveyApp.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SurveyApp.Models
{   
    public class SchoolContext : DbContext
    {
        public SchoolContext()
            : base("DefaultConnection")
        {
        }
        
        public DbSet<School> Schools { get; set; }
    }
    [Table("School")]
    public class School
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SchoolId { get; set; }
        [Required(ErrorMessage = "Please provide school name", AllowEmptyStrings = false)]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public static List<School> SchoolGetAll()
        {
            List<School> lstSchools = new List<School>();
            using (var context = new SchoolContext())
            {
                foreach (School objSchool in context.Schools.ToList())
                {
                    if (objSchool.IsDeleted == false)
                    {
                        lstSchools.Add(objSchool);
                    }
                }
            }

            return lstSchools;
        }

        public static List<School> SchoolGetByUser(int userId)
        {
            List<School> lstSchools = new List<School>();
            DataSet ds = DataHelper.SchoolGetByUser(userId);
            if(ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0){
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    School objSchool = new School();
                    objSchool.SchoolId = dr["SchoolId"] != DBNull.Value ? Convert.ToInt32(dr["SchoolId"]) : 0;
                    objSchool.Name = dr["Name"] != DBNull.Value ? Convert.ToString(dr["Name"]) : string.Empty;
                    objSchool.IsDeleted = dr["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(dr["IsDeleted"]) : true;

                    lstSchools.Add(objSchool);
                }
            }

            return lstSchools;
        }

        public static bool IsUserAuthorizedForSchool(int userId, int schoolId)
        {
            bool isUserAuthorized = false;
            DataSet ds = DataHelper.SchoolGetByUser(userId);
            

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int id = dr["SchoolId"] != DBNull.Value ? Convert.ToInt32(dr["SchoolId"]) : 0;
                    if (id == schoolId)
                    {
                        isUserAuthorized = true;                        
                    }
                }
            }

            return isUserAuthorized;
        }
    }
}

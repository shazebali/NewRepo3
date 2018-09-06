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
    public class StudyContext : DbContext
    {
        public StudyContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Study> Studies { get; set; }        
    }

    public class Study_Survey_ScheduleContext : DbContext
    {
        public Study_Survey_ScheduleContext()
            : base("DefaultConnection")
        {
        }
        
        public DbSet<Study_Survey_Schedule> SSSs { get; set; }
    }

    [Table("Study")]
    public class Study
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide name for study")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please select status of study")]
        public int Status { get; set; }
        public bool IsDeleted { get; set; }

        public static List<Study> StudyGetAll()
        {
            List<Study> lst = new List<Study>();
            using (var context = new StudyContext())
            {
                foreach (Study st in context.Studies.ToList().OrderBy(s => s.Name))
                {
                    if (st.IsDeleted == false && st.Status != 2)
                    {
                        lst.Add(st);
                    }
                }
            }

            return lst;
        }

        public static List<Study> StudyGetAllByUserId(int userId)
        {
            List<Study> lst = new List<Study>();

            DataSet ds = DataHelper.StudyGetAllByUserId(userId);
            if(ds != null && ds.Tables != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    Study objStudy = new Study();
                    objStudy.Id = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"]) : 0;
                    objStudy.Name = dr["Name"] != DBNull.Value ? Convert.ToString(dr["Name"]) : string.Empty;
                    objStudy.Status = dr["Status"] != DBNull.Value ? Convert.ToInt32(dr["Status"]) : 0;
                    objStudy.IsDeleted = dr["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(dr["IsDeleted"]) : true;

                    lst.Add(objStudy);
                }
            }

            return lst;
        }

        public static bool IsUserAuthorizedForStudy(int userId, int studyId)
        {
            bool isUserAuthorized = false;
            DataSet ds = DataHelper.StudyGetAllByUserId(userId);


            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int id = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"]) : 0;
                    if (id == studyId)
                    {
                        isUserAuthorized = true;
                    }
                }
            }

            return isUserAuthorized;
        }
    }

    

    [Table("Study_Survey_Schedule")]
    public class Study_Survey_Schedule
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int StudyId { get; set; }
        public int SurveyId { get; set; }
        public int ScheduleIdTeacher { get; set; }
        public int ScheduleIdParent { get; set; }
        public int ScheduleIdChild { get; set; }
    }

    public class StudyStatus{
        public int StatusId{ get; set; }
        public string StatusOption { get; set; }
        
        public static List<StudyStatus> GetAllStatus()
        {
            return new List<StudyStatus>() { new StudyStatus() { StatusId = 1, StatusOption = "Active" }, new StudyStatus() { StatusId = 2, StatusOption = "Inactive" }/*, new StudyStatus() { StatusId = 3, StatusOption = "Incomplete" }*/ };
        }
    }

    
}
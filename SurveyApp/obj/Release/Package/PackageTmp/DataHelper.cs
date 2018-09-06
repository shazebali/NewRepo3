﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Data.Entity;
using System.Linq;
using WebMatrix.WebData;

namespace SurveyApp
{
    public class DataHelper
    {
        //comment
        public static DataSet ExecuteCommandAsDataSet(SqlCommand Command) 
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                Command.Connection = conn;
                Command.CommandTimeout = 10000;
                if (Command.CommandType == CommandType.StoredProcedure)
                    Command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(Command);
                da.Fill(ds);
                conn.Close();
                return ds;
            }
        }
        public static DataTable ExecuteCommandAsDataTable(SqlCommand Command)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                Command.Connection = conn;
                Command.CommandTimeout = 10000;
                if (Command.CommandType == CommandType.StoredProcedure)
                    Command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(Command);
                da.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }
        }
        public static int ExecuteCommandAsNonQuery(SqlCommand Command)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                Command.Connection = conn;
                Command.CommandTimeout = 10000;
                if (Command.CommandType == CommandType.StoredProcedure)
                    Command.CommandType = CommandType.StoredProcedure;
                Command.Connection.Open();
                int r = Command.ExecuteNonQuery();
                conn.Close();
                return r;
            }
        }
        public static DataSet QuestionGetbySurveyID(int SurveyID, int childid, int UserID, DateTime FetchDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SurveyID", SqlDbType.Int);
            cmd.Parameters["@SurveyID"].Value = SurveyID;

            cmd.Parameters.Add("@childID", SqlDbType.Int);
            cmd.Parameters["@childID"].Value = Convert.ToInt32(childid);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = UserID;
            cmd.Parameters.Add("@FetchDate", SqlDbType.DateTime);
            cmd.Parameters["@FetchDate"].Value = FetchDate;
            
            cmd.CommandText = "Question_GetbySurveyID";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        public static DataSet QuestionGetFilledAnswers(int userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserId", SqlDbType.Int);
            cmd.Parameters["@UserId"].Value = userId;

            cmd.CommandText = "Question_GetFilledAnswers";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }        
        public static DataSet SurveyGetByID(int SurveyID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SurveyID", SqlDbType.Int);
            cmd.Parameters["@SurveyID"].Value = SurveyID;

            cmd.CommandText = "Survey_GetByID";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet SurveyGetAll()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            

            cmd.CommandText = "Survey_GetAll";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet SchoolGetAll(int userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserId", SqlDbType.Int);
            cmd.Parameters["@UserId"].Value = userId;

            cmd.CommandText = "School_GetAll";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet SchoolGetByUser(int userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserId", SqlDbType.Int);
            cmd.Parameters["@UserId"].Value = userId;

            cmd.CommandText = "School_GetByUser";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet ScheduleGetAll()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "Schedule_GetAll";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        public static DataSet ScheduleGetAllOccurence()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "Schedule_GetAllOccurence";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet StudyGetByID(int Id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Value = Id;

            cmd.CommandText = "Study_GetByID";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet StudyGetAll(int userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserId", SqlDbType.Int);
            cmd.Parameters["@UserId"].Value = userId;

            cmd.CommandText = "Study_GetAll";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet StudyGetAllByUserId(int userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserId", SqlDbType.Int);
            cmd.Parameters["@UserId"].Value = userId;

            cmd.CommandText = "StudyGetAllByUserId";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        

        public static DataSet Child_TeacherGetAll(int? childId = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            cmd.Parameters["@ChildID"].Value = childId.HasValue ? childId : null;

            cmd.CommandText = "Child_TeacherGetAll";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet Child_Study_TeacherGetAll(int childId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            cmd.Parameters["@ChildID"].Value = childId;

            cmd.CommandText = "Child_Study_TeacherGetAll";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet UserProfileGetUserByID(int UserID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = UserID;

            cmd.CommandText = "UserProfile_GetUserByID";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static Models.UserProfile UserProfileGetUserByUserName(string userName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar);
            cmd.Parameters["@UserName"].Value = userName;

            cmd.CommandText = "UserProfile_GetUserByName";

            Models.UserProfile objUser = new Models.UserProfile();
            DataSet dsUser = DataHelper.ExecuteCommandAsDataSet(cmd);

            if(dsUser != null && dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
            {
                objUser.FullName = dsUser.Tables[0].Rows[0]["FullName"] != DBNull.Value ? dsUser.Tables[0].Rows[0]["FullName"].ToString() : "";
                objUser.UserId = Convert.ToInt32(dsUser.Tables[0].Rows[0]["UserId"]);
                objUser.UserName = dsUser.Tables[0].Rows[0]["UserName"].ToString();
            }

            return objUser;
        }

        public static List<SurveyApp.Models.UserProfile> Parent_GetAll()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "Parent_GetAll";
            DataTable dt = DataHelper.ExecuteCommandAsDataTable(cmd);
            
            var convertedList = (from rw in dt.AsEnumerable()
                                 select new SurveyApp.Models.UserProfile()
                                 {
                                     UserId = Convert.ToInt32(rw["UserId"]),
                                     UserName = rw["UserName"].ToString(),
                                     FullName = Convert.ToString(rw["FullName"])
                                 }).ToList();

            return convertedList;

        }

        public static DataSet UserManagementGetUsers(int? UserID = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            if (UserID.HasValue)
            {
                cmd.Parameters["@UserID"].Value = UserID.Value;
            }
            else
            {
                cmd.Parameters["@UserID"].Value = DBNull.Value;
            }
            cmd.CommandText = "UserManagement_GetUsers";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet ChildGetAll(int? userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserId", SqlDbType.Int);
            if (userId.HasValue)
            {
                cmd.Parameters["@UserId"].Value = userId.Value;
            }
            else
            {
                cmd.Parameters["@UserId"].Value = DBNull.Value;
            }

            cmd.CommandText = "Child_GetAll";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        public static int SaveuserQuestions(int UserID, string  questionid, string answerid, string score, string childid, string SurveyID, string status, string percentage, DateTime dtSchedule, int scheduleId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            if (UserID != null) { cmd.Parameters["@UserID"].Value = UserID; } else { cmd.Parameters["@UserID"].Value = DBNull.Value; }
           
            
            
            cmd.Parameters.Add("@QuestionID", SqlDbType.Int);
            if (questionid != "") { cmd.Parameters["@QuestionID"].Value = questionid; } else { cmd.Parameters["@QuestionID"].Value = DBNull.Value; }

           
        
            cmd.Parameters.Add("@PAnswer", SqlDbType.NVarChar);
            if (answerid != "") { cmd.Parameters["@PAnswer"].Value = answerid; } else { cmd.Parameters["@PAnswer"].Value=DBNull.Value; }

            cmd.Parameters.Add("@Score", SqlDbType.Int);
            if (score != "" && score!="NaN") { cmd.Parameters["@Score"].Value = Convert.ToInt32(score); } else { cmd.Parameters["@Score"].Value = DBNull.Value; }
           

            cmd.Parameters.Add("@ChildId", SqlDbType.Int);
            if (childid != "") { cmd.Parameters["@ChildId"].Value = Convert.ToInt32(childid); } else { cmd.Parameters["@ChildId"].Value = DBNull.Value; }
            


            cmd.Parameters.Add("@SurveyID", SqlDbType.Int);
            if(SurveyID!=""){  cmd.Parameters["@SurveyID"].Value = Convert.ToInt32(SurveyID);} else {cmd.Parameters["@SurveyID"].Value = DBNull.Value;}

       
            cmd.Parameters.Add("@Pstatus", SqlDbType.NVarChar);
            if (status != "") { cmd.Parameters["@Pstatus"].Value = status; } else { cmd.Parameters["@Pstatus"].Value=DBNull.Value; }

            cmd.Parameters.Add("@Percentage", SqlDbType.NVarChar);
            if (percentage != "") { cmd.Parameters["@Percentage"].Value = percentage; } else { cmd.Parameters["@Percentage"].Value = DBNull.Value; }

            cmd.Parameters.Add("@ScheduleDate", SqlDbType.NVarChar);
            if (dtSchedule != DateTime.MinValue) { cmd.Parameters["@ScheduleDate"].Value = dtSchedule; } else { cmd.Parameters["@ScheduleDate"].Value = DBNull.Value; }

            cmd.Parameters.Add("@ScheduleId", SqlDbType.Int);
            if (scheduleId > 0) { cmd.Parameters["@ScheduleId"].Value = scheduleId; } else { cmd.Parameters["@ScheduleId"].Value = DBNull.Value; }


            cmd.CommandText = "SaveUserQuestions";
            return DataHelper.ExecuteCommandAsNonQuery(cmd);



        }
        public static int savequestionsAdverseReaction(
                string AdverseReaction,
                string DateOccured,
                string DateResolved,
                string Medication,
                string DateStart,
                string DateEnd,
                string DateSubmitted,
                int ChildID,
                int UserID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("@AdverseReaction", SqlDbType.NVarChar); 
			cmd.Parameters.Add("@DateOccured", SqlDbType.DateTime);
			cmd.Parameters.Add("@DateResolved", SqlDbType.DateTime); 
			cmd.Parameters.Add("@Medication", SqlDbType.NVarChar); 
			cmd.Parameters.Add("@DateStart", SqlDbType.DateTime); 
			cmd.Parameters.Add("@DateEnd", SqlDbType.DateTime); 
			cmd.Parameters.Add("@DateSubmitted", SqlDbType.DateTime); 
			cmd.Parameters.Add("@ChildID", SqlDbType.Int); 
			cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters["@AdverseReaction"].Value = AdverseReaction; 
			cmd.Parameters["@DateOccured"].Value     = IfDBNULL(DateOccured); 
			cmd.Parameters["@DateResolved"].Value    = IfDBNULL(DateResolved);
			cmd.Parameters["@Medication"].Value      = IfDBNULL(Medication); 
			cmd.Parameters["@DateStart"].Value       = IfDBNULL(DateStart); 
			cmd.Parameters["@DateEnd"].Value         = IfDBNULL(DateEnd); 
			cmd.Parameters["@DateSubmitted"].Value   = DateTime.Now; 
			cmd.Parameters["@ChildID"].Value         = ChildID;
            cmd.Parameters["@UserID"].Value = UserID;

            cmd.CommandText = "AdverseReaction_Add";
            return DataHelper.ExecuteCommandAsNonQuery(cmd);



        }

        public static int savequestionsCI(
                string title,
                string startDate,
                string endDate,
                string entryDate,
                int childId,
                int userId,
                int classroomIID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add("@Title", SqlDbType.NVarChar);
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@EntryDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@ClassroomIID", SqlDbType.Int);

            cmd.Parameters["@Title"].Value = title;
            cmd.Parameters["@StartDate"].Value = IfDBNULL(startDate);
            cmd.Parameters["@EndDate"].Value = IfDBNULL(endDate);
            cmd.Parameters["@EntryDate"].Value = IfDBNULL(entryDate);
            cmd.Parameters["@ChildID"].Value = childId;
            cmd.Parameters["@UserID"].Value = userId;
            cmd.Parameters["@ClassroomIID"].Value = classroomIID;

            cmd.CommandText = "ClassroomIntervention_Add";
            return DataHelper.ExecuteCommandAsNonQuery(cmd);



        }

        public static int savequestionsLifeEvent(
                string EventCategory,
                string EventName,
                string EventDate,
                string EventEndDate,
                string DateSubmitted,
                int ChildID,
                int UserID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@EventCategory", SqlDbType.NVarChar); 
            cmd.Parameters.Add("@EventName", SqlDbType.NVarChar); 
            cmd.Parameters.Add("@EventDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@EventEndDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@DateSubmitted", SqlDbType.DateTime); 
            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            cmd.Parameters["@EventCategory"].Value =IfDBNULL(EventCategory);
            cmd.Parameters["@EventName"].Value     =IfDBNULL(EventName);
            cmd.Parameters["@EventDate"].Value     =IfDBNULL(EventDate);
            cmd.Parameters["@EventEndDate"].Value = IfDBNULL(EventEndDate);
            cmd.Parameters["@DateSubmitted"].Value = DateTime.Now;
            cmd.Parameters["@ChildID"].Value       =ChildID ;
            cmd.Parameters["@UserID"].Value = UserID;

 

            cmd.CommandText = "LifeEvent_Add";
            return DataHelper.ExecuteCommandAsNonQuery(cmd);



        }
        public static object IfDBNULL(object value) { 
            if(value == null){ return DBNull.Value; }
            if (value.ToString() == "") { return DBNull.Value; }
            return value;
        }

        public static DataSet GetChildbyUserID(int Id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ID", SqlDbType.Int);
            cmd.Parameters["@ID"].Value = Id;

            cmd.CommandText = "Getchild_ByUserID";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        public static DataSet GetChildbyStudyID(int studyId, int userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters["@StudyID"].Value = studyId;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userId;

            cmd.CommandText = "Getchild_ByStudyID";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        public static DataSet getRespondentsByStudyId(int studyId, int userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters["@StudyID"].Value = studyId;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userId;

            cmd.CommandText = "getRespondentsByStudyId";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet Stats_GetRespondentsStats(int studyId, int respId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters["@StudyID"].Value = studyId;

            cmd.Parameters.Add("@RespondentId", SqlDbType.Int);
            cmd.Parameters["@RespondentId"].Value = respId;

            cmd.CommandText = "Stats_GetRespondentsStats";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet Stats_GetRespondentsCompletion()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "Stats_GetRespondentsCompletion";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet RolesGetAll()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.CommandText = "Roles_GetAll";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet ScheduleDeviationGetSchedules(int? studyId = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters["@StudyID"].Value = studyId;

            cmd.CommandText = "ScheduleDeviation_GetSchedules";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet getChildrenSchedulesByUserId(int userId, DateTime dtNow)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userId;

            cmd.Parameters.Add("@dtNow", SqlDbType.DateTime);
            cmd.Parameters["@dtNow"].Value = dtNow;

            cmd.CommandText = "User_GetChildSchedules";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }


        public static DataSet GetAdverseReaction(int userid, int studyId, int? childid = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            cmd.Parameters["@ChildID"].Value = childid;


            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters["@StudyID"].Value = studyId;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userid;
        

            cmd.CommandText = "OtherSurvey_Stats";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        public static DataSet GetWeeklyProgressForGraph(int StudyID, int childId, int userId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters["@StudyID"].Value = StudyID;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            if(childId > 0)
            {
                cmd.Parameters["@ChildID"].Value = childId;
            }
            else
            {
                cmd.Parameters["@ChildID"].Value = DBNull.Value;
            }            

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            if(userId > 0)
            {
                cmd.Parameters["@UserID"].Value = userId;
            }
            else
            {
                cmd.Parameters["@UserID"].Value = DBNull.Value;
            }            

            cmd.CommandText = "getWeeklyProgressForGraph";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        #region Dashborad
        public static DataSet DashboardGetByStudyId(int? studyId = 0, int? userId = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyId", SqlDbType.Int);
            cmd.Parameters["@StudyId"].Value = studyId;

            cmd.Parameters.Add("@UserId", SqlDbType.Int);
            cmd.Parameters["@UserId"].Value = userId;

            cmd.CommandText = "Dashboard_GetByStudyId";
            return DataHelper.ExecuteCommandAsDataSet(cmd);            
        }

        public static DataSet DashboardGetSchedulesByStudyId(int? studyId = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyId", SqlDbType.Int);
            cmd.Parameters["@StudyId"].Value = studyId;

            cmd.CommandText = "Dashboard_GetSchedulesByStudyId";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet DashboardGetSurveyQAInfo(int surveyId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SurveyId", SqlDbType.Int);
            cmd.Parameters["@SurveyId"].Value = surveyId;

            cmd.CommandText = "Dashboard_GetSurveyQAInfo";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        public static DataSet DashboardGetSchedulesForUserRoles(int? studyId = 0, int? userId = 0, int? childIdts = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyId", SqlDbType.Int);
            cmd.Parameters["@StudyId"].Value = studyId;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userId;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            if(childIdts == 0)
            {
                cmd.Parameters["@ChildId"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@ChildId"].Value = childIdts;
            }            

            cmd.CommandText = "Dashboard_GetSchedulesForUserRoles";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet Dashboard_getCompletion(int userId, DateTime dtNow, int? studyId = 0, int? childIdts = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyId", SqlDbType.Int);
            cmd.Parameters["@StudyId"].Value = studyId;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userId;

            cmd.Parameters.Add("@dtNow", SqlDbType.DateTime);
            cmd.Parameters["@dtNow"].Value = dtNow;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            if (childIdts == 0)
            {
                cmd.Parameters["@ChildId"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@ChildId"].Value = childIdts;
            }

            cmd.CommandText = "Dashboard_getCompletion";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        
        public static DataSet DashboardGetDetailComparison(int studyId, DateTime dtNow, int? childId = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyId", SqlDbType.Int);
            cmd.Parameters["@StudyId"].Value = studyId;

            cmd.Parameters.Add("@dtNow", SqlDbType.DateTime);
            cmd.Parameters["@dtNow"].Value = dtNow;

            cmd.Parameters.Add("@ChildId", SqlDbType.Int);
            if(childId == 0)
            {
                cmd.Parameters["@ChildId"].Value = null;
            }
            else
            {
                cmd.Parameters["@ChildId"].Value = childId;
            }            

            cmd.CommandText = "Dashboard_GetDetailComparison";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        #region Charts
        public static DataSet DashboardGetCharts(int studyId, int userId, int childId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StudyId", SqlDbType.Int);
            cmd.Parameters["@StudyId"].Value = studyId;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userId;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            if(childId > 0)
            {
                cmd.Parameters["@ChildID"].Value = childId;
            }
            else
            {
                cmd.Parameters["@ChildID"].Value = DBNull.Value;
            }
            

            cmd.CommandText = "Dashboard_GetChartsFixed";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet DashboardGetGraphs(int studyId, int userId, int childId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StudyId", SqlDbType.Int);
            cmd.Parameters["@StudyId"].Value = studyId;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userId;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            if (childId > 0)
            {
                cmd.Parameters["@ChildID"].Value = childId;
            }
            else
            {
                cmd.Parameters["@ChildID"].Value = DBNull.Value;
            }


            cmd.CommandText = "Dashboard_GetGraphs";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet DashboardGetLifeEventChart(int studyId, int userId, int childId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StudyId", SqlDbType.Int);
            cmd.Parameters["@StudyId"].Value = studyId;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userId;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            if (childId > 0)
            {
                cmd.Parameters["@ChildID"].Value = childId;
            }
            else
            {
                cmd.Parameters["@ChildID"].Value = DBNull.Value;
            }

            cmd.CommandText = "Dashboard_GetLifeEventChart";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        #endregion
        #endregion

        #region DataManager
        public static DataSet getAllData(int studyId, int userId, int childId, int surveyId, DateTime scheduleDate)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters["@StudyID"].Value = studyId;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userId;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);            
            cmd.Parameters["@ChildID"].Value = childId;

            cmd.Parameters.Add("@SurveyID", SqlDbType.Int);
            cmd.Parameters["@SurveyID"].Value = surveyId;

            cmd.Parameters.Add("@ScheduleDate", SqlDbType.DateTime);
            if (scheduleDate == DateTime.MinValue)
            {
                cmd.Parameters["@ScheduleDate"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@ScheduleDate"].Value = scheduleDate.ToShortDateString();
            }
            

            cmd.CommandText = "Data_GetAllData";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        public static DataSet getDefaultDataValues(int studyId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters["@StudyID"].Value = studyId;            

            cmd.CommandText = "Data_GetDefaultDataValues";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet getDefaultDataValuesBySchoolId(int? studyId = null, int? userId = null, int? surveyId = null, int? childId = null, int? schoolId = null, bool? includeTestRepondent = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@SurveyID", SqlDbType.Int);
            cmd.Parameters.Add("@ChildId", SqlDbType.Int);
            cmd.Parameters.Add("@SchoolId", SqlDbType.Int);
            cmd.Parameters.Add("@IncludeTestRespondents", SqlDbType.Int);

            if (studyId.HasValue && studyId.Value > 0) { cmd.Parameters["@StudyID"].Value = studyId.Value; } else { cmd.Parameters["@StudyID"].Value = DBNull.Value; }
            if (userId.HasValue && userId.Value > 0) { cmd.Parameters["@UserID"].Value = userId.Value; } else { cmd.Parameters["@UserID"].Value = DBNull.Value; }
            if (surveyId.HasValue && surveyId.Value > 0) { cmd.Parameters["@SurveyID"].Value = surveyId.Value; } else { cmd.Parameters["@SurveyID"].Value = DBNull.Value; }
            if (childId.HasValue && childId.Value > 0) { cmd.Parameters["@ChildID"].Value = childId.Value; } else { cmd.Parameters["@ChildID"].Value = DBNull.Value; }
            if (schoolId.HasValue && schoolId.Value > 0) { cmd.Parameters["@SchoolId"].Value = schoolId.Value; } else { cmd.Parameters["@SchoolId"].Value = DBNull.Value; }
            if (includeTestRepondent.HasValue) { cmd.Parameters["@IncludeTestRespondents"].Value = includeTestRepondent.Value; } else { cmd.Parameters["@IncludeTestRespondents"].Value = DBNull.Value; }


            cmd.CommandText = "Data_GetDefaultDataValuesBySchoolId";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet getAssignment(int? schId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ScheduleID", SqlDbType.Int);
            if (schId.HasValue && schId.Value > 0)
            {
                cmd.Parameters["@ScheduleID"].Value = schId;
            }
            else
            {
                cmd.Parameters["@ScheduleID"].Value = DBNull.Value;
            }

            cmd.CommandText = "Data_getAssignment";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        public static DataSet getAssignmentDetails(int studyId, int? childId = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyId", SqlDbType.Int);
            if(studyId > 0)
            {
                cmd.Parameters["@StudyId"].Value = studyId;
            }
            else
            {
                cmd.Parameters["@StudyId"].Value = DBNull.Value;
            }
            

            cmd.Parameters.Add("@ChildId", SqlDbType.Int);
            if (childId == 0)
            {
                cmd.Parameters["@ChildId"].Value = null;
            }
            else
            {
                cmd.Parameters["@ChildId"].Value = childId;
            }

            cmd.CommandText = "Data_getAssignmentDetails";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet getSubmittedData(int studyId, int userId, int? surveyId = 0)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters.Add("@SurveyID", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);

            if (studyId > 0)
            {
                cmd.Parameters["@StudyID"].Value = studyId;
            }
            else
            {
                cmd.Parameters["@StudyID"].Value = null;
            }

            if (userId > 0)
            {
                cmd.Parameters["@UserID"].Value = userId;
            }
            else
            {
                cmd.Parameters["@UserID"].Value = null;
            }

            if (surveyId.HasValue)
            {
                cmd.Parameters["@SurveyID"].Value = surveyId;
            }
            else
            {
                cmd.Parameters["@SurveyID"].Value = null;
            }

            cmd.CommandText = "Data_getSubmittedData";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        
        #endregion

        public static DataSet getClassroomInterventions(int criId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CriID", SqlDbType.Int);
            if(criId == 0)
            {
                cmd.Parameters["@CriID"].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters["@CriID"].Value = criId;
            }            

            cmd.CommandText = "GetClassroomInterventions";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet getRespondentsAndSurveys(int childId, int studyId, bool newChild)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@childId", SqlDbType.Int);            
            cmd.Parameters["@ChildId"].Value = childId;

            cmd.Parameters.Add("@StudyId", SqlDbType.Int);
            cmd.Parameters["@StudyId"].Value = studyId;

            cmd.Parameters.Add("@NewChild", SqlDbType.Bit);
            cmd.Parameters["@NewChild"].Value = newChild;

            cmd.CommandText = "Child_GetRespondentsAndSurveys";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static int deleteCRIntervention(int criId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CriId", SqlDbType.Int);
            cmd.Parameters["@CriId"].Value = criId;

            cmd.CommandText = "Assessment_DeleteCRIntervention";
            return DataHelper.ExecuteCommandAsNonQuery(cmd);
        }

        public static int deleteAdverseReaction(int arId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ArId", SqlDbType.Int);
            cmd.Parameters["@ArId"].Value = arId;

            cmd.CommandText = "Assessment_DeleteAdverseReaction";
            return DataHelper.ExecuteCommandAsNonQuery(cmd);
        }

        public static int deleteLifeEvent(int leId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@LeId", SqlDbType.Int);
            cmd.Parameters["@LeId"].Value = leId;

            cmd.CommandText = "Assessment_DeleteLifeEvent";
            return DataHelper.ExecuteCommandAsNonQuery(cmd);
        }
        #region cred
        public static int saveCred(string em, string pwd)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@em", SqlDbType.NVarChar);
            cmd.Parameters["@em"].Value = em;

            cmd.Parameters.Add("@pwd", SqlDbType.NVarChar);
            cmd.Parameters["@pwd"].Value = pwd;

            cmd.CommandText = "saveCred";
            return DataHelper.ExecuteCommandAsNonQuery(cmd);
        }

        public static DataSet getCred(string em)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@em", SqlDbType.NVarChar);
            cmd.Parameters["@em"].Value = em.Trim();
            
            cmd.CommandText = "GetGred";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        #endregion

        public static DataSet getAssignedChildrenByUserId(int userId, int studyId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userId;

            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters["@StudyID"].Value = studyId;

            cmd.CommandText = "Child_getAssignedChildrenByUserId";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet getStudiesByScheduleId(int scheduleId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ScheduleID", SqlDbType.Int);
            cmd.Parameters["@ScheduleID"].Value = scheduleId;

            cmd.CommandText = "Schedule_getStudiesByScheduleId";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet removePreviousScheduleDates(int childId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            cmd.Parameters["@ChildID"].Value = childId;

            cmd.CommandText = "Schedule_RemovePreviousScheduleDates";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        #region Child_Study_Respondents        
        public static DataSet getRespondents(int studyId, int childId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters["@StudyID"].Value = studyId;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            cmd.Parameters["@ChildID"].Value = childId;

            cmd.CommandText = "Child_GetRespondents";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        #endregion

        #region Consents
        public static DataSet Consent_GetAll()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.CommandText = "Consent_GetAll";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        public static DataSet Consent_GetByUserName(string userName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar);
            cmd.Parameters["@UserName"].Value = userName;

            cmd.CommandText = "Consent_GetByUserName";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        public static DataSet Consent_GetChildConsents(int childId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            cmd.Parameters["@ChildID"].Value = childId;

            cmd.CommandText = "Consent_GetChildConsents";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        #endregion
        #region ErrorLog
        public static int ErrorLog_Add(DateTime date, string url, string urlReferrer, string errorMessage, string stackTrace, string userAgent, string userHostAddress, string sessionId, string userName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Date", SqlDbType.DateTime);
            cmd.Parameters["@Date"].Value = date;

            cmd.Parameters.Add("@Url", SqlDbType.NVarChar);
            cmd.Parameters["@Url"].Value = url;

            cmd.Parameters.Add("@UrlReferrer", SqlDbType.NVarChar);
            cmd.Parameters["@UrlReferrer"].Value = urlReferrer;

            cmd.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar);
            cmd.Parameters["@ErrorMessage"].Value = errorMessage;

            cmd.Parameters.Add("@StackTrace", SqlDbType.NVarChar);
            cmd.Parameters["@StackTrace"].Value = stackTrace;

            cmd.Parameters.Add("@UserAgent", SqlDbType.NVarChar);
            cmd.Parameters["@UserAgent"].Value = userAgent;

            cmd.Parameters.Add("@UserHostAddress", SqlDbType.NVarChar);
            cmd.Parameters["@UserHostAddress"].Value = userHostAddress;

            cmd.Parameters.Add("@SessionId", SqlDbType.NVarChar);
            cmd.Parameters["@SessionId"].Value = sessionId;

            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar);
            cmd.Parameters["@UserName"].Value = userName;

            cmd.CommandText = "ErrorLog_Add";
            return DataHelper.ExecuteCommandAsNonQuery(cmd);
        }
        #endregion

        public static int updateFilledQuestionByScheduleId(int schId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ScheduleID", SqlDbType.Int);
            cmd.Parameters["@ScheduleID"].Value = schId;

            cmd.CommandText = "Schedule_updateFilledQuestionByScheduleId";
            return DataHelper.ExecuteCommandAsNonQuery(cmd);
        }

        #region Report
        public static DataSet getAccountRequests() {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.CommandText = "Report_GetAccountRequests";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        public static DataSet getSurveyScore(int? studyId = null, int? userId = null, int? surveyId = null, int? childId = null, int? schoolId = null, bool? includeTestRepondent = null)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@SurveyID", SqlDbType.Int);
            cmd.Parameters.Add("@ChildId", SqlDbType.Int);
            cmd.Parameters.Add("@SchoolId", SqlDbType.Int);
            cmd.Parameters.Add("@IncludeTestRespondents", SqlDbType.Int);

            if (studyId.HasValue && studyId.Value > 0) { cmd.Parameters["@StudyID"].Value = studyId.Value; } else { cmd.Parameters["@StudyID"].Value = DBNull.Value; }
            if (userId.HasValue && userId.Value > 0) { cmd.Parameters["@UserID"].Value = userId.Value; } else { cmd.Parameters["@UserID"].Value = DBNull.Value; }
            if (surveyId.HasValue && surveyId.Value > 0) { cmd.Parameters["@SurveyID"].Value = surveyId.Value; } else { cmd.Parameters["@SurveyID"].Value = DBNull.Value; }
            if (childId.HasValue && childId.Value > 0) { cmd.Parameters["@ChildID"].Value = childId.Value; } else { cmd.Parameters["@ChildID"].Value = DBNull.Value; }
            if (schoolId.HasValue && schoolId.Value > 0) { cmd.Parameters["@SchoolId"].Value = schoolId.Value; } else { cmd.Parameters["@SchoolId"].Value = DBNull.Value; }
            if (includeTestRepondent.HasValue) { cmd.Parameters["@IncludeTestRespondents"].Value = includeTestRepondent.Value; } else { cmd.Parameters["@IncludeTestRespondents"].Value = DBNull.Value; }
            
            cmd.CommandText = "Report_GetSurveyScore";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        #endregion

        public static int child_updateStatus(int childId, int newStatusId, DateTime dtEntryDate, string userName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ChildID", SqlDbType.Int);
            cmd.Parameters["@ChildID"].Value = childId;

            cmd.Parameters.Add("@StatusID", SqlDbType.Int);
            cmd.Parameters["@StatusID"].Value = newStatusId;

            cmd.Parameters.Add("@EntryDate", SqlDbType.DateTime);
            cmd.Parameters["@EntryDate"].Value = dtEntryDate;

            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar);
            cmd.Parameters["@UserName"].Value = userName;

            cmd.CommandText = "Child_UpdateStatus";
            return DataHelper.ExecuteCommandAsNonQuery(cmd);
        }

        public static int study_updateStatus(int studyId, int newStatusId, DateTime dtEntryDate, string userName)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@StudyID", SqlDbType.Int);
            cmd.Parameters["@StudyID"].Value = studyId;

            cmd.Parameters.Add("@StatusID", SqlDbType.Int);
            cmd.Parameters["@StatusID"].Value = newStatusId;

            cmd.Parameters.Add("@EntryDate", SqlDbType.DateTime);
            cmd.Parameters["@EntryDate"].Value = dtEntryDate;

            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar);
            cmd.Parameters["@UserName"].Value = userName;

            cmd.CommandText = "Study_UpdateStatus";
            return DataHelper.ExecuteCommandAsNonQuery(cmd);
        }

        public static DataSet Child_GetStatusHistory(int childId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@childId", SqlDbType.Int);
            cmd.Parameters["@childId"].Value = childId;

            cmd.CommandText = "Child_GetStatusHistory";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }

        public static DataSet Study_GetStatusHistory(int Id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Id", SqlDbType.Int);
            cmd.Parameters["@Id"].Value = Id;

            cmd.CommandText = "Study_GetStatusHistory";
            return DataHelper.ExecuteCommandAsDataSet(cmd);
        }
        
    }
}
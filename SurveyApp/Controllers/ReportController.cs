using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;

namespace SurveyApp.Controllers
{
    [System.Web.Mvc.Authorize]
    public class ReportController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AccountRequest()
        {
            return View();
        }
        public ActionResult Completion()
        {            
            return View();
        }
        public ActionResult SubmittedData()
        {
            return View();
        }

        public ActionResult SurveyScore()
        {
            if (!Roles.IsUserInRole(WebSecurity.CurrentUserName, "Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult DownloadSubmittedData(FormCollection collection)
        {

            if (!Roles.IsUserInRole(WebSecurity.CurrentUserName, "Administrator"))
            {
                return RedirectToAction("Index", "Home");
            }

            int surveyId = collection["hdnSurveyId"] != null ? Convert.ToInt32(collection["hdnSurveyId"]) : 0;
            string fileName = collection["hdnFileName"];
            if (WebSecurity.IsAuthenticated)
            {
                DataSet ds = DataHelper.getSubmittedData(0, WebSecurity.CurrentUserId, surveyId);

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ds.Tables[0]);
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Style.Font.Bold = true;

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".xlsx");

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }
            }            

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Net;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Scorecard.Models;
using System.IO;
using System.Data;

namespace Scorecard.Controllers
{
    public class HomeController : Controller

    {
        private HovisDWEntities db = new HovisDWEntities();

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Hovis Excellence Performance Management System.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Performance Management System contact page. ";

            return View();
        }

        [AllowAnonymous]
        public ActionResult BIME()
        {
            ViewBag.Message = "BIME reports page.";

            return View();
        }

        [Authorize(Roles = "canEditMasterData")]
        public ActionResult ImportExcelFileToDatabase()
        {

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "canEditMasterData")]
        public ActionResult ImportExcelFileToDatabase(HttpPostedFileBase file)
        {
            if (Request.Files["FileUpload"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["FileUpload"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    // Create a folder in App_Data named ExcelFiles because you need to save the file temporarily location and getting data from there. 
                    string fileLocation = Server.MapPath("~//Content/UploadedFolder/FileUpload.xls");
                    //string fileLocation = "/Users/andy.taylor/Documents/Visual Studio 2013/Projects/Scorecard/Scorecard/App_Data/ExcelFiles/FileUpload.xlsx";
                    //string fileLocation = string.Format("{0}/{1}",Server.MapPath("~/App_Data/ExcelFiles"), Request.Files["FileUpload"].FileName);
                    if (System.IO.File.Exists(fileLocation))
                        System.IO.File.Delete(fileLocation);
                    Request.Files["FileUpload"].SaveAs(fileLocation);
                    string excelConnectionString = string.Empty;
                    excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                    //connection String for xls file format.
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    //connection String for xlsx file format.
                    else if (fileExtension == ".xlsx")
                    {
                         excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }

                    //Create Connection to Excel work book and add oledb namespace
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();
                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }

                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;

                    //excel data saves in temp file here.

                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["TABLE_NAME"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                    DataSet ds = new DataSet();
                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    var Newdetail = new t_Scorecard_Target_Details_Upload();

                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Newdetail = new t_Scorecard_Target_Details_Upload();
                        Newdetail.Year = Convert.ToInt32(ds.Tables[0].Rows[i]["Year"].ToString());
                        Newdetail.Period = Convert.ToInt32(ds.Tables[0].Rows[i]["Period"].ToString());
                        Newdetail.SiteCode = ds.Tables[0].Rows[i]["SiteCode"].ToString();
                        Newdetail.MinorCatID = Convert.ToInt32(ds.Tables[0].Rows[i]["MinorCatID"].ToString());
                        Newdetail.Target = Convert.ToDouble(ds.Tables[0].Rows[i]["Target"].ToString());
                        Newdetail.Budget = Convert.ToDouble(ds.Tables[0].Rows[i]["Budget"].ToString());
                        Newdetail.Target2 = Convert.ToDouble(ds.Tables[0].Rows[i]["Target2"].ToString());
                        Newdetail.Budget2 = Convert.ToDouble(ds.Tables[0].Rows[i]["Budget2"].ToString());
                        Newdetail.ChangedDate = DateTime.Now;
                        db.t_Scorecard_Target_Details_Upload.Add(Newdetail);
                    }
                    db.SaveChanges();
                    excelConnection.Close();
                    excelConnection.Dispose();
                    excelConnection1.Close();
                    excelConnection1.Dispose();
                    ViewBag.message = "Information saved successfully.";

                }
                else
                {
                    ModelState.AddModelError("", "Plese select Excel File.");
                }
            }
            return View();
        }
    }
}
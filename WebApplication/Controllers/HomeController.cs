using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ParkInspectGroupC.DOMAIN;
using ParkInspectGroupC.Miscellaneous;
using MigraDoc.Rendering;
using System.IO;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string message)
        {
            if (message != "" && message != null)
            {
                ViewBag.Message = message;
            }
            
            return View();
        }

        public ActionResult Overzicht(string Id)
        {
            bool found = false;
            int id = Convert.ToInt32(Id);

            using (var context = new ParkInspectEntities())
            {
                foreach (var client in context.Customer)
                {
                    if (client.Id == id)
                    {
                        found = true;
                    }
                }

                if (found)
                {
                    //Get all the assigments from this customer
                    List<int> AssigmentList = new List<int>();
                    foreach (var ass in context.Assignment)
                    {
                        if (ass.CustomerId == id)
                        {
                            AssigmentList.Add(ass.Id);
                        }
                    }

                    //Get all the reports from the assigments
                    List<Report> ReportList = new List<Report>();
                    foreach (var ass in AssigmentList)
                    {
                        foreach (var report in context.Report)
                        {
                            if (report.AssignmentId == ass)
                            {
                                ReportList.Add(report);
                            }
                        }
                    }

                    ViewBag.Id = id;
                    return View(ReportList);
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { @message = "Klant nummer bestaat niet!" });
                }
            }    
        }

        public ActionResult Pdf(string Rid, string Cid)
        {
            int rid = Convert.ToInt32(Rid);
            using (var context = new ParkInspectEntities())
            {
                Report report = (from r in context.Report.Where(x => x.Id == rid) select r ).First();

                //Do pdf shit
                PdfGenerator pdf = new PdfGenerator();
                PdfDocumentRenderer renderer = pdf.GeneratePdfWeb(report, report.Employee, report.Title);

                //Save
                renderer.PdfDocument.Save("C:/Users/Public/" + report.Title + ".pdf");
            }

            //Return to view
            return RedirectToAction("Overzicht", "Home", new { @Id = Cid });
        }
    }
}
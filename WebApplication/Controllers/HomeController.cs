using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ParkInspectGroupC.DOMAIN;
using ParkInspectGroupC.Miscellaneous;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string message)
        {
            if ((message != "") && (message != null))
                ViewBag.Message = message;

            return View();
        }

        public ActionResult Overzicht(string Id)
        {
            var found = false;
            var id = Convert.ToInt32(Id);

            using (var context = new ParkInspectEntities())
            {
                foreach (var client in context.Customer)
                    if (client.Id == id)
                        found = true;

                if (found)
                {
                    //Get all the assigments from this customer
                    var AssigmentList = new List<int>();
                    foreach (var ass in context.Assignment)
                        if (ass.CustomerId == id)
                            AssigmentList.Add(ass.Id);

                    //Get all the reports from the assigments
                    var ReportList = new List<Report>();
                    foreach (var ass in AssigmentList)
                        foreach (var report in context.Report)
                            if (report.AssignmentId == ass)
                                ReportList.Add(report);

                    ViewBag.Id = id;
                    return View(ReportList);
                }
                return RedirectToAction("Index", "Home", new {message = "Klant nummer bestaat niet!"});
            }
        }

        public ActionResult Pdf(string Rid, string Cid)
        {
            var rid = Convert.ToInt32(Rid);
            using (var context = new ParkInspectEntities())
            {
                var report = (from r in context.Report.Where(x => x.Id == rid) select r).First();

                //Do pdf shit
                var pdf = new PdfGenerator();
                var renderer = pdf.GeneratePdfWeb(report, report.Employee, report.Title);

                //Save
                renderer.PdfDocument.Save("C:/Users/Public/" + report.Title + ".pdf");
            }

            //Return to view
            return RedirectToAction("Overzicht", "Home", new {Id = Cid});
        }
    }
}
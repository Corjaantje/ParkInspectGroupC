using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ParkInspectGroupC.DOMAIN;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using MigraDoc.DocumentObjectModel.Shapes;

namespace ParkInspectGroupC.Miscellaneous
{
	public class PdfGenerator
	{

		public PdfGenerator()
		{
			
		}

		public void GeneratePdf(Report report, Employee employee, string filename, string destination)
		{
			Document document = CreateDocument(report, employee);
			PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);

			renderer.Document = document;
			renderer.RenderDocument();
			renderer.PdfDocument.Save(filename);

			// Ask if the user wants to open the saved document
			AskToOpenFile(filename);

		}

		private Document CreateDocument(Report report, Employee employee)
		{
			Report loadedReport;
			List<ReportSection> loadedReportSections;
		    Employee loadedEmployee;
			using (var context = new ParkInspectEntities())
			{
				loadedReport = (from a in context.Report where a.Id == report.Id select a).First();
				loadedReportSections = (from a in context.ReportSection where a.ReportId == report.Id select a).ToList();
			    loadedEmployee = (from a in context.Employee where a.Id == employee.Id select a).First();
			}

			Document document = new Document();
			document.Info.Title = loadedReport.Title;
			document.Info.Author = loadedEmployee.FirstName + " " + loadedEmployee.SurName;

			this.DefineStyles(document);
			this.DefineCover(document, loadedReport, loadedEmployee);
			this.DefineSummary(document, loadedReport);

			// ReportSection
			foreach (var section in loadedReportSections)
			{
				DefineSection(document, section);
			}

			return document;
		}

		private void DefineStyles(Document document)
		{
			MigraDoc.DocumentObjectModel.Style style = document.Styles["Normal"];
			style.Font.Name = "Verdana";

			style = document.Styles["Heading1"];
			style.Font.Name = "Tahoma";
			style.Font.Size = 14;
			style.Font.Bold = true;
			style.Font.Color = Colors.DarkBlue;
			style.ParagraphFormat.PageBreakBefore = true;
			style.ParagraphFormat.SpaceAfter = 6;

			style = document.Styles["Heading2"];
			style.Font.Size = 12;
			style.Font.Bold = true;
			style.ParagraphFormat.PageBreakBefore = false;
			style.ParagraphFormat.SpaceBefore = 6;
			style.ParagraphFormat.SpaceAfter = 6;

			style = document.Styles["Heading3"];
			style.Font.Size = 10;
			style.Font.Bold = true;
			style.Font.Italic = true;
			style.ParagraphFormat.SpaceBefore = 6;
			style.ParagraphFormat.SpaceAfter = 3;

			style = document.Styles[StyleNames.Header];
			style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

			style = document.Styles[StyleNames.Footer];
			style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

			// Create a new style called TextBox based on style Normal
			style = document.Styles.AddStyle("TextBox", "Normal");
			style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
			style.ParagraphFormat.Borders.Width = 2.5;
			style.ParagraphFormat.Borders.Distance = "3pt";
			style.ParagraphFormat.Shading.Color = Colors.SkyBlue;

			// Create a new style called TOC based on style Normal
			style = document.Styles.AddStyle("TOC", "Normal");
			style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right, TabLeader.Dots);
			style.ParagraphFormat.Font.Color = Colors.Blue;
		}

		private void DefineCover(Document document, Report report, Employee employee)
		{
			Section section = document.AddSection();

			Paragraph paragraph = section.AddParagraph();
			paragraph.Format.SpaceAfter = "1cm";

			Image image = section.AddImage("../../Image/Logo_WhiteBack.PNG");
			image.Width = "10cm";

			paragraph = section.AddParagraph(report.Title);
			paragraph.Format.Font.Size = 22;
			paragraph.Format.Font.Color = Colors.Black;
			paragraph.Format.SpaceBefore = "1cm";

			Paragraph maker = section.AddParagraph();
			paragraph.Format.Font.Size = 15;
			paragraph.Format.Font.Color = Colors.Black;
			maker.AddText(employee.FirstName + " " + employee.Prefix  + " " +  employee.SurName);

			Paragraph date = section.AddParagraph();
			paragraph.Format.Font.Size = 15;
			paragraph.Format.Font.Color = Colors.Black;
			maker.AddText("; " + DateTime.Today);
		}

		private void DefineSummary(Document document, Report report)
		{
			Section section = document.AddSection();

			Paragraph title = section.AddParagraph();
			title.Format.Font.Size = 45;
			title.Format.Font.Bold = true;
			title.Format.SpaceAfter = 30;
			title.AddText(report.Title);

			Paragraph summary = section.AddParagraph();
			title.Format.Font.Size = 13;
			summary.AddText(report.Summary);
		}

		private void DefineSection(Document document, ReportSection reportSection)
		{
			Section section = document.AddSection();

			Paragraph title = section.AddParagraph();
			title.Format.Font.Size = 45;
			title.Format.Font.Bold = true;
			title.Format.SpaceAfter = 30;
			title.AddText(reportSection.Title);

			Paragraph summary = section.AddParagraph();
			title.Format.Font.Size = 13;
			summary.AddText(reportSection.Summary);

			DefineDiagram(section);
			DefineImage(section);
		}

		private void DefineDiagram(Section section)
		{
			
		}

		private void DefineImage(Section section)
		{
	        Paragraph paragraph = section.AddParagraph();
           
            Random random = new Random();
		    string current = Environment.CurrentDirectory;
            List<Image> images = new List<Image>();

		    string imageName = "0" + random.Next(1, 9);
		    string pathName = "../../Image/Testfotos/" + imageName + ".jpg";

            MigraDoc.DocumentObjectModel.Shapes.Image img = paragraph.AddImage(pathName);
		    img.LockAspectRatio = true;
            img.Width = Unit.FromPoint(500);
            img.Height = Unit.FromPoint(300);
        }

        private void AskToOpenFile(string filename)
        {
            //MessageBoxResult result = MessageBox.Show("Wilt u het bestand openen?", filename, MessageBoxButton.YesNo);

            //if (result == MessageBoxResult.Yes)
            //{
                Process.Start(filename);
            //}
        }
    }
}

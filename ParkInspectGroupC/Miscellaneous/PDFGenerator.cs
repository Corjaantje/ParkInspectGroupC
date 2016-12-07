using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using ParkInspectGroupC.DOMAIN;

namespace ParkInspectGroupC.Miscellaneous
{
	class PdfGenerator
	{

		public PdfGenerator()
		{
			
		}

		public void GeneratePdf(Report report, string filename, string destination)
		{
			var writer = new PdfWriter(filename);
			var pdf = new PdfDocument(writer);
			var document = new Document(pdf);

			// Title page
			document.Add(new Paragraph(report.Title));


			

			document.Add(new Paragraph("Hello World!"));
			document.Close();
		}	
	}
}

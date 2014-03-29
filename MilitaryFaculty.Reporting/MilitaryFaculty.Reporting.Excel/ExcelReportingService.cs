using System;
using System.Collections.Generic;
using System.IO;
using MilitaryFaculty.Extensions;
using MilitaryFaculty.Reporting.ReportObjectDomain;
using OfficeOpenXml;

namespace MilitaryFaculty.Reporting.Excel
{
	public class ExcelReportingService
	{
		public void ExportReport(string filePath, ReportObject reportObject)
		{
			if (String.IsNullOrWhiteSpace(filePath))
			{
				throw new ArgumentNullException("filePath");
			}

			File.Delete(filePath);
			var newFile = new FileInfo(filePath);

			using (var xlPackage = new ExcelPackage(newFile))
			{
				var ws = xlPackage.Workbook.Worksheets.Add("Resuts");

				reportObject.FormulasTables
					.ForEach(table => ExportTable(ws, table));

				xlPackage.Save();
			}
		}

		public void ExportReport(string filePath, ICollection<ReportObject> reportObject)
		{
			//if (String.IsNullOrWhiteSpace(filePath))
			//{
			//	throw new ArgumentNullException("filePath");
			//}

			//File.Delete(filePath);
			//var newFile = new FileInfo(filePath);

			//using (var xlPackage = new ExcelPackage(newFile))
			//{
			//	var ws = xlPackage.Workbook.Worksheets.Add("Resuts");

			//	reportObject.FormulasTables
			//		.ForEach(table => ExportTable(ws, table));

			//	xlPackage.Save();
			//}
		}

		private void ExportTable(ExcelWorksheet workSheet, ReportTable table)
		{
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			if (workSheet == null)
			{
				throw new ArgumentNullException("workSheet");
			}

			const int firstColumn = 2;
			var firstLine = workSheet.Dimension != null ? workSheet.Dimension.End.Row + 2 : 2;
			var xlWriter = new SingleInstanceExcelWriter(workSheet, firstLine, firstColumn);

			int totalRating = 0;

			xlWriter.PutName(table.Name);

			foreach (var group in table.FormulasGroups)
			{
				xlWriter.PutSubName(group.Name);

				foreach (var formulaInfo in group.FormulasInfo)
				{
					xlWriter.PutFieldLine(formulaInfo.Name,
										  formulaInfo.Value,
										  formulaInfo.MaxValue);

					totalRating += formulaInfo.Value;
				}
			}

			xlWriter.PutResults("Итог", totalRating);
			xlWriter.SetTableStyle();
		}
	}
}
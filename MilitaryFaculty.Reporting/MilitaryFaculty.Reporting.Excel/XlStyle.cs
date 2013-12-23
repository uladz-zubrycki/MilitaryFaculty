using System;
using Microsoft.Office.Interop.Excel;

//using Worksheet = DocumentFormat.OpenXml.Spreadsheet.Worksheet;

namespace MilitaryFaculty.Reporting.Excel
{
    public static class XlStyle
    {
        #region Class Public Methods

        public static void SetNameStyle(Range xlRange, string name)
        {
            if (xlRange == null)
            {
                throw new ArgumentNullException("xlRange");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name");
            }

            xlRange.Merge(false);
            xlRange.Value = name;
            xlRange.Font.Bold = true;
            xlRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }

        public static void SetSubNameStyle(Range xlRange, string name)
        {
            if (xlRange == null)
            {
                throw new ArgumentNullException("xlRange");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            xlRange.Merge(false);
            xlRange.Value = name;
            xlRange.Font.Bold = true;
        }

        public static void SetTableStyle(Range xlRange)
        {
            if (xlRange == null)
            {
                throw new ArgumentNullException("xlRange");
            }

            xlRange.BorderAround(XlLineStyle.xlDouble, XlBorderWeight.xlThick,
                                    XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic);
            xlRange.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;

            xlRange.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
            xlRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
            xlRange.WrapText = true;
        }

        public static void SetSheetStyle(Worksheet xlWorkSheet, int nameColumn, int totalRationColumn, int maxRatingColumn)
        {
            if (xlWorkSheet == null)
            {
                throw new ArgumentNullException("xlWorkSheet");
            }
            if (nameColumn < 1)
            {
                throw new ArgumentException("nameColumn");
            }
            if (totalRationColumn < 1)
            {
                throw new ArgumentException("totalRationColumn");
            }
            if (maxRatingColumn < 1)
            {
                throw new ArgumentException("maxRatingColumn");
            }

            ((Range)xlWorkSheet.Columns[1, Type.Missing]).EntireColumn.ColumnWidth = 2.5;
            ((Range)xlWorkSheet.Columns[nameColumn, Type.Missing]).EntireColumn.ColumnWidth = 100;
            ((Range)xlWorkSheet.Columns[totalRationColumn, Type.Missing]).EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)xlWorkSheet.Columns[maxRatingColumn, Type.Missing]).EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }

        #endregion // Class Public Methods
    }
}
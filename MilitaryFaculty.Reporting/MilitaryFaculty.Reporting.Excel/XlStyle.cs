using System;
using ClosedXML.Excel;

namespace MilitaryFaculty.Reporting.Excel
{
    public static class XlStyle
    {
        #region Class Static Variables

        public static int FirstColumn = 2;
        public static int LastColumn = 5;

        #endregion Class Static Variables

        #region Class Public Methods

        public static void SetNameStyle(IXLRange xlRange, string name)
        {
            if (xlRange == null)
            {
                throw new ArgumentNullException("xlRange");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name");
            }

            xlRange.Merge();
            xlRange.Value = name;
            xlRange.Style.Font.Bold = true;
            xlRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        public static void SetSubNameStyle(IXLRange xlRange, string name)
        {
            if (xlRange == null)
            {
                throw new ArgumentNullException("xlRange");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }

            xlRange.Merge();
            xlRange.Value = name;
            xlRange.Style.Font.Bold = true;
        }

        public static void SetTableStyle(IXLRange xlRange)
        {
            if (xlRange == null)
            {
                throw new ArgumentNullException("xlRange");
            }

            xlRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thick;
            xlRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            xlRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            xlRange.Style.Alignment.SetWrapText(true);
        }

        public static void SetSheetStyle(IXLWorksheet xlWorkSheet)
        {
            if (xlWorkSheet == null)
            {
                throw new ArgumentNullException("xlWorkSheet");
            }

            xlWorkSheet.Column(1).Width = 5;
            xlWorkSheet.Column(FirstColumn).Width = 5;
            xlWorkSheet.Column(FirstColumn + 1).Width = 105;
            xlWorkSheet.Column(FirstColumn + 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            xlWorkSheet.Column(FirstColumn + 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        #endregion // Class Public Methods
    }
}

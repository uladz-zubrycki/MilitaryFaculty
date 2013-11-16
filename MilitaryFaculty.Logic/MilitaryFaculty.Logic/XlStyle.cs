using System;
using Microsoft.Office.Interop.Excel;

namespace MilitaryFaculty.Logic
{
    public static class XlStyle
    {
        #region Class Public Methods

        public static void SetTableNameStyle(Range xlRange, string name)
        {
            xlRange.Merge(false);
            xlRange.FormulaR1C1 = name;
            xlRange.Font.Bold = true;
            xlRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;
        }

        public static void SetTableSubNameStyle(Range xlRange, string name)
        {
            xlRange.Merge(false);
            xlRange.FormulaR1C1 = name;
            xlRange.Font.Bold = true;
        }

        public static void SetTableStyle(Range xlRange)
        {
            xlRange.BorderAround(XlLineStyle.xlDouble, XlBorderWeight.xlThick,
                                    XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic);
            xlRange.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;

            xlRange.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
            xlRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
            xlRange.WrapText = true;
        }

        public static void SetSheetStyle(Worksheet xlWorkSheet, int nameColumn)
        {
            ((Range)xlWorkSheet.Columns[nameColumn, Type.Missing]).EntireColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            ((Range)xlWorkSheet.Columns[nameColumn, Type.Missing]).EntireColumn.ColumnWidth = 70; // ~500px
        }

        #endregion // Class Public Methods
    }
}

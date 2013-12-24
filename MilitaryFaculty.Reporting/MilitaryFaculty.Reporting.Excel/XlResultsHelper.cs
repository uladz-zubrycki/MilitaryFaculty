using System;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MilitaryFaculty.Reporting.Excel
{
    public class XlResultsHelper
    {
        #region Class Fields

        private readonly int firstColumn;
        private readonly int nameColumn;
        private readonly int valueColumn;
        private readonly int maxValueColumn;

        #endregion // Class Fields

        #region Class Constructors

        public XlResultsHelper(int firstColumn)
        {
            if (firstColumn < 1)
            {
                throw new ArgumentException("firstColumn");
            }

            this.firstColumn = firstColumn;
            nameColumn = firstColumn + 1;
            valueColumn = firstColumn + 2;
            maxValueColumn = firstColumn + 3;
        }

        #endregion // Class Constructors

        #region Class Public Methods

        public void PutName(string value, ExcelWorksheet workSheet, ref int curLine)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("value");
            }
            if (workSheet == null)
            {
                throw new ArgumentNullException("workSheet");
            }
            if (curLine < 1)
            {
                throw new ArgumentException("curLine");
            }

            var range = workSheet.Cells[curLine, firstColumn, curLine + 1, maxValueColumn];
            range.Merge = true;
            range.Value = value;
            range.Style.Font.Bold = true;
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            curLine += 2;
        }

        public void PutSubName(string value, ExcelWorksheet workSheet, ref int curLine)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("value");
            }
            if (workSheet == null)
            {
                throw new ArgumentNullException("workSheet");
            }
            if (curLine < 1)
            {
                throw new ArgumentException("curLine");
            }

            var range = workSheet.Cells[curLine, firstColumn, curLine, maxValueColumn];
            range.Merge = true;
            range.Value = value;
            range.Style.Font.Bold = true;
            curLine++;
        }

        public void PutFieldLine(string name, string value, string maxValue, ExcelWorksheet workSheet, ref int curLine)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("value");
            }
            if (String.IsNullOrWhiteSpace(maxValue))
            {
                throw new ArgumentNullException("maxValue");
            }
            if (workSheet == null)
            {
                throw new ArgumentNullException("workSheet");
            }
            if (curLine < 1)
            {
                throw new ArgumentException("curLine");
            }

            workSheet.Cells[curLine, nameColumn].Value = name;
            workSheet.Cells[curLine, valueColumn].Value = value;
            workSheet.Cells[curLine, maxValueColumn].Value = maxValue;
            curLine++;
        }

        public void PutResults(string name, string value, ExcelWorksheet workSheet, ref int curLine)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("value");
            }
            if (workSheet == null)
            {
                throw new ArgumentNullException("workSheet");
            }
            if (curLine < 1)
            {
                throw new ArgumentException("curLine");
            }

            var range = workSheet.Cells[curLine, firstColumn, curLine, nameColumn];
            range.Merge = true;
            range.Value = name;
            range.Style.Font.Bold = true;
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Cells[curLine, valueColumn].Value = value;
            curLine++;
        }

        public void SetTableStyle(int fistLine, int lastLine, ExcelWorksheet workSheet)
        {
            if (workSheet == null)
            {
                throw new ArgumentNullException("workSheet");
            }
            if (fistLine < 1)
            {
                throw new ArgumentException("curLine");
            }
            if (lastLine < 1)
            {
                throw new ArgumentException("curLine");
            }

            var range = workSheet.Cells[fistLine, firstColumn, lastLine, maxValueColumn];

            //Text wrap
            range.Style.WrapText = true;

            //Border
            var border = range.Style.Border;

            //Border inside
            const ExcelBorderStyle bStyle = ExcelBorderStyle.Thin;
            border.Top.Style = bStyle;
            border.Right.Style = bStyle;
            border.Bottom.Style = bStyle;
            border.Left.Style = bStyle;

            //Border around
            border.BorderAround(ExcelBorderStyle.Double);

            //Columns width
            if(firstColumn != 1) workSheet.Column(firstColumn - 1).Width = 5;
            workSheet.Column(firstColumn).Width = 5;
            workSheet.Column(nameColumn).Width = 105;
            workSheet.Column(valueColumn).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Column(maxValueColumn).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        #endregion // Class Public Methods
    }
}
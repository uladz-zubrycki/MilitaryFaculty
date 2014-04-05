using System;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MilitaryFaculty.Reporting.Excel
{
    public class MultipleInstancesExcelWriter
    {
        private readonly ExcelWorksheet _excelWorksheet;
        private readonly int _firstColumn;
        private readonly int _firstLine;
        private readonly int _maxValueColumn;
        private readonly int _nameColumn;
        private readonly int _valueColumn;
        private int _curLine;

        public MultipleInstancesExcelWriter(ExcelWorksheet excelWorksheet, int firstLine, int firstColumn)
        {
            if (excelWorksheet == null)
            {
                throw new ArgumentNullException("excelWorksheet");
            }
            if (firstLine < 1)
            {
                throw new ArgumentException("firstLine");
            }
            if (firstColumn < 1)
            {
                throw new ArgumentException("firstColumn");
            }

            _excelWorksheet = excelWorksheet;

            _curLine = firstLine;
            _firstLine = firstLine;

            _firstColumn = firstColumn;
            _nameColumn = firstColumn + 1;
            _valueColumn = firstColumn + 2;
            _maxValueColumn = firstColumn + 3;
        }

        public void PutName(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("value");
            }

            var range = _excelWorksheet.Cells[_curLine, _firstColumn, _curLine + 1, _maxValueColumn];
            range.Merge = true;
            range.Value = value;
            range.Style.Font.Bold = true;
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            _curLine += 2;
        }

        public void PutSubName(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException("value");
            }

            var range = _excelWorksheet.Cells[_curLine, _firstColumn, _curLine, _maxValueColumn];
            range.Merge = true;
            range.Value = value;
            range.Style.Font.Bold = true;

            _curLine += 1;
        }

        public void PutFieldLine(string name, int value, int maxValue)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            if (value < 0)
            {
                throw new ArgumentException("value");
            }
            if (maxValue < 0)
            {
                throw new ArgumentException("maxValue");
            }

            _excelWorksheet.Cells[_curLine, _nameColumn].Value = name;
            _excelWorksheet.Cells[_curLine, _valueColumn].Value = value;
            _excelWorksheet.Cells[_curLine, _maxValueColumn].Value = maxValue == 0 ? (object) "-" : maxValue;

            _curLine += 1;
        }

        public void PutResults(string name, int value)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name");
            }
            if (value < 0)
            {
                throw new ArgumentException("value");
            }

            //Name writing
            var range = _excelWorksheet.Cells[_curLine, _firstColumn, _curLine + 1, _nameColumn];
            range.Merge = true;
            range.Style.Font.Bold = true;
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            range.Value = name;

            //Value writing
            range = _excelWorksheet.Cells[_curLine, _valueColumn, _curLine + 1, _maxValueColumn];
            range.Merge = true;
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            range.Value = value;

            _curLine += 2;
        }

        public void SetTableStyle()
        {
            var lastLine = _curLine - 1;

            var range = _excelWorksheet.Cells[_firstLine, _firstColumn, lastLine, _maxValueColumn];

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
            if (_firstColumn != 1) _excelWorksheet.Column(_firstColumn - 1).Width = 5;
            _excelWorksheet.Column(_firstColumn).Width = 5;
            _excelWorksheet.Column(_nameColumn).Width = 105;
            _excelWorksheet.Column(_valueColumn).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            _excelWorksheet.Column(_maxValueColumn).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }
    }
}

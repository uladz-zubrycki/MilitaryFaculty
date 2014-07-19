using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace MilitaryFaculty.Reporting.Excel
{
    internal class ExcelWriter
    {
        private readonly ExcelWorksheet _excelWorksheet;
        private readonly int _firstLine;
        private readonly int _entityColumnsCount;
        //First columns:
        private readonly int _tableColumn;
        private readonly int _groupColumn;
        private readonly int _rowColumn;
        private readonly int _maxResultsColumn;
        private readonly int _entitiesColumn;

        private const string MaxConst = "Макс.";
        private const string ResultConst = "Итог";
        private static readonly Color GroupResultColor = ColorTranslator.FromHtml("#D9D9D9");
        private static readonly Color TableResultsColor = ColorTranslator.FromHtml("#A6A6A6");

        private int _curLine;
        private int _lastGroupLine;

        internal ExcelWriter(ExcelWorksheet excelWorksheet, int firstLine, int firstColumn,
            int entitiesCount)
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

            _lastGroupLine = firstLine;
            _curLine = firstLine;
            _firstLine = firstLine;
            _entityColumnsCount = entitiesCount;

            _tableColumn = firstColumn;
            _groupColumn = _tableColumn + 1;
            _rowColumn = _groupColumn + 1;
            _maxResultsColumn = _rowColumn + 1;
            _entitiesColumn = _maxResultsColumn + 1;
        }

        internal void PutTableHead(string tableName, ICollection<string> entityNames)
        {
            if (String.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException("tableName");
            }
            if (entityNames == null)
            {
                throw new ArgumentNullException("entityNames");
            }
            if (entityNames.Count == 0)
            {
                throw new ArgumentException("entityNames");
            }

            var range = _excelWorksheet.Cells[_curLine, _tableColumn, _curLine, _rowColumn];
            range.Merge = true;
            range.Value = tableName;
            range.Style.Font.Bold = true;

            _excelWorksheet.Cells[_curLine, _maxResultsColumn].Value = MaxConst;
            for (var i = 0; i < entityNames.Count; i++)
            {
                _excelWorksheet.Cells[_curLine, _entitiesColumn + i].Value = entityNames.ElementAt(i);
            }

            SetTableHeadStyles(_curLine);
            _curLine += 1;
        }

        internal void PutGroupHead(string groupName)
        {
            if (String.IsNullOrWhiteSpace(groupName))
            {
                throw new ArgumentNullException("groupName");
            }

            var range =
                _excelWorksheet.Cells[_curLine, _groupColumn, _curLine, _entitiesColumn + _entityColumnsCount - 1];
            range.Merge = true;
            range.Value = groupName;
            range.Style.Font.Bold = true;

            SetGroupHeadStyles(_curLine);
            _lastGroupLine = _curLine;
            _curLine += 1;
        }

        internal void PutResultsRow(string rowName, int maxRes, ICollection<int> results)
        {
            if (String.IsNullOrWhiteSpace(rowName))
            {
                throw new ArgumentNullException("rowName");
            }
            if (results == null)
            {
                throw new ArgumentNullException("results");
            }
            if (results.Count == 0)
            {
                throw new ArgumentException("results");
            }
            if (maxRes < 0)
            {
                throw new ArgumentException("maxRes");
            }

            _excelWorksheet.Cells[_curLine, _rowColumn].Value = rowName;

            _excelWorksheet.Cells[_curLine, _maxResultsColumn].Value = maxRes == 0 ? (object) "-" : maxRes;
            for (var i = 0; i < results.Count; i++)
            {
                _excelWorksheet.Cells[_curLine, _entitiesColumn + i].Value = results.ElementAt(i);
            }

            SetResultRowStyles(_curLine);
            _curLine += 1;
        }

        internal void PutGroupResults(ICollection<int> results)
        {
            if (results == null)
            {
                throw new ArgumentNullException("results");
            }
            if (results.Count == 0)
            {
                throw new ArgumentException("results");
            }

            var range = _excelWorksheet.Cells[_curLine, _groupColumn, _curLine, _maxResultsColumn];
            range.Merge = true;
            range.Value = ResultConst;

            for (var i = 0; i < results.Count; i++)
            {
                _excelWorksheet.Cells[_curLine, _entitiesColumn + i].Value = results.ElementAt(i);
            }

            SetGroupResultsStyles(_curLine);
            _curLine += 1;
        }

        internal void PutTableResults(ICollection<int> results)
        {
            if (results == null)
            {
                throw new ArgumentNullException("results");
            }
            if (results.Count == 0)
            {
                throw new ArgumentException("results");
            }

            var range = _excelWorksheet.Cells[_curLine, _tableColumn, _curLine, _maxResultsColumn];
            range.Merge = true;
            range.Value = ResultConst;

            for (var i = 0; i < results.Count; i++)
            {
                _excelWorksheet.Cells[_curLine, _entitiesColumn + i].Value = results.ElementAt(i);
            }

            SetTableResultsStyles(_curLine);
            _curLine += 1;
        }

        internal void SetGlobalStyles()
        {
            var lastLine = _curLine - 1;
            var range =
                _excelWorksheet.Cells[_firstLine, _tableColumn, lastLine, _entitiesColumn + _entityColumnsCount - 1];

            range.Style.WrapText = true;

            SetTableColumnsWidth();
            SetBordersAround(range);
        }

        #region Styles

        private void SetTableHeadStyles(int curLine)
        {
            var range =
                _excelWorksheet.Cells[curLine, _tableColumn, curLine, _entitiesColumn + _entityColumnsCount - 1];
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            SetBordersInside(range);
        }

        private void SetGroupHeadStyles(int curLine)
        {
            var range =
                _excelWorksheet.Cells[curLine, _groupColumn, curLine, _entitiesColumn + _entityColumnsCount - 1];
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            SetBordersInside(range);
        }

        private void SetResultRowStyles(int curLine)
        {
            var range = _excelWorksheet.Cells[curLine, _rowColumn, curLine, _entitiesColumn + _entityColumnsCount - 1];
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            SetBordersInside(range);
        }

        private void SetGroupResultsStyles(int curLine)
        {
            var lastLine = curLine - 1;
            var range = _excelWorksheet.Cells[_lastGroupLine + 1, _groupColumn, lastLine, _groupColumn];
            SetRangeColor(range, GroupResultColor);

            range = _excelWorksheet.Cells[curLine, _groupColumn, curLine, _entitiesColumn + _entityColumnsCount - 1];
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            SetRangeColor(range, GroupResultColor);

            SetBordersInside(range, false);
        }

        private void SetTableResultsStyles(int curLine)
        {
            var lastLine = curLine - 1;
            var range = _excelWorksheet.Cells[_firstLine + 1, _tableColumn, lastLine, _tableColumn];
            SetRangeColor(range, TableResultsColor);

            SetBordersInside(range, false, true, false, false);

            range = _excelWorksheet.Cells[curLine, _tableColumn, curLine, _entitiesColumn + _entityColumnsCount - 1];
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            SetRangeColor(range, TableResultsColor);

            SetBordersInside(range, false, true, false, false);
        }

        private void SetTableColumnsWidth()
        {
            if (_tableColumn != 1) _excelWorksheet.Column(_tableColumn - 1).Width = 5;
            _excelWorksheet.Column(_tableColumn).Width = 3;
            _excelWorksheet.Column(_groupColumn).Width = 3;
            _excelWorksheet.Column(_rowColumn).Width = 80;
            _excelWorksheet.Column(_maxResultsColumn).Width = 10;

            for (var i = 0; i < _entityColumnsCount; i++)
            {
                _excelWorksheet.Column(_entitiesColumn + i).Width = 25;
            }

            _excelWorksheet.Column(_maxResultsColumn).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        private void SetBordersInside(
            ExcelRange range,
            bool top = true,
            bool right = true,
            bool bottom = true,
            bool left = true)
        {
            const ExcelBorderStyle bStyle = ExcelBorderStyle.Thin;

            var border = range.Style.Border;
            if (top)
            {
                border.Top.Style = bStyle;
            }
            if (right)
            {
                border.Right.Style = bStyle;
            }
            if (bottom)
            {
                border.Bottom.Style = bStyle;
            }
            if (left)
            {
                border.Left.Style = bStyle;
            }
        }

        private void SetBordersAround(ExcelRange range)
        {
            var border = range.Style.Border;
            border.BorderAround(ExcelBorderStyle.Double);
        }

        private void SetRangeColor(ExcelRange range, Color color)
        {
            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(color);
        }

        #endregion // Styles
    }
}
using System;
using System.Text;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;

namespace Excel
{
    public class Utility
    {
        public IWorkbook GetIWorkbook(Stream stream, string fileExtens)
        {
            IWorkbook iworkbook = null;
            try
            {
                if (fileExtens.EndsWith(".xls"))
                {
                    iworkbook = new HSSFWorkbook(stream);
                }
                else if (fileExtens.EndsWith(".xlsx"))
                {
                    iworkbook = new XSSFWorkbook(stream);
                }
            }
            catch (Exception et)
            {
                throw et;
            }
            return iworkbook;
        }

        public IWorkbook GetIWorkbook(string filePath)
        {
            IWorkbook iworkbook = null;
            try
            {
                if (filePath.EndsWith(".xls"))
                {
                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        iworkbook = new HSSFWorkbook(file);
                    }
                }
                else if (filePath.EndsWith(".xlsx"))
                {
                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        iworkbook = new XSSFWorkbook(file);
                    }
                }
            }
            catch (Exception et)
            {
                throw et;
            }
            return iworkbook;
        }

        public ISheet GetISheet(string filePath, int index = 0)
        {
            IWorkbook iworkbook = GetIWorkbook(filePath);
            ISheet isheet = iworkbook.GetSheetAt(index);
            return isheet;
        }

        public ISheet GetISheet(string filePath, string sheetName)
        {
            IWorkbook iworkbook = GetIWorkbook(filePath);
            ISheet isheet = iworkbook.GetSheet(sheetName);
            return isheet;
        }

        public ISheet GetISheet(Stream stream, string fileExtens, int index = 0)
        {
            IWorkbook iworkbook = GetIWorkbook(stream, fileExtens);
            ISheet isheet = iworkbook.GetSheetAt(index);
            return isheet;
        }

        public ISheet CreateExcelSheet(string sheetName = "autosheet")
        {
            IWorkbook iworkbook = new HSSFWorkbook();
            ISheet isheet = iworkbook.CreateSheet(sheetName);
            return isheet;
        }

        public void WriteDataToSheet(ISheet sheet, string line, int rowIndex)
        {
            IRow row = sheet.CreateRow(rowIndex);
            string[] values = line.Split(',');
            int columIndex = 0;
            foreach (var value in values)
            {
                ICell iCell = row.CreateCell(columIndex);
                iCell.SetCellValue(value);
                columIndex++;
            }
        }

        public void WriteDataToSheet(object objectSheet, string line, int rowIndex)
        {
            ISheet sheet = (ISheet)objectSheet;
            IRow row = sheet.CreateRow(rowIndex);
            string[] values = line.Split(',');
            int columIndex = 0;
            foreach (var value in values)
            {
                ICell iCell = row.CreateCell(columIndex);
                iCell.SetCellValue(value);
                columIndex++;
            }
        }

        public void SaveSheetData(ISheet sheet, string excelFullPath)
        {
            if (string.IsNullOrEmpty(excelFullPath))
                throw new Exception("parameter filePath is empty, so wrong!");

            using (MemoryStream ms = new MemoryStream())
            {
                sheet.Workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                using (FileStream fs = new FileStream(excelFullPath, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        public void SaveSheetData(object objectSheet, string filePath)
        {
            ISheet sheet = (ISheet)objectSheet;
            SaveSheetData(sheet, filePath);
        }

        /// <summary>
        ///  获取Table某个TD合并的列数和行数等信息。与Excel中对应Cell的合并行数和列数一致。
        /// </summary>
        /// <param name="rowIndex">行号</param>
        /// <param name="colIndex">列号</param>
        /// <param name="colspan">TD中需要合并的行数</param>
        /// <param name="rowspan">TD中需要合并的列数</param>
        /// <param name="rowspan">此单元格是否被某个行合并包含在内。如果被包含在内，将不输出TD。</param>
        /// <returns></returns>
        private void GetTdMergedInfo(ISheet sheet, int rowIndex, int colIndex, out int colspan, out int rowspan, out bool isByRowMerged)
        {
            colspan = 1;
            rowspan = 1;
            isByRowMerged = false;
            int regionsCuont = sheet.NumMergedRegions;
            CellRangeAddress region;
            for (int i = 0; i < regionsCuont; i++)
            {
                region = sheet.GetMergedRegion(i);
                if (region.FirstRow == rowIndex && region.FirstColumn == colIndex)
                {
                    colspan = region.LastColumn - region.FirstColumn + 1;
                    rowspan = region.LastRow - region.FirstRow + 1;

                    return;
                }
                else if (rowIndex > region.FirstRow && rowIndex <= region.LastRow && colIndex >= region.FirstColumn && colIndex <= region.LastColumn)
                {
                    isByRowMerged = true;
                }
            }
        }

        public string ToHtml(ISheet sheet)
        {
            string excelContent = "";
            //取行Excel的最大行数
            int rowsCount = sheet.PhysicalNumberOfRows;
            //为保证Table布局与Excel一样，这里应该取所有行中的最大列数（需要遍历整个Sheet）。
            //为少一交全Excel遍历，提高性能，我们可以人为把第0行的列数调整至所有行中的最大列数。
            int colsCount = sheet.GetRow(0).PhysicalNumberOfCells;

            int colSpan;
            int rowSpan;
            bool isByRowMerged;

            StringBuilder table = new StringBuilder(rowsCount * 32);

            table.Append("<table border='1px'>");
            for (int rowIndex = 0; rowIndex < rowsCount; rowIndex++)
            {
                table.Append("<tr>");
                for (int colIndex = 0; colIndex < colsCount; colIndex++)
                {
                    GetTdMergedInfo(sheet, rowIndex, colIndex, out colSpan, out rowSpan, out isByRowMerged);
                    //如果已经被行合并包含进去了就不输出TD了。
                    //注意被合并的行或列不输出的处理方式不一样，见下面一处的注释说明了列合并后不输出TD的处理方式。
                    if (isByRowMerged)
                    {
                        continue;
                    }

                    table.Append("<td");
                    if (colSpan > 1)
                        table.Append(string.Format(" colSpan={0}", colSpan));
                    if (rowSpan > 1)
                        table.Append(string.Format(" rowSpan={0}", rowSpan));
                    table.Append(">");
                    IRow currentRow = sheet.GetRow(rowIndex);
                    ICell currentCell = null;
                    if (currentRow == null)
                    {
                        continue;
                    }
                    else
                    {
                        currentCell = currentRow.GetCell(colIndex);
                    }

                    if (currentCell != null && currentCell.CellType == CellType.String)
                    {
                        table.Append(currentCell.RichStringCellValue);
                    }
                    else if (currentCell != null && currentCell.CellType == CellType.Numeric)
                    {
                        table.Append(currentCell.NumericCellValue);
                    }
                    else if (currentCell != null && currentCell.CellType == CellType.Formula)
                    {
                        table.Append(currentCell.NumericCellValue.ToString("#.###"));
                    }

                    //列被合并之后此行将少输出colSpan-1个TD。
                    if (colSpan > 1)
                        colIndex += colSpan - 1;

                    table.Append("</td>");

                }
                table.Append("</tr>");
            }
            table.Append("</table>");

            excelContent = table.ToString();
            return excelContent;
        }

        public void MergedCells(string targetExcel, string excelFullPath )
        {
            ISheet sheet = GetISheet(targetExcel);
            int regionsCuont = sheet.NumMergedRegions;
            CellRangeAddress region;
            for (int i = regionsCuont -1; i >= 0; i--)
            {
                region = sheet.GetMergedRegion(i);
                if(region == null)
                {
                    continue;
                }
                string content = GetMergedContent(region,sheet);
                int fRow = region.FirstRow;
                int lRow = region.LastRow;
                int fColumn = region.FirstColumn;
                int lColumn = region.LastColumn;
                sheet.RemoveMergedRegion(i);//此处有坑，会造成NumMergedRegions的减少，从后面移除

                for (int row = fRow; row <= lRow ; row++)
                {
                    for (int colum = fColumn; colum <= lColumn; colum++)
                    {
                        IRow iRow = sheet.GetRow(row);
                        if(iRow != null)
                        {
                            ICell iCell = iRow.GetCell(colum);
                            if (iCell != null)
                            {
                                iCell.SetCellValue(content);
                            }
                        }
                    }
                }
            }

            SaveSheetData(sheet, excelFullPath);
        }

        private string GetMergedContent(CellRangeAddress region ,ISheet sheet)
        {
            string content = "";
            int fRow = region.FirstRow;
            int lRow = region.LastRow;
            int fColumn = region.FirstColumn;
            int lColumn = region.LastColumn;
            for (int row = fRow; row <= lRow; row++)
            {
                for (int colum = fColumn; colum <= lColumn; colum++)
                {
                    IRow iRow = sheet.GetRow(row);
                    if (iRow != null)
                    {
                        ICell iCell = iRow.GetCell(colum);
                        if (iCell != null)
                        {                          
                            if(!string.IsNullOrEmpty(iCell.StringCellValue))
                            {
                                content = iCell.StringCellValue ;
                                break;
                            }
                        }
                    }
                }
            }
            return content;
        }
    }
}

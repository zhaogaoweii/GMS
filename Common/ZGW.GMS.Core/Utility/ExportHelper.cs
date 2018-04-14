using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AppLibrary.WriteExcel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 导出功能的辅助类
    /// </summary>
    public class ExportHelper
    {
        /// <summary>
        /// 导出到Excel中
        /// </summary>
        /// <param name="data">导出的数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">导出的SheetName</param>
        /// <param name="columnsWidth">设定每列的宽度</param>
        public static void ExportToExcel(DataTable data, int[] columnsWidth, string fileName, string sheetName = "sheet1")
        {
            HttpContext hc = HttpContext.Current;
            ArrayList al = new ArrayList();
            int count = data.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                string t = data.Columns[i].Caption;
                al.Add(t);
            }

            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = hc.Server.UrlEncode(fileName) + ".xls";
            AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add(sheetName);
            AppLibrary.WriteExcel.Cells cells = sheet.Cells;
            #region 第一个样式
            AppLibrary.WriteExcel.XF XFstyle = doc.NewXF();//添加样式的
            XFstyle.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
            XFstyle.Font.FontName = "宋体";//字体
            XFstyle.UseMisc = true;
            XFstyle.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;//文本位置
            XFstyle.Font.Bold = false;//加粗
            #region 边框线的样式
            XFstyle.BottomLineStyle = 1;
            XFstyle.LeftLineStyle = 1;
            XFstyle.TopLineStyle = 1;
            XFstyle.RightLineStyle = 1;
            #endregion
            XFstyle.UseBorder = true;
            XFstyle.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
            XFstyle.PatternColor = AppLibrary.WriteExcel.Colors.White;
            XFstyle.Pattern = 1;
            #region 宽度
            if (columnsWidth != null)
            {
                for (int i = 0; i < columnsWidth.Length; i++)
                {
                    ColumnInfo colInfo = new ColumnInfo(doc, sheet);
                    colInfo.ColumnIndexStart = (ushort)i;
                    colInfo.ColumnIndexEnd = (ushort)i;
                    colInfo.Width = (ushort)(columnsWidth[i] * 256);
                    sheet.AddColumnInfo(colInfo);
                }
            }
            #endregion
            //AppLibrary.WriteExcel.ColumnInfo colInfo = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo.ColumnIndexStart = 0;
            //colInfo.ColumnIndexEnd = 4;
            //colInfo.Width = 10 * 256;
            //sheet.AddColumnInfo(colInfo);
            //AppLibrary.WriteExcel.ColumnInfo colInfo1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo1.ColumnIndexStart = 5;
            //colInfo1.ColumnIndexEnd = 7;
            //colInfo1.Width = 15 * 256;
            //sheet.AddColumnInfo(colInfo1);

            //AppLibrary.WriteExcel.ColumnInfo colInfo2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo2.ColumnIndexStart = 8;
            //colInfo2.ColumnIndexEnd = 9;
            //colInfo2.Width = 15 * 256;
            //sheet.AddColumnInfo(colInfo2);

            //AppLibrary.WriteExcel.ColumnInfo colInfo3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo3.ColumnIndexStart = 12;
            //colInfo3.ColumnIndexEnd = 12;
            //colInfo3.Width = 15 * 256;
            //sheet.AddColumnInfo(colInfo3);

            //AppLibrary.WriteExcel.ColumnInfo colInfo4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo4.ColumnIndexStart = 13;
            //colInfo4.ColumnIndexEnd = 13;
            //colInfo4.Width = 50 * 256;
            //sheet.AddColumnInfo(colInfo4);

            //AppLibrary.WriteExcel.ColumnInfo colInfo5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo5.ColumnIndexStart = 14;
            //colInfo5.ColumnIndexEnd = 14;
            //colInfo5.Width = 15 * 256;
            //sheet.AddColumnInfo(colInfo5);

            //AppLibrary.WriteExcel.ColumnInfo colInfo6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo6.ColumnIndexStart = 15;
            //colInfo6.ColumnIndexEnd = 15;
            //colInfo6.Width = 50 * 256;
            //sheet.AddColumnInfo(colInfo6);
            //#endregion
            #endregion
            #region 添加表头
            int idx = 1;
            for (int i = 0; i < al.Count; i++)
            {
                cells.Add(1, idx, al[i], XFstyle);
                idx++;
            }
            #endregion
            int f = 2;//从第二行开始填充数据
            foreach (DataRow row in data.Rows)
            {
                for (int c = 0; c < data.Columns.Count; c++)
                {
                    cells.Add(f, c + 1, row[c] != DBNull.Value ? row[c] : null, XFstyle);
                }
                f++;
            }
            doc.Send();
            hc.Response.Flush();
            hc.Response.End();
        }
        /// <summary>
        /// 导出到Excel中
        /// </summary>
        /// <param name="data">导出的数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">导出的SheetName</param>
        /// <param name="columnsWidth">设定每列的宽度</param>
        public static void ExportToExcelNew(DataTable data, int[] columnsWidth, string fileName, string sheetName = "sheet1")
        {
            HttpContext hc = HttpContext.Current;
            ArrayList al = new ArrayList();
            int count = data.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                string t = data.Columns[i].Caption;
                al.Add(t);
            }

            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = hc.Server.UrlEncode(fileName) + ".xls";

            //一个SHEET显示多少条
            int intBase = 20000;
            //需要几个sheet
            int intSheet = data.Rows.Count / intBase;
            //剩余数据
            int leaveData = data.Rows.Count % intBase;
            if (leaveData > 0)
            {
                intSheet = intSheet + 1;
            }
            int f = 2;//从第二行开始填充数据
            for (int sheet_i = 0; sheet_i < intSheet; sheet_i++)
            {
                f = 2;

                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add(sheetName + (sheet_i + 1).ToString());
                AppLibrary.WriteExcel.Cells cells = sheet.Cells;
                #region 第一个样式
                AppLibrary.WriteExcel.XF XFstyle = doc.NewXF();//添加样式的
                XFstyle.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
                XFstyle.Font.FontName = "宋体";//字体
                XFstyle.UseMisc = true;
                XFstyle.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;//文本位置
                XFstyle.Font.Bold = false;//加粗
                #region 边框线的样式
                XFstyle.BottomLineStyle = 1;
                XFstyle.LeftLineStyle = 1;
                XFstyle.TopLineStyle = 1;
                XFstyle.RightLineStyle = 1;
                #endregion
                XFstyle.UseBorder = true;
                XFstyle.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
                XFstyle.PatternColor = AppLibrary.WriteExcel.Colors.White;
                XFstyle.Pattern = 1;
                #region 宽度
                if (columnsWidth != null)
                {
                    for (int i = 0; i < columnsWidth.Length; i++)
                    {
                        ColumnInfo colInfo = new ColumnInfo(doc, sheet);
                        colInfo.ColumnIndexStart = (ushort)i;
                        colInfo.ColumnIndexEnd = (ushort)i;
                        colInfo.Width = (ushort)(columnsWidth[i] * 256);
                        sheet.AddColumnInfo(colInfo);
                    }
                }
                #endregion

                #endregion
                #region 添加表头
                int idx = 1;
                for (int i = 0; i < al.Count; i++)
                {
                    cells.Add(1, idx, al[i], XFstyle);
                    idx++;
                }
                #endregion
                //填充数据
                int forIntRow = intBase;
                if ((sheet_i + 1) == intSheet && leaveData > 0)
                {
                    forIntRow = leaveData;
                }
                //从每个sheet，填充开始row
                for (int for_i = 0 + (sheet_i * intBase); for_i - (sheet_i * intBase) < forIntRow; for_i++)
                {
                    DataRow row = data.Rows[for_i];
                    for (int c = 0; c < data.Columns.Count; c++)
                    {
                        cells.Add(f, c + 1, row[c] != DBNull.Value ? row[c] : null, XFstyle);
                    }
                    f++;
                }
                ////int f = 2;//从第二行开始填充数据
                //foreach (DataRow row in data.Rows)
                //{
                //    for (int c = 0; c < data.Columns.Count; c++)
                //    {
                //        cells.Add(f, c + 1, row[c] != DBNull.Value ? row[c] : null, XFstyle);
                //    }
                //    f++;
                //}
            }
            doc.Send();
            hc.Response.Flush();
            hc.Response.End();
        }
        /// <summary>
        /// 导出到Excel中
        /// </summary>
        /// <param name="data">导出的数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="filePath">excel到处路径</param>
        /// <param name="sheetName">导出的SheetName</param>
        /// <param name="columnsWidth">设定每列的宽度</param>
        public static void ExportToExcelSave(DataTable data, int[] columnsWidth, string fileName, string filePath, string sheetName = "sheet1")
        {
            HttpContext hc = HttpContext.Current;
            ArrayList al = new ArrayList();
            int count = data.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                string t = data.Columns[i].Caption;
                al.Add(t);
            }

            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = fileName + ".xls";
            AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add(sheetName);
            AppLibrary.WriteExcel.Cells cells = sheet.Cells;
            #region 第一个样式
            AppLibrary.WriteExcel.XF XFstyle = doc.NewXF();//添加样式的
            XFstyle.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
            XFstyle.Font.FontName = "宋体";//字体
            XFstyle.UseMisc = true;
            XFstyle.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;//文本位置
            XFstyle.Font.Bold = false;//加粗
            #region 边框线的样式
            XFstyle.BottomLineStyle = 1;
            XFstyle.LeftLineStyle = 1;
            XFstyle.TopLineStyle = 1;
            XFstyle.RightLineStyle = 1;
            #endregion
            XFstyle.UseBorder = true;
            XFstyle.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
            XFstyle.PatternColor = AppLibrary.WriteExcel.Colors.White;
            XFstyle.Pattern = 1;
            #region 宽度
            if (columnsWidth != null)
            {
                for (int i = 0; i < columnsWidth.Length; i++)
                {
                    ColumnInfo colInfo = new ColumnInfo(doc, sheet);
                    colInfo.ColumnIndexStart = (ushort)i;
                    colInfo.ColumnIndexEnd = (ushort)i;
                    colInfo.Width = (ushort)(columnsWidth[i] * 256);
                    sheet.AddColumnInfo(colInfo);
                }
            }
            #endregion
            //AppLibrary.WriteExcel.ColumnInfo colInfo = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo.ColumnIndexStart = 0;
            //colInfo.ColumnIndexEnd = 4;
            //colInfo.Width = 10 * 256;
            //sheet.AddColumnInfo(colInfo);
            //AppLibrary.WriteExcel.ColumnInfo colInfo1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo1.ColumnIndexStart = 5;
            //colInfo1.ColumnIndexEnd = 7;
            //colInfo1.Width = 15 * 256;
            //sheet.AddColumnInfo(colInfo1);

            //AppLibrary.WriteExcel.ColumnInfo colInfo2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo2.ColumnIndexStart = 8;
            //colInfo2.ColumnIndexEnd = 9;
            //colInfo2.Width = 15 * 256;
            //sheet.AddColumnInfo(colInfo2);

            //AppLibrary.WriteExcel.ColumnInfo colInfo3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo3.ColumnIndexStart = 12;
            //colInfo3.ColumnIndexEnd = 12;
            //colInfo3.Width = 15 * 256;
            //sheet.AddColumnInfo(colInfo3);

            //AppLibrary.WriteExcel.ColumnInfo colInfo4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo4.ColumnIndexStart = 13;
            //colInfo4.ColumnIndexEnd = 13;
            //colInfo4.Width = 50 * 256;
            //sheet.AddColumnInfo(colInfo4);

            //AppLibrary.WriteExcel.ColumnInfo colInfo5 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo5.ColumnIndexStart = 14;
            //colInfo5.ColumnIndexEnd = 14;
            //colInfo5.Width = 15 * 256;
            //sheet.AddColumnInfo(colInfo5);

            //AppLibrary.WriteExcel.ColumnInfo colInfo6 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            //colInfo6.ColumnIndexStart = 15;
            //colInfo6.ColumnIndexEnd = 15;
            //colInfo6.Width = 50 * 256;
            //sheet.AddColumnInfo(colInfo6);
            //#endregion
            #endregion
            #region 添加表头
            int idx = 1;
            for (int i = 0; i < al.Count; i++)
            {
                cells.Add(1, idx, al[i], XFstyle);
                idx++;
            }
            #endregion
            int f = 2;//从第二行开始填充数据
            foreach (DataRow row in data.Rows)
            {
                for (int c = 0; c < data.Columns.Count; c++)
                {
                    cells.Add(f, c + 1, row[c] != DBNull.Value ? row[c] : null, XFstyle);
                }
                f++;
            }
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            doc.Save(filePath);

        }
        /// <summary>
        /// 导出到Excel中--多个sheet
        /// </summary>
        /// <param name="data">导出的数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="filePath">excel到处路径</param>
        /// <param name="sheetName">导出的SheetName</param>
        /// <param name="columnsWidth">设定每列的宽度</param>
        public static void ExportToExcelSaveNew(DataTable data, int[] columnsWidth, string fileName, string filePath, string sheetName = "sheet1")
        {
            HttpContext hc = HttpContext.Current;
            ArrayList al = new ArrayList();
            int count = data.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                string t = data.Columns[i].Caption;
                al.Add(t);
            }

            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = fileName + ".xls";
            //一个SHEET显示多少条
            int intBase = 10000;
            //需要几个sheet
            int intSheet = data.Rows.Count / intBase;
            //剩余数据
            int leaveData = data.Rows.Count % intBase;
            if (leaveData > 0)
            {
                intSheet = intSheet + 1;
            }
            int f = 2;//从第二行开始填充数据
            for (int sheet_i = 0; sheet_i < intSheet; sheet_i++)
            {
                f = 2;
                #region 添加多个sheet
                if (string.IsNullOrEmpty(sheetName))
                    sheetName = "sheet";
                AppLibrary.WriteExcel.Worksheet sheet = null;
                if (intSheet == 1)
                    sheet = doc.Workbook.Worksheets.Add(sheetName);
                else
                    sheet = doc.Workbook.Worksheets.Add(sheetName + (sheet_i + 1).ToString());
                AppLibrary.WriteExcel.Cells cells = sheet.Cells;
                #region 第一个样式
                AppLibrary.WriteExcel.XF XFstyle = doc.NewXF();//添加样式的
                XFstyle.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
                XFstyle.Font.FontName = "宋体";//字体
                XFstyle.UseMisc = true;
                XFstyle.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;//文本位置
                XFstyle.Font.Bold = false;//加粗
                #region 边框线的样式
                XFstyle.BottomLineStyle = 1;
                XFstyle.LeftLineStyle = 1;
                XFstyle.TopLineStyle = 1;
                XFstyle.RightLineStyle = 1;
                #endregion
                XFstyle.UseBorder = true;
                XFstyle.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
                XFstyle.PatternColor = AppLibrary.WriteExcel.Colors.White;
                XFstyle.Pattern = 1;
                #region 宽度
                if (columnsWidth != null)
                {
                    for (int i = 0; i < columnsWidth.Length; i++)
                    {
                        ColumnInfo colInfo = new ColumnInfo(doc, sheet);
                        colInfo.ColumnIndexStart = (ushort)i;
                        colInfo.ColumnIndexEnd = (ushort)i;
                        colInfo.Width = (ushort)(columnsWidth[i] * 256);
                        sheet.AddColumnInfo(colInfo);
                    }
                }
                #endregion
                #endregion
                #region 添加表头
                int idx = 1;
                for (int i = 0; i < al.Count; i++)
                {
                    cells.Add(1, idx, al[i], XFstyle);
                    idx++;
                }
                #endregion
                //填充数据
                int forIntRow = intBase;
                if ((sheet_i + 1) == intSheet && leaveData > 0)
                {
                    forIntRow = leaveData;
                }
                //从每个sheet，填充开始row
                for (int for_i = 0 + (sheet_i * intBase); for_i - (sheet_i * intBase) < forIntRow; for_i++)
                {
                    DataRow row = data.Rows[for_i];
                    for (int c = 0; c < data.Columns.Count; c++)
                    {
                        cells.Add(f, c + 1, row[c] != DBNull.Value ? row[c] : null, XFstyle);
                    }
                    f++;
                }
                //int f = 2;//从第二行开始填充数据
                //foreach (DataRow row in data.Rows)
                //{
                //    for (int c = 0; c < data.Columns.Count; c++)
                //    {
                //        cells.Add(f, c + 1, row[c] != DBNull.Value ? row[c] : null, XFstyle);
                //    }
                //    f++;
                //}
                #endregion
            }

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            doc.Save(filePath);

        }


        /// <summary>
        /// 导出到Excel中--多个sheet(可以设置单元格字体，主要是为了解决调基导出医疗报盘问题)
        /// </summary>
        /// <param name="data">导出的数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="filePath">excel到处路径</param>
        /// <param name="sheetName">导出的SheetName</param>
        /// <param name="columnsWidth">设定每列的宽度</param>
        public static void ExportToExcelSaveNewFont(DataTable data, int[] columnsWidth, string fileName, string filePath, string sheetName = "sheet1", string font = "宋体")
        {
            HttpContext hc = HttpContext.Current;
            ArrayList al = new ArrayList();
            int count = data.Columns.Count;
            for (int i = 0; i < count; i++)
            {
                string t = data.Columns[i].Caption;
                al.Add(t);
            }

            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = fileName + ".xls";
            //一个SHEET显示多少条
            int intBase = 10000;
            //需要几个sheet
            int intSheet = data.Rows.Count / intBase;
            //剩余数据
            int leaveData = data.Rows.Count % intBase;
            if (leaveData > 0)
            {
                intSheet = intSheet + 1;
            }
            int f = 2;//从第二行开始填充数据
            for (int sheet_i = 0; sheet_i < intSheet; sheet_i++)
            {
                f = 2;
                #region 添加多个sheet
                if (string.IsNullOrEmpty(sheetName))
                    sheetName = "sheet";
                AppLibrary.WriteExcel.Worksheet sheet = null;
                if (intSheet == 1)
                    sheet = doc.Workbook.Worksheets.Add(sheetName);
                else
                    sheet = doc.Workbook.Worksheets.Add(sheetName + (sheet_i + 1).ToString());
                AppLibrary.WriteExcel.Cells cells = sheet.Cells;
                #region 第一个样式
                AppLibrary.WriteExcel.XF XFstyle = doc.NewXF();//添加样式的
                XFstyle.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
                XFstyle.Font.FontName = font;//字体
                XFstyle.UseMisc = true;
                XFstyle.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;//文本位置
                XFstyle.Font.Bold = false;//加粗
                #region 边框线的样式
                XFstyle.BottomLineStyle = 1;
                XFstyle.LeftLineStyle = 1;
                XFstyle.TopLineStyle = 1;
                XFstyle.RightLineStyle = 1;
                #endregion
                XFstyle.UseBorder = true;
                XFstyle.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
                XFstyle.PatternColor = AppLibrary.WriteExcel.Colors.White;
                XFstyle.Pattern = 1;
                #region 宽度
                if (columnsWidth != null)
                {
                    for (int i = 0; i < columnsWidth.Length; i++)
                    {
                        ColumnInfo colInfo = new ColumnInfo(doc, sheet);
                        colInfo.ColumnIndexStart = (ushort)i;
                        colInfo.ColumnIndexEnd = (ushort)i;
                        colInfo.Width = (ushort)(columnsWidth[i] * 256);
                        sheet.AddColumnInfo(colInfo);
                    }
                }
                #endregion
                #endregion
                #region 添加表头
                int idx = 1;
                for (int i = 0; i < al.Count; i++)
                {
                    cells.Add(1, idx, al[i], XFstyle);
                    idx++;
                }
                #endregion
                //填充数据
                int forIntRow = intBase;
                if ((sheet_i + 1) == intSheet && leaveData > 0)
                {
                    forIntRow = leaveData;
                }
                //从每个sheet，填充开始row
                for (int for_i = 0 + (sheet_i * intBase); for_i - (sheet_i * intBase) < forIntRow; for_i++)
                {
                    DataRow row = data.Rows[for_i];
                    for (int c = 0; c < data.Columns.Count; c++)
                    {
                        cells.Add(f, c + 1, row[c] != DBNull.Value ? row[c] : null, XFstyle);
                    }
                    f++;
                }
                //int f = 2;//从第二行开始填充数据
                //foreach (DataRow row in data.Rows)
                //{
                //    for (int c = 0; c < data.Columns.Count; c++)
                //    {
                //        cells.Add(f, c + 1, row[c] != DBNull.Value ? row[c] : null, XFstyle);
                //    }
                //    f++;
                //}
                #endregion
            }

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            doc.Save(filePath);

        }

        //// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dtSource">数据源</param>
        /// <param name="tempFilePath">Excel模板路径</param>
        /// <param name="strFileName">输出Excel名</param>
        /// <param name="rowIndex">数据在Excel输出的起始行数</param>
        /// <returns></returns>
        public static void ExportTmpToExcel(DataTable dtSource, string tempFilePath, string strFileName, int rowIndex)
        {
            string sheetName = "Sheet1";
            ExportTmpToExcel(dtSource, tempFilePath, strFileName, rowIndex, sheetName);
        }

        /// <summary>
        /// 根据模板导出Excel
        /// </summary>
        /// <param name="dtSource">数据源</param>
        /// <param name="stream">The stream.</param>
        /// <param name="strFileName">输出Excel名</param>
        /// <param name="rowIndex">数据在Excel输出的起始行数</param>
        /// <param name="sheetName">数据注入的sheet名称</param>
        public static void ExportTmpToExcel(DataTable dtSource, Stream stream, string strFileName, int rowIndex, string sheetName)
        {
            // DataTable的数据行数
            int dtRowIndex = dtSource.Rows.Count;
            int InitRowIndex = rowIndex;

            //读入excel模板
            HSSFWorkbook workbook = new HSSFWorkbook(stream);
            stream.Close();
            HSSFSheet sheet = workbook.GetSheet(sheetName);
            HSSFRow headerRow = sheet.GetRow(0);
            #region 单元格样式
            //普通单元格样式
            HSSFCellStyle cellstyle = workbook.CreateCellStyle();
            cellstyle.BorderBottom = HSSFCellStyle.BORDER_THIN;
            cellstyle.BorderLeft = HSSFCellStyle.BORDER_THIN;
            cellstyle.BorderRight = HSSFCellStyle.BORDER_THIN;
            cellstyle.BorderTop = HSSFCellStyle.BORDER_THIN;
            cellstyle.TopBorderColor = HSSFColor.BLACK.index;
            cellstyle.BottomBorderColor = HSSFColor.BLACK.index;
            cellstyle.RightBorderColor = HSSFColor.BLACK.index;
            cellstyle.LeftBorderColor = HSSFColor.BLACK.index;

            //日期单元格样式
            HSSFCellStyle dateTimeStyle = workbook.CreateCellStyle();
            dateTimeStyle.BorderBottom = HSSFCellStyle.BORDER_THIN;
            dateTimeStyle.BorderLeft = HSSFCellStyle.BORDER_THIN;
            dateTimeStyle.BorderRight = HSSFCellStyle.BORDER_THIN;
            dateTimeStyle.BorderTop = HSSFCellStyle.BORDER_THIN;
            dateTimeStyle.TopBorderColor = HSSFColor.BLACK.index;
            dateTimeStyle.BottomBorderColor = HSSFColor.BLACK.index;
            dateTimeStyle.RightBorderColor = HSSFColor.BLACK.index;
            dateTimeStyle.LeftBorderColor = HSSFColor.BLACK.index;
            HSSFDataFormat format = workbook.CreateDataFormat();
            dateTimeStyle.DataFormat = format.GetFormat("yyyy/mm/dd");
            #endregion

            #region 单元格内容处理
            foreach (DataRow row in dtSource.Rows)
            {
                if (rowIndex != 0 && (rowIndex % 65535) == 0)
                {
                    int sheetNum = (rowIndex - (rowIndex % 65535)) / 65535;
                    sheet = workbook.GetSheet("Sheet" + (sheetNum + 1));
                    rowIndex = InitRowIndex;
                }
                HSSFRow dataRow = sheet.CreateRow(rowIndex);
                int columnIndex = 0;        // 开始列
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count)
                        break;

                    HSSFCell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);
                    newCell.CellStyle = cellstyle;
                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            if (!string.IsNullOrEmpty(drValue))
                            {
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.CellStyle = dateTimeStyle;
                                newCell.SetCellValue(dateV);
                            }
                            else
                            {
                                newCell.SetCellValue(string.Empty);
                            }
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                    columnIndex++;
                }

                rowIndex++;
            }
            #endregion

            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            int cellCount = headerRow == null ? 0 : headerRow.LastCellNum;

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;
                HttpContext curContext = HttpContext.Current;
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.ContentEncoding = Encoding.UTF8;
                curContext.Response.AppendHeader("Content-Disposition",
                                                 "attachment;filename=" + HttpUtility.UrlEncode(strFileName + ".xls", Encoding.UTF8));
                curContext.Response.BinaryWrite(ms.GetBuffer());
                curContext.Response.End();
            }
        }

        /// <summary>
        /// 根据模板导出Excel
        /// </summary>
        /// <param name="dtSource">数据源</param>
        /// <param name="tempFilePath">Excel模板路径</param>
        /// <param name="strFileName">输出Excel名</param>
        /// <param name="rowIndex">数据在Excel输出的起始行数</param>
        /// <param name="sheetName">数据注入的sheet名称</param>
        /// <returns></returns>
        public static void ExportTmpToExcel(DataTable dtSource, string tempFilePath, string strFileName, int rowIndex, string sheetName)
        {
            // DataTable的数据行数
            int dtRowIndex = dtSource.Rows.Count;
            int InitRowIndex = rowIndex;

            //读入excel模板
            FileStream file = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            file.Close();
            HSSFSheet sheet = workbook.GetSheet(sheetName);
            HSSFRow headerRow = sheet.GetRow(0);
            #region 单元格样式
            //普通单元格样式
            HSSFCellStyle cellstyle = workbook.CreateCellStyle();
            cellstyle.BorderBottom = HSSFCellStyle.BORDER_THIN;
            cellstyle.BorderLeft = HSSFCellStyle.BORDER_THIN;
            cellstyle.BorderRight = HSSFCellStyle.BORDER_THIN;
            cellstyle.BorderTop = HSSFCellStyle.BORDER_THIN;
            cellstyle.TopBorderColor = HSSFColor.BLACK.index;
            cellstyle.BottomBorderColor = HSSFColor.BLACK.index;
            cellstyle.RightBorderColor = HSSFColor.BLACK.index;
            cellstyle.LeftBorderColor = HSSFColor.BLACK.index;

            //日期单元格样式
            HSSFCellStyle dateTimeStyle = workbook.CreateCellStyle();
            dateTimeStyle.BorderBottom = HSSFCellStyle.BORDER_THIN;
            dateTimeStyle.BorderLeft = HSSFCellStyle.BORDER_THIN;
            dateTimeStyle.BorderRight = HSSFCellStyle.BORDER_THIN;
            dateTimeStyle.BorderTop = HSSFCellStyle.BORDER_THIN;
            dateTimeStyle.TopBorderColor = HSSFColor.BLACK.index;
            dateTimeStyle.BottomBorderColor = HSSFColor.BLACK.index;
            dateTimeStyle.RightBorderColor = HSSFColor.BLACK.index;
            dateTimeStyle.LeftBorderColor = HSSFColor.BLACK.index;
            HSSFDataFormat format = workbook.CreateDataFormat();
            dateTimeStyle.DataFormat = format.GetFormat("yyyy/mm/dd");
            #endregion

            #region 单元格内容处理
            foreach (DataRow row in dtSource.Rows)
            {
                if (rowIndex != 0 && (rowIndex % 65535) == 0)
                {
                    int sheetNum = (rowIndex - (rowIndex % 65535)) / 65535;
                    sheet = workbook.GetSheet("Sheet" + (sheetNum + 1));
                    rowIndex = InitRowIndex;
                }
                HSSFRow dataRow = sheet.CreateRow(rowIndex);
                int columnIndex = 0;        // 开始列
                foreach (DataColumn column in dtSource.Columns)
                {
                    // 列序号赋值
                    if (columnIndex >= dtSource.Columns.Count)
                        break;

                    HSSFCell newCell = dataRow.GetCell(columnIndex);
                    if (newCell == null)
                        newCell = dataRow.CreateCell(columnIndex);
                    newCell.CellStyle = cellstyle;
                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            if (!string.IsNullOrEmpty(drValue))
                            {
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.CellStyle = dateTimeStyle;
                                newCell.SetCellValue(dateV);
                            }
                            else
                            {
                                newCell.SetCellValue(string.Empty);
                            }
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                    columnIndex++;
                }

                rowIndex++;
            }
            #endregion

            // 格式化当前sheet，用于数据total计算
            sheet.ForceFormulaRecalculation = true;

            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            int cellCount = headerRow == null ? 0 : headerRow.LastCellNum;

            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                sheet = null;
                workbook = null;
                HttpContext curContext = HttpContext.Current;
                curContext.Response.ContentType = "application/vnd.ms-excel";
                curContext.Response.ContentEncoding = Encoding.UTF8;
                curContext.Response.AppendHeader("Content-Disposition",
                                                 "attachment;filename=" + HttpUtility.UrlEncode(strFileName + ".xls", Encoding.UTF8));
                curContext.Response.BinaryWrite(ms.GetBuffer());
                curContext.Response.End();
            }
        }
        /// <summary>
        /// 导出到Excel中
        /// </summary>
        /// <param name="data">导出的数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">导出的SheetName</param>
        /// <param name="columnsWidth">设定每列的宽度</param>
        public static void ExportToExcels(DataTable[] datas, int[][] columnsWidths, string fileName, string[] sheetNames)
        {
            HttpContext hc = HttpContext.Current;
            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = fileName + ".xls";

            for (int j = 0; j < datas.Length; j++)
            {
                DataTable data = datas[j];
                string sheetName = sheetNames[j];
                ArrayList al = new ArrayList();
                int count = data.Columns.Count;
                for (int i = 0; i < count; i++)
                {
                    string t = data.Columns[i].Caption;
                    al.Add(t);
                }




                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add(sheetName);
                AppLibrary.WriteExcel.Cells cells = sheet.Cells;
                #region 第一个样式
                AppLibrary.WriteExcel.XF XFstyle = doc.NewXF();//添加样式的
                XFstyle.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
                XFstyle.Font.FontName = "宋体";//字体
                XFstyle.UseMisc = true;
                XFstyle.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;//文本位置
                XFstyle.Font.Bold = false;//加粗
                #region 边框线的样式
                XFstyle.BottomLineStyle = 1;
                XFstyle.LeftLineStyle = 1;
                XFstyle.TopLineStyle = 1;
                XFstyle.RightLineStyle = 1;
                #endregion
                XFstyle.UseBorder = true;
                XFstyle.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
                XFstyle.PatternColor = AppLibrary.WriteExcel.Colors.White;
                XFstyle.Pattern = 1;
                #region 宽度
                if (columnsWidths != null)
                {
                    for (int m = 0; m < columnsWidths.Length; m++)
                    {
                        int[] columnsWidth = columnsWidths[m];

                        for (int n = 0; n < columnsWidth.Length; n++)
                        {
                            ColumnInfo colInfo = new ColumnInfo(doc, sheet);
                            colInfo.ColumnIndexStart = (ushort)n;
                            colInfo.ColumnIndexEnd = (ushort)n;
                            colInfo.Width = (ushort)(columnsWidth[n] * 256);
                            sheet.AddColumnInfo(colInfo);
                        }

                    }
                }

                #endregion

                #endregion
                #region 添加表头
                int idx = 1;
                for (int i = 0; i < al.Count; i++)
                {
                    cells.Add(1, idx, al[i], XFstyle);
                    idx++;
                }
                #endregion
                int f = 2;//从第二行开始填充数据
                foreach (DataRow row in data.Rows)
                {
                    for (int c = 0; c < data.Columns.Count; c++)
                    {
                        cells.Add(f, c + 1, row[c] != DBNull.Value ? row[c] : null, XFstyle);
                    }
                    f++;
                }
            }

            //doc.Send();
            //hc.Response.Flush();
            //hc.Response.End();

            string fileUrl = hc.Server.MapPath("../../uploads/GJJ/TJ/");
            doc.Save(fileUrl);
            fileUrl = Path.Combine(fileUrl, doc.FileName);
            ExportToExcels(fileUrl);
            File.Delete(fileUrl);
        }

        /// <summary>
        /// 导出到Excel中
        /// </summary>
        /// <param name="data">导出的数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">导出的SheetName</param>
        /// <param name="columnsWidth">设定每列的宽度</param>
        public static string ExportToExcels(DataSet ds, int[][] columnsWidths, string fileName, string fileUrl)
        {
            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = fileName + ".xls";

            foreach (DataTable item in ds.Tables)
            {

                DataTable data = item;
                string sheetName = item.TableName;
                ArrayList al = new ArrayList();
                int count = data.Columns.Count;
                for (int i = 0; i < count; i++)
                {
                    string t = data.Columns[i].Caption;
                    al.Add(t);
                }




                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add(sheetName);
                AppLibrary.WriteExcel.Cells cells = sheet.Cells;
                #region 第一个样式
                AppLibrary.WriteExcel.XF XFstyle = doc.NewXF();//添加样式的
                XFstyle.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
                XFstyle.Font.FontName = "宋体";//字体
                XFstyle.UseMisc = true;
                XFstyle.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;//文本位置
                XFstyle.Font.Bold = false;//加粗
                #region 边框线的样式
                XFstyle.BottomLineStyle = 1;
                XFstyle.LeftLineStyle = 1;
                XFstyle.TopLineStyle = 1;
                XFstyle.RightLineStyle = 1;
                #endregion
                XFstyle.UseBorder = true;
                XFstyle.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
                XFstyle.PatternColor = AppLibrary.WriteExcel.Colors.White;
                XFstyle.Pattern = 1;
                #region 宽度
                if (columnsWidths != null)
                {
                    for (int m = 0; m < columnsWidths.Length; m++)
                    {
                        int[] columnsWidth = columnsWidths[m];

                        for (int n = 0; n < columnsWidth.Length; n++)
                        {
                            ColumnInfo colInfo = new ColumnInfo(doc, sheet);
                            colInfo.ColumnIndexStart = (ushort)n;
                            colInfo.ColumnIndexEnd = (ushort)n;
                            colInfo.Width = (ushort)(columnsWidth[n] * 256);
                            sheet.AddColumnInfo(colInfo);
                        }

                    }
                }

                #endregion

                #endregion
                #region 添加表头
                int idx = 1;
                for (int i = 0; i < al.Count; i++)
                {
                    cells.Add(1, idx, al[i], XFstyle);
                    idx++;
                }
                #endregion
                int f = 2;//从第二行开始填充数据
                foreach (DataRow row in data.Rows)
                {
                    for (int c = 0; c < data.Columns.Count; c++)
                    {
                        cells.Add(f, c + 1, row[c] != DBNull.Value ? row[c] : null, XFstyle);
                    }
                    f++;
                }
            }
            doc.Save(System.IO.Path.GetDirectoryName(fileUrl));
            fileUrl = Path.Combine(System.IO.Path.GetDirectoryName(fileUrl), doc.FileName);
            return fileUrl;
        }

        /// <summary>
        /// 导出到Excel中
        /// </summary>
        /// <param name="data">导出的数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">导出的SheetName</param>
        /// <param name="columnsWidth">设定每列的宽度</param>
        public static void BillHeadTPExportToExcels(DataTable[] datas, int[][] columnsWidths, string fileName, string[] sheetNames)
        {
            HttpContext hc = HttpContext.Current;
            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
            doc.FileName = fileName + ".xls";
            
            for (int j = 0; j < datas.Length; j++)
            {
                DataTable data = datas[j];
                string sheetName = sheetNames[j];
                ArrayList al = new ArrayList();
                int count = data.Columns.Count;
                for (int i = 0; i < count; i++)
                {
                    string t = data.Columns[i].Caption;
                    al.Add(t);
                }
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add(sheetName);
                AppLibrary.WriteExcel.Cells cells = sheet.Cells;
                #region 第一个样式
                AppLibrary.WriteExcel.XF XFstyle = doc.NewXF();//添加样式的
                XFstyle.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
                XFstyle.Font.FontName = "宋体";//字体
                XFstyle.UseMisc = true;
                XFstyle.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;//文本位置
                XFstyle.Font.Bold = false;//加粗
                #region 边框线的样式
                XFstyle.BottomLineStyle = 1;
                XFstyle.LeftLineStyle = 1;
                XFstyle.TopLineStyle = 1;
                XFstyle.RightLineStyle = 1;
                #endregion
                XFstyle.UseBorder = true;
                XFstyle.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
                XFstyle.PatternColor = AppLibrary.WriteExcel.Colors.White;
                XFstyle.Pattern = 1;
                #region 宽度
                if (columnsWidths != null)
                {
                    for (int m = 0; m < columnsWidths.Length; m++)
                    {
                        int[] columnsWidth = columnsWidths[m];

                        for (int n = 0; n < columnsWidth.Length; n++)
                        {
                            ColumnInfo colInfo = new ColumnInfo(doc, sheet);
                            colInfo.ColumnIndexStart = (ushort)n;
                            colInfo.ColumnIndexEnd = (ushort)n;
                            colInfo.Width = (ushort)(columnsWidth[n] * 256);
                            sheet.AddColumnInfo(colInfo);
                        }

                    }
                }

                #endregion

                #endregion
                #region 添加表头
                int idx = 1;
                for (int i = 0; i < al.Count; i++)
                {
                    cells.Add(1, idx, al[i], XFstyle);
                    idx++;
                }
                #endregion
                int f = 2;//从第二行开始填充数据
                foreach (DataRow row in data.Rows)
                {
                    for (int c = 0; c < data.Columns.Count; c++)
                    {
                        cells.Add(f, c + 1, row[c] != DBNull.Value ? row[c] : null, XFstyle);
                    }
                    f++;
                }
            }

            doc.Send();
            hc.Response.Flush();
            hc.Response.End();
        }


        /// <summary>
        /// 导出到Excel中  by 2016-7-14 添加
        /// </summary>
        /// <param name="data">导出的数据</param>
        /// <param name="fileName">文件名</param>
        /// <param name="sheetName">导出的SheetName</param>
        /// <param name="columnsWidth">设定每列的宽度</param>
        public static void BillHeadTPExportToExcels(DataTable[] datas, int[][] columnsWidths, string fileName, string[] sheetNames,string ModuleCode)
        {
            HttpContext hc = HttpContext.Current;
            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            fileName = HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8);
            doc.FileName = fileName + ".xls";
            doc.SummaryInformation.Title = ModuleCode;  //设置  文件右键属性详细信息里面的标题
            for (int j = 0; j < datas.Length; j++)
            {
                DataTable data = datas[j];
                string sheetName = sheetNames[j];
                ArrayList al = new ArrayList();
                int count = data.Columns.Count;
                for (int i = 0; i < count; i++)
                {
                    string t = data.Columns[i].Caption;
                    al.Add(t);
                }
                AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add(sheetName);
                AppLibrary.WriteExcel.Cells cells = sheet.Cells;
                #region 第一个样式
                AppLibrary.WriteExcel.XF XFstyle = doc.NewXF();//添加样式的
                XFstyle.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
                XFstyle.Font.FontName = "宋体";//字体
                XFstyle.UseMisc = true;
                XFstyle.TextDirection = AppLibrary.WriteExcel.TextDirections.LeftToRight;//文本位置
                XFstyle.Font.Bold = false;//加粗
                #region 边框线的样式
                XFstyle.BottomLineStyle = 1;
                XFstyle.LeftLineStyle = 1;
                XFstyle.TopLineStyle = 1;
                XFstyle.RightLineStyle = 1;
                #endregion
                XFstyle.UseBorder = true;
                XFstyle.PatternBackgroundColor = AppLibrary.WriteExcel.Colors.Blue;
                XFstyle.PatternColor = AppLibrary.WriteExcel.Colors.White;
                XFstyle.Pattern = 1;
                #region 宽度
                if (columnsWidths != null)
                {
                    for (int m = 0; m < columnsWidths.Length; m++)
                    {
                        int[] columnsWidth = columnsWidths[m];

                        for (int n = 0; n < columnsWidth.Length; n++)
                        {
                            ColumnInfo colInfo = new ColumnInfo(doc, sheet);
                            colInfo.ColumnIndexStart = (ushort)n;
                            colInfo.ColumnIndexEnd = (ushort)n;
                            colInfo.Width = (ushort)(columnsWidth[n] * 256);
                            sheet.AddColumnInfo(colInfo);
                        }

                    }
                }

                #endregion

                #endregion
                #region 添加表头
                int idx = 1;
                for (int i = 0; i < al.Count; i++)
                {
                    cells.Add(1, idx, al[i], XFstyle);
                    idx++;
                }
                #endregion
                int f = 2;//从第二行开始填充数据
                foreach (DataRow row in data.Rows)
                {
                    for (int c = 0; c < data.Columns.Count; c++)
                    {
                        cells.Add(f, c + 1, row[c] != DBNull.Value ? row[c] : null, XFstyle);
                    }
                    f++;
                }
            }

            doc.Send();
            hc.Response.Flush();
            hc.Response.End();
        }


        /// <summary>
        /// 导出到Excel中
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void ExportToExcels(string filePath)
        {
            HttpContext curContext = HttpContext.Current;

            using (FileStream FileStreamDoc = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                //提取文件名
                FileInfo fileInfo = new FileInfo(filePath);
                string fileName = string.Empty;
                string fileStr = curContext.Server.UrlPathEncode(fileInfo.Name);
                fileStr = HttpUtility.UrlDecode(fileStr);
                try
                {
                    string extension = Path.GetExtension(fileStr);
                    int index = 0;
                    if (!string.IsNullOrEmpty(extension))
                    {
                        fileName = fileStr;
                        fileStr = fileStr.Replace(extension, "");
                        index = fileStr.LastIndexOf("_");
                        if (index != -1)
                        {
                            fileName = fileStr.Substring(0, index) + extension;
                        }
                    }
                    else
                    {
                        fileName = fileStr;
                    }
                }
                catch
                {
                    fileName = fileStr;
                }

                long FileSize = FileStreamDoc.Length;
                byte[] Buffer = new byte[(int)FileSize];
                FileStreamDoc.Read(Buffer, 0, (int)FileSize);
                FileStreamDoc.Close();
                curContext.Response.Clear();
                curContext.Response.ClearHeaders();
                curContext.Response.Buffer = false;
                curContext.Response.ContentEncoding = Encoding.Default;
                curContext.Response.ContentType = "application/octet-stream";
                curContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlPathEncode(fileName));
                curContext.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
                curContext.Response.BinaryWrite(Buffer);
                curContext.Response.Flush();
                curContext.Response.Clear();
                curContext.Response.End();
            }
        }

        /// <summary>
        /// 导出到PDF中
        /// </summary>
        /// <param name="data">数据源</param>
        /// <param name="fileName">文件名</param>
        public static void ExportToPDF(DataTable data, string fileName)
        {
            Document pdfDoc = new Document();
            MemoryStream pdfStream = new MemoryStream();
            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, pdfStream);

            pdfDoc.Open();
            pdfDoc.NewPage();

            iTextSharp.text.Font font8 = FontFactory.GetFont("ARIAL", 7);

            PdfPTable PdfTable = new PdfPTable(data.Columns.Count);
            PdfPCell PdfPCell = null;

            //Add Header of the pdf table
            for (int column = 0; column < data.Columns.Count; column++)
            {
                PdfPCell = new PdfPCell(new Phrase(new Chunk(data.Columns[column].Caption, font8)));
                PdfTable.AddCell(PdfPCell);
            }

            //How add the data from datatable to pdf table
            for (int rows = 0; rows < data.Rows.Count; rows++)
            {
                for (int column = 0; column < data.Columns.Count; column++)
                {
                    PdfPCell = new PdfPCell(new Phrase(new Chunk(data.Rows[rows][column].ToString(), font8)));
                    PdfTable.AddCell(PdfPCell);
                }
            }

            PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table
            pdfDoc.Add(PdfTable); // add pdf table to the document
            pdfDoc.Close();
            pdfWriter.Close();

            HttpResponse response = HttpContext.Current.Response;
            response.ClearContent();
            response.ClearHeaders();
            response.ContentType = "application/pdf";
            response.AppendHeader("Content-Disposition", "attachment; filename=" + HttpContext.Current.Server.UrlEncode(fileName) + ".pdf");
            response.BinaryWrite(pdfStream.ToArray());
            response.End();
        }


        /// <summary>
        /// Exports to excel add cells.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="sheetName">Name of the sheet.</param>
        /// <param name="cells">key:以"-"分隔的数字 => [ rowIndex-colIndex ]; value:cell内容</param>
        public static Stream ExportToExcelAddCells(string filePath, string sheetName, Dictionary<string, string> cells)
        {
            //读入excel模板
            FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            HSSFWorkbook workbook = new HSSFWorkbook(file);
            file.Close();
            HSSFSheet sheet = workbook.GetSheet(sheetName);

            foreach (KeyValuePair<string, string> kv in cells)
            {
                var rc = kv.Key.Split('-');//字典的key储存以"-"分隔的数字 => [ rowIndex-colIndex ]
                if (rc.Any() && rc.Count() == 2)
                {
                    var rowIndex = int.Parse(rc[0]);
                    var colIndex = int.Parse(rc[1]);
                    var row = sheet.GetRow(rowIndex);
                    var cell = row.CreateCell(colIndex, (int)CellTypes.Text);
                    var cellVal = kv.Value;
                    if (System.Text.RegularExpressions.Regex.IsMatch(kv.Value, "{{.*}}"))
                    {
                        cellVal = System.Text.RegularExpressions.Regex.Replace(kv.Value, "{{.*}}", "");
                        //下拉框填充项
                        var dropdownSource = System.Text.RegularExpressions.Regex.
                            Match(kv.Value, "{{.*}}").Value.Replace("{{", "").Replace("}}", "").Split(',').ToList();
                        #region CREATE DROPDOWN
                        //创建一个Sheet专门用于存储下拉项的值，并将各下拉项的值写入其中：
                        var sheetTempName = "Sheet_" + DateTime.Now.ToString("mmssfffff");
                        HSSFSheet sheet2 = workbook.CreateSheet(sheetTempName);
                        //隐藏该sheet
                        workbook.SetSheetHidden(workbook.GetSheetIndex(sheet2), true);
                        var rIndex = 0;
                        dropdownSource.ForEach(x =>
                        {
                            sheet2.CreateRow(rIndex).CreateCell(0).SetCellValue(x);
                            rIndex++;
                        });                        
                        
                        CellRangeAddressList rangeList = new CellRangeAddressList();
                        rangeList.AddCellRangeAddress(new CellRangeAddress(1, 1000, colIndex, colIndex));

                        DVConstraint dvconstraint = DVConstraint.CreateFormulaListConstraint(string.Format("{0}!$A$1:$A${1}", sheetTempName, dropdownSource.Count));
                        HSSFDataValidation dataValidation = new
                                HSSFDataValidation(rangeList, dvconstraint);
                        
                        ((HSSFSheet)sheet).AddValidationData(dataValidation); 
                        #endregion
                    }
                    cell.SetCellValue(cellVal);
                    sheet.SetColumnWidth(colIndex, cellVal.Length * 2 * 256);
                }
            }

            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            sheet = null;
            workbook = null;
            return ms;
        }
    }
}

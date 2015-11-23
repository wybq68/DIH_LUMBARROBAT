﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace LumbarRobot.Common
{
    public class ExcelHelper
    {
        #region 变量
        
       
        private Excel.Application _excelApp = null;
        private Excel.Workbooks _books = null;
        private Excel._Workbook _book = null;
        private Excel.Sheets _sheets = null;
        private Excel._Worksheet _sheet = null;
        private Excel.Range _range = null;
        private Excel.Font _font = null;
        // Optional argument variable
        private object _optionalValue = Missing.Value;
        #endregion

        #region 读取Excel文件


        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="pPath"></param>
        /// <returns></returns>
        public DataTable LoadExcel(string pPath)
        {
            //Driver={Driver do Microsoft Excel(*.xls)} 这种连接写法不需要创建一个数据源DSN，DRIVERID表示驱动ID，Excel2003后都使用790，FIL表示Excel文件类型，Excel2007用excel 8.0，MaxBufferSize表示缓存大小，DBQ表示读取Excel的文件名（全路径）

            string connString = "Driver={Driver do Microsoft Excel(*.xls)};DriverId=790;SafeTransactions=0;ReadOnly=1;MaxScanRows=16;Threads=3;MaxBufferSize=2024;UserCommitSync=Yes;FIL=excel 8.0;PageTimeout=5;";
            connString += "DBQ=" + pPath;
            OdbcConnection conn = new OdbcConnection(connString);
            OdbcCommand cmd = new OdbcCommand();
            cmd.Connection = conn;
            //获取Excel中第一个Sheet名称，作为查询时的表名
            string sheetName = this.GetExcelSheetName(pPath);
            string sql = "select * from [" + sheetName.Replace('.', '#') + "$]";
            cmd.CommandText = sql;
            OdbcDataAdapter da = new OdbcDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception x)
            {
                ds = null;
                throw new Exception("从Excel文件中获取数据时发生错误！");
            }
            finally
            {
                cmd.Dispose();
                cmd = null;
                da.Dispose();
                da = null;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn = null;
            }
        }
        private string GetExcelSheetName(string pPath)
        {
            //打开一个Excel应用

            _excelApp = new Excel.Application();
            if (_excelApp == null)
            {
                throw new Exception("打开Excel应用时发生错误！");
            }
            _books = _excelApp.Workbooks;
            //打开一个现有的工作薄
            _book = _books.Add(pPath);
            _sheets = _book.Sheets;
            //选择第一个Sheet页
            _sheet = (Excel._Worksheet)_sheets.get_Item(1);
            string sheetName = _sheet.Name;

            ReleaseCOM(_sheet);
            ReleaseCOM(_sheets);
            ReleaseCOM(_book);
            ReleaseCOM(_books);
            _excelApp.Quit();
            ReleaseCOM(_excelApp);
            return sheetName;
        }
        #endregion

        #region 释放COM对象


        /// <summary>
        /// 释放COM对象
        /// </summary>
        /// <param name="pObj"></param>
        private void ReleaseCOM(object pObj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pObj);
            }
            catch
            {
                throw new Exception("释放资源时发生错误！");
            }
            finally
            {
                pObj = null;
            }
        }
        #endregion

        #region 保存到Excel


        /// <summary>
        /// 保存到Excel
        /// </summary>
        /// <param name="excelName"></param>
        public void SaveToExcel(string excelName, DataTable dataTable)
        {
            try
            {
                if (dataTable != null)
                {
                    if (dataTable.Rows.Count != 0)
                    {
                        Mouse.SetCursor(Cursors.Wait);
                        CreateExcelRef();
                        FillSheet(dataTable);
                        SaveExcel(excelName);
                        Mouse.SetCursor(Cursors.Arrow);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while generating Excel report");
            }
            finally
            {
                ReleaseCOM(_sheet);
                ReleaseCOM(_sheets);
                ReleaseCOM(_book);
                ReleaseCOM(_books);
                ReleaseCOM(_excelApp);
            }
        }
        #endregion

        #region 将内存中Excel保存到本地路径
        /// <summary>
        /// 将内存中Excel保存到本地路径
        /// </summary>
        /// <param name="excelName"></param>
        private void SaveExcel(string excelName)
        {
            _excelApp.Visible = false;
            //保存为Office2003和Office2007都兼容的格式 xlExcel8
            _book.SaveAs(excelName, Microsoft.Office.Interop.Excel.XlFileFormat.xlExcel7, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            _excelApp.Quit();

        }
        #endregion

        #region 将数据填充到内存Excel的工作表

        /// <summary>
        /// 将数据填充到内存Excel的工作表
        /// </summary>
        /// <param name="dataTable"></param>
        private void FillSheet(DataTable dataTable)
        {
            object[] header = CreateHeader(dataTable);
            WriteData(header, dataTable);
        }


        private void WriteData(object[] header, DataTable dataTable)
        {
            object[,] objData = new object[dataTable.Rows.Count, header.Length];

            for (int j = 0; j < dataTable.Rows.Count; j++)
            {
                var item = dataTable.Rows[j];
                for (int i = 0; i < header.Length; i++)
                {
                    var y = dataTable.Rows[j][i];
                    objData[j, i] = (y == null) ? "" : y.ToString();
                }
            }
            AddExcelRows("A2", dataTable.Rows.Count, header.Length, objData);
            AutoFitColumns("A1", dataTable.Rows.Count + 1, header.Length);
        }


        private void AutoFitColumns(string startRange, int rowCount, int colCount)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.Columns.AutoFit();
        }


        private object[] CreateHeader(DataTable dataTable)
        {

            List<object> objHeaders = new List<object>();
            for (int n = 0; n < dataTable.Columns.Count; n++)
            {
                objHeaders.Add(dataTable.Columns[n].ColumnName);
            }

            var headerToAdd = objHeaders.ToArray();
            //工作表的单元是从“A1”开始
            AddExcelRows("A1", 1, headerToAdd.Length, headerToAdd);
            SetHeaderStyle();

            return headerToAdd;
        }
        #endregion

        #region 将表头加粗显示
        /// <summary>
        /// 将表头加粗显示
        /// </summary>
        private void SetHeaderStyle()
        {
            _font = _range.Font;
            _font.Bold = true;
        }
        #endregion

        #region 将数据填充到Excel工作表的单元格中
        /// <summary>
        /// 将数据填充到Excel工作表的单元格中
        /// </summary>
        /// <param name="startRange"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <param name="values"></param>
        private void AddExcelRows(string startRange, int rowCount, int colCount, object values)
        {
            _range = _sheet.get_Range(startRange, _optionalValue);
            _range = _range.get_Resize(rowCount, colCount);
            _range.set_Value(_optionalValue, values);
        }
        #endregion

        #region 创建一个Excel程序实例
        /// <summary>
        /// 创建一个Excel程序实例
        /// </summary>
        private void CreateExcelRef()
        {
            _excelApp = new Excel.Application();
            _books = (Excel.Workbooks)_excelApp.Workbooks;
            _book = (Excel._Workbook)(_books.Add(_optionalValue));
            _sheets = (Excel.Sheets)_book.Worksheets;
            _sheet = (Excel._Worksheet)(_sheets.get_Item(1));
        }
        #endregion

        #region 导出Excel
        public void dataTableToCsv(DataTable table, string file)
        {
            try
            {
                string title = "";

                FileStream fs = new FileStream(file, FileMode.OpenOrCreate);

                //FileStream fs1 = File.Open(file, FileMode.Open, FileAccess.Read);

                StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    title += table.Columns[i].ColumnName + "\t"; //栏位：自动跳到下一单元格
                }

                title = title.Substring(0, title.Length - 1) + "\n";

                sw.Write(title);

                foreach (DataRow row in table.Rows)
                {

                    string line = "";

                    for (int i = 0; i < table.Columns.Count; i++)
                    {

                        line += row[i].ToString().Trim() + "\t"; //内容：自动跳到下一单元格

                    }

                    line = line.Substring(0, line.Length - 1) + "\n";

                    sw.Write(line);

                }

                sw.Close();

                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("导出Excel失败！", "提示信息", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }
        #endregion
    }
}
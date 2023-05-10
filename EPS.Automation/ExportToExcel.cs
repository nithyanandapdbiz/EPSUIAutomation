using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EPS.Automation
{
    public static class ExportToExcel
    {

        /// <summary>
        /// This method creates a data table from an XML file
        /// </summary>
        /// <param name="XmlFile"></param>
        /// <returns>data table</returns>
        public static System.Data.DataTable CreateDataTableFromXml(string XmlFile)
        {

            System.Data.DataTable dataTable = new System.Data.DataTable();
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(XmlFile);
                dataTable.Load(ds.CreateDataReader());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        /// <summary>
        /// This method creates a data set from an XML file
        /// having multiple datatables.
        /// </summary>
        /// <param name="XmlFile"></param>
        /// <returns>data set</returns>
        public static System.Data.DataSet CreateDataSetFromXml(string XmlFile)
        {
            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(XmlFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// This method copies each data table's data in the data set 
        /// to a new excel sheet in a excel file.
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="Xlfile"></param>
        public static void ExportDataTableToExcel(System.Data.DataSet ds, string Xlfile)
        {
            try
            {
                Application excel = new Application();
                Workbook book = excel.Application.Workbooks.Add(Type.Missing);
                excel.Visible = false;
                excel.DisplayAlerts = false;
                if (ds != null && ds.Tables != null)
                {
                    for (int tableIndex = 0; tableIndex < ds.Tables.Count; tableIndex++)
                    {
                        if (!ds.Tables[tableIndex].TableName.StartsWith(ExportToExcelResource.DataTableNameString))
                        {
                            if (tableIndex == 1)
                            {
                                Worksheet excelWorkSheet = (Worksheet)book.ActiveSheet;
                                excelWorkSheet.Name = ds.Tables[tableIndex].TableName;
                                // Creating Header Column In Excel
                                for (int i = 1; i < ds.Tables[tableIndex].Columns.Count + 1; i++)
                                {
                                    excelWorkSheet.Cells[1, i] = ds.Tables[tableIndex].Columns[i - 1].ColumnName;
                                    excelWorkSheet.Cells[1, i].Font.Bold = true;
                                }

                                // Exporting Rows in Excel
                                for (int j = 0; j < ds.Tables[tableIndex].Rows.Count; j++)
                                {
                                    for (int k = 0; k < ds.Tables[tableIndex].Columns.Count; k++)
                                    {
                                        excelWorkSheet.Cells[j + 2, k + 1] = ds.Tables[tableIndex].Rows[j].ItemArray[k].ToString();
                                    }
                                }
                            }
                            else
                            {
                                Worksheet additionalWorkSheet = book.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
                                additionalWorkSheet.Name = ds.Tables[tableIndex].TableName;
                                // Creating Header Column In Excel
                                for (int i = 1; i < ds.Tables[tableIndex].Columns.Count + 1; i++)
                                {
                                    additionalWorkSheet.Cells[1, i] = ds.Tables[tableIndex].Columns[i - 1].ColumnName;
                                    additionalWorkSheet.Cells[1, i].Font.Bold = true;
                                }

                                // Exporting Rows in Excel
                                for (int j = 0; j < ds.Tables[tableIndex].Rows.Count; j++)
                                {
                                    for (int k = 0; k < ds.Tables[tableIndex].Columns.Count; k++)
                                    {
                                        additionalWorkSheet.Cells[j + 2, k + 1] = ds.Tables[tableIndex].Rows[j].ItemArray[k].ToString();
                                    }
                                }
                            }
                        }
                    }
                }

                book.SaveAs(Xlfile);
                book.Close(true);
                excel.Quit();

                Marshal.ReleaseComObject(book);
                Marshal.ReleaseComObject(book);
                Marshal.ReleaseComObject(excel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

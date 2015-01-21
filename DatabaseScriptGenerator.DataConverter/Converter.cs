using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DatabaseScriptGenerator.DataConverter
{
    public static class Converter
    {
        public static string CreateInsertScript(DataTable dt, string[] identityColumns, string tableName)
        {
            var sb = new StringBuilder();
            string columns = GetColumns(dt, identityColumns);
            foreach (DataRow row in dt.Rows)
            {
                string strRow = GetRows(row, dt.Columns, identityColumns);
                string insertScript = String.Format("INSERT INTO {0} ({1}) VALUES ({2})", tableName, columns, strRow);

                sb.Append(insertScript);
            }

            return sb.ToString();
        }

        public static void SaveInsertScript(DataSet ds, string tableName)
        {
            
        }


        //table functions
        public static string GetRows(DataRow dr, DataColumnCollection columns, string[] identityColumns)
        {
            string rowData = "";
            foreach (DataColumn col in columns)
            {
                if(identityColumns.Contains(col.ToString()))
                    continue;

                string comma =  (rowData != "" && col != columns[columns.Count - 1]) ? "," : "";
                rowData += comma + dr[col.ColumnName];
            }

            return rowData;
        }

        public static string GetColumns(DataTable dt, string[] identityColumns)
        {
            string columnNames = "";
            foreach (DataColumn column in dt.Columns)
            {
                if (identityColumns.Contains(column.ToString()))
                    continue;
                    

                string comma = (columnNames != "" && column != dt.Columns[dt.Columns.Count - 1]) ? "," : "";
                columnNames += comma + column.ToString();
            }

            return columnNames;
        }
    }
}

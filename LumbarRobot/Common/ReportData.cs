﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace LumbarRobot.Common
{
    public class ReportData
    {
        private Dictionary<string, object> _reportDocumentValues = new Dictionary<string, object>();
        /// <summary>
        /// Gets a list of document values
        /// </summary>
        public Dictionary<string, object> ReportDocumentValues
        {
            get { return _reportDocumentValues; }
        }

        private List<DataTable> _dataTables = new List<DataTable>();
        /// <summary>
        /// Gets a list of data tables
        /// </summary>
        public List<DataTable> DataTables
        {
            get { return _dataTables; }
        }

        private bool _showUnknownValues = true;
        /// <summary>
        /// Shows all unknown values on the page
        /// </summary>
        public bool ShowUnknownValues
        {
            get { return _showUnknownValues; }
            set { _showUnknownValues = value; }
        }


        /// <summary>
        /// Gets a data table by table name
        /// </summary>
        /// <param name="tableName">table name (case insensitive)</param>
        /// <returns>null, if DataTable not found</returns>
        public DataTable GetDataTableByName(string tableName)
        {
            foreach (DataTable table in _dataTables)
            {
                if (table == null) continue;
                if (table.TableName == null) continue;
                if (table.TableName.Equals(tableName, System.StringComparison.InvariantCultureIgnoreCase)) return table;
            }
            return null;
        }

        /// <summary>
        /// Sets all DataRow values to document values
        /// </summary>
        /// <param name="dataRow">data row containing the document values</param>
        /// <param name="prefix">add prefix to name</param>
        public void SetDataRowValuesToDocumentValues(DataRow dataRow, string prefix)
        {
            if (prefix == null) prefix = "";
            foreach (DataColumn column in dataRow.Table.Columns)
            {
                _reportDocumentValues[prefix + column.ColumnName] = dataRow[column];
            }
        }

        /// <summary>
        /// Sets all DataRow values to document values
        /// </summary>
        /// <param name="dataRow">data row containing the document values</param>
        public void SetDataRowValuesToDocumentValues(DataRow dataRow)
        {
            SetDataRowValuesToDocumentValues(dataRow, "");
        }

        /// <summary>
        /// Sets all DataRow values to document values
        /// </summary>
        /// <param name="dataRowView">data row containing the document values</param>
        /// <param name="prefix">add prefix to name</param>
        public void SetDataRowValuesToDocumentValues(DataRowView dataRowView, string prefix)
        {
            if (prefix == null) prefix = "";
            foreach (DataColumn column in dataRowView.Row.Table.Columns)
            {
                _reportDocumentValues[prefix + column.ColumnName] = dataRowView.Row[column];
            }
        }

        /// <summary>
        /// Sets all DataRow values to document values
        /// </summary>
        /// <param name="dataRowView">data row containing the document values</param>
        public void SetDataRowValuesToDocumentValues(DataRowView dataRowView)
        {
            SetDataRowValuesToDocumentValues(dataRowView, "");
        }
    }
}

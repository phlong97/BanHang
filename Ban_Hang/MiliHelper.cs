using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraRichEdit.Fields;
using LiteDB;
using System;
using System.Collections.Generic;

namespace Ban_Hang
{
    public static class MiliHelper
    {
        public static string CreateKey()
        {
            return ObjectId.NewObjectId().ToString();
        }
        public static DateTime LayNgayTaoKey(string key)
        {
            var ObjId = new ObjectId(key);
            return ObjId.CreationTime.ToUniversalTime();
        }
        public static GridColumn NewGridCloumn(string _caption, string _fieldName, int _width = 0, string _colType = "T", string _formatString = "N0",bool _allowEdit = false, bool _visible = true)
        {
            GridColumn col = new GridColumn()
            {
                Caption = _caption,                
                FieldName = _fieldName,
                Visible = _visible,
            };
            col.OptionsColumn.AllowEdit = _allowEdit;
            if (_width != 0) col.Width = _width;
            if (_colType.Equals("N"))
            {
                col.UnboundDataType = typeof(double);
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                col.DisplayFormat.FormatString = _formatString;
            }
            else if (_colType.Equals("D"))
            {
                col.UnboundDataType = typeof(DateTime);
                col.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                col.DisplayFormat.FormatString = _formatString;
            }
            else if (_colType.Equals("T"))
            {
                col.UnboundDataType = typeof(string);
                col.DisplayFormat.FormatString = _formatString;
            }
            else if (_colType.Equals("I"))
            {
                col.UnboundDataType = typeof(object);
            }
            return col;
        }
    }
}

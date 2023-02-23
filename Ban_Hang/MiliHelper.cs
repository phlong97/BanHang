using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
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
            GridColumn col = new()
            {
                Caption = _caption,
                Name = _fieldName,
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
                col.UnboundType = DevExpress.Data.UnboundColumnType.Object;
                RepositoryItemPictureEdit ri = new RepositoryItemPictureEdit();
                ri.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
                ri.CustomHeight = 0;
                col.ColumnEdit = ri;                
            }            
            col.Tag = _colType;
            return col;
        }
    }
}

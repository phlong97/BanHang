using DevExpress.CodeParser;
using DevExpress.DirectX.Common.DirectWrite;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace Ban_Hang
{
    public partial class DanhMuc_HangHoa : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        BindingList<HangHoa> dataSource;
        public DanhMuc_HangHoa()
        {
            InitializeComponent();
            InitGridViewHangHoa();           
            gridControl.DataSource = DuLieuBanHang.DSHangHoa.Select(x => x.ToHangHoa()).ToList();
            bsiRecordsCount.Caption = "RECORDS : " + DuLieuBanHang.DSHangHoa.Count;
        }

        //private BindingList<HangHoa> GetDataSource()
        //{
        //    return new BindingList<HangHoa>(DuLieuBanHang.DSHangHoa.Select(x => x.ToHangHoa()).ToList());
        //}

        private void InitGridViewHangHoa()
        {
            gridView.Columns.Clear();
            gridView.OptionsView.RowAutoHeight = true;
            gridView.OptionsView.ColumnAutoWidth= true;
            gridView.Columns.Add(MiliHelper.NewGridCloumn("Mã hàng hóa", "MaHH"));
            gridView.Columns.Add(MiliHelper.NewGridCloumn("Tên hàng hóa", "TenHH"));
            gridView.Columns.Add(MiliHelper.NewGridCloumn("ĐVT", "Dvt"));
            gridView.Columns.Add(MiliHelper.NewGridCloumn("Nhóm hàng", "TenNhom"));
            gridView.Columns.Add(MiliHelper.NewGridCloumn("Giá bán", "GiaBan", 0, "N"));
            gridView.Columns.Add(MiliHelper.NewGridCloumn("Mô tả", "MoTa"));
            gridView.Columns.Add(MiliHelper.NewGridCloumn("Hình ảnh", "Image",0,"I"));

            gridView.CustomUnboundColumnData -= gridView_CustomUnboundColumnData;
            gridView.CustomUnboundColumnData += gridView_CustomUnboundColumnData;

            gridView.RowCellDefaultAlignment -= GridView_RowCellDefaultAlignment;
            gridView.RowCellDefaultAlignment += GridView_RowCellDefaultAlignment;

        }

        private void GridView_RowCellDefaultAlignment(object sender, RowCellAlignmentEventArgs e)
        {
            string colType = e.Column.Tag.ToString();
            if (!string.IsNullOrEmpty(colType))
            {
                e.HorzAlignment = colType.Equals("N") ? DevExpress.Utils.HorzAlignment.Far : DevExpress.Utils.HorzAlignment.Center;
            }
        }

        Dictionary<string, Image> imageCache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);
        void gridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "Image" && e.IsGetData)
            {
                GridView view = sender as GridView;
                string fileName = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), "HinhAnh") as string ?? string.Empty;
                if (!imageCache.ContainsKey(fileName))
                {
                    Image img = GetImage(fileName);
                    imageCache.Add(fileName, img);
                }
                e.Value = imageCache[fileName];
            }
        }

        

        Image GetImage(string fileName)
        {
            // Load an image by its local path, URL, etc.
            // The following code loads the image from te specified file.
            Image img = null;
            if (File.Exists(fileName))
                img = Image.FromFile(fileName);
            else
                img = Image.FromFile(Path.Combine(Environment.CurrentDirectory,@"imgs\no_img.jpg"));
            return img;
        }
    }  
    
}
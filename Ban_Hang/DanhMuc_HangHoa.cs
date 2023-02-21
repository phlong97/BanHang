using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraRichEdit.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            GridColumn ImgCol = MiliHelper.NewGridCloumn("Hình ảnh", "HinhAnh", 20, "I");
            AssignPictureEdittoImageColumn(ImgCol);
            gridView.Columns.Add(ImgCol);
            gridView.Columns.Add(MiliHelper.NewGridCloumn("Mã hàng hóa", "MaHH", 15));
            gridView.Columns.Add(MiliHelper.NewGridCloumn("Tên hàng hóa", "TenHH", 40));
            gridView.Columns.Add(MiliHelper.NewGridCloumn("ĐVT", "Dvt",15));
            gridView.Columns.Add(MiliHelper.NewGridCloumn("Nhóm hàng", "TenNhom", 25));
            gridView.Columns.Add(MiliHelper.NewGridCloumn("Giá bán", "GiaBan", 25, "N"));
            gridView.Columns.Add(MiliHelper.NewGridCloumn("Mô tả", "MoTa",50));

            gridView.CustomUnboundColumnData -= gridView_CustomUnboundColumnData;
            gridView.CustomUnboundColumnData += gridView_CustomUnboundColumnData;

        }
        void AssignPictureEdittoImageColumn(GridColumn column)
        {
            // Create and customize the PictureEdit repository item.
            RepositoryItemPictureEdit riPictureEdit = new RepositoryItemPictureEdit();
            riPictureEdit.SizeMode = PictureSizeMode.Zoom;

            // Add the PictureEdit to the grid's RepositoryItems collection.
            gridControl.RepositoryItems.Add(riPictureEdit);

            // Assign the PictureEdit to the 'Image' column.
            column.ColumnEdit = riPictureEdit;
        }
        Dictionary<string, Image> imageCache = new Dictionary<string, Image>();
        private void gridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsSetData)
                return;
            CardView view = sender as CardView;
            var path = (string)view.GetListSourceRowCellValue(e.ListSourceRowIndex, "HinhAnh");
            if (string.IsNullOrEmpty(path))
                return;
            try
            {
                e.Value = LoadImage(path);
            }
            catch { }
        }

        Image LoadImage(string path)
        {
            Image img;
            if (!imageCache.TryGetValue(path, out img))
            {
                if (File.Exists(path))
                    img = Image.FromFile(path);
                imageCache.Add(path, img);
            }
            return img;
        }
    }  
    
}
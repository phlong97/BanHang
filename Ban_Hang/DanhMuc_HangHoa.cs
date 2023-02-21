using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
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

            //gridView.CustomUnboundColumnData -= gridView_CustomUnboundColumnData;
            //gridView.CustomUnboundColumnData += gridView_CustomUnboundColumnData;

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
        Dictionary<string, Image> imageCache = new Dictionary<string, Image>(StringComparer.OrdinalIgnoreCase);

        void gridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "HinhAnh" && e.IsGetData)
            {
                GridView view = sender as GridView;
                string fileName = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), "ImagePath") as string ?? string.Empty;
                if (!imageCache.ContainsKey(fileName))
                {
                    Image img = GetImage(fileName);
                    imageCache.Add(fileName, img);
                }
                e.Value = imageCache[fileName];
            }
        }

        Image GetImage(string path)
        {
            // Load an image by its local path, URL, etc.
            // The following code loads the image from te specified file.
            Image img = null;
            if (File.Exists(path))
                img = Image.FromFile(path);
            else
                img = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "imgs", "no_img.jpg"));
            return img;
        }

        private void gridView_CustomUnboundColumnData_1(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "HinhAnh" && e.IsGetData)
            {
                GridView view = sender as GridView;
                string fileName = view.GetRowCellValue(view.GetRowHandle(e.ListSourceRowIndex), "ImagePath") as string ?? string.Empty;
                if (!imageCache.ContainsKey(fileName))
                {
                    Image img = GetImage(fileName);
                    imageCache.Add(fileName, img);
                }
                e.Value = imageCache[fileName];
            }
        }
    }  
    
}
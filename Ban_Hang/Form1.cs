using DevExpress.Utils.Extensions;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Columns;
using LiteDB;
using System.Linq;
using System.Windows.Forms;

namespace Ban_Hang
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            Init();
            RefreshData();
        }
        private void RefreshData()
        {
            using (var db = new LiteDatabase(TuDien.LITEDB_LOCAL_PATH))
            {
                var coll = db.GetCollection<NhomHangCloud>(TuDien.ColName.NhomHang);
                DuLieuBanHang.DSNhomHang.Clear();
                DuLieuBanHang.DSNhomHang.AddRange(coll.Find(x => true).ToList());
                lookUpEdit1.Properties.DataSource = coll.Find(x => true).ToList();               
                
                gridNK.DataSource = DuLieuBanHang.DSNhomHang.Select(x => x.ToNhomHang()).ToList();
            }
        }
        private void Init()
        {
            lookUpEdit1.Clear();
            lookUpEdit1.Text = "";
            lookUpEdit1.Properties.ValueMember = "Id";
            lookUpEdit1.Properties.DisplayMember = "TenNhom";
            lookUpEdit1.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo
            {
                FieldName = "TenNhom",
                Caption = "Tên nhóm",
                Alignment = DevExpress.Utils.HorzAlignment.Center,
                Width= 15,                
            });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn { FieldName = "TenNhom", Caption = "Tên nhóm",Visible = true });
            gridView1.Columns.Add(new DevExpress.XtraGrid.Columns.GridColumn { FieldName = "TenNhomCha", Caption = "Tên nhóm cha",Visible = true });
        }
        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            using (var db = new LiteDatabase(TuDien.LITEDB_LOCAL_PATH))
            {
                var coll = db.GetCollection<NhomHangCloud>(TuDien.ColName.NhomHang);
                coll.Insert(new NhomHangCloud()
                {
                    Id = MiliHelper.CreateKey(),
                    TenNhom = txtTenNK.Text,
                    IdNhomCha = lookUpEdit1.EditValue == null ? string.Empty : lookUpEdit1.EditValue.ToString()
                });
            }
            RefreshData();
        }
    }
}

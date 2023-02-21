using DevExpress.Utils.Extensions;
using DevExpress.Utils.Menu;
using DevExpress.Utils.Serializing;
using DevExpress.XtraGrid.Columns;
using LiteDB;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ban_Hang
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
            LoadDanhMuc();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            using (DanhMuc_HangHoa f = new DanhMuc_HangHoa())
            {                
                f.ShowDialog();
            }
        }

        private void LoadDanhMuc()
        {
            using (var db = new LiteDatabase(TuDien.LITEDB_LOCAL_PATH))
            {
                var collNH = db.GetCollection<NhomHangCloud>(TuDien.ColName.NhomHang);
                DuLieuBanHang.DSNhomHang.Clear();
                DuLieuBanHang.DSNhomHang.AddRange(collNH.FindAll());

                var collHH = db.GetCollection<HangHoaCloud>(TuDien.ColName.HangHoa);
                DuLieuBanHang.DSHangHoa.Clear();
                DuLieuBanHang.DSHangHoa.AddRange(collHH.FindAll());
            }
        }
        private async void btnAdd_Click(object sender, System.EventArgs e)
        {
            await Task.Run(() => Generator.TaoDanhMucLiteDb());
            
            MessageBox.Show(this, "Tạo danh mục thành công!");
        }
    }
}

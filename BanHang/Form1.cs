using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace BanHang
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTaoDM_Click(object sender, EventArgs e)
        {
            Generator.TaoDanhMucJSon();
            MessageBox.Show("Tạo danh mục thành công!");
        }

        private void btnLoadDM_Click(object sender, EventArgs e)
        {
            //Danh mục nhóm hàng
            var DsNH = Generator.ReadFromJsonFile<List<NhomHang>>("DanhMuc/nhomhang.txt");
            DanhmucChung.DSNhomHang.Clear();
            DanhmucChung.DSNhomHang.AddRange(DsNH);
            //Danh mục hàng hóa
            var DsHH = Generator.ReadFromJsonFile<List<HangHoaCloud>>("DanhMuc/hanghoa.txt");
            DanhmucChung.DSHangHoa.Clear();
            DanhmucChung.DSHangHoa.AddRange(DsHH.Select(x => x.ToHangHoa()).ToList());
            //Danh mục nhóm khách
            var DsNK = Generator.ReadFromJsonFile<List<NhomKhach>>("DanhMuc/nhomkhach.txt");
            DanhmucChung.DSNhomKhach.Clear();
            DanhmucChung.DSNhomKhach.AddRange(DsNK);
            //Danh mục nhân viên
            var DsNV = Generator.ReadFromJsonFile<List<NhanVien>>("DanhMuc/nhanvien.txt");
            DanhmucChung.DSNhanVien.Clear();
            DanhmucChung.DSNhanVien.AddRange(DsNV);
            //Danh mục khách hàng
            var DSKH = Generator.ReadFromJsonFile<List<KhachHangCloud>>("DanhMuc/khachhang.txt");
            DanhmucChung.DSKhachHang.Clear();
            DanhmucChung.DSKhachHang.AddRange(DSKH.Select(x => x.ToKhachHang()).ToList());
            //CTCongNo
            var CTCN = Generator.ReadFromJsonFile<List<CTCongNo>>("DanhMuc/CTCongNo.txt");
            DanhmucChung.CTCongNo.Clear();
            DanhmucChung.CTCongNo.AddRange(CTCN);
            //TheKho
            var TheKho = Generator.ReadFromJsonFile<List<OTheKho>>("DanhMuc/thekho.txt");
            DanhmucChung.CTTheKho.Clear();
            DanhmucChung.CTTheKho.AddRange(TheKho);

            dgTheKho.DataSource = DanhmucChung.CTTheKho;
            dgKH.DataSource = DanhmucChung.DSKhachHang;
            dgNV.DataSource = DanhmucChung.DSNhanVien;
            dgHangHoa.DataSource = DanhmucChung.DSHangHoa;
            dgCTCNo.DataSource = DanhmucChung.CTCongNo;

        }

        private void btnTHCN_Click(object sender, EventArgs e)
        {
            dgTongHopCN.DataSource = DanhmucChung.THCNNguoiMua(new DateTime(2021, 1, 1), new DateTime(2021, 12, 31));
        }

        private void btnTHTK_Click(object sender, EventArgs e)
        {
            dgTonKho.DataSource = DanhmucChung.THTonKho(new DateTime(2021, 1, 1), new DateTime(2021, 12, 31));
        }
    }


}

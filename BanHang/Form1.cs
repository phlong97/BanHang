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
            DuLieuBanHang.DSNhomHang.Clear();
            DuLieuBanHang.DSNhomHang.AddRange(DsNH);
            //Danh mục hàng hóa
            var DsHH = Generator.ReadFromJsonFile<List<HangHoaCloud>>("DanhMuc/hanghoa.txt");
            DuLieuBanHang.DSHangHoa.Clear();
            DuLieuBanHang.DSHangHoa.AddRange(DsHH.Select(x => x.ToHangHoa()).ToList());
            //Danh mục nhóm khách
            var DsNK = Generator.ReadFromJsonFile<List<NhomKhach>>("DanhMuc/nhomkhach.txt");
            DuLieuBanHang.DSNhomKhach.Clear();
            DuLieuBanHang.DSNhomKhach.AddRange(DsNK);
            //Danh mục nhân viên
            var DsNV = Generator.ReadFromJsonFile<List<NhanVien>>("DanhMuc/nhanvien.txt");
            DuLieuBanHang.DSNhanVien.Clear();
            DuLieuBanHang.DSNhanVien.AddRange(DsNV);
            //Danh mục khách hàng
            var DSKH = Generator.ReadFromJsonFile<List<KhachHangCloud>>("DanhMuc/khachhang.txt");
            DuLieuBanHang.DSKhachHang.Clear();
            DuLieuBanHang.DSKhachHang.AddRange(DSKH.Select(x => x.ToKhachHang()).ToList());
            //Quỹ tiền tệ
            var qtt = Generator.ReadFromJsonFile<List<QuyTienTe>>("DanhMuc/quytiente.txt");
            DuLieuBanHang.DSQuyTT.Clear();
            DuLieuBanHang.DSQuyTT.AddRange(qtt);
            //TheKho
            var SoCai = Generator.ReadFromJsonFile<List<SoCai>>("DanhMuc/socai.txt");
            DuLieuBanHang.SoCaiTongHop.Clear();
            DuLieuBanHang.SoCaiTongHop.AddRange(SoCai);


            dgTHTonQuy.DataSource = DuLieuBanHang.DSKhachHang;
            dgNV.DataSource = DuLieuBanHang.DSNhanVien;
            dgHangHoa.DataSource = DuLieuBanHang.DSHangHoa;
            dgCTCNo.DataSource = DuLieuBanHang.CTCongNo;
            dgSoCai.DataSource = DuLieuBanHang.SoCaiTongHop;

        }

        private void btnTHCN_Click(object sender, EventArgs e)
        {
            dgTongHopCN.DataSource = DuLieuBanHang.THCongNo(new DateTime(2021, 1, 1), new DateTime(2021, 12, 31), 0);
        }

        private void btnTHTK_Click(object sender, EventArgs e)
        {
            dgTonKho.DataSource = DuLieuBanHang.THTonKho(new DateTime(2021, 1, 1), new DateTime(2021, 12, 31));
        }

        private void btnTHTQ_Click(object sender, EventArgs e)
        {
            dgTHTonQuy.DataSource = DuLieuBanHang.THTonQuy(new DateTime(2021, 1, 1), new DateTime(2021, 12, 31), "TM");
        }
    }


}

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
            Generator.TaoDanhMucXML();
            MessageBox.Show("Tạo danh mục thành công!");
        }

        private void btnLoadDM_Click(object sender, EventArgs e)
        {
            //Danh mục nhóm hàng
            var DsNH = Generator.ReadFromXmlFile<List<NhomHang>>("DanhMuc/nhomhang.txt");
            DanhmucChung.DSNhomHang.Clear();
            DanhmucChung.DSNhomHang.AddRange(DsNH);
            //Danh mục hàng hóa
            var DsHH = Generator.ReadFromXmlFile<List<HangHoaCloud>>("DanhMuc/hanghoa.txt");
            DanhmucChung.DSHangHoa.Clear();
            DanhmucChung.DSHangHoa.AddRange(DsHH.Select(x => x.ToHangHoa()).ToList());
            //Danh mục nhóm khách
            var DsNK = Generator.ReadFromXmlFile<List<NhomKhach>>("DanhMuc/nhomkhach.txt");
            DanhmucChung.DSNhomKhach.Clear();
            DanhmucChung.DSNhomKhach.AddRange(DsNK);
            //Danh mục nhân viên
            var DsNV = Generator.ReadFromXmlFile<List<NhanVien>>("DanhMuc/nhanvien.txt");
            DanhmucChung.DSNhanVien.Clear();
            DanhmucChung.DSNhanVien.AddRange(DsNV);
            //Danh mục khách hàng
            var DSKH = Generator.ReadFromXmlFile<List<KhachHangCloud>>("DanhMuc/khachhang.txt");
            DanhmucChung.DSKhachHang.Clear();
            DanhmucChung.DSKhachHang.AddRange(DSKH.Select(x => x.ToKhachHang()).ToList());

            MessageBox.Show("Load danh mục thành công!");

            dgDanhMuc.DataSource = DanhmucChung.DSKhachHang;
        }
    }


}

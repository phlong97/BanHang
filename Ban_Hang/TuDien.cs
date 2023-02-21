using System;
using System.IO;

namespace Ban_Hang
{
    internal static class TuDien
    {
        public const string FIREBASE_URL = "", FIREBASE_SECRET = "";
        public static string LITEDB_LOCAL_PATH = Path.Combine(Environment.CurrentDirectory, "Ban_Hang.db");

        public static string[] dsCollection = { "KhachHangCloud", "DonHangCloud","TheKhoCloud","NhanVien","CTTienTeCloud",
            "QuyTienTe","HangHoaCloud","BangGiaCloud","NhomHang","NhomKhach", "Kho"};
        public static class ColName
        {
            public const string HangHoa = "HangHoaCloud", NhanVien = "NhanVien", KhachHang = "KhachHang",
                BangGia = "BangGiaCloud", NhomHang = "NhomHangCloud", CTTTe = "CTTTeCloud", QuyTienTe = "QuyTienTe",
                Kho = "Kho", DonHang = "DonHangCloud", TheKho = "TheKhoCloud", NhomKhach = "NhomKhach";
        }
        public static class DonHang
        {
            public const string DonHangMua = "N1",
                DonHangBan = "X1", NguoiMuaTraLai = "N2", TraLaiNguoiBan = "X2";
        }
        public static class HangHoa
        {
            public const string LaHangBan = "l1",
                TonKho = "n1", TonMin = "n2", TonMax = "n3",
                HinhAnh = "t1", MoTa = "t2", MaVach = "t3", NgungKinhDoanh = "l2", CollName = "HangHoaCloud";
        }
        public static class BangGia
        {
            public const string ApDungToanQuoc = "l1", CollName = "BangGiaCloud";
        }
        public static class KhachHang
        {
            public const string LaCaNhan = "l1", CollName = "KhachHang",
            NgaySinh = "d1", EMail = "t1", DiaChi = "t2", DienThoai = "t3";
        }

        public static class MaQuanTri
        {
            public const string Quy = "Q", PhapNhan = "PN", DoanhThu = "DT", ChiPhi = "CP", Thue = "THUE",
                GiaVon = "GV", HangTonKho = "HTK", ChietKhau = "CK";
        }
        public static class LoaiCT
        {
            public const string DonHangMua = "N1",
                DonHangBan = "X1", NguoiMuaTraLai = "N2", TraLaiNguoiBan = "X2";
        }
    }
}
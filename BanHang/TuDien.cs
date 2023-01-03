using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHang
{
    internal static class TuDien
    {
        public static class DonHang
        {
            public const string DonHangMua = "N1",
                DonHangBan = "X1", NguoiMuaTraLai = "N2", TraLaiNguoiBan = "X2";
        }
        public static class HangHoa
        {
            public const string LaHangBan = "l1",
                TonKho = "n1", TonMin = "n2", TonMax = "n3",
                HinhAnh = "t1", MoTa = "t2", MaVach = "t3", NgungKinhDoanh = "l2";
        }
        public static class BangGia
        {

        }

        public static readonly string BG_ApDungToanQuoc = "l1";

        public static readonly string KH_LaCaNhan = "l1";


        public static readonly string NgaySinh = "d1";

        public static readonly string EMail = "t1";
        public static readonly string DiaChi = "t2";
        public static readonly string DienThoai = "t3";
        public static class DoiTuong
        {
            public const string Quy = "Q", PhapNhan = "PN", DoanhThu = "DT", ChiPhi = "CP", Thue = "THUE";
        }
    }
}
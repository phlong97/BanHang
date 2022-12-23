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
                GiaVon = "n1", TonKho = "n2", TonMin = "n3", TonMax = "n4",
                HinhAnh = "t1", MoTa = "t2", NgungKinhDoanh = "l2";
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
    }
}
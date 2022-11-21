using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHang
{
    internal static class DanhmucChung
    {
        public static List<NhomHang> DSNhomHang = new();
        public static List<HangHoa> DSHangHoa = new();

        public static List<NhomKhach> DSNhomKhach = new();
        public static List<KhachHang> DSKhachHang = new();
        public static List<string> ListToanTu = new() { "<", "<=", "=", ">", ">=" };
        public static List<TenTruongTruyVan> ListTenTruong = new();
    }
    internal class TenTruongTruyVan
    {
        public string Id { get; set; }
        public string Ten { get; set; }
    }
    internal class NhomHang
    {
        public string Id { get; set; }
        public string TenNhom { get; set; }
        public string IdNhomCha { get; set; }
    }

    internal class LoaiHang
    {
        public string Id { get; set; }
        public string TenLoai { get; set; }
    }

    internal class DinhMuc
    {
        public string IdHH { get; set; }
        public float SoLuong { get; set; }
    }
    internal class DVTMoRong
    {
        public string TenDonVi { get; set; }
        public float GiaTriQuyDoi { get; set; }
        public float GiaBan { get; set; }
        public List<ThuocTinh> DSThuocTinh { get; set; } = new();
    }
    internal class ThuocTinh
    {
        public string Ten { get; set; }
        public string GiaTri { get; set; }
        public char KieuGiaTri { get; set; }
    }

    internal class HangHoa
    {
        public string Id { get; set; }
        public string MaHH { get; set; }
        public string MaLoai { get; set; }
        public string TenHH { get; set; }
        private string _IdNhom;
        public string IdNhom
        {
            get => _IdNhom;
            set
            {
                _IdNhom = value;
                var nh = DanhmucChung.DSNhomHang.FirstOrDefault(x => x.Equals(_IdNhom));
                TenNhom = nh == null ? string.Empty : nh.TenNhom;
            }
        }

        public string TenNhom { get; set; }
        public double GiaBan { get; set; }
        public string DVT { get; set; }
        public List<DVTMoRong> DVTMoRong { get; set; } = new();
        public bool LaHangBan { get; set; }
        public float GiaVon { get; set; }
        public float TonKho { get; set; }
        public float TonMin { get; set; }
        public float TonMax { get; set; }
        public List<DinhMuc> LstDinhMuc { get; set; } = new();
        public string HinhAnh { get; set; }
        public string MoTa { get; set; }
        public bool NgungKinhDoanh { get; set; }
    }

    internal class BangGia
    {
        public string Id { get; set; }
        public string Ten { get; set; }
        public bool ApDungToanQuoc { get; set; }
        public List<string> DSChiNhanh { get; set; } = new();
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public List<HangHoaKhuyenMai> DSHangHoaKM { get; set; } = new();
        public bool ApDung { get; set; }
    }
    internal class HangHoaKhuyenMai
    {
        private string _IdHH;
        public string IdHH
        {
            get => _IdHH;
            set
            {
                _IdHH = value;
                var hh = DanhmucChung.DSHangHoa.FirstOrDefault(x => x.Id.Equals(_IdHH));
                TenHH = hh == null ? string.Empty : hh.TenHH;
            }
        }
        public string TenHH { get; set; }
        public float GiaVon { get; set; }
        public float GiaNhapCuoi { get; set; }
        public float GiaChung { get; set; }
        public float GiaMoi { get; set; }
    }
    internal class DieuKienKH
    {
        private string _IdTT;
        public string IdTT
        {
            get => _IdTT;
            set
            {
                _IdTT = value;
                var TT = DanhmucChung.ListTenTruong.FirstOrDefault(x => x.Id.Equals(_IdTT));
                TenTruong = TT == null ? string.Empty : TT.Ten;
            }
        }
        public string TenTruong { get; set; }
        public string ToanTu { get; set; }
        public float GiaTri { get; set; }
    }
    internal class NhomKhach
    {
        public string Id { get; set; }
        public string TenNhom { get; set; }
        public float GiamGia { get; set; }
        public bool GiamGiaTrucTiep { get; set; }
        public List<DieuKienKH> DSDieuKien { get; set; } = new();
        public string GhiChu { get; set; }
    }
    internal class KhachHang
    {
        public string Id { get; set; }        
        public string TenKhach { get; set; }
        private string _IdNhomKhach;

        public string IdNhomKhach 
        { 
            get => _IdNhomKhach;
            set
            {
                _IdNhomKhach = value;
                var nk = DanhmucChung.DSNhomKhach.FirstOrDefault(x => x.Id == _IdNhomKhach);
                TenNhomKhach = nk == null ? string.Empty : nk.TenNhom;
            } 
        }
        public string TenNhomKhach { get; set; }
        public bool LaCaNhan { get; set; }
        public DateTime NgaySinh { get; set; }
        public string EMail { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }

    }

}

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
    /// <summary>
    /// Sử dụng cho điều kiện khách hàng
    /// </summary>
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
        public DinhMuc MakeCopy()
        {
            return (DinhMuc)this.MemberwiseClone();
        }
    }
    internal class DVTMoRong
    {
        public string TenDonVi { get; set; }
        public float GiaTriQuyDoi { get; set; }
        public float GiaBan { get; set; }
        public List<ThuocTinh> DSThuocTinh { get; set; } = new();
        public DVTMoRong MakeCopy()
        {
            DVTMoRong mr = (DVTMoRong)this.MemberwiseClone();
            mr.DSThuocTinh = this.DSThuocTinh.Select(x => x.MakeCopy()).ToList();
            return mr;
        }
    }

    internal class ThuocTinh
    {
        public string Ten { get; set; }
        public string GiaTri { get; set; }
        public char KieuGiaTri { get; set; }
        public ThuocTinh MakeCopy()
        {
            return (ThuocTinh)this.MemberwiseClone();
        }
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
        public double GiaBan { get => nf.GetValue("GiaBan"); set => nf.SetValue("GiaBan", value); }
        public string DVT { get => tf.GetValue("DVT"); set => tf.SetValue("DVT", value); }
        public List<DVTMoRong> DVTMoRong { get; set; } = new();
        public bool LaHangBan { get => lf.GetValue("LaHangBan"); set => lf.SetValue("LaHangBan", value); }
        public double GiaVon { get => nf.GetValue("GiaVon"); set => nf.SetValue("GiaVon", value); }
        public double TonKho { get => nf.GetValue("TonKho"); set => nf.SetValue("TonKho", value); }
        public double TonMin { get => nf.GetValue("TonMin"); set => nf.SetValue("TonMin", value); }
        public double TonMax { get => nf.GetValue("TonMax"); set => nf.SetValue("TonMax", value); }
        public List<DinhMuc> LstDinhMuc { get; set; } = new();
        public string HinhAnh { get => tf.GetValue("HinhAnh"); set => tf.SetValue("HinhAnh", value); }
        public string MoTa { get => tf.GetValue("MoTa"); set => tf.SetValue("MoTa", value); }
        public bool NgungKinhDoanh { get => lf.GetValue("NgungKinhDoanh"); set => lf.SetValue("NgungKinhDoanh", value); }
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
        public HangHoaCloud ToHangHoaCloud()
        {
            var hh = new HangHoaCloud()
            {
                Id = this.Id,
                IdNhom = this.IdNhom,
                TenHH = this.TenHH,
                MaHH = this.MaHH,
                MaLoai = this.MaLoai,
                DVTMoRong = this.DVTMoRong.Select(x => x.MakeCopy()).ToList(),
                LstDinhMuc = this.LstDinhMuc.Select(x => x.MakeCopy()).ToList(),
                tf = this.tf.MakeCopy(),
                nf = this.nf.MakeCopy(),
                df = this.df.MakeCopy(),
                lf = this.lf.MakeCopy()
            };
            return hh;
        }
    }

    internal class HangHoaCloud
    {
        public string Id { get; set; }
        public string MaHH { get; set; }
        public string MaLoai { get; set; }
        public string TenHH { get; set; }
        public string IdNhom { get; set; }
        public List<DVTMoRong> DVTMoRong { get; set; } = new();
        public List<DinhMuc> LstDinhMuc { get; set; } = new();
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
        public HangHoa ToHangHoa()
        {
            var hh = new HangHoa()
            {
                Id = this.Id,
                IdNhom = this.IdNhom,
                TenHH = this.TenHH,
                MaHH = this.MaHH,
                MaLoai = this.MaLoai,
                DVTMoRong = this.DVTMoRong.Select(x => x.MakeCopy()).ToList(),
                LstDinhMuc = this.LstDinhMuc.Select(x => x.MakeCopy()).ToList(),
                tf = this.tf.MakeCopy(),
                nf = this.nf.MakeCopy(),
                df = this.df.MakeCopy(),
                lf = this.lf.MakeCopy()
            };
            return hh;
        }
    }

    internal class BangGia
    {
        public string Id { get; set; }
        public string Ten { get; set; }
        public bool ApDungToanQuoc { get => lf.GetValue("ApDungToanQuoc"); set => lf.SetValue("ApDungToanQuoc", value); }
        public List<string> DSChiNhanh { get; set; } = new();
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public List<HangHoaKhuyenMai> DSHangHoaKM { get; set; } = new();
        public bool ApDung { get; set; }
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
        public BangGiaCloud ToBangGiaCloud()
        {
            var bg = new BangGiaCloud()
            {
                Id = this.Id,
                Ten = this.Ten,
                TuNgay = this.TuNgay,
                DenNgay = this.DenNgay,
                ApDung = this.ApDung,
                DSChiNhanh = this.DSChiNhanh,
                tf = this.tf.MakeCopy(),
                nf = this.nf.MakeCopy(),
                df = this.df.MakeCopy(),
                lf = this.lf.MakeCopy()
            }
        }
    }
    internal class BangGiaCloud
    {
        public string Id { get; set; }
        public string Ten { get; set; }
        public List<string> DSChiNhanh { get; set; } = new();
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public bool ApDung { get; set; }
        public List<HangHoaKhuyenMai> DSHangHoaKM { get; set; } = new();
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
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
                MaHang = hh == null ? string.Empty : hh.MaHH;
            }
        }
        public string MaHang { get; set; }
        public string TenHH { get; set; }

        public float GiaVon { get; set; }
        public float GiaNhapCuoi { get; set; }
        public float GiaChung { get; set; }
        public float GiaMoi { get; set; }
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
        public HangHoaKhuyenMaiCloud ToHangHoaKhuyenMaiCloud()
        {
            var hh = new HangHoaKhuyenMaiCloud()
            {
                IdHH = this.IdHH,
                GiaVon = this.GiaVon,
                GiaChung = this.GiaChung,
                GiaMoi = this.GiaMoi,
                GiaNhapCuoi = this.GiaNhapCuoi,
                tf = this.tf.MakeCopy(),
                nf = this.nf.MakeCopy(),
                df = this.df.MakeCopy(),
                lf = this.lf.MakeCopy()
            };
            return hh;
        }
    }
    internal class HangHoaKhuyenMaiCloud
    {
        public string IdHH { get; set; }
        public float GiaVon { get; set; }
        public float GiaNhapCuoi { get; set; }
        public float GiaChung { get; set; }
        public float GiaMoi { get; set; }
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
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
        public string MaKH { get; set; }
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
        public bool LaCaNhan { get => lf.GetValue("LaCaNhan"); set => lf.SetValue("LaCaNhan", value); }
        public DateTime NgaySinh { get => df.GetValue("NgaySinh"); set => df.SetValue("NgaySinh", value); }
        public string EMail { get => tf.GetValue("EMail"); set => tf.SetValue("EMail", value); }
        public string DiaChi { get => tf.GetValue("DiaChi"); set => tf.SetValue("DiaChi", value); }
        public string DienThoai { get => tf.GetValue("DienThoai"); set => tf.SetValue("DienThoai", value); }
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
        public KhachHangCloud ToKhachHangCloud()
        {
            var kh = new KhachHangCloud()
            {
                Id = this.Id,
                MaKH = this.MaKH,
                TenKhach = this.TenKhach,
                IdNhomKhach = this.IdNhomKhach,
                tf = this.tf.MakeCopy(),
                nf = this.nf.MakeCopy(),
                df = this.df.MakeCopy(),
                lf = this.lf.MakeCopy()
            };
            return kh;
        }

    }

    internal class KhachHangCloud
    {
        public string Id { get; set; }
        public string MaKH { get; set; }
        public string TenKhach { get; set; }
        public string IdNhomKhach { get; set; }
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
        public KhachHang ToKhachHang()
        {
            var kh = new KhachHang()
            {
                Id = this.Id,
                MaKH = this.MaKH,
                TenKhach = this.TenKhach,
                IdNhomKhach = this.IdNhomKhach,
                tf = this.tf.MakeCopy(),
                nf = this.nf.MakeCopy(),
                df = this.df.MakeCopy(),
                lf = this.lf.MakeCopy()
            };
            return kh;
        }
    }

}

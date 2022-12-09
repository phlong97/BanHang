using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BanHang
{
    public static class DanhmucChung
    {
        public static List<NhomHang> DSNhomHang = new();
        public static List<HangHoa> DSHangHoa = new();
        public static List<BangGia> DSBangGia = new();
        public static List<NhomKhach> DSNhomKhach = new();
        public static List<KhachHang> DSKhachHang = new();
        public static List<NhanVien> DSNhanVien = new();
        public static List<string> ListToanTu = new() { "<", "<=", "=", ">", ">=" };
        public static List<TenTruongTruyVan> ListTenTruong = new();
        public static List<CTCongNo> CTCongNo = new();
        public static List<TongHopCongNo> THCNNguoiMua(DateTime TuNgay, DateTime DenNgay)
        {
            List<TongHopCongNo> ttcnnm = new();
            foreach (var kh in DSKhachHang.Where(x => x.LoaiKhach == 0))
            {
                var DKCo = CTCongNo.Where(x => x.IdKH.Equals(kh.Id) && x.Ngay < TuNgay).Sum(x => x.PSCo);
                var lstCongNo = CTCongNo.Where(x => x.IdKH.Equals(kh.Id)).Select(x =>
                    new TongHopCongNo
                    {
                        MaKH = kh.MaKH,
                        TenKH = kh.TenKhach,

                    });

                //ttcnnm.Add(new TongHopCongNo
                //{
                //    MaKH = kh.MaKH,
                //    TenKH = kh.TenKhach,
                //    DKNo = 
                //});
            }
            return ttcnnm;
        }
    }
    public class TongHopCongNo
    {
        public string MaKH { get; set; }
        public string TenKH { get; set; }
        public double DKNo { get; set; }
        public double DKCo { get; set; }
        public double PSNo { get; set; }
        public double PSCo { get; set; }
        public double CK => DKNo - DKCo + PSNo - PSCo;
        public double CKNo => Math.Max(CK, 0);
        public double CKCo => Math.Max(-CK, 0);

    }
    /// <summary>
    /// Sử dụng cho điều kiện khách hàng
    /// </summary>
    public class TenTruongTruyVan
    {
        public string Id { get; set; } = String.Empty;
        public string Ten { get; set; } = String.Empty;
    }
    public class NhanVien
    {
        public string Id { get; set; } = string.Empty;
        public string MaNV { get; set; } = string.Empty;
        public string TenNV { get; set; } = string.Empty;
    }
    public class NhomHang
    {
        public string Id { get; set; } = String.Empty;
        public string TenNhom { get; set; } = String.Empty;
        public string IdNhomCha { get; set; } = String.Empty;

    }

    public class DinhMuc
    {
        public string IdHH { get; set; } = String.Empty;
        public float SoLuong { get; set; }
        public DinhMuc MakeCopy()
        {
            return (DinhMuc)this.MemberwiseClone();
        }
    }
    public class DVTMoRong
    {
        public string TenDonVi { get; set; } = String.Empty;
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

    public class ThuocTinh
    {
        public string Ten { get; set; } = String.Empty;
        public string GiaTri { get; set; } = String.Empty;
        public char KieuGiaTri { get; set; }
        public ThuocTinh MakeCopy()
        {
            return (ThuocTinh)this.MemberwiseClone();
        }
    }

    public class HangHoa
    {
        public string Id { get; set; } = String.Empty;
        public string MaHH { get; set; } = String.Empty;
        public string MaLoai { get; set; } = String.Empty;
        public string TenHH { get; set; } = String.Empty;
        public string Dvt { get; set; } = String.Empty;

        private string _IdNhom = String.Empty;
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

        public string TenNhom { get; set; } = String.Empty;
        public double GiaBan { get; set; }
        public List<DVTMoRong> DVTMoRong { get; set; } = new();
        public bool LaHangBan { get => lf.GetValue(TuDien.HH_LaHangBan); set => lf.SetValue(TuDien.HH_LaHangBan, value); }
        public double GiaVon { get => nf.GetValue(TuDien.HH_GiaVon); set => nf.SetValue(TuDien.HH_GiaVon, value); }
        public double TonKho { get => nf.GetValue(TuDien.HH_TonKho); set => nf.SetValue(TuDien.HH_TonKho, value); }
        public double TonMin { get => nf.GetValue(TuDien.HH_TonMin); set => nf.SetValue(TuDien.HH_TonMin, value); }
        public double TonMax { get => nf.GetValue(TuDien.HH_TonMax); set => nf.SetValue(TuDien.HH_TonMax, value); }
        public List<DinhMuc> LstDinhMuc { get; set; } = new();
        public string HinhAnh { get => tf.GetValue("HinhAnh") ?? string.Empty; set => tf.SetValue("HinhAnh", value); }
        public string MoTa { get => tf.GetValue("MoTa") ?? string.Empty; set => tf.SetValue("MoTa", value); }
        public bool NgungKinhDoanh { get => lf.GetValue("NgungKinhDoanh"); set => lf.SetValue("NgungKinhDoanh", value); }
        public ObjectBeginValue dn { get; set; } = new();
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
                MaHH = this.MaHH,
                TenHH = this.TenHH,
                Dvt = this.Dvt,
                MaLoai = this.MaLoai,
                DVTMoRong = this.DVTMoRong.Select(x => x.MakeCopy()).ToList(),
                LstDinhMuc = this.LstDinhMuc.Select(x => x.MakeCopy()).ToList(),
                tf = this.tf.MakeCopy(),
                nf = this.nf.MakeCopy(),
                df = this.df.MakeCopy(),
                lf = this.lf.MakeCopy(),
                dn = this.dn.MakeCopy()
            };
            return hh;
        }
        public HangHoa MakeCopy()
        {
            var hh = (HangHoa)this.MemberwiseClone();
            hh.DVTMoRong = this.DVTMoRong.Select(x => x.MakeCopy()).ToList();
            hh.LstDinhMuc = this.LstDinhMuc.Select(x => x.MakeCopy()).ToList();
            hh.tf = this.tf.MakeCopy();
            hh.nf = this.nf.MakeCopy();
            hh.df = this.df.MakeCopy();
            hh.lf = this.lf.MakeCopy();
            hh.dn = this.dn.MakeCopy();
            return hh;
        }
        public double GiaTriTonKhoDauNam(string? idKho = null, int nam = 0)
        {
            return dn.Ds.Where(x => string.IsNullOrEmpty(idKho) ? true : x.key.Equals(idKho)
                && x.year.Equals(nam == 0 ? DateTime.Now.Year : nam)).Sum(x => x.value);
        }
        public double SoLuongTonKhoDauNam(string? idKho = null, int nam = 0)
        {
            return dn.Ds.Where(x => string.IsNullOrEmpty(idKho) ? true : x.key.Equals(idKho)
                && x.year.Equals(nam == 0 ? DateTime.Now.Year : nam)).Sum(x => x.value2);
        }
    }

    public class HangHoaCloud
    {
        public string Id { get; set; } = String.Empty;
        public string MaHH { get; set; } = String.Empty;
        public string MaLoai { get; set; } = String.Empty;
        public string TenHH { get; set; } = String.Empty;
        public string Dvt { get; set; } = String.Empty;
        public string IdNhom { get; set; } = String.Empty;
        public List<DVTMoRong> DVTMoRong { get; set; } = new();
        public List<DinhMuc> LstDinhMuc { get; set; } = new();
        public ObjectBeginValue dn { get; set; } = new();
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
                Dvt = this.Dvt,
                MaLoai = this.MaLoai,
                DVTMoRong = this.DVTMoRong.Select(x => x.MakeCopy()).ToList(),
                LstDinhMuc = this.LstDinhMuc.Select(x => x.MakeCopy()).ToList(),
                tf = this.tf.MakeCopy(),
                nf = this.nf.MakeCopy(),
                df = this.df.MakeCopy(),
                lf = this.lf.MakeCopy(),
                dn = this.dn.MakeCopy()
            };
            return hh;
        }
        public HangHoaCloud MakeCopy()
        {
            var hh = (HangHoaCloud)this.MemberwiseClone();
            hh.DVTMoRong = this.DVTMoRong.Select(x => x.MakeCopy()).ToList();
            hh.LstDinhMuc = this.LstDinhMuc.Select(x => x.MakeCopy()).ToList();
            hh.tf = this.tf.MakeCopy();
            hh.nf = this.nf.MakeCopy();
            hh.df = this.df.MakeCopy();
            hh.lf = this.lf.MakeCopy();
            hh.dn = this.dn.MakeCopy();
            return hh;
        }
    }

    public class BangGia
    {
        public string Id { get; set; } = String.Empty;
        public string Ten { get; set; } = String.Empty;
        public bool ApDungToanQuoc { get => lf.GetValue(TuDien.BG_ApDungToanQuoc); set => lf.SetValue(TuDien.BG_ApDungToanQuoc, value); }
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
                DSHangHoaKM = this.DSHangHoaKM.Select(x => x.ToHangHoaKhuyenMaiCloud()).ToList(),
                tf = this.tf.MakeCopy(),
                nf = this.nf.MakeCopy(),
                df = this.df.MakeCopy(),
                lf = this.lf.MakeCopy()
            };
            return bg;
        }
    }
    public class BangGiaCloud
    {
        public string Id { get; set; } = String.Empty;
        public string Ten { get; set; } = String.Empty;
        public List<string> DSChiNhanh { get; set; } = new();
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public bool ApDung { get; set; }
        public List<HangHoaKhuyenMaiCloud> DSHangHoaKM { get; set; } = new();
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
    }
    public class HangHoaKhuyenMai
    {
        private string _IdHH = String.Empty;
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
        public string MaHang { get; set; } = String.Empty;
        public string TenHH { get; set; } = String.Empty;

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
    public class HangHoaKhuyenMaiCloud
    {
        public string IdHH { get; set; } = String.Empty;
        public float GiaVon { get; set; }
        public float GiaNhapCuoi { get; set; }
        public float GiaChung { get; set; }
        public float GiaMoi { get; set; }
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
        public HangHoaKhuyenMai ToHangHoaKhuyenMai()
        {
            var hh = new HangHoaKhuyenMai()
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
    public class DieuKienKH
    {
        private string _IdTT = String.Empty;
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
        public string TenTruong { get; set; } = String.Empty;
        public string ToanTu { get; set; } = String.Empty;
        public float GiaTri { get; set; }
    }
    public class NhomKhach
    {
        public string Id { get; set; } = String.Empty;
        public string TenNhom { get; set; } = String.Empty;
        public float GiamGia { get; set; }
        public bool GiamGiaTrucTiep { get; set; }
        public List<DieuKienKH> DSDieuKien { get; set; } = new();
        public string GhiChu { get; set; } = String.Empty;
    }
    public class KhachHang
    {
        public string Id { get; set; } = String.Empty;
        public string MaKH { get; set; } = String.Empty;
        public string TenKhach { get; set; } = String.Empty;
        private string _IdNhomKhach = String.Empty;

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
        public string TenNhomKhach { get; set; } = String.Empty;
        public bool LaCaNhan { get => lf.GetValue(TuDien.KH_LaCaNhan); set => lf.SetValue(TuDien.KH_LaCaNhan, value); }
        /// <summary>
        /// 0: Ngưới mua 1: Người bán 2: Mua và bán
        /// </summary>
        public int LoaiKhach { get; set; }
        public DateTime NgaySinh { get => df.GetValue(TuDien.NgaySinh); set => df.SetValue(TuDien.NgaySinh, value); }
        public string EMail { get => tf.GetValue(TuDien.EMail); set => tf.SetValue(TuDien.EMail, value); }
        public string DiaChi { get => tf.GetValue(TuDien.DiaChi); set => tf.SetValue(TuDien.DiaChi, value); }
        public string DienThoai { get => tf.GetValue(TuDien.DienThoai); set => tf.SetValue(TuDien.DienThoai, value); }
        public ObjectBeginValue dn { get; set; } = new();
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
                lf = this.lf.MakeCopy(),
                dn = this.dn.MakeCopy()
            };
            return kh;
        }

        public double CongNoDauNam(string? loaiTien = null, int nam = 0)
        {
            return dn.Ds.Where(x => (String.IsNullOrEmpty(loaiTien) ? true : x.key.Equals(loaiTien))
                && x.year.Equals(nam == 0 ? DateTime.Now.Year : nam)).Sum(x => x.value);

        }
        public double NguyenTeDauNam(string loaiTien, int nam = 0)
        {
            return dn.Ds.Where(x => x.key.Equals(loaiTien)
                && x.year.Equals(nam == 0 ? DateTime.Now.Year : nam)).Sum(x => x.value2);

        }

    }

    public class KhachHangCloud
    {
        public string Id { get; set; } = String.Empty;
        public string MaKH { get; set; } = String.Empty;
        public string TenKhach { get; set; } = String.Empty;
        public string IdNhomKhach { get; set; } = String.Empty;
        public ObjectBeginValue dn { get; set; } = new();
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
                lf = this.lf.MakeCopy(),
                dn = this.dn.MakeCopy()
            };
            return kh;
        }
    }

    public class DonHangCT
    {
        private string _IdHH;
        public string IdHH
        {
            get => _IdHH;
            set
            {
                _IdHH = value;
                var hh = DanhmucChung.DSHangHoa.FirstOrDefault(x => x.Equals(_IdHH));
                MaHang = hh == null ? string.Empty : hh.MaHH;
                TenHang = hh == null ? string.Empty : hh.TenHH;
                DonGia = hh == null ? 0 : hh.GiaBan;
            }
        }
        public string MaHang { get; set; } = string.Empty;
        public string TenHang { get; set; } = string.Empty;
        public double SoLuong { get; set; }
        public double DonGia { get; set; }
        public double ThanhTien => SoLuong * DonGia;
        public bool LaHangKM { get; set; }
        public DonHangCTCloud ToDHCTCloud()
        {
            return new DonHangCTCloud()
            {
                IdHH = this.IdHH,
                SoLuong = this.SoLuong,
                DonGia = this.DonGia,
                LaHangKM = this.LaHangKM
            };
        }
        public DonHangCT MakeCopy()
        {
            return (DonHangCT)this.MemberwiseClone();
        }
    }

    public class DonHangCTCloud
    {
        public string IdHH { get; set; } = String.Empty;
        public double SoLuong { get; set; }
        public double DonGia { get; set; }
        public bool LaHangKM { get; set; }
        public DonHangCT ToDHCT()
        {
            return new DonHangCT()
            {
                IdHH = this.IdHH,
                SoLuong = this.SoLuong,
                DonGia = this.DonGia,
                LaHangKM = this.LaHangKM
            };
        }

        public DonHangCTCloud MakeCopy()
        {
            return (DonHangCTCloud)this.MemberwiseClone();
        }
    }

    public class DonHangBan
    {
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// 0: Đang soạn thảo
        /// 1: Đã xác nhận
        /// 2: Đã giao hàng
        /// </summary>
        public int TrangThai { get; set; }
        public string LoaiCT { get; set; }
        public DateTime Ngay { get; set; }
        public string SoPhieu { get; set; } = string.Empty;
        private string _IdKhach = string.Empty;
        public string IdKhach
        {
            get => _IdKhach;
            set
            {
                _IdKhach = value;
                var kh = DanhmucChung.DSKhachHang.FirstOrDefault(x => x.Id.Equals(_IdKhach));
                TenKhach = kh == null ? string.Empty : kh.TenKhach;
                MaKhach = kh == null ? string.Empty : kh.MaKH;
                SDT = kh == null ? string.Empty : kh.DienThoai;
                DiaChi = kh == null ? string.Empty : kh.DiaChi;
            }
        }
        public string MaKhach { get; set; } = string.Empty;
        public string TenKhach { get; set; } = string.Empty;
        public string SDT { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        /// <summary>
        /// Người tạo đơn
        /// </summary>
        private string _IdNV;
        public string IdNV
        {
            get => _IdNV;
            set
            {
                _IdNV = value;
                var nv = DanhmucChung.DSNhanVien.FirstOrDefault(x => x.Id.Equals(_IdNV));
                TenNV = nv == null ? string.Empty : nv.TenNV;
            }
        }

        public string TenNV { get; set; } = string.Empty;
        private string _IdBangGia = string.Empty;
        public string IdBangGia
        {
            get => _IdBangGia;
            set
            {
                _IdBangGia = value;
                var bg = DanhmucChung.DSBangGia.FirstOrDefault(x => x.Equals(_IdBangGia));
                TenBangGia = bg == null ? string.Empty : bg.Ten;
            }
        }
        public string TenBangGia { get; set; } = string.Empty;
        public List<DonHangCT> CTDonHang { get; set; } = new();

        public double TienHang { get; set; }
        public double TienKM { get; set; }
        public double TongTien { get; set; }
        public double DiemThuong { get; set; }
        public string GhiChu { get; set; } = string.Empty;
        public List<TienTrinh> DsTienTrinh { get; set; } = new();
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
        public DonHangBanCloud ToDonHangCloud()
        {
            var dh = new DonHangBanCloud()
            {
                Id = this.Id,
                IdKhach = this.IdKhach,
                IdBangGia = this.IdBangGia,
                IdNV = this.IdNV,
                Ngay = this.Ngay,
                SoPhieu = this.SoPhieu,
                CTDonHang = this.CTDonHang.Select(x => x.ToDHCTCloud()).ToList(),
                TienHang = this.TienHang,
                TienKM = this.TienKM,
                TongTien = this.TongTien,
                TrangThai = this.TrangThai,
                DiemThuong = this.DiemThuong,
                DsTienTrinh = this.DsTienTrinh.Select(x => x.MakeCopy()).ToList(),
                GhiChu = this.GhiChu,
                tf = this.tf.MakeCopy(),
                nf = this.nf.MakeCopy(),
                df = this.df.MakeCopy(),
                lf = this.lf.MakeCopy(),
            };
            return dh;
        }
        public DonHangBan MakeCopy()
        {
            var dh = (DonHangBan)this.MemberwiseClone();
            dh.CTDonHang = this.CTDonHang.Select(x => x.MakeCopy()).ToList();
            dh.tf = this.tf.MakeCopy();
            dh.nf = this.nf.MakeCopy();
            dh.df = this.df.MakeCopy();
            dh.lf = this.lf.MakeCopy();
            return dh;
        }
    }

    public class DonHangBanCloud
    {
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// 0: Đang soạn thảo
        /// 1: Đã xác nhận
        /// 2: Đã giao hàng
        /// </summary>
        public int TrangThai { get; set; }
        public DateTime Ngay { get; set; }
        public string SoPhieu { get; set; } = string.Empty;
        public string IdKhach { get; set; } = string.Empty;
        public string IdNV { get; set; } = string.Empty;
        public string IdBangGia { get; set; } = string.Empty;
        public List<DonHangCTCloud> CTDonHang { get; set; } = new();
        public double TienHang { get; set; }
        public double TienKM { get; set; }
        public double TongTien { get; set; }
        public double DiemThuong { get; set; }
        public string GhiChu { get; set; } = string.Empty;
        public List<TienTrinh> DsTienTrinh { get; set; } = new();
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
        public DonHangBan ToDonHang()
        {
            var dh = new DonHangBan()
            {
                Id = this.Id,
                IdKhach = this.IdKhach,
                IdBangGia = this.IdBangGia,
                IdNV = this.IdNV,
                Ngay = this.Ngay,
                SoPhieu = this.SoPhieu,
                CTDonHang = this.CTDonHang.Select(x => x.ToDHCT()).ToList(),
                TienHang = this.TienHang,
                TienKM = this.TienKM,
                TongTien = this.TongTien,
                TrangThai = this.TrangThai,
                DiemThuong = this.DiemThuong,
                DsTienTrinh = this.DsTienTrinh.Select(x => x.MakeCopy()).ToList(),
                GhiChu = this.GhiChu,
                tf = this.tf.MakeCopy(),
                nf = this.nf.MakeCopy(),
                df = this.df.MakeCopy(),
                lf = this.lf.MakeCopy(),
            };
            return dh;
        }
        public DonHangBanCloud MakeCopy()
        {
            var dh = (DonHangBanCloud)this.MemberwiseClone();
            dh.CTDonHang = this.CTDonHang.Select(x => x.MakeCopy()).ToList();
            dh.tf = this.tf.MakeCopy();
            dh.nf = this.nf.MakeCopy();
            dh.df = this.df.MakeCopy();
            dh.lf = this.lf.MakeCopy();
            return dh;
        }
    }

    public class DonHangMua
    {
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// 0: Đang soạn thảo
        /// 1: Đã xác nhận
        /// 2: Đã giao hàng
        /// </summary>
        public int TrangThai { get; set; }
        public string LoaiCT { get; set; }
        public DateTime Ngay { get; set; }
        public string SoPhieu { get; set; } = string.Empty;
        private string _IdKhach = string.Empty;
        public string IdKhach
        {
            get => _IdKhach;
            set
            {
                _IdKhach = value;
                var kh = DanhmucChung.DSKhachHang.FirstOrDefault(x => x.Id.Equals(_IdKhach));
                TenKhach = kh == null ? string.Empty : kh.TenKhach;
                MaKhach = kh == null ? string.Empty : kh.MaKH;
                SDT = kh == null ? string.Empty : kh.DienThoai;
                DiaChi = kh == null ? string.Empty : kh.DiaChi;
            }
        }
        public string MaKhach { get; set; } = string.Empty;
        public string TenKhach { get; set; } = string.Empty;
        public string SDT { get; set; } = string.Empty;
        public string DiaChi { get; set; } = string.Empty;
        /// <summary>
        /// Người tạo đơn
        /// </summary>
        private string _IdNV;
        public string IdNV
        {
            get => _IdNV;
            set
            {
                _IdNV = value;
                var nv = DanhmucChung.DSNhanVien.FirstOrDefault(x => x.Id.Equals(_IdNV));
                TenNV = nv == null ? string.Empty : nv.TenNV;
            }
        }
        public string TenNV { get; set; } = string.Empty;
        private string _IdBangGia = string.Empty;
        public string IdBangGia
        {
            get => _IdBangGia;
            set
            {
                _IdBangGia = value;
                var bg = DanhmucChung.DSBangGia.FirstOrDefault(x => x.Equals(_IdBangGia));
                TenBangGia = bg == null ? string.Empty : bg.Ten;
            }
        }
        public string TenBangGia { get; set; } = string.Empty;
        public List<DonHangCT> CTDonHang { get; set; } = new();
        public double TienHang { get; set; }
        public double TienKM { get; set; }
        public double TongTien { get; set; }
        public double DiemThuong { get; set; }
        public string GhiChu { get; set; } = string.Empty;
        public List<TienTrinh> DsTienTrinh { get; set; } = new();
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
        public DonHangMuaCloud ToDonHangCloud()
        {
            var dh = new DonHangMuaCloud()
            {
                Id = this.Id,
                IdKhach = this.IdKhach,
                IdBangGia = this.IdBangGia,
                IdNV = this.IdNV,
                Ngay = this.Ngay,
                SoPhieu = this.SoPhieu,
                CTDonHang = this.CTDonHang.Select(x => x.ToDHCTCloud()).ToList(),
                TienHang = this.TienHang,
                TienKM = this.TienKM,
                TongTien = this.TongTien,
                TrangThai = this.TrangThai,
                DiemThuong = this.DiemThuong,
                DsTienTrinh = this.DsTienTrinh.Select(x => x.MakeCopy()).ToList(),
                GhiChu = this.GhiChu,
                tf = this.tf.MakeCopy(),
                nf = this.nf.MakeCopy(),
                df = this.df.MakeCopy(),
                lf = this.lf.MakeCopy(),
            };
            return dh;
        }
        public DonHangMua MakeCopy()
        {
            var dh = (DonHangMua)this.MemberwiseClone();
            dh.CTDonHang = this.CTDonHang.Select(x => x.MakeCopy()).ToList();
            dh.tf = this.tf.MakeCopy();
            dh.nf = this.nf.MakeCopy();
            dh.df = this.df.MakeCopy();
            dh.lf = this.lf.MakeCopy();
            return dh;
        }
    }

    public class DonHangMuaCloud
    {
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// 0: Đang soạn thảo
        /// 1: Đã xác nhận
        /// 2: Đã giao hàng
        /// </summary>
        public int TrangThai { get; set; }
        public DateTime Ngay { get; set; }
        public string SoPhieu { get; set; } = string.Empty;
        public string IdKhach { get; set; } = string.Empty;
        public string IdNV { get; set; } = string.Empty;
        public string IdBangGia { get; set; } = string.Empty;
        public List<DonHangCTCloud> CTDonHang { get; set; } = new();
        public double TienHang { get; set; }
        public double TienKM { get; set; }
        public double TongTien { get; set; }
        public double DiemThuong { get; set; }
        public string GhiChu { get; set; } = string.Empty;
        public List<TienTrinh> DsTienTrinh { get; set; } = new();
        public ObjectExtendProperties<string> tf { get; set; } = new();
        public ObjectExtendProperties<DateTime> df { get; set; } = new();
        public ObjectExtendProperties<double> nf { get; set; } = new();
        public ObjectExtendProperties<bool> lf { get; set; } = new();
        public DonHangMua ToDonHang()
        {
            var dh = new DonHangMua()
            {
                Id = this.Id,
                IdKhach = this.IdKhach,
                IdBangGia = this.IdBangGia,
                IdNV = this.IdNV,
                Ngay = this.Ngay,
                SoPhieu = this.SoPhieu,
                CTDonHang = this.CTDonHang.Select(x => x.ToDHCT()).ToList(),
                TienHang = this.TienHang,
                TienKM = this.TienKM,
                TongTien = this.TongTien,
                TrangThai = this.TrangThai,
                DiemThuong = this.DiemThuong,
                DsTienTrinh = this.DsTienTrinh.Select(x => x.MakeCopy()).ToList(),
                GhiChu = this.GhiChu,
                tf = this.tf.MakeCopy(),
                nf = this.nf.MakeCopy(),
                df = this.df.MakeCopy(),
                lf = this.lf.MakeCopy(),
            };
            return dh;
        }
        public DonHangMuaCloud MakeCopy()
        {
            var dh = (DonHangMuaCloud)this.MemberwiseClone();
            dh.CTDonHang = this.CTDonHang.Select(x => x.MakeCopy()).ToList();
            dh.tf = this.tf.MakeCopy();
            dh.nf = this.nf.MakeCopy();
            dh.df = this.df.MakeCopy();
            dh.lf = this.lf.MakeCopy();
            return dh;
        }
    }
    public class TienTrinh
    {
        public string IdUser { get; set; } = string.Empty;
        public DateTime ThoiGian { get; set; }
        public string NoiDung { get; set; } = string.Empty;

        public int CongViec { get; set; }
        public TienTrinh MakeCopy()
        {
            return (TienTrinh)this.MemberwiseClone();
        }
    }

    public class CTCongNo
    {
        public string SoCT { get; set; }
        public string LoaiCT { get; set; }
        public DateTime Ngay { get; set; }
        public string NoiDung { get; set; }
        public string IdKH { get; set; }
        /// <summary>
        /// Nợ: Mua hàng của người bán, nhận tiền của người mua, nhận hàng trả lại,...
        /// </summary>
        public double PSNo { get; set; }
        /// <summary>
        /// Có: Trả cho người bán, Trả hàng cho người bán, xuất bán,...
        /// </summary>
        public double PSCo { get; set; }
    }
    public class CTNhapXuat
    {
        public string SoCT { get; set; }
        public string LoaiCT { get; set; }
        public DateTime Ngay { get; set; }
        public string NoiDung { get; set; }
        public string IdHH { get; set; }
        public string IdKhoNhap { get; set; }
        public double SLNhap { get; set; }
        public double DGNhap { get; set; }

        public string IdKhoXuat { get; set; }
        public double SLXuat { get; set; }
        public double GTXuat { get; set; }
    }

    //DonHangBan:
    /* BC kinh doanh
     * BC Kho: -> DonHangMua, DonHangBan -> Collection TongHopNhapXuat
     * BC Công nợ
     * 
     * 
     */
}

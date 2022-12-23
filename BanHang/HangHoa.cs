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
    public static class DuLieuBanHang
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
        public static List<TheKho> CTTheKho = new();
        public static List<QuyTienTe> DSQuyTT = new();
        public static List<Kho> DSKho = new();
        public static User user = new();

        public static List<TongHopCongNo> THCNNguoiMua(DateTime TuNgay, DateTime DenNgay)
        {
            List<TongHopCongNo> ttcnnm = new();
            Parallel.ForEach(DSKhachHang.Where(x => x.LoaiKhach == 0).OrderBy(x => x.TenKhach), kh =>
            {
                var ct = CTCongNo.Where(x => x.IdKH.Equals(kh.Id) && x.Ngay < DenNgay);
                var cndk = kh.CongNoDauNam() + ct.Where(x => x.Ngay < TuNgay).Sum(x => x.PSNo - x.PSCo);
                ttcnnm.Add(new TongHopCongNo
                {
                    MaKH = kh.MaKH,
                    TenKH = kh.TenKhach,
                    DKNo = Math.Max(cndk, 0),
                    DKCo = Math.Max(-cndk, 0),
                    PSNo = ct.Where(x => x.Ngay >= TuNgay).Sum(x => x.PSNo),
                    PSCo = ct.Where(x => x.Ngay >= TuNgay).Sum(x => x.PSCo)
                });
            });
            return ttcnnm;
        }
        public static List<TongHopTonKho> THTonKho(DateTime TuNgay, DateTime DenNgay, string IdKho = null)
        {
            List<TongHopTonKho> thtk = new();

            Parallel.ForEach(DSHangHoa, hh =>
            {
                var dstk = CTTheKho.Where(x => x.Ngay <= DenNgay && x.IdHH.Equals(hh.Id));

                lock (thtk) thtk.Add(new TongHopTonKho
                {
                    IdHH = hh.Id,
                    MaHH = hh.MaHH,
                    TenHH = hh.TenHH,
                    DVT = hh.Dvt,
                    SLDK = hh.SoLuongTonKhoDauNam(IdKho) + dstk.Where(x => x.Ngay < TuNgay).Sum(x => (IdKho == null || x.IdKhoNhap.Equals(IdKho) ? 1 : 0) *
                    x.SLNhap - (IdKho == null || x.IdKhoXuat.Equals(IdKho) ? 1 : 0) * x.SLXuat),
                    GTDK = hh.GiaTriTonKhoDauNam(IdKho) + dstk.Where(x => x.Ngay < TuNgay).Sum(x => (IdKho == null || x.IdKhoNhap.Equals(IdKho) ? 1 : 0) *
                    x.SLNhap * x.DonGiaVon - (IdKho == null || x.IdKhoXuat.Equals(IdKho) ? 1 : 0) * x.SLXuat * x.DonGiaVon),
                    SLNhap = dstk.Where(x => x.Ngay >= TuNgay).Sum(x => (IdKho == null || x.IdKhoNhap.Equals(IdKho) ? 1 : 0) * x.SLNhap -
                    (IdKho == null || x.IdKhoXuat.Equals(IdKho) ? 1 : 0) * x.SLXuat),
                    GTNhap = dstk.Where(x => x.Ngay >= TuNgay).Sum(x => (IdKho == null || x.IdKhoNhap.Equals(IdKho) ? 1 : 0) *
                    x.SLNhap * x.DonGiaVon - (IdKho == null || x.IdKhoXuat.Equals(IdKho) ? 1 : 0) * x.SLXuat * x.DonGiaVon),
                    SLXuat = dstk.Where(x => x.Ngay >= TuNgay).Sum(x => (IdKho == null || x.IdKhoNhap.Equals(IdKho) ? 1 : 0) * x.SLNhap -
                    (IdKho == null || x.IdKhoXuat.Equals(IdKho) ? 1 : 0) * x.SLXuat),
                    GTXuat = dstk.Where(x => x.Ngay >= TuNgay).Sum(x => (IdKho == null || x.IdKhoNhap.Equals(IdKho) ? 1 : 0) *
                    x.SLNhap * x.DonGiaVon - (IdKho == null || x.IdKhoXuat.Equals(IdKho) ? 1 : 0) * x.SLXuat * x.DonGiaVon),
                });
            });

            return thtk;
        }
    }

    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }

    public class Kho
    {
        public string Id { get; set; }
        public string TenKho { get; set; }
        public string DiaChi { get; set; }
    }
    public class CTCongNo
    {
        public string IdCT { get; set; }
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

    public class TheKho
    {
        public string IdCT { get; set; }
        public string SoCT { get; set; }
        public string LoaiCT { get; set; }
        public DateTime Ngay { get; set; }
        private string _IdHH;
        public string IdHH
        {
            get => _IdHH;
            set
            {
                _IdHH = value;
                var hh = DuLieuBanHang.DSHangHoa.FirstOrDefault(x => x.Id.Equals(_IdHH));
                MaHH = hh == null ? string.Empty : hh.MaHH;
                TenHH = hh == null ? string.Empty : hh.TenHH;
            }
        }
        public string MaHH { get; set; }
        public string TenHH { get; set; }
        public double SoLuong { get; set; }
        public double DonGia { get; set; }
        public double DonGiaVon { get; set; }
        public double ThanhTien => SoLuong * DonGia;
        public string IdKhoNhap { get; set; }
        public double SLNhap { get; set; }
        public double GTNhap => SLNhap * DonGiaVon;
        public string IdKhoXuat { get; set; }
        public double SLXuat { get; set; }
        public double GTXuat => SLXuat * DonGiaVon;


    }
    public class TheKhoCloud
    {
        public string Id { get; set; }
        public string SoCT { get; set; }
        public string LoaiCT { get; set; }
        public DateTime Ngay { get; set; }
        public string IdHH { get; set; }

        public double SoLuong { get; set; }
        public double DonGia { get; set; }
        public double DonGiaVon { get; set; }
        public string IdKhoNhap { get; set; }
        public double SLNhap { get; set; }
        public string IdKhoXuat { get; set; }
        public double SLXuat { get; set; }

    }
    /// <summary>
    /// Đơn giá vốn
    /// TH1: Giá bình quân gia quyền - Tính theo tháng
    ///  = (Giá trị tồn đầu + giá trị nhập)/(SL tồn dầu + SL nhập)
    /// TH2: Giá theo lô
    ///  Phái có mã lô hàng khi nhập và xuất
    ///  Giá vốn = đơn giá của lô đó
    /// TH3: Giá bình quân liên tiếp - Mõi lần nhập giá vốn thay đổi
    /// </summary>
    public class TongHopTonKho
    {
        public string IdHH { get; set; }
        public string MaHH { get; set; }
        public string TenHH { get; set; }
        public string DVT { get; set; }
        public double SLDK { get; set; }
        public double GTDK { get; set; }
        public double SLNhap { get; set; }
        public double GTNhap { get; set; }
        public double SLXuat { get; set; }
        public double GTXuat { get; set; }
        public double SLCK => SLDK + SLNhap - SLXuat;
        public double GTCK => GTDK + GTNhap - GTXuat;
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
        public string Id { get; set; } = string.Empty;
        public string TenNhom { get; set; } = string.Empty;
        public string IdNhomCha { get; set; } = string.Empty;

    }

    public class DinhMuc
    {
        public string IdHH { get; set; } = string.Empty;
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



    public class HangHoa : ObjectExtends
    {
        public string Id { get; set; } = String.Empty;
        public string MaHH { get; set; } = String.Empty;

        public string TenHH { get; set; } = String.Empty;
        public string Dvt { get; set; } = String.Empty;

        private string _IdNhom = String.Empty;
        public string IdNhom
        {
            get => _IdNhom;
            set
            {
                _IdNhom = value;
                var nh = DuLieuBanHang.DSNhomHang.FirstOrDefault(x => x.Id.Equals(_IdNhom));
                TenNhom = nh == null ? string.Empty : nh.TenNhom;
            }
        }
        public string MaLoai { get; set; } = String.Empty;
        public string TenNhom { get; set; } = String.Empty;
        public double GiaBan { get; set; }
        public List<DVTMoRong> DVTMoRong { get; set; } = new();
        public bool LaHangBan { get => GetLogicField(TuDien.HH_LaHangBan); set => SetLogicField(TuDien.HH_LaHangBan, value); }
        public double GiaVon { get => GetNumberField(TuDien.HH_GiaVon); set => SetNumberField(TuDien.HH_GiaVon, value); }
        public double TonKho { get => GetNumberField(TuDien.HH_TonKho); set => SetNumberField(TuDien.HH_TonKho, value); }
        public double TonMin { get => GetNumberField(TuDien.HH_TonMin); set => SetNumberField(TuDien.HH_TonMin, value); }
        public double TonMax { get => GetNumberField(TuDien.HH_TonMax); set => SetNumberField(TuDien.HH_TonMax, value); }
        public List<DinhMuc> LstDinhMuc { get; set; } = new();
        public string HinhAnh { get => GetTextField("HinhAnh") ?? string.Empty; set => SetTextField("HinhAnh", value); }
        public string MoTa { get => GetTextField("MoTa") ?? string.Empty; set => SetTextField("MoTa", value); }
        public bool NgungKinhDoanh { get => GetLogicField("NgungKinhDoanh"); set => SetLogicField("NgungKinhDoanh", value); }
        public ObjectBeginValue dn { get; set; } = new();

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
                dn = this.dn.MakeCopy()
            };
            hh.CopySource(this);
            return hh;
        }
        public HangHoa MakeCopy()
        {
            var hh = (HangHoa)this.MemberwiseClone();
            hh.DVTMoRong = this.DVTMoRong.Select(x => x.MakeCopy()).ToList();
            hh.LstDinhMuc = this.LstDinhMuc.Select(x => x.MakeCopy()).ToList();
            hh.dn = this.dn.MakeCopy();
            hh.CopySource(this);
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

    public class HangHoaCloud : ObjectExtends
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
                dn = this.dn.MakeCopy(),
            };
            hh.CopySource(this);
            return hh;
        }
        public HangHoaCloud MakeCopy()
        {
            var hh = (HangHoaCloud)this.MemberwiseClone();
            hh.DVTMoRong = this.DVTMoRong.Select(x => x.MakeCopy()).ToList();
            hh.LstDinhMuc = this.LstDinhMuc.Select(x => x.MakeCopy()).ToList();
            hh.dn = this.dn.MakeCopy();
            hh.CopySource(this);
            return hh;
        }
    }

    public class BangGia : ObjectExtends
    {
        public string Id { get; set; } = String.Empty;
        public string Ten { get; set; } = String.Empty;
        public bool ApDungToanQuoc { get => GetLogicField(TuDien.BG_ApDungToanQuoc); set => SetLogicField(TuDien.BG_ApDungToanQuoc, value); }
        public List<string> DSChiNhanh { get; set; } = new();
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public List<HangHoaKhuyenMai> DSHangHoaKM { get; set; } = new();
        public bool ApDung { get; set; }

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
            };
            bg.CopySource(this);
            return bg;
        }
    }
    public class BangGiaCloud : ObjectExtends
    {
        public string Id { get; set; } = String.Empty;
        public string Ten { get; set; } = String.Empty;
        public List<string> DSChiNhanh { get; set; } = new();
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public bool ApDung { get; set; }
        public List<HangHoaKhuyenMaiCloud> DSHangHoaKM { get; set; } = new();

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
                var hh = DuLieuBanHang.DSHangHoa.FirstOrDefault(x => x.Id.Equals(_IdHH));
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
        public HangHoaKhuyenMaiCloud ToHangHoaKhuyenMaiCloud()
        {
            var hh = new HangHoaKhuyenMaiCloud()
            {
                IdHH = this.IdHH,
                GiaVon = this.GiaVon,
                GiaChung = this.GiaChung,
                GiaMoi = this.GiaMoi,
                GiaNhapCuoi = this.GiaNhapCuoi,
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
        public HangHoaKhuyenMai ToHangHoaKhuyenMai()
        {
            var hh = new HangHoaKhuyenMai()
            {
                IdHH = this.IdHH,
                GiaVon = this.GiaVon,
                GiaChung = this.GiaChung,
                GiaMoi = this.GiaMoi,
                GiaNhapCuoi = this.GiaNhapCuoi,
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
                var TT = DuLieuBanHang.ListTenTruong.FirstOrDefault(x => x.Id.Equals(_IdTT));
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
    public class KhachHang : ObjectExtends
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
                var nk = DuLieuBanHang.DSNhomKhach.FirstOrDefault(x => x.Id == _IdNhomKhach);
                TenNhomKhach = nk == null ? string.Empty : nk.TenNhom;
            }
        }
        public string TenNhomKhach { get; set; } = String.Empty;
        public bool LaCaNhan { get => GetLogicField(TuDien.KH_LaCaNhan); set => SetLogicField(TuDien.KH_LaCaNhan, value); }
        /// <summary>
        /// 0: Ngưới mua 1: Người bán 2: Mua và bán
        /// </summary>
        public int LoaiKhach { get; set; }
        public DateTime NgaySinh { get => GetDateField(TuDien.NgaySinh); set => SetDateField(TuDien.NgaySinh, value); }
        public string EMail { get => GetTextField(TuDien.EMail); set => SetTextField(TuDien.EMail, value); }
        public string DiaChi { get => GetTextField(TuDien.DiaChi); set => SetTextField(TuDien.DiaChi, value); }
        public string DienThoai { get => GetTextField(TuDien.DienThoai); set => SetTextField(TuDien.DienThoai, value); }
        public ObjectBeginValue dn { get; set; } = new();

        public KhachHangCloud ToKhachHangCloud()
        {
            var kh = new KhachHangCloud()
            {
                Id = this.Id,
                MaKH = this.MaKH,
                TenKhach = this.TenKhach,
                IdNhomKhach = this.IdNhomKhach,
                dn = this.dn.MakeCopy()
            };
            kh.CopySource(this);
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

    public class KhachHangCloud : ObjectExtends
    {
        public string Id { get; set; } = String.Empty;
        public string MaKH { get; set; } = String.Empty;
        public string TenKhach { get; set; } = String.Empty;
        public string IdNhomKhach { get; set; } = String.Empty;
        public ObjectBeginValue dn { get; set; } = new();
        public KhachHang ToKhachHang()
        {
            var kh = new KhachHang()
            {
                Id = this.Id,
                MaKH = this.MaKH,
                TenKhach = this.TenKhach,
                IdNhomKhach = this.IdNhomKhach,
            };
            kh.CopySource(this);
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
                var hh = DuLieuBanHang.DSHangHoa.FirstOrDefault(x => x.Equals(_IdHH));
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

    public class DonHangBan : ObjectExtends
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
                var kh = DuLieuBanHang.DSKhachHang.FirstOrDefault(x => x.Id.Equals(_IdKhach));
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
                var nv = DuLieuBanHang.DSNhanVien.FirstOrDefault(x => x.Id.Equals(_IdNV));
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
                var bg = DuLieuBanHang.DSBangGia.FirstOrDefault(x => x.Equals(_IdBangGia));
                TenBangGia = bg == null ? string.Empty : bg.Ten;
            }
        }
        public string TenBangGia { get; set; } = string.Empty;
        public string _IdKho;
        public string IdKho
        {
            get => _IdKho;
            set
            {
                _IdKho = value;
                var kho = DuLieuBanHang.DSKho.FirstOrDefault(x => x.Id.Equals(_IdKho));
                TenKho = kho == null ? string.Empty : kho.TenKho;
            }
        }
        public string TenKho { get; set; }
        public List<DonHangCT> CTDonHang { get; set; } = new();

        public double TienHang { get; set; }
        public double TienKM { get; set; }
        public double TongTien { get; set; }
        public double DiemThuong { get; set; }
        public string GhiChu { get; set; } = string.Empty;
        public List<TienTrinh> DsTienTrinh { get; set; } = new();

        public DonHangCloud ToDonHangCloud()
        {
            var dh = new DonHangCloud()
            {
                Id = this.Id,
                LoaiCT = LoaiCT,
                IdKhach = this.IdKhach,
                IdBangGia = this.IdBangGia,
                IdKho = this.IdKho,
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
            };
            dh.CopySource(this);
            return dh;
        }
        public DonHangBan MakeCopy()
        {
            var dh = (DonHangBan)this.MemberwiseClone();
            dh.CTDonHang = this.CTDonHang.Select(x => x.MakeCopy()).ToList();
            dh.CopySource(this);
            return dh;
        }
        public List<TheKho> ToTheKho()
        {
            List<TheKho> ds = new();
            ds = CTDonHang.Select(ct => new TheKho
            {
                IdHH = ct.IdHH,
                LoaiCT = this.LoaiCT,
                SoCT = this.SoPhieu,
                Ngay = this.Ngay,
                DonGia = ct.DonGia,
                DonGiaVon = ct.DonGia,
                IdKhoXuat = this.IdKho,
                SLXuat = ct.SoLuong,
            }).ToList();

            return ds;
        }
        public CTCongNo ToCTCongNo()
        {
            return new CTCongNo
            {
                LoaiCT = this.LoaiCT,
                Ngay = this.Ngay,
                IdKH = this.IdKhach,
                SoCT = this.SoPhieu,
                NoiDung = this.GhiChu,
                PSCo = this.TongTien
            };
        }

    }
    public class DonHangMua : ObjectExtends
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
                var kh = DuLieuBanHang.DSKhachHang.FirstOrDefault(x => x.Id.Equals(_IdKhach));
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
                var nv = DuLieuBanHang.DSNhanVien.FirstOrDefault(x => x.Id.Equals(_IdNV));
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
                var bg = DuLieuBanHang.DSBangGia.FirstOrDefault(x => x.Equals(_IdBangGia));
                TenBangGia = bg == null ? string.Empty : bg.Ten;
            }
        }
        public string TenBangGia { get; set; } = string.Empty;
        public string _IdKho;
        public string IdKho
        {
            get => _IdKho;
            set
            {
                _IdKho = value;
                var kho = DuLieuBanHang.DSKho.FirstOrDefault(x => x.Id.Equals(_IdKho));
                TenKho = kho == null ? string.Empty : kho.TenKho;
            }
        }
        public string TenKho { get; set; }
        public List<DonHangCT> CTDonHang { get; set; } = new();
        public double TienHang { get; set; }
        public double TienKM { get; set; }
        public double TongTien { get; set; }
        public double DiemThuong { get; set; }
        public string GhiChu { get; set; } = string.Empty;
        public List<TienTrinh> DsTienTrinh { get; set; } = new();

        public DonHangCloud ToDonHangCloud()
        {
            var dh = new DonHangCloud()
            {
                Id = this.Id,
                LoaiCT = LoaiCT,
                IdKhach = this.IdKhach,
                IdBangGia = this.IdBangGia,
                IdKho = this.IdKho,
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
            };
            dh.CopySource(this);
            return dh;
        }
        public DonHangMua MakeCopy()
        {
            var dh = (DonHangMua)this.MemberwiseClone();
            dh.CTDonHang = this.CTDonHang.Select(x => x.MakeCopy()).ToList();
            dh.CopySource(this);
            return dh;
        }
        public List<TheKho> ToTheKho()
        {
            List<TheKho> ds = new();
            ds = CTDonHang.Select(ct => new TheKho
            {
                IdHH = ct.IdHH,
                LoaiCT = this.LoaiCT,
                SoCT = this.SoPhieu,
                Ngay = this.Ngay,
                DonGia = ct.DonGia,
                DonGiaVon = ct.DonGia,
                IdKhoNhap = this.IdKho,
                SLNhap = ct.SoLuong,
            }).ToList();

            return ds;
        }
        public CTCongNo ToCTCongNo()
        {
            return new CTCongNo
            {
                LoaiCT = this.LoaiCT,
                Ngay = this.Ngay,
                IdKH = this.IdKhach,
                SoCT = this.SoPhieu,
                NoiDung = this.GhiChu,
                PSNo = this.TongTien
            };
        }
    }

    public class DonHangCloud : ObjectExtends
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
        public string IdKhach { get; set; } = string.Empty;
        public string IdNV { get; set; } = string.Empty;
        public string IdBangGia { get; set; } = string.Empty;
        public List<DonHangCTCloud> CTDonHang { get; set; } = new();
        public double TienHang { get; set; }
        public double TienKM { get; set; }
        public double TongTien { get; set; }
        public double DiemThuong { get; set; }
        public string IdKho { get; set; }
        public string GhiChu { get; set; } = string.Empty;
        public List<TienTrinh> DsTienTrinh { get; set; } = new();
        public DonHangMua ToDonHangMua()
        {
            var dh = new DonHangMua()
            {
                Id = this.Id,
                LoaiCT = this.LoaiCT,
                IdKhach = this.IdKhach,
                IdBangGia = this.IdBangGia,
                IdKho = this.IdKho,
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
            };
            dh.CopySource(this);
            return dh;
        }
        public DonHangBan ToDonHangBan()
        {
            var dh = new DonHangBan()
            {
                Id = this.Id,
                LoaiCT = this.LoaiCT,
                IdKhach = this.IdKhach,
                IdBangGia = this.IdBangGia,
                IdKho = this.IdKho,
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
            };
            dh.CopySource(this);
            return dh;
        }
        public DonHangCloud MakeCopy()
        {
            var dh = (DonHangCloud)this.MemberwiseClone();
            dh.CTDonHang = this.CTDonHang.Select(x => x.MakeCopy()).ToList();
            dh.CopySource(this);
            return dh;
        }
        public async Task UpdateToCloud()
        {
            CapNhatCongNo();
            CapNhatTheKho();
        }

        private void CapNhatTheKho()
        {
            List<TheKho> ds = CTDonHang.Select(ct => new TheKho
            {
                IdCT = this.Id,
                IdHH = ct.IdHH,
                LoaiCT = this.LoaiCT,
                SoCT = this.SoPhieu,
                Ngay = this.Ngay,
                DonGia = ct.DonGia,
                DonGiaVon = ct.DonGia,
                SLNhap = this.LoaiCT.StartsWith("N") ? ct.SoLuong : 0,
                IdKhoNhap = this.LoaiCT.StartsWith("N") ? IdKho : string.Empty,
                SLXuat = this.LoaiCT.StartsWith("X") ? ct.SoLuong : 0,
                IdKhoXuat = this.LoaiCT.StartsWith("X") ? IdKho : string.Empty,
            }).ToList();
            DuLieuBanHang.CTTheKho.AddRange(ds);
        }

        private void CapNhatCongNo()
        {
            CTCongNo ct = new()
            {
                IdCT = this.Id,
                SoCT = this.SoPhieu,
                IdKH = this.IdKhach,
                LoaiCT = this.LoaiCT,
                Ngay = this.Ngay,
                PSNo = LoaiCT.StartsWith("X") ? this.TongTien : 0,
                PSCo = LoaiCT.StartsWith("N") ? this.TongTien : 0,
                NoiDung = this.GhiChu,
            };
            DuLieuBanHang.CTCongNo.Add(ct);
        }

        public async Task DeleteFromCloud()
        {
            HuyBoCongNo();
            HuyBoTheKho();
        }

        private void HuyBoTheKho()
        {
            DuLieuBanHang.CTTheKho.RemoveAll(x => x.IdCT.Equals(this.Id));
        }

        private void HuyBoCongNo()
        {
            var cn = DuLieuBanHang.CTCongNo.FirstOrDefault(x => x.IdCT.Equals(this.Id));
            if (cn != null)
                DuLieuBanHang.CTCongNo.Remove(cn);
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

    public class QuyTienTe
    {
        public string Id { get; set; }
        public string Ma { get; set; }
        public string Ten { get; set; }
        /// <summary>
        /// 0: Tien Mat
        /// 1: Tien Gui
        /// </summary>
        public int LoaiQuy { get; set; }
        public string ThongTinQuy { get; set; }
    }

    public class CTTienTe
    {
        public string Id { get; set; }
        public string SoPhieu { get; set; }
        public string LoaiCT { get; set; }
        public string IdCTLQ { get; set; }
        public DateTime Ngay { get; set; }
        private string _IdKhach;
        public string IdKhach
        {
            get => _IdKhach;
            set
            {
                _IdKhach = value;
                var kh = DuLieuBanHang.DSKhachHang.FirstOrDefault(x => x.Id.Equals(_IdKhach));
                TenKhach = kh == null ? string.Empty : kh.TenKhach;
                DiaChi = kh == null ? string.Empty : kh.DiaChi;
            }
        }
        public string TenKhach { get; set; }
        public string DiaChi { get; set; }
        public string DienGiai { get; set; }
        public double SoTien { get; set; }
        private string _IdQuy;
        public string IdQuy
        {
            get => _IdQuy;
            set
            {
                _IdQuy = value;
                var quy = DuLieuBanHang.DSQuyTT.FirstOrDefault(x => x.Id.Equals(_IdQuy));
                TenQuy = quy == null ? string.Empty : quy.Ten;
            }
        }
        public string TenQuy { get; set; }
        public string IdUser { get; set; }

        public CTCongNo ToCTCongNo()
        {
            return new CTCongNo
            {
                IdCT = this.Id,
                IdKH = this.IdKhach,
                LoaiCT = this.LoaiCT,
                Ngay = this.Ngay,
                SoCT = this.SoPhieu,
                NoiDung = this.DienGiai,
                PSCo = this.LoaiCT.StartsWith("T") ? this.SoTien : 0,
                PSNo = this.LoaiCT.StartsWith("C") ? this.SoTien : 0
            };
        }
        public CTTienTeCloud ToCTTienTeuCloud()
        {
            CTTienTeCloud p = new CTTienTeCloud
            {
                IdKhach = this.IdKhach,
                SoPhieu = this.SoPhieu,
                LoaiCT = this.LoaiCT,
                IdCTLQ = this.IdCTLQ,
                Ngay = this.Ngay,
                DienGiai = this.DienGiai,
                IdQuy = this.IdQuy,
                SoTien = this.SoTien,
                IdUser = this.IdUser
            };
            return p;
        }
        public CTTienTe MakeCopy()
        {
            return (CTTienTe)this.MemberwiseClone();
        }
    }
    public class CTTienTeCloud
    {
        public string Id { get; set; }
        public string SoPhieu { get; set; }
        public string LoaiCT { get; set; }
        public string IdCTLQ { get; set; }
        public DateTime Ngay { get; set; }
        public string IdKhach { get; set; }
        public string DienGiai { get; set; }
        public double SoTien { get; set; }
        public string IdQuy { get; set; }
        public string IdUser { get; set; }

        public CTCongNo ToCTCongNo()
        {
            return new CTCongNo
            {
                IdCT = this.IdCTLQ,
                IdKH = this.IdKhach,
                LoaiCT = this.LoaiCT,
                Ngay = this.Ngay,
                SoCT = this.SoPhieu,
                NoiDung = this.DienGiai,
                PSCo = this.LoaiCT.StartsWith("T") ? this.SoTien : 0,
                PSNo = this.LoaiCT.StartsWith("C") ? this.SoTien : 0
            };
        }
        public CTTienTe ToCTTienTe()
        {
            CTTienTe p = new CTTienTe
            {
                IdKhach = this.IdKhach,
                SoPhieu = this.SoPhieu,
                LoaiCT = this.LoaiCT,
                IdCTLQ = this.IdCTLQ,
                Ngay = this.Ngay,
                DienGiai = this.DienGiai,
                IdQuy = this.IdQuy,
                SoTien = this.SoTien,
                IdUser = this.IdUser,

            };
            return p;
        }
        public CTTienTeCloud MakeCopy()
        {
            return (CTTienTeCloud)this.MemberwiseClone();
        }
        public async Task UpdateToCloud()
        {
            //CapNhatCongNo();

        }
        public async Task DeleteFromCloud()
        {
            //HuyBoCongNo();

        }
    }

    public class DanhMucPhuongThuc
    {
        public string LoaiCT { get; set; }
        public string Ten { get; set; }
        /// <summary>
        /// 0: Khong theo doi CP
        /// 1: Co theo doi CP
        /// </summary>
        public int LaDTCP { get; set; }
        public bool CoSuDung { get; set; }
    }

    //DonHangBan:
    /* BC kinh doanh
     * BC Kho: -> DonHangMua, DonHangBan -> Collection TongHopNhapXuat
     * BC Công nợ
     * 
     * 
     */
}

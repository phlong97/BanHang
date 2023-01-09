using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BanHang
{
    public static class DuLieuBanHang
    {
        public static bool DaLapSo = false;
        public static bool SystemBusy = false;
        public static List<SoCai> SoCaiTongHop = new();
        public static List<TheKho> SoKhoTongHop = new();
        public static List<NhomHang> DSNhomHang = new();
        public static List<HangHoa> DSHangHoa = new();
        public static List<BangGia> DSBangGia = new();
        public static List<NhomKhach> DSNhomKhach = new();
        public static List<KhachHang> DSKhachHang = new();
        public static List<NhanVien> DSNhanVien = new();
        public static List<string> ListToanTu = new() { "<", "<=", "=", ">", ">=" };
        public static List<TenTruongTruyVan> ListTenTruong = new();
        public static List<GiaVon_BTV> BangGiaVon = new();
        public static List<CTTienTeCloud> CTTienTe = new();
        public static List<QuyTienTe> DSQuyTT = new();
        public static List<Kho> DSKho = new();
        public static List<DonHangCloud> DSDonHang = new();
        public static User user = new();

        public static double LayGiaVon(string IdHH, int thang)
        {
            return BangGiaVon.FirstOrDefault(x => x.IdHH.Equals(IdHH))?.GetGiaVon(thang) ?? 0;
        }
        public static async Task TaoSoCai()
        {
            if (SystemBusy) return;
            SystemBusy = true;
            await Task.Run(() =>
            {
                SoCaiTongHop.AddRange(DuLieuBanHang.DSDonHang.Select(x => x.ToSoCai()));
                SoCaiTongHop.AddRange(DuLieuBanHang.CTTienTe.Select(x => x.ToSoCai()));
            });
            SystemBusy = false;
        }
        public static List<TongHopCongNo> THCongNo(DateTime TuNgay, DateTime DenNgay, int LoaiKhach)
        {
            List<TongHopCongNo> ttcnnm = new();
            Parallel.ForEach(DSKhachHang.Where(x => x.LoaiKhach == LoaiKhach).OrderBy(x => x.TenKhach), kh =>
            {
                var ct = SoCaiTongHop.Where(x => x.IdKhach.Equals(kh.Id) && x.Ngay < DenNgay);
                var cndk = kh.CongNoDauNam() + ct.Where(x => x.Ngay < TuNgay).Sum(x => x.No - x.Co);
                lock (ttcnnm)
                    ttcnnm.Add(new TongHopCongNo
                    {
                        MaKH = kh.MaKH,
                        TenKH = kh.TenKhach,
                        DKNo = Math.Max(cndk, 0),
                        DKCo = Math.Max(-cndk, 0),
                        PSNo = ct.Where(x => x.Ngay >= TuNgay).Sum(x => x.No),
                        PSCo = ct.Where(x => x.Ngay >= TuNgay).Sum(x => x.Co)
                    });
            });
            return ttcnnm;
        }
        public static List<TongHopTonKho> THTonKho(DateTime TuNgay, DateTime DenNgay, string IdKho = null)
        {
            List<TongHopTonKho> thtk = new();

            Parallel.ForEach(DSHangHoa, hh =>
            {
                var dstk = SoKhoTongHop.Where(x => x.Ngay <= DenNgay && x.IdHH.Equals(hh.Id));

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
        public static List<TongHopTonQuy> THTonQuy(DateTime TuNgay, DateTime DenNgay, string LoaiQuy = null)
        {
            List<TongHopTonQuy> thtq = new();
            Parallel.ForEach(DSQuyTT.Where(x => string.IsNullOrEmpty(LoaiQuy) ? true : x.LoaiQuy.Equals(LoaiQuy)), quy =>
            {
                var ct = SoCaiTongHop.Where(x => x.IdQuy.Equals(quy.Id) && x.Ngay < DenNgay);

                lock (thtq)
                    thtq.Add(new TongHopTonQuy
                    {
                        IdQuy = quy.Id,
                        MaQuy = quy.Ma,
                        TenQuy = quy.Ten,
                        TonDauKy = ct.Where(x => x.Ngay < TuNgay).Sum(x => x.No - x.Co),
                        TongThu = ct.Where(x => x.Ngay >= TuNgay).Sum(x => x.No),
                        TongChi = ct.Where(x => x.Ngay >= TuNgay).Sum(x => x.Co)
                    });
            });

            return thtq;
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
    public class SoCai
    {
        public string SoPhieu { get; set; }
        public string LoaiCT { get; set; }
        public DateTime Ngay { get; set; }
        public string DienGiai { get; set; }
        /// <summary>
        /// DT Quy:Q -DT PhapNhan:PN -DT DoanhThu: -DT ChiPhi -DT Thue
        /// </summary>
        public string LoaiDTNo { get; set; }
        public string IdNo { get; set; }
        public string LoaiDTCo { get; set; }
        public string IdCo { get; set; }
        public double SoTien { get; set; }

        //Xuat ban:
        //- Doanh Thu: Idno ; khach ; idco: ma doanh thu
        //- Chi Phí - giá vốn: idno: ma gia von (ma rieng); idco: hang ton kho
        //- Thuế: idno: ma khach; idco: thuế (mã riêng)

        //Mua ngoai:
        //- Tien hang: idco: khach; idno: hang ton kho (ma rieng)
        //- Chi phi:  idco: khach; idno: chi phi mua hang (ma rieng)
        //- Thue: idco: khach; idno: thue (ma rieng)
    }
    public class TongHopTonQuy
    {
        public string IdQuy { get; set; }
        public string MaQuy { get; set; }
        public string TenQuy { get; set; }
        public double TonDauKy { get; set; }
        public double TongThu { get; set; }
        public double TongChi { get; set; }
        public double TonCuoiKy => TonDauKy + TongThu - TongChi;
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


    public class GiaVon_BTV
    {
        public string IdHH { get; set; }

        public Dictionary<int, double> GiaVon = new();

        public double GetGiaVon(int thang) => GiaVon.ContainsKey(thang) ? GiaVon[thang] : 0;

        public void SetGiaVon(int thang, double giaVon)
        {
            if (GiaVon.ContainsKey(thang)) GiaVon[thang] = giaVon;
            else GiaVon.Add(thang, giaVon);
        }

        public DateTime NgayDauThang(int thang) => new DateTime(DateTime.Now.Year, thang, 1);
        public DateTime NgayCuoiThang(int thang) => new DateTime(DateTime.Now.Year, thang, DateTime.DaysInMonth(DateTime.Now.Year, thang));

        public void UpdateGiaVon(int thang)
        {
            var soLuongTonTruoc = DuLieuBanHang.SoKhoTongHop.Where(x => x.Ngay < NgayDauThang(thang) && x.IdHH.Equals(IdHH)).Sum(x => x.SLNhap - x.SLXuat) +
                DuLieuBanHang.DSHangHoa.FirstOrDefault(x => x.Id.Equals(IdHH))?.SoLuongTonKhoDauNam() ?? 0;
            var giaTriTonTruoc = DuLieuBanHang.SoKhoTongHop.Where(x => x.Ngay < NgayDauThang(thang) && x.IdHH.Equals(IdHH)).Sum(x => (x.SLNhap - x.SLXuat) * x.DonGiaVon) +
                DuLieuBanHang.DSHangHoa.FirstOrDefault(x => x.Id.Equals(IdHH))?.GiaTriTonKhoDauNam() ?? 0;
            for (int i = thang; i <= 12; i++)
            {
                var listNhap = DuLieuBanHang.SoKhoTongHop.Where(x => x.Ngay >= NgayDauThang(thang) && x.Ngay <= NgayCuoiThang(thang) && x.IdHH.Equals(IdHH)).ToList();
                Parallel.ForEach(listNhap, tk => tk.DonGiaVon = tk.DonGia);

                var soLuongNhap = listNhap.Sum(x => x.SLNhap);
                var giaTriNhap = listNhap.Sum(x => x.SLNhap * x.DonGia);
                var giaVon = (soLuongTonTruoc + soLuongNhap) == 0 ? 0 : (giaTriTonTruoc + giaTriNhap) / (soLuongTonTruoc + soLuongNhap);
                SetGiaVon(i, giaVon);
                var listXuat = DuLieuBanHang.SoKhoTongHop.Where(x => x.Ngay >= NgayDauThang(thang) && x.Ngay <= NgayCuoiThang(thang) && x.IdHH.Equals(IdHH)).ToList();


                var soLuongXuat = listXuat.Sum(x => x.SLXuat);

                soLuongTonTruoc += soLuongNhap - soLuongXuat;
                giaTriTonTruoc += giaTriNhap - soLuongXuat * giaVon;


                Parallel.ForEach(listXuat, tk => tk.DonGiaVon = giaVon);

            }
        }
    }
    public class GiaVon
    {
        public string Id { get; set; }
        public double Gia { get; set; }
        public double SoLuongTon { get; set; }
        public DateTime NgayThayDoi { get; set; }

        public GiaVon MakeCopy()
        {
            return (GiaVon)this.MemberwiseClone();
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
        public bool LaHangBan { get => GetLogicField(TuDien.HangHoa.LaHangBan); set => SetLogicField(TuDien.HangHoa.LaHangBan, value); }
        public double TonKho { get => GetNumberField(TuDien.HangHoa.TonKho); set => SetNumberField(TuDien.HangHoa.TonKho, value); }
        public double TonMin { get => GetNumberField(TuDien.HangHoa.TonMin); set => SetNumberField(TuDien.HangHoa.TonMin, value); }
        public double TonMax { get => GetNumberField(TuDien.HangHoa.TonMax); set => SetNumberField(TuDien.HangHoa.TonMax, value); }
        public List<DinhMuc> LstDinhMuc { get; set; } = new();
        public string HinhAnh { get => GetTextField(TuDien.HangHoa.HinhAnh); set => SetTextField(TuDien.HangHoa.HinhAnh, value); }
        public string MaVach { get => GetTextField(TuDien.HangHoa.MaVach); set => SetTextField(TuDien.HangHoa.MaVach, value); }
        public string MoTa { get => GetTextField(TuDien.HangHoa.MoTa) ?? string.Empty; set => SetTextField(TuDien.HangHoa.MoTa, value); }
        public bool NgungKinhDoanh { get => GetLogicField(TuDien.HangHoa.NgungKinhDoanh); set => SetLogicField(TuDien.HangHoa.NgungKinhDoanh, value); }
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
        public double LayGiaVon(string IdHH, DateTime Ngay)
        {
            var GiaVon = DuLieuBanHang.BangGiaVon.Where(x => x.Id.Equals(IdHH) && x.NgayThayDoi <= Ngay)
                .OrderBy(x => x.NgayThayDoi).LastOrDefault();
            return GiaVon == null ? 0 : GiaVon.Gia;
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
        public double GiaVon { get; set; }
        public double ThanhTien => SoLuong * DonGia;
        public bool LaHangKM { get; set; }
        public DonHangCTCloud ToDHCTCloud()
        {
            return new DonHangCTCloud()
            {
                IdHH = this.IdHH,
                SoLuong = this.SoLuong,
                DonGia = this.DonGia,
                GiaVon = this.GiaVon,
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
        public string IdHH { get; set; }
        public string MaDoiTuong { get; set; }
        public double SoLuong { get; set; }
        public double DonGia { get; set; }
        public double GiaVon { get; set; }
        public bool LaHangKM { get; set; }
        public DonHangCT ToDHCT()
        {
            return new DonHangCT()
            {
                IdHH = this.IdHH,
                SoLuong = this.SoLuong,
                DonGia = this.DonGia,
                GiaVon = this.GiaVon,
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
        public SoCai ToSoCai()
        {
            return new SoCai
            {
                SoPhieu = this.SoPhieu,
                LoaiCT = this.LoaiCT,
                IdKhach = this.IdKhach,
                Ngay = this.Ngay,
                Co = this.TongTien,
                DienGiai = ""
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
        public SoCai ToSoCai()
        {
            return new SoCai
            {
                SoPhieu = this.SoPhieu,
                LoaiCT = this.LoaiCT,
                IdKhach = this.IdKhach,
                Ngay = this.Ngay,
                No = this.TongTien,
                DienGiai = ""
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
        public string LoaiCT { get; set; } = string.Empty;
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
        public string IdKho { get; set; } = string.Empty;
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
        public void CapNhatGiaVon()
        {
            if (LoaiCT.Equals("N1"))
            {
                Parallel.ForEach(CTDonHang, ct =>
                {
                    var hanghoa = DuLieuBanHang.DSHangHoa.FirstOrDefault(x => x.Id.Equals(ct.IdHH));
                    if (hanghoa != null)
                    {
                        double GiaVonCu = hanghoa.LayGiaVon(hanghoa.Id, this.Ngay);
                        double SLTon = DuLieuBanHang.SoKhoTongHop.Where(x => x.IdHH.Equals(hanghoa.Id) && x.Ngay < this.Ngay).
                            Sum(x => x.SLNhap - x.SLXuat) + hanghoa.SoLuongTonKhoDauNam();

                        //Tĩnh: Lưu lên cloud
                        lock (DuLieuBanHang.BangGiaVon)
                            DuLieuBanHang.BangGiaVon.Add(new GiaVon
                            {
                                Id = hanghoa.Id,
                                NgayThayDoi = this.Ngay,
                                Gia = (GiaVonCu * SLTon + ct.SoLuong * ct.DonGia) / (SLTon + ct.SoLuong),
                                SoLuongTon = SLTon + ct.SoLuong
                            });
                    }
                });
            }
        }
        public SoCai ToSoCai()
        {
            return new SoCai
            {
                SoPhieu = this.SoPhieu,
                LoaiCT = this.LoaiCT,
                Ngay = this.Ngay,
                IdKhach = this.IdKhach,
                No = LoaiCT.StartsWith("N") ? this.TongTien : 0,
                Co = LoaiCT.StartsWith("X") ? this.TongTien : 0,
                DienGiai = this.GhiChu
            };
        }
        public async Task UpdateToCloud()
        {
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
            DuLieuBanHang.SoKhoTongHop.AddRange(ds);
        }

        public async Task DeleteFromCloud()
        {
            HuyBoCongNo();
            HuyBoTheKho();
        }

        private void HuyBoTheKho()
        {
            DuLieuBanHang.SoKhoTongHop.RemoveAll(x => x.IdCT.Equals(this.Id));
        }

        private void HuyBoCongNo()
        {

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
        public string LoaiQuy { get; set; }
        public string ThongTinQuy { get; set; }
    }

    public class CTThuChi
    {
        public string IdQuy { get; set; }
        public string MaDoiTuong { get; set; }
        public string DienGiai { get; set; }
        public double SoTien { get; set; }
    }
    public class CTTienTe
    {
        public string Id { get; set; }
        public string SoPhieu { get; set; }
        public string LoaiCT { get; set; }
        public string IdCT { get; set; }
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
        public CTTienTeCloud ToCTTienTeuCloud()
        {
            CTTienTeCloud p = new CTTienTeCloud
            {
                Id = this.Id,
                IdKhach = this.IdKhach,
                SoPhieu = this.SoPhieu,
                LoaiCT = this.LoaiCT,
                IdCT = this.IdCT,
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
        public string IdCT { get; set; }
        public DateTime Ngay { get; set; }
        public string IdKhach { get; set; }
        public List<CTThuChi> ChiTiet { get; set; }
        public string IdUser { get; set; }

        public List<SoCai> ToSoCai()
        {
            List<SoCai> lst = new();
            foreach (var ct in ChiTiet)
            {
                lst.Add(new SoCai
                {
                    SoPhieu = this.SoPhieu,
                    LoaiCT = this.LoaiCT,
                    Ngay = this.Ngay,
                    LoaiDTCo = LoaiCT.StartsWith("T") ? TuDien.DoiTuong.PhapNhan : ct.MaDoiTuong,
                    LoaiDTNo = LoaiCT.StartsWith("T") ? ct.MaDoiTuong : TuDien.DoiTuong.PhapNhan,
                    IdCo = LoaiCT.StartsWith("T") ? IdKhach : ct.IdQuy,
                    IdNo = LoaiCT.StartsWith("T") ? ct.IdQuy : IdKhach,
                    SoTien = ct.SoTien,
                    DienGiai = ct.DienGiai
                });
            }
            return lst;
        }
        public CTTienTe ToCTTienTe()
        {
            CTTienTe p = new CTTienTe
            {
                Id = this.Id,
                IdKhach = this.IdKhach,
                SoPhieu = this.SoPhieu,
                LoaiCT = this.LoaiCT,
                IdCT = this.IdCT,
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

        }



        public async Task DeleteFromCloud()
        {
            HuyBoCongNo();
        }

        private void HuyBoCongNo()
        {
            //Cần xóa trên Cloud            
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

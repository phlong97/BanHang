using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ban_Hang
{
    public static class DuLieuBanHang
    {
        static readonly FireBaseDB fb = new FireBaseDB(TuDien.FIREBASE_URL, TuDien.FIREBASE_SECRET);
        static bool _InternetOff;
        public static int FinancialYear = DateTime.Now.Year;
        public static bool InternetOff
        {
            get => _InternetOff;
            set
            {
                if (value && _InternetOff == false)
                {
                    _InternetOff = value;
                    //
                    Parallel.ForEach(TuDien.dsCollection, coll =>
                    {
                        switch (coll)
                        {
                            case TuDien.ColName.KhachHang:
                                PushToFirebase<KhachHang>();
                                break;
                            case TuDien.ColName.DonHang:
                                PushToFirebase<DonHangCloud>();
                                break;
                            case TuDien.ColName.TheKho:
                                PushToFirebase<TheKhoCloud>();
                                break;
                            case TuDien.ColName.CTTTe:
                                PushToFirebase<CTTienTeCloud>();
                                break;
                            case TuDien.ColName.QuyTienTe:
                                PushToFirebase<QuyTienTe>();
                                break;
                            case TuDien.ColName.HangHoa:
                                PushToFirebase<HangHoaCloud>();
                                break;
                            case TuDien.ColName.BangGia:
                                PushToFirebase<BangGiaCloud>();
                                break;
                            case TuDien.ColName.NhomHang:
                                PushToFirebase<NhomHang>();
                                break;
                            case TuDien.ColName.NhomKhach:
                                PushToFirebase<NhomKhach>();
                                break;
                            case TuDien.ColName.Kho:
                                PushToFirebase<Kho>();
                                break;
                            case TuDien.ColName.NhanVien:
                                PushToFirebase<NhanVien>();
                                break;
                            default:
                                break;
                        }
                    });
                }

            }
        }
        public static bool PushToFirebase<T>() where T : MiliFirebase
        {
            //B1: Ktra co internet hay khong? Khong co => return; Neu co sang B2
            //B2: Lay tat ca cac ban ghi co key trong hoac del = true
            //B3: Duyet tran ds vua lay 
            // Key khac trong => xoa tren firebase
            //B4: Ghi ban ghi hien tai len firebase theo key moi
            if (DuLieuBanHang.InternetOff)
                return false;
            using (var db = new LiteDatabase(TuDien.LITEDB_LOCAL_PATH))
            {
                Type type = typeof(T);
                var coll = db.GetCollection<T>(type.Name);
                var query = coll.Query();
                var Ds = query.Where(x => x.snc == null || (bool)x.snc == false).ToList();
                if (Ds.Count == 0)
                    return false;
                Parallel.ForEach(Ds, async item =>
                {
                    item.Id = MiliHelper.CreateKey();
                    item.snc = true;
                    await fb.UpdateToFirebase<T>(type.Name, item);
                });
                return true;
            }

        }
        public static bool DaLapSo = false;
        public static bool SystemBusy = false;
        public static List<SoCai> SoCaiTongHop = new();
        public static List<TheKho> SoKhoTongHop = new();
        public static List<NhomHangCloud> DSNhomHang = new();
        public static List<HangHoaCloud> DSHangHoa = new();
        public static List<BangGia> DSBangGia = new();
        public static List<NhomKhach> DSNhomKhach = new();
        public static List<KhachHangCloud> DSKhachHang = new();
        public static List<NhanVien> DSNhanVien = new();
        public static List<TenTruongTruyVan> ListTenTruong = new();
        public static List<GiaVon> BangGiaVon = new();
        public static List<CTTienTeCloud> CTTienTe = new();
        public static List<QuyTienTe> DSQuyTT = new();
        public static List<Kho> DSKho = new();
        public static List<DonHangCloud> DSDonHang = new();
        public static List<string> ListToanTu = new() { "<", "<=", "=", ">", ">=" };
        public static User user = new();
        //public static void TinhLaiGiaVon()
        //{
        //    Parallel.ForEach(DSHangHoa, hh =>
        //    {
        //        var Gvon = new GiaVon(hh);
        //        Gvon.TinhGiaVon();
        //    });
        //}


        //public static double LayGiaVon(string IdHH, int thang)
        //{
        //    return BangGiaVon.FirstOrDefault(x => x.IdHH.Equals(IdHH))?.GetGiaVon(thang) ?? 0;
        //}
        public static async Task TaoSoCai()
        {
            if (SystemBusy) return;
            SystemBusy = true;
            await Task.Run(() =>
            {
                //SoCaiTongHop.AddRange(DuLieuBan_Hang.DSDonHang.Select(x => x.ToSoCai()));
                List<SoCai> sctc = new();
                foreach (var item in DuLieuBanHang.CTTienTe)
                {
                    sctc.AddRange(item.ToSoCai());
                }
                SoCaiTongHop.AddRange(sctc);
            });
            SystemBusy = false;
        }
        public static List<TongHopCongNo> THCongNo(DateTime TuNgay, DateTime DenNgay, int LoaiKhach)
        {
            List<TongHopCongNo> ttcnnm = new();
            Parallel.ForEach(DSKhachHang.Where(x => x.LoaiKhach == LoaiKhach).OrderBy(x => x.TenKhach), khc =>
            {
                var kh = khc.ToKhachHang();
                var ctno = SoCaiTongHop.Where(x => x.IdNo.Equals(kh.Id) && x.Ngay < DenNgay);
                var ctco = SoCaiTongHop.Where(x => x.IdCo.Equals(kh.Id) && x.Ngay < DenNgay);
                var cndk = kh.CongNoDauNam() + ctno.Where(x => x.Ngay < TuNgay).Sum(x => x.SoTien) -
                ctco.Where(x => x.Ngay < TuNgay).Sum(x => x.SoTien);
                lock (ttcnnm)
                    ttcnnm.Add(new TongHopCongNo
                    {
                        MaKH = kh.MaKH,
                        TenKH = kh.TenKhach,
                        DKNo = Math.Max(cndk, 0),
                        DKCo = Math.Max(-cndk, 0),
                        PSNo = ctno.Where(x => x.Ngay >= TuNgay).Sum(x => x.SoTien),
                        PSCo = ctco.Where(x => x.Ngay >= TuNgay).Sum(x => x.SoTien)
                    });
            });
            return ttcnnm;
        }
        public static List<TongHopTonKho> THTonKho(DateTime TuNgay, DateTime DenNgay, string IdKho = null)
        {
            List<TongHopTonKho> thtk = new();

            Parallel.ForEach(DSHangHoa, hhc =>
            {

                var dstk = SoKhoTongHop.Where(x => x.Ngay <= DenNgay && x.IdHH.Equals(hhc.Id));
                var hh = hhc.ToHangHoa();
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
                var ctno = SoCaiTongHop.Where(x => x.IdNo.Equals(quy.Id) && x.Ngay < DenNgay);
                var ctco = SoCaiTongHop.Where(x => x.IdCo.Equals(quy.Id) && x.Ngay < DenNgay);
                lock (thtq)
                    thtq.Add(new TongHopTonQuy
                    {
                        IdQuy = quy.Id,
                        MaQuy = quy.Ma,
                        TenQuy = quy.Ten,
                        TonDauKy = ctno.Where(x => x.Ngay < TuNgay).Sum(x => x.SoTien) - ctco.Where(x => x.Ngay < TuNgay).Sum(x => x.SoTien),
                        TongThu = ctno.Where(x => x.Ngay >= TuNgay).Sum(x => x.SoTien),
                        TongChi = ctco.Where(x => x.Ngay >= TuNgay).Sum(x => x.SoTien)
                    });
            });

            return thtq;
        }


    }
}

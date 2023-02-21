using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using LiteDB;

namespace Ban_Hang
{
    public static class Generator
    {
        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomNumberString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static DateTime RandomDay(int YearStart)
        {
            DateTime start = new DateTime(YearStart, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }

        public static void TaoDanhMucLiteDb()
        {
            using (var db = new LiteDatabase(TuDien.LITEDB_LOCAL_PATH))
            {
                //Tạo danh mục nhóm khách hàng
                List<NhomKhach> DsNhomKhach = new();
                for (int i = 1; i <= 5; i++)
                {
                    DsNhomKhach.Add(new NhomKhach
                    {
                        Id = $"ID_NK{i.ToString("D1")}",
                        TenNhom = $"Nhóm khách {i}",
                    });
                }
                var collectionNk = db.GetCollection<NhomKhach>(TuDien.ColName.NhomKhach);
                if (collectionNk.Count() > 0) collectionNk.DeleteAll();
                collectionNk.InsertBulk(DsNhomKhach);

                List<NhanVien> DsNhanVien = new();
                for (int i = 1; i <= 10; i++)
                {
                    DsNhanVien.Add(new NhanVien
                    {
                        Id = $"ID_NV{i.ToString("D4")}",
                        MaNV = $"MA_NV{i.ToString("D4")}",
                        TenNV = $"Nhân viên {i}"
                    });
                }
                var collectionNv = db.GetCollection<NhanVien>(TuDien.ColName.NhanVien);
                if(collectionNv.Count() > 0) collectionNv.DeleteAll();
                collectionNv.InsertBulk(DsNhanVien);

                //Tạo danh mục nhóm hàng
                List<NhomHang> DsNhomHang = new();
                for (int i = 1; i <= 50; i++)
                {
                    DsNhomHang.Add(new NhomHang
                    {
                        Id = $"ID_NH{i.ToString("D4")}",
                        TenNhom = $"Nhóm hàng {i}",
                    });
                }
                var collectionNh = db.GetCollection<NhomHang>(TuDien.ColName.NhomHang);
                if (collectionNh.Count() > 0) collectionNh.DeleteAll();
                collectionNh.InsertBulk(DsNhomHang);

                //Tạo danh mục khách hàng
                string[] DsHo = "Nguyễn,Trần,Lê,Phan,Phạm".Split(',');
                string[] DsTen = ("Tiểu Bảo,Hữu Cảnh,Trọng Chính,Bá Cường,Hưng Ðạo,Ðắc Di,Tiến Ðức,Nghĩa Dũng,Trọng Dũng,Anh Khoa," +
                    "Anh Khôi,Trung Kiên,Ðức Hải,Công Hiếu,Nhật Hùng,Tuấn Hùng,Minh Huy,Xuân Lộc,Trí Minh,Xuân Nam,Hữu Nghĩa,Bình " +
                    "Nguyên,Hoài Phong,Ngọc Quang,Cao Tiến,Minh Toàn,Hữu Trác,Hữu Trí,Ðức Tuấn,Công Thành,Duy Thành,Gia Vinh,").Split(','); ;
                List<KhachHangCloud> DsKH = new();
                for (int i = 1; i <= 100; i++)
                {
                    DsKH.Add(new KhachHang
                    {
                        Id = $"ID_KH{i.ToString("D5")}",
                        MaKH = $"MA_KH{i.ToString("D5")}",
                        TenKhach = DsHo[random.Next(DsHo.Count())] + " " + DsTen[random.Next(DsTen.Count())],
                        DiaChi = RandomString(15),
                        DienThoai = RandomNumberString(10),
                        EMail = $"{RandomString(10)}@gmail.com",
                        NgaySinh = RandomDay(1945),
                        LaCaNhan = true,
                        IdNhomKhach = $"ID_NK{random.Next(1, 6).ToString("D1")}",
                    }.ToKhachHangCloud());
                }
                var collectionKH = db.GetCollection<KhachHangCloud>(TuDien.ColName.KhachHang);
                if (collectionKH.Count() > 0) collectionKH.DeleteAll();
                collectionKH.InsertBulk(DsKH);
                //Tạo danh mục hàng hóa
                string Dvt = "Kg,g,m,cm,tạ,tấn,l,ml,thùng,cái";
                List<HangHoaCloud> DsHangHoa = new();
                for (int i = 0; i < 500; i++)
                {
                    var hh = new HangHoa
                    {
                        Id = $"ID_HH{(i + 1).ToString("D5")}",
                        MaHH = $"MaHH_HH{(i + 1).ToString("D5")}",
                        TenHH = $"Hàng hóa {i + 1}",
                        MaLoai = $"MA_LOAI{i.ToString("D5")}",
                        IdNhom = $"ID_NH{random.Next(0, 50).ToString("D4")}",
                        Dvt = Dvt.Split(',')[random.Next(0, 10)],
                        LaHangBan = random.NextDouble() > 0.5,
                        NgungKinhDoanh = false,
                        TonMin = 1,
                        TonMax = 100,
                        GiaBan = random.Next(5000, 50000)
                    };
                    hh.TonKho = hh.TonMax - 1;
                    DsHangHoa.Add(hh.ToHangHoaCloud());

                }
                var collectionHH = db.GetCollection<HangHoaCloud>(TuDien.ColName.HangHoa);
                if (collectionHH.Count() > 0) collectionHH.DeleteAll();
                collectionHH.InsertBulk(DsHangHoa);
            }

        }
        static public void TaoDanhMucXML()
        {
            System.IO.Directory.CreateDirectory("DanhMuc");
            //Tạo danh mục nhân viên
            List<NhanVien> DsNhanVien = new();
            for (int i = 1; i <= 10; i++)
            {
                DsNhanVien.Add(new NhanVien
                {
                    Id = $"ID_NV{i.ToString("D4")}",
                    MaNV = $"MA_NV{i.ToString("D4")}",
                    TenNV = $"Nhân viên {i}"
                });
            }
            WriteToXmlFile<List<NhanVien>>("DanhMuc/nhanvien.txt", DsNhanVien);
            //Tạo danh mục nhóm hàng
            List<NhomHang> DsNhomHang = new();
            for (int i = 1; i <= 50; i++)
            {
                DsNhomHang.Add(new NhomHang
                {
                    Id = $"ID_NH{i.ToString("D4")}",
                    TenNhom = $"Nhóm hàng {i}",
                });
            }
            WriteToXmlFile<List<NhomHang>>("DanhMuc/nhomhang.txt", DsNhomHang);
            //Tạo danh mục nhóm khách hàng
            List<NhomKhach> DsNhomKhach = new();
            for (int i = 1; i <= 5; i++)
            {
                DsNhomKhach.Add(new NhomKhach
                {
                    Id = $"ID_NK{i.ToString("D1")}",
                    TenNhom = $"Nhóm khách {i}",
                });
            }
            WriteToXmlFile<List<NhomKhach>>("DanhMuc/nhomkhach.txt", DsNhomKhach);

            //Tạo danh mục khách hàng
            string[] DsHo = "Nguyễn,Trần,Lê,Phan,Phạm".Split(',');
            string[] DsTen = ("Tiểu Bảo,Hữu Cảnh,Trọng Chính,Bá Cường,Hưng Ðạo,Ðắc Di,Tiến Ðức,Nghĩa Dũng,Trọng Dũng,Anh Khoa," +
                "Anh Khôi,Trung Kiên,Ðức Hải,Công Hiếu,Nhật Hùng,Tuấn Hùng,Minh Huy,Xuân Lộc,Trí Minh,Xuân Nam,Hữu Nghĩa,Bình " +
                "Nguyên,Hoài Phong,Ngọc Quang,Cao Tiến,Minh Toàn,Hữu Trác,Hữu Trí,Ðức Tuấn,Công Thành,Duy Thành,Gia Vinh,").Split(','); ;
            List<KhachHang> DsKH = new();
            for (int i = 1; i <= 500; i++)
            {
                DsKH.Add(new KhachHang
                {
                    Id = $"ID_KH{i.ToString("D5")}",
                    MaKH = $"MA_KH{i.ToString("D5")}",
                    TenKhach = DsHo[random.Next(DsHo.Count())] + " " + DsTen[random.Next(DsTen.Count())],
                    DiaChi = RandomString(15),
                    DienThoai = RandomNumberString(10),
                    EMail = $"{RandomString(10)}@gmail.com",
                    NgaySinh = RandomDay(1945),
                    LaCaNhan = true,
                    IdNhomKhach = $"ID_NK{random.Next(1, 6).ToString("D1")}",
                });
            }
            WriteToXmlFile<List<KhachHangCloud>>("DanhMuc/khachhang.txt", DsKH.Select(x => x.ToKhachHangCloud()).ToList());
            //Tạo danh mục hàng hóa
            string Dvt = "Kg,g,m,cm,tạ,tấn,l,ml,thùng,cái";
            List<HangHoa> DsHangHoa = new();
            for (int i = 0; i < 5000; i++)
            {
                var hh = new HangHoa
                {
                    Id = $"ID_HH{(i + 1).ToString("D5")}",
                    MaHH = $"MaHH_HH{(i + 1).ToString("D5")}",
                    TenHH = $"Hàng hóa {i + 1}",
                    IdNhom = $"ID_NH{random.Next(0, 50).ToString("D4")}",
                    Dvt = Dvt.Split(',')[random.Next(0, 10)],
                    LaHangBan = random.NextDouble() > 0.5,
                    NgungKinhDoanh = false,
                    TonMin = 1,
                    TonMax = 100,
                };
                hh.GiaBan = random.Next(5000, 50000);
                hh.TonKho = hh.TonMax - 1;
                DsHangHoa.Add(hh);
            }
            WriteToXmlFile<List<HangHoaCloud>>("DanhMuc/hanghoa.txt", DsHangHoa.Select(x => x.ToHangHoaCloud()).ToList());

        }

        static public void TaoDanhMucJSon()
        {
            System.IO.Directory.CreateDirectory("DanhMuc");
            //Tạo danh mục nhân viên
            List<NhanVien> DsNhanVien = new();
            for (int i = 1; i <= 10; i++)
            {
                DsNhanVien.Add(new NhanVien
                {
                    Id = $"ID_NV{i.ToString("D4")}",
                    MaNV = $"MA_NV{i.ToString("D4")}",
                    TenNV = $"Nhân viên {i}"
                });
            }
            WriteToJsonFile<List<NhanVien>>("DanhMuc/nhanvien.txt", DsNhanVien);
            //Tạo danh mục nhóm hàng
            List<NhomHang> DsNhomHang = new();
            for (int i = 1; i <= 50; i++)
            {
                DsNhomHang.Add(new NhomHang
                {
                    Id = $"ID_NH{i.ToString("D4")}",
                    TenNhom = $"Nhóm hàng {i}",
                });
            }
            WriteToJsonFile<List<NhomHang>>("DanhMuc/nhomhang.txt", DsNhomHang);
            //Tạo danh mục nhóm khách hàng
            List<NhomKhach> DsNhomKhach = new();
            for (int i = 1; i <= 5; i++)
            {
                DsNhomKhach.Add(new NhomKhach
                {
                    Id = $"ID_NK{i.ToString("D1")}",
                    TenNhom = $"Nhóm khách {i}",
                });
            }
            WriteToJsonFile<List<NhomKhach>>("DanhMuc/nhomkhach.txt", DsNhomKhach);

            //Tạo danh mục khách hàng
            string[] DsHo = "Nguyễn,Trần,Lê,Phan,Phạm".Split(',');
            string[] DsTen = ("Tiểu Bảo,Hữu Cảnh,Trọng Chính,Bá Cường,Hưng Ðạo,Ðắc Di,Tiến Ðức,Nghĩa Dũng,Trọng Dũng,Anh Khoa," +
                "Anh Khôi,Trung Kiên,Ðức Hải,Công Hiếu,Nhật Hùng,Tuấn Hùng,Minh Huy,Xuân Lộc,Trí Minh,Xuân Nam,Hữu Nghĩa,Bình " +
                "Nguyên,Hoài Phong,Ngọc Quang,Cao Tiến,Minh Toàn,Hữu Trác,Hữu Trí,Ðức Tuấn,Công Thành,Duy Thành,Gia Vinh,").Split(','); ;
            List<KhachHang> DsKH = new();
            for (int i = 1; i <= 500; i++)
            {
                DsKH.Add(new KhachHang
                {
                    Id = $"ID_KH{i.ToString("D5")}",
                    MaKH = $"MA_KH{i.ToString("D5")}",
                    TenKhach = DsHo[random.Next(DsHo.Count() - 1)] + " " + DsTen[random.Next(DsTen.Count() - 1)],
                    DiaChi = RandomString(15),
                    DienThoai = RandomNumberString(10),
                    EMail = $"{RandomString(10)}@gmail.com",
                    NgaySinh = RandomDay(1945),
                    LaCaNhan = true,
                    IdNhomKhach = $"ID_NK{random.Next(1, 6).ToString("D1")}",
                });
            }
            WriteToJsonFile<List<KhachHangCloud>>("DanhMuc/khachhang.txt", DsKH.Select(x => x.ToKhachHangCloud()).ToList());
            //Tạo danh mục hàng hóa
            string Dvt = "Kg,g,m,cm,tạ,tấn,l,ml,thùng,cái";
            List<HangHoa> DsHangHoa = new();
            for (int i = 0; i < 5000; i++)
            {
                var hh = new HangHoa
                {
                    Id = $"ID_HH{(i + 1).ToString("D5")}",
                    MaHH = $"MaHH_HH{(i + 1).ToString("D5")}",
                    TenHH = $"Hàng hóa {i + 1}",
                    MaLoai = $"MA_LOAI{i.ToString("D5")}",
                    IdNhom = $"ID_NH{random.Next(0, 50).ToString("D4")}",
                    Dvt = Dvt.Split(',')[random.Next(0, 10)],
                    LaHangBan = random.NextDouble() > 0.5,
                    NgungKinhDoanh = false,
                    TonMin = 1,
                    TonMax = 100,
                };
                hh.GiaBan = random.Next(5000, 50000);
                hh.TonKho = hh.TonMax - 1;
                DsHangHoa.Add(hh);
            }
            WriteToJsonFile<List<HangHoaCloud>>("DanhMuc/hanghoa.txt", DsHangHoa.Select(x => x.ToHangHoaCloud()).ToList());



            //Tạo Thẻ kho
            List<TheKhoCloud> DSTheKho = new();
            for (int i = 0; i < 5000; i++)
            {
                var ct = new TheKhoCloud()
                {
                    SoCT = $"CT_{random.Next(501).ToString("D5")}",
                    LoaiCT = "X1",
                    IdHH = $"ID_HH{random.Next(5001).ToString("D5")}",
                    Ngay = RandomDay(2020),
                    IdKhoNhap = $"ID_KHO{random.Next(51).ToString("D4")}",
                    IdKhoXuat = $"ID_KHO{random.Next(51).ToString("D4")}",
                    SLNhap = random.Next(11),
                    SLXuat = random.Next(11),
                    DonGia = random.Next(50000),
                    DonGiaVon = random.Next(50000),
                    SoLuong = random.Next(101)
                };
                DSTheKho.Add(ct);
            }
            WriteToJsonFile<List<TheKhoCloud>>("DanhMuc/thekho.txt", DSTheKho);

            //Tạo danh mục quỹ
            List<QuyTienTe> DsQuy = new();
            string[] dsloaiquy = "TM,TG".Split(',');
            for (int i = 1; i <= 10; i++)
            {
                DsQuy.Add(new QuyTienTe
                {
                    Id = $"ID_QUY{i.ToString("D3")}",
                    Ma = $"MA_QUY{i.ToString("D3")}",
                    Ten = $"Quỹ {i}",
                    LoaiQuy = dsloaiquy[random.Next(dsloaiquy.Count())]
                });
            }
            WriteToJsonFile<List<QuyTienTe>>("DanhMuc/quytiente.txt", DsQuy);

            //Tạo đơn hàng
            List<DonHangCloud> DsDonHang = new();
            string[] dsloaict = "X1,X2,N1,N2".Split(',');
            for (int i = 0; i < 10; i++)
            {
                var dh = new DonHangCloud()
                {
                    IdKhach = $"ID_KH{random.Next(501).ToString("D5")}",
                    IdBangGia = $"ID_BG{random.Next(11).ToString("D3")}",
                    IdKho = $"ID_KHO{random.Next(51).ToString("D4")}",
                    IdNV = $"ID_NV{random.Next(11).ToString("D4")}",
                    LoaiCT = dsloaict[random.Next(dsloaict.Count())],
                    SoPhieu = $"CT_{i.ToString("D5")}",
                    TienKM = random.Next(5000, 10001),
                    Ngay = RandomDay(2022),
                    CTDonHang = new()
                    {
                        new DonHangCTCloud()
                        {
                            IdHH = $"ID_HH{random.Next(5001).ToString("D5")}",
                            SoLuong = random.Next(11),
                            DonGia = random.Next(50001),
                        },
                        new DonHangCTCloud()
                        {
                            IdHH = $"ID_HH{random.Next(5001).ToString("D5")}",
                            SoLuong = random.Next(11),
                            DonGia = random.Next(50001),
                        }
                    }

                };
                //dh.TienHang = dh.CTDonHang.Sum(x => x.SoLuong * x.DonGia);
                dh.TongTien = dh.TienHang - dh.TienKM;
                DsDonHang.Add(dh);
            }
            WriteToJsonFile<List<DonHangCloud>>("DanhMuc/donhang.txt", DsDonHang);

            //Tạo phiếu thu chi
            List<CTTienTeCloud> DsPhieuTC = new();
            string[] dsloaip = "T1,C1".Split(',');
            for (int i = 0; i < 10; i++)
            {
                var p = new CTTienTeCloud()
                {
                    LoaiCT = dsloaip[random.Next(dsloaip.Count())],
                    SoPhieu = $"CT_{i.ToString("D5")}",
                    Ngay = RandomDay(2022),
                    IdKhach = $"ID_KH{random.Next(501).ToString("D5")}",


                };
                DsPhieuTC.Add(p);
            }
            WriteToJsonFile<List<CTTienTeCloud>>("DanhMuc/thuchi.txt", DsPhieuTC);

            //Tạo sổ cái tổng hợp 
            //List<SoCai> SoCaiTH = new();
            //string[] dsloaict = "X1,X2,N1,N2,T1,C1".Split(",");
            //for (int i = 0; i < 5000; i++)
            //{
            //    string loaict = dsloaict[random.Next(dsloaict.Count())];
            //    var ct = new SoCai()
            //    {
            //        SoPhieu = $"CT_{random.Next(1001).ToString("D5")}",
            //        LoaiCT = loaict,
            //        IdKhach = $"ID_KH{random.Next(501).ToString("D5")}",
            //        IdQuy = loaict.StartsWith("C") || loaict.StartsWith("T") ? $"ID_QUY{random.Next(11).ToString("D3")}" : string.Empty,
            //        Ngay = RandomDay(2020),
            //        No = loaict.StartsWith("N") || loaict.StartsWith("T") ? random.Next(1000001) : 0,
            //        Co = loaict.StartsWith("X") || loaict.StartsWith("C") ? random.Next(1000001) : 0,
            //    };
            //    SoCaiTH.Add(ct);
            //}
            //WriteToJsonFile<List<SoCai>>("DanhMuc/socai.txt", SoCaiTH);
        }

        internal static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        internal static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }


        internal static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        internal static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}

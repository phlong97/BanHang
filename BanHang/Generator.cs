using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BanHang
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
        public static DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
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
                    NgaySinh = RandomDay(),
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
                    Dvt = Dvt.Split(",")[random.Next(0, 10)],
                    GiaVon = random.Next(1000, 500000),
                    LaHangBan = random.NextDouble() > 0.5,
                    NgungKinhDoanh = false,
                    TonMin = 1,
                    TonMax = 100,
                };
                hh.GiaBan = hh.GiaVon + random.Next(5000, 50000);
                hh.TonKho = hh.TonMax - 1;
                DsHangHoa.Add(hh);
            }
            WriteToXmlFile<List<HangHoaCloud>>("DanhMuc/hanghoa.txt", DsHangHoa.Select(x => x.ToHangHoaCloud()).ToList());

        }

        /// <summary>
        /// Writes the given object instance to an XML file.
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
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

        /// <summary>
        /// Reads an object instance from an XML file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the XML file.</returns>
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

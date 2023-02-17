using LiteDB;
using System;

namespace Ban_Hang
{
    public class MiliObject
    {
        public string Key { get; set; }
        [BsonId]
        public string Id { get; set; }
        public bool Del { get; set; }
        public bool Sync { get; set; } // false: Da sua chua luu len firebase (Update)
    }
    public static class MiliHelper
    {
        public static string CreateKey()
        {
            return ObjectId.NewObjectId().ToString();
        }
        public static DateTime LayNgayTao(string key)
        {
            var ObjId = new ObjectId(key);
            return ObjId.CreationTime.ToUniversalTime();
        }
    }
    public class FirebaseDataNode : MiliObject
    {
        public string Id { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public bool Deleted { get; set; } = false;
        public string KeyName { get; set; } = string.Empty;
        public string ModelName { get; set; } = string.Empty;
        public string ChildPath { get; set; } = string.Empty;
        public string LocalFileName { get; set; } = string.Empty;
        public string CurrentValue { get; set; } = string.Empty;
        public bool DataOpened { get; set; } = false;
        public bool LocalStorage { get; set; }
        public bool DataChanged { get; set; }
        public string LocalLastKey { get; set; } = string.Empty;
        public string LastKey { get; set; } = "0";
    }
    //public static List<FirebaseDataNode> ListDataNode = new List<FirebaseDataNode>()
    //    {

    //        new FirebaseDataNode { KeyName = TuDien.CollectionName.UserNode, ModelName="UserCloudModel", ChildPath = "User", LocalFileName = offlineStorageLocation + "\\User.uvf" },
    //        new FirebaseDataNode { KeyName = TuDien.CollectionName.HangHoaNode, ModelName="HangHoaCloudModel",  ChildPath = "HangHoa", LocalFileName = offlineStorageLocation + "\\HangHoa.uvf" },
    //        new FirebaseDataNode { KeyName = TuDien.CollectionName.KhachHangNode, ModelName="KhachHangCloudModel", ChildPath = "KhachHang", LocalFileName = offlineStorageLocation + "\\KhachHang.uvf" },
    //        new FirebaseDataNode { KeyName = TuDien.CollectionName.NhomHangNode, ModelName="NhomHangCloudModel", ChildPath = "NhomHang", LocalFileName = offlineStorageLocation + "\\NhomHang.uvf" },
    //        new FirebaseDataNode { KeyName = TuDien.CollectionName.NhomKhachNode, ModelName="NhomKhachCloudModel", ChildPath = "NhomKhach", LocalFileName = offlineStorageLocation + "\\NhomKhach.uvf" },
    //        new FirebaseDataNode { KeyName = TuDien.CollectionName.ChucNangNode, ModelName="ChucNangCloudModel", ChildPath = "Public/ChucNang", LocalFileName = offlineStorageLocation + "\\ChucNang.uvf" },
    //        new FirebaseDataNode { KeyName = TuDien.CollectionName.DonHangNode, ModelName="DonHangCloudModel", ChildPath = "DonHang", LocalFileName = offlineStorageLocation + "\\DonHang.uvf" },


    //    };
}

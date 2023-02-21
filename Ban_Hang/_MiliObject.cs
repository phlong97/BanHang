using Firebase.Database;
using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using static Ban_Hang.TuDien;

namespace Ban_Hang
{    
    public class MiliFirebase
    {
        [BsonId]
        public string Id { get; set; }
        public bool? snc { get; set; }
        public bool? del { get; set; }
        public string udm { get; set; }

        public MiliFirebase()
        {
            Id = NewGuid;
        }
        public const int lenTimesteam = 13;
      
        internal static string NewGuid => ObjectId.NewObjectId().ToString();
        public static string prefix(string keyName) => TuDien.dsCollection.Contains(keyName) ?
               "00" : DuLieuBanHang.FinancialYear.ToString().Substring(2);

        internal static string GetUpdateMark(string keyName)
        {
            return $"{prefix(keyName)}{DateTime.Now.ToString().PadLeft(lenTimesteam, '0')}";
        }
        internal static string udmMin(string keyName) => $"{prefix(keyName)}{"0".PadLeft(MiliFirebase.lenTimesteam, '0')}";
        internal static string udmMax(string keyName) => $"{prefix(keyName)}{"9".PadLeft(MiliFirebase.lenTimesteam, '9')}";

        internal static bool BetWeen(string udmCheck, string udmMin, string udmMax)
        {
            return (udmCheck.CompareTo(udmMin) > 0 && udmCheck.CompareTo(udmMax) < 0);
        }
        public void CopyBase(MiliFirebase Source)
        {
            Id = Source.Id;
            snc = Source.snc;
            del = Source.del;
        }
        public static T CopyObject<T>(T source) where T : class
        {
            // Sử dụng Newtonsoft.Json để serialize đối tượng nguồn thành một chuỗi JSON
            var json = JsonConvert.SerializeObject(source);

            // Deserialize chuỗi JSON thành một đối tượng mới
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static MiliFirebase ToFirebaseObject<T>(T obj, string keyName) where T : MiliFirebase
        {
            var dt = MiliFirebase.CopyObject<T>(obj);
            dt.Id = null;
            dt.snc = null;
            dt.udm = GetUpdateMark(keyName);
            return dt;
        }

        public static MiliFirebase FromFirebaseObject<T>(FirebaseObject<T> obj, string keyName) where T : MiliFirebase
        {
            var dt = MiliFirebase.CopyObject<T>(obj.Object);
            dt.Id = obj.Key;
            return dt;
        }
        internal static string GetUpdateMarkBeginDate(DateTime ngayKK)
        {
            throw new NotImplementedException();
        }

        internal static string GetUpdateMarkEndDate(DateTime ngayKK)
        {
            throw new NotImplementedException();
        }

        internal static string GetUdmStaAt(string keyName, string LastUdm)
        {
            if (LastUdm == null) return prefix(keyName) + "0".PadLeft(MiliFirebase.lenTimesteam, '0');
            else  return $"{prefix(keyName)}{(long.Parse(LastUdm.Substring(2)) + 1).ToString().PadLeft(MiliFirebase.lenTimesteam, '0')}";
        }
    }
  
    public class objExtObject : MiliFirebase
    {
        public Dictionary<string, bool> lf { get; set; } = new Dictionary<string, bool>();
        public Dictionary<string, string> tf { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, double> nf { get; set; } = new Dictionary<string, double>();
        public Dictionary<string, DateTime> df { get; set; } = new Dictionary<string, DateTime>();
        public string GetTextField(string key, string ValueDefault = default(string)) => tf.ContainsKey(key) ? tf[key] : ValueDefault;
        public double GetNumberField(string key, double ValueDefault = 0) => nf.ContainsKey(key) ? nf[key] : ValueDefault;
        public bool GetLogicField(string key, bool ValueDefault = false) => lf.ContainsKey(key) ? lf[key] : ValueDefault;
        public DateTime GetDateField(string key, DateTime ValueDefault = default(DateTime)) => df.ContainsKey(key) ? df[key] : ValueDefault;

        public void SetTextField(string key, string value)
        {
            if (tf.ContainsKey(key)) tf[key] = value; 
            else tf.Add(key, value);

        }
        public void SetNumberField(string key, double value)
        {
            if (nf.ContainsKey(key)) nf[key] = value;
            else nf.Add(key, value);

        }
        public void SetLogicField(string key, bool value)
        {
            if (lf.ContainsKey(key)) lf[key] = value;
            else lf.Add(key, value);

        }
        public void SetDateField(string key, DateTime value)
        {
            if (df.ContainsKey(key)) df[key] = value;
            else df.Add(key, value);

        }

        internal void CopyExtension(objExtObject Source)
        {
            this.tf = Source.tf.ToDictionary(x => x.Key, x => x.Value);
            this.nf = Source.nf.ToDictionary(x => x.Key, x => x.Value);
            this.df = Source.df.ToDictionary(x => x.Key, x => x.Value);
            this.lf = Source.lf.ToDictionary(x => x.Key, x => x.Value);
        }
    }
    public class objExtPlan
    {
        public Dictionary<int, double> Plan { get; set; } = new Dictionary<int, double>();
                                                                                                     
        public void CopyPlan(objExtPlan Source)
        {
            this.Plan = Source.Plan.ToDictionary(x => x.Key, x => x.Value);
        }

        public double GetPlan(int thang)
        {
            return Plan.ContainsKey(thang) ? Plan[thang] : 0;
        }

        public void SetPlan(int thang, double value)
        {
            if (Plan.ContainsKey(thang)) Plan[thang] = value;
            else Plan.Add(thang, value);
        }

        public double SumPlan => Plan.Sum(x => x.Value);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ban_Hang
{
    public class ObjBeginValue
    {
        public int year { get; set; }
        public string key { get; set; } = String.Empty;
        public double value { get; set; }
        public double value2 { get; set; }
        public ObjBeginValue MakeCopy()
        {
            return (ObjBeginValue)this.MemberwiseClone();
        }
    }
    public class ObjectBeginValue
    {

        public List<ObjBeginValue> Ds { get; set; } = new List<ObjBeginValue>();
        public double GetValue(string key, int year = 0)
        {
            var obj = Ds.FirstOrDefault(x => x.key.Equals(key) && x.year.Equals((year == 0 ? DateTime.Now.Year : year)));
            if (obj == null) return 0;
            else return obj.value;
        }
        public double GetValue2(string key, int year = 0)
        {
            var obj = Ds.FirstOrDefault(x => x.key.Equals(key) && x.year.Equals((year == 0 ? DateTime.Now.Year : year)));
            if (obj == null) return 0;
            else return obj.value2;
        }
        public void SetValue(string key, double value, double value2 = 0, int year = 0)
        {
            var obj = Ds.FirstOrDefault(x => x.key.Equals(key) && x.year.Equals((year == 0 ? DateTime.Now.Year : year)));
            if (obj == null) Ds.Add(new ObjBeginValue { key = key, value = value, value2 = value2, year = (year == 0 ? DateTime.Now.Year : year) });
            else
            {
                obj.value = value;
                obj.value2 = value2;
            }
        }
        public ObjectBeginValue MakeCopy()
        {
            var copy = (ObjectBeginValue)this.MemberwiseClone();
            copy.Ds = this.Ds.Select(x => x.MakeCopy()).ToList();
            return copy;
        }
    }
}

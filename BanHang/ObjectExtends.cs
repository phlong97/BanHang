using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanHang
{
    public class ObjectExt<T>
    {
        public string key { get; set; }
        public T value { get; set; }
        public ObjectExt<T> MakeCopy()
        {
            return (ObjectExt<T>)this.MemberwiseClone();
        }
    }
    public class ObjectExtendProperties<T>
    {

        public List<ObjectExt<T>> objectExts { get; set; } = new List<ObjectExt<T>>();
        public T GetValue(string key)
        {
            var obj = objectExts.FirstOrDefault(x => x.key.Equals(key));
            if (obj == null) return default(T);
            else return obj.value;
        }
        public void SetValue(string key, T value)
        {
            var obj = objectExts.FirstOrDefault(x => x.key.Equals(key));
            if (obj == null) objectExts.Add(new ObjectExt<T> { key = key, value = value });
            else obj.value = value;
        }
        public ObjectExtendProperties<T> MakeCopy()
        {
            var copy = (ObjectExtendProperties<T>)this.MemberwiseClone();
            copy.objectExts = this.objectExts.Select(x => x.MakeCopy()).ToList();
            return copy;
        }
    }
}

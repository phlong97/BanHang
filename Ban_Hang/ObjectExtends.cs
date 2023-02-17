using System;
using System.Collections.Generic;

namespace Ban_Hang
{
    public class ObjectExtends : MiliObject
    {
        public Dictionary<string, string> tf { get; set; } = new();
        public Dictionary<string, DateTime> df { get; set; } = new();
        public Dictionary<string, double> nf { get; set; } = new();
        public Dictionary<string, bool> lf { get; set; } = new();

        public string GetTextField(string key, string defaultValue = "")
        {
            return tf.ContainsKey(key) ? tf[key] : defaultValue;
        }

        public void SetTextField(string key, string value)
        {
            if (tf.ContainsKey(key)) tf[key] = value;
            else tf.Add(key, value);
        }

        public DateTime GetDateField(string key, DateTime defaultValue = default(DateTime))
        {
            return df.ContainsKey(key) ? df[key] : defaultValue;
        }

        public void SetDateField(string key, DateTime value)
        {
            if (df.ContainsKey(key)) df[key] = value;
            else df.Add(key, value);
        }

        public double GetNumberField(string key, double defaultValue = 0)
        {
            return nf.ContainsKey(key) ? nf[key] : defaultValue;
        }

        public void SetNumberField(string key, double value)
        {
            if (nf.ContainsKey(key)) nf[key] = value;
            else nf.Add(key, value);
        }
        public bool GetLogicField(string key, bool defaultValue = false)
        {
            return lf.ContainsKey(key) ? lf[key] : defaultValue;
        }

        public void SetLogicField(string key, bool value)
        {
            if (lf.ContainsKey(key)) lf[key] = value;
            else lf.Add(key, value);
        }

        public void CopySource(ObjectExtends source)
        {
            tf = new Dictionary<string, string>(source.tf);
            nf = new Dictionary<string, double>(source.nf);
            df = new Dictionary<string, DateTime>(source.df);
            lf = new Dictionary<string, bool>(source.lf);
        }
    }
}

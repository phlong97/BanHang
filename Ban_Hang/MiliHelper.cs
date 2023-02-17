using LiteDB;
using System;
using System.Collections.Generic;

namespace Ban_Hang
{
    public static class MiliHelper
    {
        public static string CreateKey()
        {
            return ObjectId.NewObjectId().ToString();
        }
        public static DateTime LayNgayTaoKey(string key)
        {
            var ObjId = new ObjectId(key);
            return ObjId.CreationTime.ToUniversalTime();
        }
    }
}

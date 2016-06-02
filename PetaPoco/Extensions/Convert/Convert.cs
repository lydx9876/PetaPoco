using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PetaPoco.Extensions
{
    public static class ConvertExtension
    {
        /// <summary>转换为相应的整数</summary>
		/// <typeparam name="T">数据类型</typeparam>
		/// <param name="obj">要转换的对象</param>
		/// <param name="defval">转换不成功时的默认值</param>
		/// <returns>相应的整数或默认值</returns>
		public static int ToInt_<T>(this T obj, int defval = 0)
        {
            if (null == obj) return defval;
            try { return Convert.ToInt32(obj); }
            catch { return obj.ToString().ToInt_(defval); }
        }
    }
}

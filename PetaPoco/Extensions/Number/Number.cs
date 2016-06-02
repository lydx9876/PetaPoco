using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetaPoco.Extensions
{
    public static class Number
    {
        /// <summary>计算记录总页数</summary>
		/// <param name="recordCount">记录数量</param>
		/// <param name="pageSize">每页记录数</param>
		/// <returns>总页数</returns>
		public static int GetPages_(int recordCount, int pageSize)
        {
            if (recordCount < 1 || pageSize < 1)
                return 1;
            return (int)Math.Ceiling((double)recordCount / (double)pageSize);
        }

        /// <summary>限制数字不大于指定值</summary>
		/// <param name="num">要限制的数字</param>
		/// <param name="value">指定的值</param>
		/// <returns>位于范围内的数字</returns>
		public static int NotGreaterThan_(this int num, int value)
        {
            return num > value ? value : num;
        }
    }
}

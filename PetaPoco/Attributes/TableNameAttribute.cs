using System;

namespace PetaPoco
{
    /// <summary>
    /// 指定数据表名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        /// <summary>
        /// 获取数据表名称
        /// </summary>
        /// <returns>
        /// 数据表名称
        /// </returns>
        public string Value { get; private set; }

        /// <summary>
        /// 初始化一个新 <see cref="TableNameAttribute" /> 实例。
        /// </summary>
        /// <param name="tableName">数据表名称.</param>
        public TableNameAttribute(string tableName)
        {
            Value = tableName;
        }
    }
}
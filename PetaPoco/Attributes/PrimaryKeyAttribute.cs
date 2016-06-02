using System;

namespace PetaPoco
{
    /// <summary>
    /// 标明实体中作为数据表主键的属性.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PrimaryKeyAttribute : Attribute
    {
        /// <summary>
        /// 获取主键名称
        /// </summary>
        /// <returns>
        /// 获取主键名称
        /// </returns>
        public string Value { get; private set; }

        /// <summary>
        /// 获取或设置作为表自增主键的序列字段名
        /// </summary>
        /// <returns>
        /// 获取或设置作为表自增主键的序列字段名.
        /// </returns>
        public string SequenceName { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示是否为自增主键
        /// </summary>
        /// <returns>
        /// 如果为自增主键，则该值为 <c>true</c>；否则为 <c>false</c>。
        /// </returns>
        public bool AutoIncrement { get; set; }

        /// <summary>
        /// 初始化一个新 <see cref="PrimaryKeyAttribute" /> 实例。
        /// </summary>
        /// <param name="primaryKey">主键名称.</param>
        public PrimaryKeyAttribute(string primaryKey)
        {
            Value = primaryKey;
            AutoIncrement = true;
        }
    }
}
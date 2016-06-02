using System;

namespace PetaPoco
{
    /// <summary>
    /// 表示此实体按所有标明为字段的属性进行映射
    /// <seealso cref="ColumnAttribute" /> 或者 <seealso cref="ResultColumnAttribute" />.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ExplicitColumnsAttribute : Attribute
    {
    }
}
using System.Reflection;

namespace PetaPoco
{
    /// <summary>
    /// 数据表字段信息
    /// </summary>
    /// <remarks>
    ///     Typically ColumnInfo is automatically populated from the attributes on a POCO object and it's properties. It can
    ///     however also be returned from the IMapper interface to provide your owning bindings between the DB and your POCOs.
    /// </remarks>
    public class ColumnInfo
    {
        /// <summary>
        /// 数据表字段名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 如果此字段是从数据库中返回的一个计算量，则将不会用于插入和更新操作
        /// </summary>
        /// <value>如果此字段是从数据库中返回的一个计算量，则该值为 <c>true</c>；否则为 <c>false</c>。</value>
        public bool ResultColumn { get; set; }

        /// <summary>
        /// 如果此字段是从数据库中的日期和时间，指定是否强制转换为 UTC
        /// </summary>
		/// <value>如果要强制转换为 UTC，则该值为 <c>true</c>；否则为 <c>false</c>。</value>
        public bool ForceToUtc { get; set; }

        /// <summary>
        /// 将实体属性转换为字段信息
        /// </summary>
        /// <param name="propertyInfo">实体属性</param>
        /// <returns>字段信息</returns>
        public static ColumnInfo FromProperty(PropertyInfo propertyInfo)
        {
            // Check if declaring poco has [Explicit] attribute
            var explicitColumns =
                propertyInfo.DeclaringType.GetCustomAttributes(typeof(ExplicitColumnsAttribute), true).Length > 0;

            // Check for [Column]/[Ignore] Attributes
            var colAttrs = propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), true);
            if (explicitColumns)
            {
                if (colAttrs.Length == 0)
                    return null;
            }
            else
            {
                if (propertyInfo.GetCustomAttributes(typeof(IgnoreAttribute), true).Length != 0)
                    return null;
            }

            var ci = new ColumnInfo();

            // Read attribute
            if (colAttrs.Length > 0)
            {
                var colattr = (ColumnAttribute) colAttrs[0];

                ci.ColumnName = colattr.Name == null ? propertyInfo.Name : colattr.Name;
                ci.ForceToUtc = colattr.ForceToUtc;
                if ((colattr as ResultColumnAttribute) != null)
                    ci.ResultColumn = true;
            }
            else
            {
                ci.ColumnName = propertyInfo.Name;
                ci.ForceToUtc = false;
                ci.ResultColumn = false;
            }

            return ci;
        }
    }
}
namespace PetaPoco
{
    /// <summary>
    /// 包装对应于 DBType.AnsiString 的 ANSI 字符串
    /// </summary>
    public class AnsiString
    {
        /// <summary>
        /// 字符串值
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// 初始化一个新 <see cref="AnsiString" /> 实例。
        /// </summary>
        /// <param name="str">要包装成 ANSI 字符串的 C# 字符串</param>
        public AnsiString(string str)
        {
            Value = str;
        }
    }
}
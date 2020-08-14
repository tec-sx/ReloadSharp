using System;
using System.ComponentModel;
using System.Reflection;

namespace Reload.Core.Extensions
{
    /// <summary>
    /// All enum extensions are found in this class.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets a string value specified in the <see cref="DescriptionAttribute"/> 
        /// from the <see cref="System.ComponentModel"/> namespace. If no attribute is provided
        /// the default <see cref="Enum.ToString()"/> value is returned.
        /// </summary>
        /// <param name="value">The enumeration value.</param>
        /// <returns>A string.</returns>
        public static string GetDescription<T>(this T value) where T : struct
        {
            MemberInfo[] memberInfo = value.GetType().GetMember(value.ToString());
            
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return value.ToString();
        }
    }
}

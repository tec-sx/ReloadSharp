#region copyright
/*
-----------------------------------------------------------------------------
Copyright (c) 2020 Ivan Trajchev

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
-----------------------------------------------------------------------------
*/
#endregion
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

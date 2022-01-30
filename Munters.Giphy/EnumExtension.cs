using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munters.Giphy
{
    public static class EnumExtension
    {
        public static string GetMemberAttr(this Enum enumItem)
        {
            var memInfo = enumItem.GetType().GetMember(enumItem.ToString());
            var attr = memInfo[0].GetCustomAttributes(false);
            return attr == null || attr.Length == 0 ? null : ((System.Runtime.Serialization.EnumMemberAttribute)attr[0]).Value;
        }
    }
}

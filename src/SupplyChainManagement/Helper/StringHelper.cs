using System.Globalization;
using System.Numerics;
using System.Text.RegularExpressions;

namespace SupplyChainManagement.Helper
{
    public static class StringHelper
    {
        public static string ToTitleCase(this string str)
        {
            var textinfo = new CultureInfo("vi-VN", false).TextInfo;
            return textinfo.ToTitleCase(str);
        }

        public static string ToPhoneString(this string phone)
        {
            Regex regex = new(@"[^\d]");
            phone = regex.Replace(phone, "");
            return $"{phone[..4]} {phone.Substring(4, 3)} {phone[7..]}";
        }
    }
}

using System.Text;

namespace TenantAPI.Extensions
{
    public static class StringExtensions
    {
        public static string GetGuid(this string text)
        {
            var bytes = new byte[16];
            int maxBytes = Encoding.UTF8.GetMaxByteCount(text.Length);

            if (maxBytes == 16)
                Encoding.UTF8.GetBytes(text, 0, text.Length, bytes, 0);
            else if (maxBytes < 16)
            {
                Encoding.UTF8.GetBytes(text, 0, text.Length, bytes, 0);
                for (int i = maxBytes; i < 16; i++)
                {
                    bytes[i] = 0;
                }
            }
            else if (maxBytes > 16)
            {
                var temp = new byte[maxBytes];
                bytes = temp.Take(16).ToArray();
            }

            return new Guid(bytes).ToString();
        }
    }
}

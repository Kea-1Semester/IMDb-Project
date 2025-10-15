using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeedData.Handlers
{
    public static class MySqlGuidHelper
    {
        // Swap to MySQL UUID_TO_BIN(UUID(), 1) format
        public static byte[] ToMySqlOptimizedBinary(Guid guid)
        {
            var bytes = guid.ToByteArray();
            SwapMySqlUuidBytes(bytes);
            return bytes;
        }

        // Swap from MySQL UUID_TO_BIN(UUID(), 1) format to .NET Guid
        public static Guid FromMySqlOptimizedBinary(byte[] bytes)
        {
            var copy = (byte[])bytes.Clone();
            SwapMySqlUuidBytes(copy);
            return new Guid(copy);
        }

        // Swaps the bytes as MySQL does with UUID_TO_BIN(UUID(), 1)
        private static void SwapMySqlUuidBytes(byte[] bytes)
        {
            // Swap first 4 bytes
            Array.Reverse(bytes, 0, 4);
            // Swap next 2 bytes
            Array.Reverse(bytes, 4, 2);
            // Swap next 2 bytes
            Array.Reverse(bytes, 6, 2);
            // The remaining 8 bytes stay the same
        }
    }
}

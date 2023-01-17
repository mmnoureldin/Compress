using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compress
{
    internal class Program
    {
        static void Main(string[] args)
        {

            byte[] dataToCompress = File.ReadAllBytes(args[0]);
            byte[] compressedData = Compress(dataToCompress);

            for (int i = 0; i < compressedData.Length; i++)
            {

                Console.Write("0x{0:X2},", compressedData[i]);
            }

            string compressedString = Encoding.UTF8.GetString(compressedData);
            Console.WriteLine("Length of compressed string: " + compressedString.Length);
            byte[] decompressedData = Decompress(compressedData);
            string deCompressedString = Encoding.UTF8.GetString(decompressedData);
            Console.WriteLine("Length of decompressed string: " + deCompressedString.Length);
        }

        public static byte[] Compress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                {
                    gzipStream.Write(bytes, 0, bytes.Length);
                }
                return memoryStream.ToArray();
            }
        }

        public static byte[] Decompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {

                using (var outputStream = new MemoryStream())
                {
                    using (var decompressStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        decompressStream.CopyTo(outputStream);
                    }
                    return outputStream.ToArray();
                }
            }
        }
    }
}

namespace Squire.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Squire.Validation;

    public static class BlobExtensions
    {
        public static string ReadAllText(this IBlob blob)
        {
            blob.VerifyParam("blob").IsNotNull();
            var text = "";
            blob.PerformRead(s =>
                {
                    using (var sr = new StreamReader(s))
                    {
                        text = sr.ReadToEnd();
                    }
                });

            return text;
        }

        public static IEnumerable<string> ReadAllLines(this IBlob blob)
        {
            blob.VerifyParam("blob").IsNotNull();
            var lines = new List<string>();
            blob.PerformRead(s =>
            {
                using (var reader = new StreamReader(s))
                {
                    while (!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                }
            });

            return lines;
        }

        public static byte[] ReadAllBytes(this IBlob blob)
        {
            blob.VerifyParam("blob").IsNotNull();
            var result = new List<byte>();
            var buf = new byte[1024];
            blob.PerformRead(stream =>
                {
                    try
                    {
                        var read = stream.Read(buf, 0, buf.Length);
                        while(read > 0)
                        {
                            result.AddRange(buf.Take(read));
                            read = stream.Read(buf, 0, buf.Length);
                        }
                    }
                    catch (EndOfStreamException)
                    {
                    }
                });

            return result.ToArray();
        }

        public static void WriteAllText(this IBlob blob, string text)
        {
            blob.VerifyParam("blob").IsNotNull();
            text.VerifyParam("text").IsNotNull();
            blob.PerformWrite(s =>
            {
                using (var sw = new StreamWriter(s, Encoding.UTF8))
                {
                    sw.Write(text);
                }
            });
        }
    }
}

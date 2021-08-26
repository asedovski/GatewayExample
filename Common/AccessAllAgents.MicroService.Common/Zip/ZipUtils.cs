using System.IO;
using System.IO.Compression;

namespace AccessAllAgents.MicroService.Common.Zip
{
    public static class ZipUtils
    {
        public static Stream ZipFile(string filePath)
        {
            var zipStream = new MemoryStream
            {
                Position = 0
            };

            using (ZipArchive zip = new ZipArchive(zipStream, ZipArchiveMode.Create, false))
            {
                ZipArchiveEntry entry = zip.CreateEntry(filePath);
                using (Stream entryStream = entry.Open())
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    streamReader.BaseStream.CopyTo(entryStream);
                }
            }
            
            zipStream.Position = 0;
            return zipStream;
        }

    }
}

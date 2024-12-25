using System.Reflection.Metadata;
using Xceed.Words.NET;

namespace ConvertToPdf.API.Helpers
{
    public static class WordHelper
    {
        public static byte[] ReplaceWords(string path, Dictionary<string, string> replacements)
        {
            using (DocX document = DocX.Load(path))
            {
                foreach (var replacement in replacements)
                {
                    document.ReplaceText(replacement.Key, replacement.Value);
                }

                using (var memoryStream = new MemoryStream())
                {
                    document.SaveAs(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}

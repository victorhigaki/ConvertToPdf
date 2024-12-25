using System.Diagnostics;

namespace ConvertToPdf.API.Helpers
{
    public static class PdfHelper
    {
        public static byte[] ConvertToPdf(string inputFilePath = null, byte[] inputFileBytes = null)
        {
            if (string.IsNullOrEmpty(inputFilePath) && inputFileBytes == null)
            {
                throw new ArgumentNullException("É necessário fornecer o caminho do arquivo ou bytes do arquivo.");
            }

            if (inputFileBytes != null)
            {
                inputFilePath = Path.GetTempFileName();
                File.WriteAllBytes(inputFilePath, inputFileBytes);
            }

            string outputFilePath = Path.ChangeExtension(inputFilePath, ".pdf");
            var processStartInfo = new ProcessStartInfo
            {
                FileName = "libreoffice",
                Arguments = $"--headless --convert-to pdf --outdir {Path.GetDirectoryName(outputFilePath)} {inputFilePath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            using (var process = Process.Start(processStartInfo))
            {
                process.WaitForExit();
                var error = process.StandardOutput.ReadToEnd();
                if (process.ExitCode != 0)
                {
                    throw new InvalidOperationException($"Erro ao converter o arquivo: {error}");
                }
            }

            var pdfBytes = File.ReadAllBytes(outputFilePath);

            if (inputFileBytes != null && File.Exists(inputFilePath))
            {
                File.Delete(inputFilePath);
            }

            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }

            return pdfBytes;
        }
    }
}

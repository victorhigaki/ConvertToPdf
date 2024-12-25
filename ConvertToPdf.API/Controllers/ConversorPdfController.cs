using ConvertToPdf.API.Helpers;
using ConvertToPdf.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConvertToPdf.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConversorPdfController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> ConverterArquivo()
        {
            var basePath = Directory.GetCurrentDirectory();
            var arquivo = Path.Combine(basePath, "uploads", "teste.docx");
            var arquivoPdf = PdfHelper.ConvertToPdf(arquivo);
            return File(arquivoPdf, "application/pdf", "teste.pdf");
        }

        [HttpPost("sobremim")]
        public async Task<ActionResult> SobreMim(Input input)
        {
            var basePath = Directory.GetCurrentDirectory();
            var arquivo = Path.Combine(basePath, "uploads", "teste.docx");

            Dictionary<string, string> replacements = new Dictionary<string, string>
            {
                { "#nome#", input.Nome },
                { "#idade#", input.Idade.ToString() },
            };

            var arquivoFinal = WordHelper.ReplaceWords(arquivo, replacements);
            //return File(arquivoFinal, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "teste.docx");
            var arquivoPdf = PdfHelper.ConvertToPdf(inputFileBytes: arquivoFinal);
            return File(arquivoPdf, "application/pdf", 
                "teste.pdf");
        }
    }
}

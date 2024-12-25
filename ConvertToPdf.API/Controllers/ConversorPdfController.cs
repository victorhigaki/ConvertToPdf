using ConvertToPdf.API.Helpers;
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
    }
}

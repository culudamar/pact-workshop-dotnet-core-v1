using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using provider.Models;
using System.Collections.Generic;

namespace provider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturaController : ControllerBase
    {
        private readonly IFaturaOkuyucu faturaOkuyucu;
        public FaturaController(IFaturaOkuyucu faturaOkuyucu)
        {
            this.faturaOkuyucu = faturaOkuyucu;
        }

        [HttpGet()]
        public IActionResult Get(int mbb, string kurum)
        {
            var faturalar = faturaOkuyucu.VeritabanindanOku(mbb, kurum);
            if (faturalar == null || faturalar.Count == 0)
            {
                return NotFound("MBB veya kurum bulunamadı!");
            }

            return Ok(faturalar);
        }
    }
}

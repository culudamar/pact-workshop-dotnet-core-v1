using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace provider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturaController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Get(string mbb, string kurum)
        {
            if (string.IsNullOrEmpty(mbb))
                return BadRequest(new { message = "mbb is required" });

            //if (this.DataMissing())
            //{
            //    return NotFound();
            //}

            if (mbb != "123")
            {
                return NotFound();
            }

            List<object> iskiFaturalari = new List<object>()
            {
                new { mbb = 123, kurum = "ISKI", sonOdemeTarihi = "1.1.2022", faturaNo = "ngut12" },
            };

            List<object> faturalar = new List<object>();

            var ayedasFaturalari = new List<object>()
            {
                new { mbb = 123, kurum = "Ayedas", sonOdemeTarihi = "1.2.2022", faturaNo = "te53bd" },
            };

            faturalar.AddRange(iskiFaturalari);
            faturalar.AddRange(ayedasFaturalari);

            if (string.IsNullOrEmpty(kurum))
                return Ok(faturalar);
            //return new JsonResult(faturalar);

            if (kurum != "ISKI")
            {
                return NotFound();
            }

            return new JsonResult(iskiFaturalari);
        }
    }
}

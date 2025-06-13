using Banka.İs.Soyut;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LimitArtirmaController : ControllerBase
    {
        private ILimitArtirmaServis _limitArtirmaServis; 

        public LimitArtirmaController(ILimitArtirmaServis limitArtirmaServis) 
        {
            _limitArtirmaServis = limitArtirmaServis;
        }


        [Authorize(Roles = "Müşteri")] 
        [HttpGet("kartlimitisteklerigetir")]
        public async Task<IActionResult> KullaniciGetir()
        {
            var sonuc = await _limitArtirmaServis.KartLimitIstekleriGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }
}

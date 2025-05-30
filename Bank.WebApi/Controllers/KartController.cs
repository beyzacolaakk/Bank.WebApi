using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KartController : ControllerBase
    {
        private readonly IKartServis _kartServis;

        public KartController(IKartServis kartServis)
        {
            _kartServis = kartServis;
        }

        [HttpGet("hepsinigetir")]
        public IActionResult HepsiniGetir()
        {
            var sonuc = _kartServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpGet("idilegetir")]
        public IActionResult IdIleGetir(int id)
        {
            var sonuc = _kartServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("ekle")]
        public IActionResult Ekle(Kart kart)
        {
            var sonuc = _kartServis.Ekle(kart);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("guncelle")]
        public IActionResult Guncelle(Kart kart)
        {
            var sonuc = _kartServis.Guncelle(kart);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("sil")]
        public IActionResult Sil(Kart kart)
        {
            var sonuc = _kartServis.Sil(kart);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }
}

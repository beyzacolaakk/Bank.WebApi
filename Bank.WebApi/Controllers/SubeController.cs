using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubeController : ControllerBase
    {
        private readonly ISubeServis _subeServis;

        public SubeController(ISubeServis subeServis)
        {
            _subeServis = subeServis;
        }

        [HttpGet("hepsinigetir")]
        public IActionResult HepsiniGetir()
        {
            var sonuc = _subeServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpGet("idilegetir")]
        public IActionResult IdIleGetir(int id)
        {
            var sonuc = _subeServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("ekle")]
        public IActionResult Ekle(Sube sube)
        {
            var sonuc = _subeServis.Ekle(sube);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("guncelle")]
        public IActionResult Guncelle(Sube sube)
        {
            var sonuc = _subeServis.Guncelle(sube);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("sil")]
        public IActionResult Sil(Sube sube)
        {
            var sonuc = _subeServis.Sil(sube);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }
}


using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GirisOlayiController : ControllerBase
    {
        private readonly IGirisOlayiServis _girisOlayiServis;

        public GirisOlayiController(IGirisOlayiServis girisOlayiServis)
        {
            _girisOlayiServis = girisOlayiServis;
        }

        [HttpGet("hepsinigetir")]
        public IActionResult HepsiniGetir()
        {
            var sonuc = _girisOlayiServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpGet("idilegetir")]
        public IActionResult IdIleGetir(int id)
        {
            var sonuc = _girisOlayiServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("ekle")]
        public IActionResult Ekle(GirisOlayi girisOlayi)
        {
            var sonuc = _girisOlayiServis.Ekle(girisOlayi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("guncelle")]
        public IActionResult Guncelle(GirisOlayi girisOlayi)
        {
            var sonuc = _girisOlayiServis.Guncelle(girisOlayi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("sil")]
        public IActionResult Sil(GirisOlayi girisOlayi)
        {
            var sonuc = _girisOlayiServis.Sil(girisOlayi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

    }
}

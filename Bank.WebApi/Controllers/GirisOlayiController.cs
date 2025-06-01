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
        public async Task<IActionResult> HepsiniGetir()
        {
            var sonuc = await _girisOlayiServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [HttpGet("idilegetir/{id}")]
        public async Task<IActionResult> IdIleGetir([FromRoute] int id)
        {
            var sonuc = await _girisOlayiServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] GirisOlayi girisOlayi)
        {
            var sonuc =await _girisOlayiServis.Ekle(girisOlayi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPut("guncelle")]
        public async Task<IActionResult> Guncelle([FromBody] GirisOlayi girisOlayi)
        {
            var sonuc = await _girisOlayiServis.Guncelle(girisOlayi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpDelete("sil")]
        public async Task<IActionResult> Sil([FromBody] GirisOlayi girisOlayi)
        {
            var sonuc = await _girisOlayiServis.Sil(girisOlayi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

    }
}

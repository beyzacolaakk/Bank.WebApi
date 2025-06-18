using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Yönetici")]
        [HttpGet("hepsinigetir")]
        public async Task<IActionResult> HepsiniGetir()
        {
            var sonuc = await _girisOlayiServis.HepsiniGetir("Zaman",true);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpGet("idilegetir/{id}")]
        public async Task<IActionResult> IdIleGetir([FromRoute] int id)
        {
            var sonuc = await _girisOlayiServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] GirisOlayi girisOlayi)
        {
            var sonuc =await _girisOlayiServis.Ekle(girisOlayi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpPut("guncelle")]
        public async Task<IActionResult> Guncelle([FromBody] GirisOlayi girisOlayi)
        {
            var sonuc = await _girisOlayiServis.Guncelle(girisOlayi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
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

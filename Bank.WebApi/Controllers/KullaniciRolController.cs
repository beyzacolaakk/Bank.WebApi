using Banka.Cekirdek.Varlıklar.Somut;
using Banka.İs.Soyut;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciRolController : ControllerBase
    {
        private readonly IKullaniciRolServis _kullaniciRolServis;

        public KullaniciRolController(IKullaniciRolServis kullaniciRolServis)
        {
            _kullaniciRolServis = kullaniciRolServis;
        }

        [Authorize(Roles = "Yönetici")]
        [HttpGet("hepsinigetir")]
        public async Task<IActionResult> HepsiniGetir()
        {
            var sonuc = await _kullaniciRolServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpGet("idilegetir/{id}")]
        public async Task<IActionResult> IdIleGetir([FromRoute] int id)
        {
            var sonuc = await _kullaniciRolServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] KullaniciRol kullaniciRol)
        {
            var sonuc =await _kullaniciRolServis.Ekle(kullaniciRol);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpPut("guncelle")]
        public async Task<IActionResult> Guncelle([FromBody] KullaniciRol kullaniciRol)
        {
            var sonuc = await _kullaniciRolServis.Guncelle(kullaniciRol);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpDelete("sil")]
        public async Task<IActionResult> Sil([FromBody] KullaniciRol kullaniciRol)
        {
            var sonuc = await _kullaniciRolServis.Sil(kullaniciRol);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }
}

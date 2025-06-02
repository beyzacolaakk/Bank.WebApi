using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KayitController : ControllerBase
    {
        private readonly IKayitServis _kayitServis;

        public KayitController(IKayitServis kayitServis)
        {
            _kayitServis = kayitServis;
        }
        [Authorize(Roles = "Yönetici")]
        [HttpGet("hepsinigetir")]
        public async Task<IActionResult> HepsiniGetir()
        {
            var sonuc = await Task.Run(() => _kayitServis.HepsiniGetir());
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri")]
        [HttpGet("idilegetir/{id}")]
        public async Task<IActionResult> IdIleGetir([FromRoute] int id)
        {
            var sonuc = await Task.Run(() => _kayitServis.IdIleGetir(id));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri")]
        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] Kayit kayit)
        {
            var sonuc = await Task.Run(() => _kayitServis.Ekle(kayit));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri")]
        [HttpPut("guncelle")]
        public async Task<IActionResult> Guncelle([FromBody] Kayit kayit)
        {
            var sonuc = await Task.Run(() => _kayitServis.Guncelle(kayit));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri")]
        [HttpDelete("sil")]
        public async Task<IActionResult> Sil([FromBody] Kayit kayit)
        {
            var sonuc = await Task.Run(() => _kayitServis.Sil(kayit));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }

}

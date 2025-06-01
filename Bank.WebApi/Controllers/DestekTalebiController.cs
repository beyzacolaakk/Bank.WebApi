using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestekTalebiController : BaseController
    {
        private readonly IDestekTalebiServis _destekTalebiServis;

        public DestekTalebiController(IDestekTalebiServis destekTalebiServis)
        {
            _destekTalebiServis = destekTalebiServis;
        }
        [Authorize(Roles = "Müşteri")]
        [HttpGet("hepsinigetir")]
        public async Task<IActionResult> HepsiniGetir()
        {
            var sonuc =await _destekTalebiServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpGet("idilegetir/{id}")]
        public async Task<IActionResult> IdIleGetir([FromRoute] int id)
        {
            var sonuc = await _destekTalebiServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] DestekTalebi destekTalebi)
        {
            var sonuc = await _destekTalebiServis.Ekle(destekTalebi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPut("guncelle")]
        public async Task<IActionResult> Guncelle([FromBody] DestekTalebi destekTalebi)
        {
            var sonuc = await _destekTalebiServis.Guncelle(destekTalebi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpDelete("sil")]
        public async Task<IActionResult> Sil([FromBody] DestekTalebi destekTalebi)
        {
            var sonuc = await _destekTalebiServis.Sil(destekTalebi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }
}

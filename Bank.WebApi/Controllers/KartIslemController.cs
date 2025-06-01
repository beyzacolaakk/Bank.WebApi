using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KartIslemController : ControllerBase
    {
        private readonly IKartIslemServis _kartIslemServis;

        public KartIslemController(IKartIslemServis kartIslemServis)
        {
            _kartIslemServis = kartIslemServis;
        }

        [HttpGet("hepsinigetir")]
        public async Task<IActionResult> HepsiniGetir()
        {
            var sonuc = await Task.Run(() => _kartIslemServis.HepsiniGetir());
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpGet("idilegetir/{id}")]
        public async Task<IActionResult> IdIleGetir([FromRoute] int id)
        {
            var sonuc = await Task.Run(() => _kartIslemServis.IdIleGetir(id));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] KartIslem kartIslem)
        {
            var sonuc = await Task.Run(() => _kartIslemServis.Ekle(kartIslem));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPut("guncelle")]
        public async Task<IActionResult> Guncelle([FromBody] KartIslem kartIslem)
        {
            var sonuc = await Task.Run(() => _kartIslemServis.Guncelle(kartIslem));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpDelete("sil")]
        public async Task<IActionResult> Sil([FromBody] KartIslem kartIslem)
        {
            var sonuc = await Task.Run(() => _kartIslemServis.Sil(kartIslem));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }

}

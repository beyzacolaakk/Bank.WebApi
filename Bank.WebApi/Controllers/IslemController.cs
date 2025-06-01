using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IslemController : ControllerBase
    {
        private readonly IIslemServis _islemServis;

        public IslemController(IIslemServis islemServis)
        {
            _islemServis = islemServis;
        }

        [HttpGet("hepsinigetir")]
        public async Task<IActionResult> HepsiniGetir()
        {
            var sonuc = await _islemServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpGet("idilegetir/{id}")]
        public async Task<IActionResult> IdIleGetir([FromRoute] int id)
        {
            var sonuc = await _islemServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] Islem islem)
        {
            var sonuc = await Task.Run(() => _islemServis.Ekle(islem));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPut("guncelle")]
        public async Task<IActionResult> Guncelle([FromBody] Islem islem)
        {
            var sonuc = await Task.Run(() => _islemServis.Guncelle(islem));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpDelete("sil")]
        public async Task<IActionResult> Sil([FromBody] Islem islem)
        {
            var sonuc = await Task.Run(() => _islemServis.Sil(islem));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }

}

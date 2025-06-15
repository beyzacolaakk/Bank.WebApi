using Banka.İs.Somut;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KartIslemController :BaseController
    {
        private readonly IKartIslemServis _kartIslemServis;

        public KartIslemController(IKartIslemServis kartIslemServis)
        {
            _kartIslemServis = kartIslemServis;
        }
        [Authorize(Roles = "Yönetici")]
        [HttpGet("hepsinigetir")]
        public async Task<IActionResult> HepsiniGetir()
        {
            var sonuc = await Task.Run(() => _kartIslemServis.HepsiniGetir());
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpGet("idilegetir/{id}")]
        public async Task<IActionResult> IdIleGetir([FromRoute] int id)
        {
            var sonuc = await Task.Run(() => _kartIslemServis.IdIleGetir(id));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] KartIslem kartIslem)
        {
            var sonuc = await Task.Run(() => _kartIslemServis.Ekle(kartIslem));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPut("guncelle")]
        public async Task<IActionResult> Guncelle([FromBody] KartIslem kartIslem)
        {
            var sonuc = await Task.Run(() => _kartIslemServis.Guncelle(kartIslem));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpDelete("sil")]
        public async Task<IActionResult> Sil([FromBody] KartIslem kartIslem)
        {
            var sonuc = await Task.Run(() => _kartIslemServis.Sil(kartIslem));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpGet("son4islemgetir")]
        public async Task<IActionResult> Son4IslemGetir()  
        {
            int KullaniciId = TokendanIdAl();
            var sonuc = await Task.Run(() => _kartIslemServis.KullaniciyaAitSon4KartIslemiGetir(KullaniciId)); 
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPost("kartileislemyap")]
        public async Task<IActionResult> KartIleIslemYap([FromBody] KartIleIslemDto kartIleIslemDto)
        {
            kartIleIslemDto.KullaniciId = TokendanIdAl();
            var sonuc = await Task.Run(() => _kartIslemServis.KartIleIslemYap(kartIleIslemDto));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }

}

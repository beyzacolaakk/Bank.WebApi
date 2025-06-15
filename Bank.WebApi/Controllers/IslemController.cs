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
    public class IslemController :BaseController
    {
        private readonly IIslemServis _islemServis;

        public IslemController(IIslemServis islemServis)
        {
            _islemServis = islemServis;
        }
        [Authorize(Roles = "Yönetici")]
        [HttpGet("hepsinigetir")]
        public async Task<IActionResult> HepsiniGetir()
        {
            var sonuc = await _islemServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpGet("idilegetir/{id}")]
        public async Task<IActionResult> IdIleGetir([FromRoute] int id)
        {
            var sonuc = await _islemServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpGet("son4islemgetir")]
        public async Task<IActionResult> Son4IslemGetir()  
        {
            var KullaniciId = TokendanIdAl();
            var sonuc = await _islemServis.KullaniciyaAitSon4KartIslemiGetir(KullaniciId);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPost("paragonderme")] 
        public async Task<IActionResult> ParaGonderme([FromBody] ParaGondermeDto paraGondermeDto) 
        {
            paraGondermeDto.KullaniciId = TokendanIdAl();
            var sonuc = await Task.Run(() => _islemServis.ParaGonderme(paraGondermeDto));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] Islem islem)
        {
            var sonuc = await Task.Run(() => _islemServis.Ekle(islem));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPost("paracekyatir")]
        public async Task<IActionResult> ParaCekYatir([FromBody] ParaCekYatirDto paraCekYatirDto) 
        {
            paraCekYatirDto.KullaniciId = TokendanIdAl();
            var sonuc = await Task.Run(() => _islemServis.ParaCekYatir(paraCekYatirDto));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPut("guncelle")]
        public async Task<IActionResult> Guncelle([FromBody] Islem islem)
        {
            var sonuc = await Task.Run(() => _islemServis.Guncelle(islem));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
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

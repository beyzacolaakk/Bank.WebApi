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
    public class KartController : BaseController
    {
        private readonly IKartServis _kartServis;

        public KartController(IKartServis kartServis)
        {
            _kartServis = kartServis;
        }
        [Authorize(Roles = "Yönetici")]
        [HttpGet("hepsinigetir")]
        public async Task<IActionResult> HepsiniGetir()
        {
            var sonuc = await Task.Run(() => _kartServis.HepsiniGetir());
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpGet("idilegetir/{id}")]
        public async Task<IActionResult> IdIleGetir([FromRoute] int id)
        {
            var sonuc = await Task.Run(() => _kartServis.KartIstekleriIdIleGetir(id));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPost("otomatikkartolustur")]
        public async Task<IActionResult> OtomatikKartOlustur(KartOlusturDto kartOlusturDto)
        {
             kartOlusturDto.KullaniciId= TokendanIdAl();
            var sonuc=await _kartServis.OtomatikKartOlustur(kartOlusturDto);
            if (sonuc.Success)
            {
                return Ok(sonuc);
            }
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpGet("idilehepsinigetir")]
        public async Task<IActionResult> IdIleHepsiniGetir() 
        {
            int KullaniciId = TokendanIdAl();
            var sonuc = await _kartServis.IdIleHepsiniGetir(KullaniciId);
            if (sonuc.Success)
            {
                return Ok(sonuc);
            }
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpGet("kartistekgetir")]
        public async Task<IActionResult> KartIstekGetir() 
        {
            var sonuc = await _kartServis.KartIstekleriGetir();
            if (sonuc.Success)
            {
                return Ok(sonuc);
            }
            return BadRequest(sonuc);
        }

        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] Kart kart)
        {
            var sonuc = await Task.Run(() => _kartServis.Ekle(kart));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPut("guncelle")]
        public async Task<IActionResult> Guncelle([FromBody] Kart kart)
        {
            var sonuc = await Task.Run(() => _kartServis.Guncelle(kart));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpDelete("sil")]
        public async Task<IActionResult> Sil([FromBody] Kart kart)
        {
            var sonuc = await Task.Run(() => _kartServis.Sil(kart));
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpPut("kartdurumguncelle")] 
        public async Task<IActionResult> KartDurumGuncelle([FromBody] DurumuGuncelleDto durumGuncelleDto) 
        {
            var sonuc = await _kartServis.KartDurumGuncelle(durumGuncelleDto);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }

}

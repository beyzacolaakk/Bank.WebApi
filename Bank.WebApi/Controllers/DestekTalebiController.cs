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
    public class DestekTalebiController : BaseController
    {
        private readonly IDestekTalebiServis _destekTalebiServis;

        public DestekTalebiController(IDestekTalebiServis destekTalebiServis)
        {
            _destekTalebiServis = destekTalebiServis;
        }
        [Authorize(Roles = "Yönetici")]
        [HttpGet("hepsinigetir")]
        public async Task<IActionResult> HepsiniGetir()
        {
            var sonuc =await _destekTalebiServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpGet("idilehepsinigetir")]
        public async Task<IActionResult> IdIleHepsiniGetir() 
        {
            int KullaniciId = TokendanIdAl();
            var sonuc = await _destekTalebiServis.IdIleHepsiniGetir(KullaniciId);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpGet("idilegetir/{id}")]
        public async Task<IActionResult> IdIleGetir([FromRoute] int id)
        {
            var sonuc = await _destekTalebiServis.IdIleGetirDestekTalebi(id); 
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpGet("destekisteklerigetir")]
        public async Task<IActionResult> DestekIstekleriGetir() 
        {
            var sonuc = await _destekTalebiServis.DestekIstekleriGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] DestekTalebi destekTalebi)
        {
            var sonuc = await _destekTalebiServis.Ekle(destekTalebi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpPut("guncelle")]
        public async Task<IActionResult> Guncelle([FromBody] DestekTalebi destekTalebi)
        {
            var sonuc = await _destekTalebiServis.Guncelle(destekTalebi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpPut("destektalebiguncelle/{id}")]
        public async Task<IActionResult> DestekTalebiGuncelle([FromRoute] int id)  
        {
            var sonuc = await _destekTalebiServis.DestekTalebiGuncelle(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPost("destektalebiolustur")]
        public async Task<IActionResult> DestekTalebiOlustur([FromBody] DestekTalebiOlusturDto destekTalebiOlusturDto) 
        {
            destekTalebiOlusturDto.KullaniciId = TokendanIdAl();
            var sonuc = await _destekTalebiServis.DestekTalebiOlustur(destekTalebiOlusturDto);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpDelete("sil/{id}")]
        public async Task<IActionResult> Sil([FromRoute] int id)
        {
            var sonuc = await _destekTalebiServis.Sil(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpPut("destektalebiguncelle")]
        public async Task<IActionResult> DestekTalebiDurumGuncelle([FromBody] DestekTalebiGuncelleDto destekTalebiGuncelle)  
        {
            var sonuc = await _destekTalebiServis.DestekTalebiDurumGuncelle(destekTalebiGuncelle);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpGet("filtre")]
        public async Task<IActionResult> Filtrele([FromQuery] string durum = "tum", [FromQuery] string arama = "")
        {
            var id = TokendanIdAl(); 
            var result = await _destekTalebiServis.DestekTalebiFiltre(id, durum, arama);

            if (result.Success)
            {
                return Ok(result.Data); 
            }

            return BadRequest(result.Message); 
        }
    }
}

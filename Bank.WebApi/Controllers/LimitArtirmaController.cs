using Banka.İs.Somut;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LimitArtirmaController : ControllerBase
    {
        private ILimitArtirmaServis _limitArtirmaServis; 

        public LimitArtirmaController(ILimitArtirmaServis limitArtirmaServis) 
        {
            _limitArtirmaServis = limitArtirmaServis;
        }


        [Authorize(Roles = "Yönetici")]
        [HttpGet("kartlimitisteklerigetir")]
        public async Task<IActionResult> KullaniciGetir()
        {
            var sonuc = await _limitArtirmaServis.KartLimitIstekleriGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPost("limitartirmaekle")]
        public async Task<IActionResult> LimitArtirmaEkle([FromBody] LimitArtirmaTalepDto limitArtirma) 
        {
            var sonuc = await _limitArtirmaServis.LimitArtirmEkle(limitArtirma);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpPost("limitguncelle")] 
        public async Task<IActionResult> LimitGuncelle([FromBody] LimitArtirmaEkleDto limitArtirma) 
        {
            var sonuc = await _limitArtirmaServis.KartLimitIstekGuncelle(limitArtirma);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Yönetici")]
        [HttpDelete("sil/{id}")]
        public async Task<IActionResult> Sil([FromRoute] int id) 
        {
            var sonuc = await _limitArtirmaServis.Sil(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpGet("idilegetir/{id}")]
        public async Task<IActionResult> IdIleGetir([FromRoute] int id)
        {
            var sonuc = await _limitArtirmaServis.KartLimitIstekleriGetirIdIle(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }
}

﻿using Banka.İs.Somut;
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
    public class HesapController : BaseController
    {
        private readonly IHesapServis _hesapServis;

        public HesapController(IHesapServis hesapServis)
        {
            _hesapServis = hesapServis;
        }
        [Authorize(Roles = "Yönetici")]
        [HttpGet("hepsinigetir")]
        public async Task<IActionResult> HepsiniGetir()
        {
            var sonuc = await _hesapServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri")]
        [HttpGet("idilegetir/{id}")]
        public async Task<IActionResult> IdIleGetir([FromRoute] int id)
        {
            var sonuc = await _hesapServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri")]
        [HttpGet("idilehepsinigetir")]
        public async Task<IActionResult> IdIleHepsiniGetir()
        {
            int KullaniciId = TokendanIdAl();
            var sonuc = await _hesapServis.IdIleHepsiniGetir(KullaniciId);
            if (sonuc.Success)
            {
                return Ok(sonuc);
            }
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri")]
        [HttpPost("ekle")]
        public async Task<IActionResult> Ekle([FromBody] Hesap hesap)
        {
            var sonuc = await _hesapServis.Ekle(hesap);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri")]
        [HttpGet("varliklargetir")]
        public async Task<IActionResult> VarliklarGetir()
        {
            int KullaniciId = TokendanIdAl();
            var sonuc = await _hesapServis.VarliklariGetirAsync(KullaniciId); 
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri")]
        [HttpPost("otomatikhesapolustur")]
        public async Task<IActionResult> Ekle(HesapOlusturDto hesapOlusturDto)
        {
            hesapOlusturDto.KullaniciId = TokendanIdAl();
            var sonuc = await _hesapServis.OtomatikHesapOlustur(hesapOlusturDto);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri")]
        [HttpPut("guncelle")]
        public async Task<IActionResult> Guncelle([FromBody] Hesap hesap)
        {
            var sonuc = await _hesapServis.Guncelle(hesap);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
        [Authorize(Roles = "Müşteri")]
        [HttpDelete("sil")]
        public async Task<IActionResult> Sil([FromBody] Hesap hesap)
        {
            var sonuc = await _hesapServis.Sil(hesap);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }

}


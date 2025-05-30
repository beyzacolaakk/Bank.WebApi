using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestekTalebiController : BaseController
    {
        IDestekTalebiServis _destekTalebiServis;

        public DestekTalebiController(IDestekTalebiServis destekTalebiServis)
        {
            _destekTalebiServis = destekTalebiServis;
        }
        [HttpGet("hepsinigetir")]
        public IActionResult HepsiniGetir()
        {
            var sonuc = _destekTalebiServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("ekle")]
        public IActionResult Ekle(DestekTalebi destekTalebi)
        {
            var sonuc = _destekTalebiServis.Ekle(destekTalebi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("sil")]
        public IActionResult Sil(DestekTalebi destekTalebi)
        {
            var sonuc = _destekTalebiServis.Sil(destekTalebi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("guncelle")]
        public IActionResult Guncelle(DestekTalebi destekTalebi)
        {
            var sonuc = _destekTalebiServis.Guncelle(destekTalebi);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpGet("idilegetir")]
        public IActionResult IdIleGetir(int id)
        {
            var sonuc = _destekTalebiServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }
}

using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HesapController : ControllerBase
    {
        private readonly IHesapServis _hesapServis;

        public HesapController(IHesapServis hesapServis)
        {
            _hesapServis = hesapServis;
        }

        [HttpGet("hepsinigetir")]
        public IActionResult HepsiniGetir()
        {
            var sonuc = _hesapServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpGet("idilegetir")]
        public IActionResult IdIleGetir(int id)
        {
            var sonuc = _hesapServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("ekle")]
        public IActionResult Ekle(Hesap hesap)
        {
            var sonuc = _hesapServis.Ekle(hesap);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("guncelle")]
        public IActionResult Guncelle(Hesap hesap)
        {
            var sonuc = _hesapServis.Guncelle(hesap);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("sil")]
        public IActionResult Sil(Hesap hesap)
        {
            var sonuc = _hesapServis.Sil(hesap);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }
}


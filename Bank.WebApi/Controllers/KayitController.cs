using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KayitController : ControllerBase
    {
        private readonly IKayitServis _kayitServis;

        public KayitController(IKayitServis kayitServis)
        {
            _kayitServis = kayitServis;
        }

        [HttpGet("hepsinigetir")]
        public IActionResult HepsiniGetir()
        {
            var sonuc = _kayitServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpGet("idilegetir")]
        public IActionResult IdIleGetir(int id)
        {
            var sonuc = _kayitServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("ekle")]
        public IActionResult Ekle(Kayit kayit)
        {
            var sonuc = _kayitServis.Ekle(kayit);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("guncelle")]
        public IActionResult Guncelle(Kayit kayit)
        {
            var sonuc = _kayitServis.Guncelle(kayit);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("sil")]
        public IActionResult Sil(Kayit kayit)
        {
            var sonuc = _kayitServis.Sil(kayit);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }
}

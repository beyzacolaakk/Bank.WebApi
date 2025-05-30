using Banka.Cekirdek.Varlıklar.Somut;
using Banka.İs.Soyut;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciRolController : ControllerBase
    {
        private readonly IKullaniciRolServis _kullaniciRolServis;

        public KullaniciRolController(IKullaniciRolServis kullaniciRolServis)
        {
            _kullaniciRolServis = kullaniciRolServis;
        }

        [HttpGet("hepsinigetir")]
        public IActionResult HepsiniGetir()
        {
            var sonuc = _kullaniciRolServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpGet("idilegetir")]
        public IActionResult IdIleGetir(int id)
        {
            var sonuc = _kullaniciRolServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("ekle")]
        public IActionResult Ekle(KullaniciRol kullaniciRol)
        {
            var sonuc = _kullaniciRolServis.Ekle(kullaniciRol);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("guncelle")]
        public IActionResult Guncelle(KullaniciRol kullaniciRol)
        {
            var sonuc = _kullaniciRolServis.Guncelle(kullaniciRol);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("sil")]
        public IActionResult Sil(KullaniciRol kullaniciRol)
        {
            var sonuc = _kullaniciRolServis.Sil(kullaniciRol);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }
}

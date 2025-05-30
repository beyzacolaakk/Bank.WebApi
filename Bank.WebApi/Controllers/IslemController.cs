using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IslemController : ControllerBase
    {
        private readonly IIslemServis _islemServis;

        public IslemController(IIslemServis islemServis)
        {
            _islemServis = islemServis;
        }

        [HttpGet("hepsinigetir")]
        public IActionResult HepsiniGetir()
        {
            var sonuc = _islemServis.HepsiniGetir();
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpGet("idilegetir")]
        public IActionResult IdIleGetir(int id)
        {
            var sonuc = _islemServis.IdIleGetir(id);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("ekle")]
        public IActionResult Ekle(Islem islem)
        {
            var sonuc = _islemServis.Ekle(islem);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("guncelle")]
        public IActionResult Guncelle(Islem islem)
        {
            var sonuc = _islemServis.Guncelle(islem);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }

        [HttpPost("sil")]
        public IActionResult Sil(Islem islem)
        {
            var sonuc = _islemServis.Sil(islem);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }
    }

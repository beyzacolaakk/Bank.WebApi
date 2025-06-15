using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Somut;
using Banka.İs.Soyut;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : BaseController
    {

        private IKullaniciServis _kullaniciServis;

        public KullaniciController(IKullaniciServis kullaniciServis)
        {
            _kullaniciServis = kullaniciServis;
        }


        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpGet("kullanicigetir")]
        public async Task<IActionResult> KullaniciGetir()  
        {
            var KullaniciId = TokendanIdAl();
            var sonuc = await _kullaniciServis.KullaniciBilgileriGetir(KullaniciId);
            if (sonuc.Success)
                return Ok(sonuc);
            return BadRequest(sonuc);
        }
    }
}

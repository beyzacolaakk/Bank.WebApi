using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Banka.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private IAuthServis _authServis;
        private IKullaniciServis _kullaniciServis;

        public AuthController(IKullaniciServis kullaniciServis, IAuthServis authServis)
        {
            _kullaniciServis = kullaniciServis;
            _authServis=authServis;
        }

        [HttpPost("giris")]
        public async Task<ActionResult> Giris([FromBody] KullaniciGirisDto kullaniciGirisDto)
        {
            var result = await _authServis.GirisVeTokenOlustur(kullaniciGirisDto);

            if (!result.Success)
                return BadRequest(result.Message);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = result.Data.Token.Expiration
            };

            Response.Cookies.Append("AuthToken", result.Data.Token.Token, cookieOptions);

            return Ok(result.Data.Token);
        }

        [HttpPost("kayit")]
        public async Task<ActionResult> Kayit([FromBody] KullaniciKayitDto kullaniciKayitDto)
        {
            var result = await _authServis.KayitIslemi(kullaniciKayitDto);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("kimlikdogrulama")]
        public async Task<ActionResult> KimlikDogrulama() 
        {
            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                var resulter = new ErrorResult("Yetkisiz erişim!");
                return Unauthorized(resulter);
            }

            var result = new SuccessResult("Giriş başarılı.");
            return Ok(result);
        }
        [Authorize(Roles = "Müşteri,Yönetici")]
        [HttpPost("cikis")]
        public async Task<ActionResult> Cikis()
        {
            int id = TokendanIdAl();
            _authServis.Cikis(id);
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                Path = "/"
            };

            Response.Cookies.Append("AuthToken", "", cookieOptions);
            var result = new SuccessResult("Çıkış İşemi Gerçekleşti");
            return Ok(result);
        }


    }
}

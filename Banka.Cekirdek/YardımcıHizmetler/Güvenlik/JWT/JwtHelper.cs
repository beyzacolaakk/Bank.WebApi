using Banka.Cekirdek.Uzantılar;
using Banka.Cekirdek.YardımcıHizmetler.Güvenlik.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Banka.Cekirdek.Varlıklar.Somut;

namespace Banka.Cekirdek.YardımcıHizmetler.Güvenlik.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions")
            .Get<TokenOptions>();

        }
        public AccessToken TokenOlustur(Kullanici kullanici, List<Rol> rol)  
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, kullanici, signingCredentials, rol);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, Kullanici kullanici,
            SigningCredentials signingCredentials, List<Rol> rol)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(kullanici, rol),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(Kullanici kullanici, List<Rol> rol)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(kullanici.Id.ToString());
            claims.AddEmail(kullanici.Email);
            claims.AddName(kullanici.Telefon);
            claims.AddRoles(rol.Select(c => c.RolAdi).ToArray());

            return claims;
        }
    }
    }

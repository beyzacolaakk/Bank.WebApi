using Banka.Cekirdek.YardımcıHizmetler.Güvenlik;
using Banka.Cekirdek.YardımcıHizmetler.Güvenlik.Encryption;
using Banka.Cekirdek.YardımcıHizmetler.Güvenlik.JWT;
using Banka.İs.Somut;
using Banka.İs.Soyut;
using Banka.VeriErisim.Somut.EntityFramework;
using Banka.VeriErisimi.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddHttpClient();

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });
// DAL kayıtları

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<BankaContext>();
builder.Services.AddScoped<IAuthServis, AuthServis>();
builder.Services.AddScoped<IDestekTalebiServis, DestekTalebiServis>();
builder.Services.AddTransient<IGirisOlayiServis, GirisOlayiServis>();
builder.Services.AddScoped<IGirisTokenServis, GirisTokenServis>();
builder.Services.AddScoped<IHesapServis, HesapServis>();
builder.Services.AddScoped<IIslemServis, IslemServis>();
builder.Services.AddScoped<IKartIslemServis, KartIslemServis>();
builder.Services.AddScoped<IKartServis, KartServis>();
builder.Services.AddScoped<IKayitServis, KayitServis>();
builder.Services.AddScoped<IKullaniciRolServis, KullaniciRolServis>();
builder.Services.AddScoped<IKullaniciServis, KullaniciServis>();
builder.Services.AddScoped<ISubeServis, SubeServis>();
builder.Services.AddScoped<IKullaniciDal, EfKullaniciDal>();
builder.Services.AddScoped<IKartDal, EfKartDal>();
builder.Services.AddScoped<IKartIslemDal, EfKartIslemDal>();
builder.Services.AddScoped<IKayitDal, EfKayitDal>();
builder.Services.AddScoped<IHesapDal, EfHesapDal>();
builder.Services.AddScoped<IIslemDal, EfIslemDal>();
builder.Services.AddScoped<ISubeDal, EfSubeDal>();
builder.Services.AddScoped<IKullaniciRolDal, EfKullaniciRolDal>();
builder.Services.AddScoped<IDestekTalebiDal, EfDestekTalebiDal>();
builder.Services.AddScoped<IGirisOlayiDal, EfGirisOlayiDal>();
builder.Services.AddScoped<IGirisTokenDal, EfGirisTokenDal>();
builder.Services.AddSingleton<ITokenHelper, JwtHelper>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

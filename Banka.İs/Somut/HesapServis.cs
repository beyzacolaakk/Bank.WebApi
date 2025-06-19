using Banka.Cekirdek.YardımcıHizmetler.Results;
using Banka.İs.Sabitler;
using Banka.İs.Soyut;
using Banka.Varlıklar.DTOs;
using Banka.Varlıklar.Somut;
using Banka.VeriErisimi.Somut.EntityFramework;
using Banka.VeriErisimi.Soyut;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class HesapServis : IHesapServis
    {
        private readonly IHesapDal _hesapDal;
        private readonly IKartServis _kartServis;

        public HesapServis(IHesapDal hesapDal, IKartServis kartServis)
        {
            _hesapDal = hesapDal;
            _kartServis = kartServis;

        }
        public async Task<IDataResult<List<HesapIstekleriDto>>> HesapIstekleriGetir()
        {
            var hesapIstekleri = await _hesapDal.HesapIstekleriGetir();
            return new SuccessDataResult<List<HesapIstekleriDto>>(hesapIstekleri, Mesajlar.BasariliGetirme);
        }

        public async Task<IDataResult<HesapIstekleriDto>> HesapIstekleriIdIleGetir(int id) 
        {
            var hesapIstekleri = await _hesapDal.HesapIstekleriGetirIdIle(id);
            return new SuccessDataResult<HesapIstekleriDto>(hesapIstekleri, Mesajlar.BasariliGetirme);
        }
        public async Task<IDataResult<IstekSayilariDto>> IsteklSayilariGetir() 
        { 
            var istekSayılar = await _hesapDal.IstekSayilariGetir();
            return new SuccessDataResult<IstekSayilariDto>(istekSayılar, Mesajlar.BasariliGetirme);
        }
        public async Task<IResult> Ekle(Hesap hesap)
        {
            await _hesapDal.Ekle(hesap);
            return new SuccessResult(Mesajlar.EklemeBasarili);
        }

        public async Task<IResult> Guncelle(Hesap hesap)
        {
            await _hesapDal.Guncelle(hesap);
            return new SuccessResult(Mesajlar.GuncellemeBasarili);
        }

        public async Task<IResult> Sil(Hesap hesap)
        {
            await _hesapDal.Sil(hesap);
            return new SuccessResult(Mesajlar.SilmeBasarili);
        }
        public async Task<IResult> OtomatikHesapOlustur(HesapOlusturDto hesapOlusturDto)
        {
            var hesap = new Hesap
            {
                KullaniciId = hesapOlusturDto.KullaniciId,
                HesapNo = HesapNoUret(),
                HesapTipi = hesapOlusturDto.HesapTipi,
                Bakiye = 0,
                Durum= "Beklemede",
                OlusturmaTarihi = DateTime.Now
            };

            await _hesapDal.Ekle(hesap);
            return new SuccessResult("Hesap başarıyla oluşturuldu.");
        }
        public async Task<IDataResult<decimal>> ParaTransferi(string gonderenId, string aliciHesapId, decimal miktar)
        {
            if (miktar <= 0)
                return new ErrorDataResult<decimal>("Transfer miktarı sıfırdan büyük olmalıdır.");

            try
            {
                var aliciResult = await HesapNoIdIleGetir(aliciHesapId);
                if (!aliciResult.Success || aliciResult.Data == null)
                    return new ErrorDataResult<decimal>("Alıcı hesap bulunamadı.");

                // Gönderen hesap mı kart mı belirle
                var gonderenHesapResult = await HesapNoIdIleGetir(gonderenId);
                bool gonderenHesapVar = gonderenHesapResult.Success && gonderenHesapResult.Data != null;

                if (gonderenHesapVar)
                {
                    var gonderen = gonderenHesapResult.Data;
                    var alici = aliciResult.Data;

                    if (gonderen.Id == alici.Id)
                        return new ErrorDataResult<decimal>("Kendinize para transferi yapamazsınız.");

                    if (gonderen.ParaBirimi != alici.ParaBirimi)
                        return new ErrorDataResult<decimal>("Hesaplar farklı para birimi kullanıyor.");

                    if (gonderen.Bakiye < miktar)
                        return new ErrorDataResult<decimal>("Gönderen hesapta yeterli bakiye yok.");

                    gonderen.Bakiye -= miktar;
                    alici.Bakiye += miktar;

                    await _hesapDal.Guncelle(gonderen);
                    await _hesapDal.Guncelle(alici);

                    return new SuccessDataResult<decimal>(gonderen.Bakiye, "Hesaptan hesaba transfer başarılı.");
                }
                else
                {
                    // Gönderen kart mı diye kontrol et
                    var gonderenKartResult = await _kartServis.KartNoIleGetir(gonderenId);
                    if (!gonderenKartResult.Success || gonderenKartResult.Data == null)
                        return new ErrorDataResult<decimal>("Gönderen hesap veya kart bulunamadı.");

                    var gonderenKart = gonderenKartResult.Data;
                    var alici = aliciResult.Data;

                    if (gonderenKart.Limit < miktar)
                        return new ErrorDataResult<decimal>("Kartta yeterli bakiye yok.");

                    gonderenKart.Limit -= miktar;
                    alici.Bakiye += miktar;

                    await _kartServis.Guncelle(gonderenKart);
                    await _hesapDal.Guncelle(alici);

                    return new SuccessDataResult<decimal>(gonderenKart.Limit!.Value, "Karttan hesaba transfer başarılı.");
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<decimal>($"Transfer sırasında bir hata oluştu: {ex.Message}");
            }
        }


        public async Task<IDataResult<decimal>> ParaCekYatir(ParaCekYatirDto paraCekYatirDto)
        {


            var gonderenTask = HesapNoIdIleGetir(paraCekYatirDto.HesapId.ToString());



            var gonderen = gonderenTask.Result;

            if (!gonderen.Success || gonderen.Data == null)
                return new ErrorDataResult<decimal>("Gönderen hesap bulunamadı.");






            try
            {
                if (paraCekYatirDto.IslemTipi == "Para Çekme")
                {
                    if (gonderen.Data.Bakiye < paraCekYatirDto.Tutar)
                        return new ErrorDataResult<decimal>("Gönderen hesapta yeterli bakiye yok.");
                    gonderen.Data.Bakiye -= paraCekYatirDto.Tutar;
                }
                else if (paraCekYatirDto.IslemTipi == "Para Yatırma")
                {
                    gonderen.Data.Bakiye += paraCekYatirDto.Tutar;

                }


                await _hesapDal.Guncelle(gonderen.Data);



                return new SuccessDataResult<decimal>(gonderen.Data.Bakiye, "Para transferi başarılı.");
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<decimal>($"Transfer sırasında bir hata oluştu: {ex.Message}");
            }
        }
        private string HesapNoUret()
        {
            var random = new Random();
            return random.Next(10000000, 100000000).ToString();
        }

        public async Task<IDataResult<List<Hesap>>> HepsiniGetir()
        {
            var hesaplar = await _hesapDal.HepsiniGetir();
            return new SuccessDataResult<List<Hesap>>(hesaplar, Mesajlar.HepsiniGetirmeBasarili);
        }



        public async Task<IDataResult<List<Hesap>>> IdIleHepsiniGetir(int id)
        {
            var hesaplar = await _hesapDal.HepsiniGetir(h => h.KullaniciId == id && h.Durum != "Beklemede" && h.Durum != "Reddedildi");
            return new SuccessDataResult<List<Hesap>>(hesaplar, Mesajlar.IdIleGetirmeBasarili);
        }



        public async Task<IDataResult<Hesap>> IdIleGetir(int id)
        {
            var hesap = await _hesapDal.Getir(h => h.Id == id);
            return new SuccessDataResult<Hesap>(hesap, Mesajlar.IdIleGetirmeBasarili);
        }
        public async Task<IDataResult<Hesap>> HesapNoIdIleGetir(string id)
        {
            var hesap = await _hesapDal.Getir(h => h.HesapNo == id);
            return new SuccessDataResult<Hesap>(hesap, Mesajlar.IdIleGetirmeBasarili);
        }
        public async Task<Hesap> KullaniciHesapGetirIlkVadesiz(int kullaniciId)
        {
            var sonuclar = await _hesapDal.HepsiniGetir(h => h.KullaniciId == kullaniciId);
            return sonuclar.FirstOrDefault(h => h.HesapTipi == "Vadesiz")!;

        }
        public async Task<IDataResult<VarlıklarDto>> VarliklariGetirAsync(int kullaniciId)
        {
            var hesaplar = await _hesapDal.GetHesaplarByKullaniciIdAsync(kullaniciId);
            var kartlar = await _kartServis.GetKartlarByKullaniciIdAsync(kullaniciId);


            decimal toplamPara = hesaplar.Sum(h => h.Bakiye);
            decimal toplamBorc = kartlar.Data.Sum(k => k.Limit ?? 0);
            int ustLimit = (int)(Math.Ceiling(toplamBorc / 5000) * 5000);

            var veri = new VarlıklarDto
            {
                Hesaplar = hesaplar,
                Kartlar = kartlar.Data,
                ToplamPara = toplamPara,
                ToplamBorc = ustLimit - toplamBorc
            };
            return new SuccessDataResult<VarlıklarDto>(veri, Mesajlar.IdIleGetirmeBasarili);

        }
        public async Task<List<int>> GetirKullaniciyaAitHesapIdler(int kullaniciId)
        {
            return new List<int>(_hesapDal.GetirKullaniciyaAitHesapIdler(kullaniciId));
        }
        public async Task<IResult> HesapDurumGuncelle(DurumuGuncelleDto durumGuncelleDto)  
        {
             var veri = await _hesapDal.HesapDurumGuncelle(durumGuncelleDto.Id!.Value, durumGuncelleDto.Durum!);
             if (veri)
             {
                  return new SuccessResult(Mesajlar.GuncellemeBasarili);
             }
             else
             {
                  return new ErrorResult(Mesajlar.GuncellemeBasarisiz);
             }
        }

    }
}


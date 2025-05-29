using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Cekirdek.YardımcıHizmetler.Güvenlik.Hashing
{
    public class HashingHelper
    {
        public static void HashSifreOlustur(string sifre, 
            out byte[] SifreHash,
            out byte[] sifreSalt)  
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                sifreSalt = hmac.Key;
                SifreHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(sifre));
            }
        }

        public static bool HashSifreDogrula(string sifre, byte[] hashSifre, byte[] saltSifre)    
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(saltSifre))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(sifre));
                for (int i = 0; i < hashSifre.Length; i++)
                {
                    if (computedHash[i] != hashSifre[i])
                    {
                        return false;

                    }

                }
            }
            return true;
        }
    }
}

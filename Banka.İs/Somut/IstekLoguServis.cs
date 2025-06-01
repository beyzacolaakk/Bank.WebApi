using Banka.Cekirdek.Varlıklar.Somut;
using Banka.Cekirdek.YardımcıHizmetler.Logger;
using Banka.İs.Soyut;
using Banka.Varlıklar.Somut;
using Banka.VeriErisim.Somut.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.İs.Somut
{
    public class IstekLoguServis : IIstekLoguServis
    {
        private readonly BankaContext _dbContext;

        public IstekLoguServis(BankaContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LogIstekAsync(IstekLogu log)
        {
            _dbContext.IstekLoglari.Add(log);
            await _dbContext.SaveChangesAsync();
        }
    }
}

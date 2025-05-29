using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banka.Cekirdek.YardımcıHizmetler.Results
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}

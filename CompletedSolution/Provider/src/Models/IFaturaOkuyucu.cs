using System;
using System.Collections.Generic;

namespace provider.Models
{
    public interface IFaturaOkuyucu
    {
        List<Fatura> VeritabanindanOku(int mbb, string kurum);
    }
}
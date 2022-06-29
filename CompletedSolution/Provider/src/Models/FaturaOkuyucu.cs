using System;
using System.Collections.Generic;

namespace provider.Models
{
    class FaturaOkuyucu : IFaturaOkuyucu
    {
        public List<Fatura> VeritabanindanOku(int mbb, string kurum)
        {
            var inMemoryDB = new List<Fatura>()
            {
                new Fatura{ Mbb = 456, Kurum = "IGDAS", SonOdemeTarihi = "3.5.2023", FaturaNo = "ngut12" },
                new Fatura{ Mbb = 789, Kurum = "IGDAS", SonOdemeTarihi = "3.5.2023", FaturaNo = "ngut12" },
                new Fatura{ Mbb = 789, Kurum = "MESKI", SonOdemeTarihi = "3.6.2022", FaturaNo = "dgdgf23" },
            };

            return inMemoryDB.FindAll(delegate (Fatura f)
            {
                return f.Mbb == mbb && (string.IsNullOrEmpty(kurum) || f.Kurum == kurum);
            });
        }
    }
}

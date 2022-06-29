using provider.Models;
using System;
using System.Collections.Generic;

namespace tests.Models
{
    public class TestFaturaOkuyucu : IFaturaOkuyucu
    {
        public List<Fatura> VeritabanindanOku(int mbb, string kurum)
        {
            var inMemoryDB = new List<Fatura>()
            {
                new Fatura{ Mbb = 123, Kurum = "ISKI", SonOdemeTarihi = "1.1.2022", FaturaNo = "ngut12" },
                new Fatura{ Mbb = 123, Kurum = "Ayedas", SonOdemeTarihi = "1.2.2022", FaturaNo = "te53bd" },
            };

            return inMemoryDB.FindAll(delegate (Fatura f)
            {
                return f.Mbb == mbb && (string.IsNullOrEmpty(kurum) || f.Kurum == kurum);
            });
        }
    }
}

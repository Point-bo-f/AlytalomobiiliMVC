using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlytaloMobiiliMVC.ViewModels
{
    public class SaunaViewModel
    {
        public int SaunaId { get; set; }

        public string SaunaNimi { get; set; }

        public int TavoiteLampotila { get; set; }

        public int? NykyLampotila { get; set; }

        public bool SaunanTila { get; set; }
    }
}
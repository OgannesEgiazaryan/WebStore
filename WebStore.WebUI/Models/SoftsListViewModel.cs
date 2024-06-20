using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.Domain.Entities;

namespace WebStore.WebUI.Models
{
    public class SoftsListViewModel
    {
        public IEnumerable<SoftWares> App { get; set; }
        public IEnumerable<Categorys> Categorys { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }

        public IEnumerable<SoftWares> SliderItems { get; set; }
        public IEnumerable<SoftWares> RightColumnItems { get; set; }
        public IEnumerable<SoftWares> TopSelles { get; set; }
        public IEnumerable<SoftWares> TwoBlocks { get; set; }

        public int ID_SoftWare { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class ProductsReq
    {
        public int size { get; set; }
        public int page { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }
}

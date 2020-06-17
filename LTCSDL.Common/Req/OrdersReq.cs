using System;
using System.Collections.Generic;
using System.Text;

namespace LTCSDL.Common.Req
{
    public class OrdersReq
    {
        public int size { get; set; }
        public int page { get; set; }
        public string keyword { get; set; }
        public DateTime dateF { get; set; }
        public DateTime dateT { get; set; }
    }
}

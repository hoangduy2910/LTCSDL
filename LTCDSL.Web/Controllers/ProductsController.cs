using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTCSDL.Web.Controllers
{
    using BLL;
    using Common.Rsp;
    using LTCSDL.Common.Req;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {
            _svc = new ProductsSvc();
        }

        private readonly ProductsSvc _svc;

        [HttpPost("get-all")]
        public IActionResult GetAllProduct()
        {
            var res = new SingleRsp();
            res.Data = _svc.All;
            return Ok(res);
        }

        [HttpPost("get-ds-mat-hang-ban-chay-nhat")]
        public IActionResult GetDSMatHangBanChayNhat([FromBody] ProductsReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.GetDSMatHangBanChayNhat(req.size, req.page, req.month, req.year);
            return Ok(res);
        }
    }
}
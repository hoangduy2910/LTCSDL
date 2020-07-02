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
    public class OrdersController : ControllerBase
    {
        public OrdersController()
        {
            _svc = new OrdersSvc();
        }

        private readonly OrdersSvc _svc;

        [HttpPost("get-ds-don-hang-nv-trong-khoang-thoi-gian-theo-keyword-ado")]
        public IActionResult GetDSDonHangNhanVienTrongKhoangThoiGianTheoKeyword([FromBody] OrdersReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.GetDSDonHangNhanVienTrongKhoangThoiGianTheoKeyword(req.size, req.page, req.keyword, req.dateF, req.dateT);
            return Ok(res);
        }

        [HttpPost("get-doanh-thu-theo-quoc-gia")]
        public IActionResult GetDoanhThuTheoQuocGia([FromBody] DoanhThuReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.GetDoanhThuTheoQuocGia(req.month, req.year);
            return Ok(res);
        }
    }
}
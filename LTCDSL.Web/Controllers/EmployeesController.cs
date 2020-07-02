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

    public class EmployeesController : ControllerBase
    {
        public EmployeesController()
        {
            _svc = new EmployeesSvc();
        }

        private readonly EmployeesSvc _svc;

        [HttpPost("get-doanh-thu-nhan-vien-trong-ngay")]
        public IActionResult GetDoanhThuNhanVienTrongNgay([FromBody] DoanhThuNhanVienReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.GetDoanhThuNhanVienTrongNgay(req.dateBegin);
            return Ok(res);
        }

        [HttpPost("get-doanh-thu-nhan-vien-trong-ngay-linq")]
        public IActionResult GetDoanhThuNhanVienTrongNgay_LinQ([FromBody] DoanhThuNhanVienReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.GetDoanhThuNhanVienTrongNgay_LinQ(req.dateBegin);
            return Ok(res);
        }

        [HttpPost("get-doanh-thu-nhan-vien-trong-khoang-thoi-gian")]
        public IActionResult GetDoanhThuNhanVienTrongKhoangThoiGian([FromBody] DoanhThuNhanVienReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.GetDoanhThuNhanVienTrongKhoangThoiGian(req.dateBegin, req.dateEnd);
            return Ok(res);
        }

        [HttpPost("get-doanh-thu-nhan-vien-trong-khoang-thoi-gian-linq")]
        public IActionResult GetDoanhThuNhanVienTrongKhoangThoiGian_LinQ([FromBody] DoanhThuNhanVienReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.GetDoanhThuNhanVienTrongKhoangThoiGian_LinQ(req.dateBegin, req.dateEnd);
            return Ok(res);
        }
    }
}
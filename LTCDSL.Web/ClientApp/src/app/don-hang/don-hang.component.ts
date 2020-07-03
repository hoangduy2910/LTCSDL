import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-don-hang',
  templateUrl: './don-hang.component.html',
  styleUrls: ['./don-hang.component.css']
})
export class DonHangComponent {
  size = 10;
  dateFrom: any;
  dateTo: any;
  maDH: any;

  listDSDH: any = {
    data: [],
    totalRecord: 0,
    totalPage: 0,
    page: 0,
    size: 0
  };
  listChiTietDH: any = [];


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
  }

  dachSachDonHangTrongKhoangThoiGian(cPage) {
    var res: any;
    var x = {
      "size": this.size,
      "page": cPage,
      "keyword": "",
      "dateF": this.dateFrom,
      "dateT": this.dateTo
    };
    this.http.post("https://localhost:44377" + "/api/Orders/get-ds-don-hang-trong-khoang-thoi-gian-linq", x).subscribe(result => {
      res = result;
      if (res.success) {
        this.listDSDH = res.data;
      }
      else {
        alert(res.message);
      }
    }, error => console.error(error));
  }

  dachSachDonHangTrongKhoangThoiGianTruoc() {
    if (this.listDSDH.page > 1) {
      var previousPage = this.listDSDH.page - 1;
      this.dachSachDonHangTrongKhoangThoiGian(previousPage);
    } else {
      alert("Bạn đang ở trang đầu !");
    }
  }

  dachSachDonHangTrongKhoangThoiGianSau() {
    if (this.listDSDH.page < this.listDSDH.totalPage) {
      var nextPage = this.listDSDH.page + 1;
      this.dachSachDonHangTrongKhoangThoiGian(nextPage);
    } else {
      alert("Bạn đang ở trang cuối !");
    }
  }

  chiTietDonHang() {
    var res: any;
    var x = {
      "id": this.maDH,
      "keyword": ""
    };
    this.http.post("https://localhost:44377" + "/api/Orders/get-chi-tiet-don-hang-linq", x).subscribe(result => {
      res = result;
      if (res.success) {
        this.listChiTietDH = res.data;
      }
      else {
        alert(res.message);
      }
    }, error => console.error(error));
  }
}

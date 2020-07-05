import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
declare var google: any;

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
  listSoLuongHangCanGiao: any = [];


  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    // this.drawChartSoLuongHangCanGiao();
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

  formatNgay(ngay) {
    return ngay.split("T")[0].split("-").reverse().join("-");
  }

  soLuongHangCanGiaoTrongKhoangThoiGian() {
    var res: any;
    var x = {
      "size": 0,
      "page": 0,
      "keyword": "",
      "dateF": this.dateFrom,
      "dateT": this.dateTo,
      "companyName": "",
      "employeeName": ""
    };
    this.http.post("https://localhost:44377" + "/api/Orders/so-luong-hang-can-giao-trong-khoang-thoi-gian", x).subscribe(result => {
      res = result;
      if (res.success) {
        this.listSoLuongHangCanGiao = res.data;
        for (let i in this.listSoLuongHangCanGiao) {
          this.listSoLuongHangCanGiao[i].ngay = this.formatNgay(this.listSoLuongHangCanGiao[i].ngay);
        }
        this.drawChartSoLuongHangCanGiao(res.data);
      }
      else {
        alert(res.message);
      }
    }, error => console.error(error));
  }

  drawChartSoLuongHangCanGiao(chartData) {
    var arrayData = [["Ngày", "Số lượng", { role: "style" }]];
    chartData.forEach(element => {
      var item = [];
      item.push(element.ngay);
      item.push(element.soLuongHangCanGiao);
      item.push("color: #0e1deb");
      arrayData.push(item);
    });

    var data = google.visualization.arrayToDataTable(arrayData);
    var view = new google.visualization.DataView(data);
    view.setColumns([0, 1,
                     { calc: "stringify",
                       sourceColumn: 1,
                       type: "string",
                       role: "annotation" },
                     2]);
    var options = {
      title: "Số lượng hàng cần giao",
      width: 600,
      height: 400,
      bar: {groupWidth: "95%"},
      legend: { position: "none" },
    };
    var chart = new google.visualization.BarChart(document.getElementById("barchart_values"));
    chart.draw(view, options);
  }
}

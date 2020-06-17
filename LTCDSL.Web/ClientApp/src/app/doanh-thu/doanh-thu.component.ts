import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
declare var google: any;

@Component({
  selector: 'app-doanh-thu',
  templateUrl: './doanh-thu.component.html',
  styleUrls: ['./doanh-thu.component.css']
})
export class DoanhThuComponent  {
  listDoanhThu: any = [];
  month: any;
  year: any;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) { 
    this.doanhThuTheoQuocGia();
  }

  doanhThuTheoQuocGia() {
    var res: any;
    var x = {
      "month": this.month,
      "year": this.year
    };
    this.http.post("https://localhost:44377" + "/api/Orders/get-doanh-thu-theo-quoc-gia", x).subscribe(result => {
      res = result;
      if (res.success) {
        this.listDoanhThu = res.data;
        this.drawChart(res.data);
      }
      else {
        alert(res.message);
      }
    }, error => console.error(error));
  }

  drawChart(chartData) {
    var arrayData = [["Quốc gia", "Doanh thu"]];
    chartData.forEach(element => {
      var item = [];
      item.push(element.shipCountry);
      item.push(element.doanhThu);
      arrayData.push(item);
    });
    var data = google.visualization.arrayToDataTable(arrayData);

    var options = {
      title: 'Doanh thu theo quốc gia'
    };

    var chart = new google.visualization.PieChart(document.getElementById('piechart'));

    chart.draw(data, options);
  }
}

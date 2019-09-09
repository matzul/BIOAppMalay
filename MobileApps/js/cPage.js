

//var datastat = [[323, 465, 56, 389, 621, 204, 523, 92, 348, 630, 203, 527],[231,156,185,198,152,168,147,151,251,144,251,187]];
/*
var resultChart = "";
//load datatables data after successful ajax transaction
var succeededAjaxFnChartZakat = function (data, textStatus, jqXHR) {
    console.log("succeededAjaxFnChartZakat: " + textStatus);
    resultChart = JSON.parse(data.d);

    //alert(resultChart);

    var table = $('#test').DataTable({
        data: result,
        columns: [
            { data: "GetSetbulan" },
            { data: "GetSetqtypayer" },
            { data: "GetSetamtzakat" }
        ]
    });

}

//notification for failed ajax transaction
var failedAjaxFnChartZakat = function (jqXHR, textStatus, errorThrown) {
    console.log("failedAjaxFnChartZakat: " + textStatus);
    alert(textStatus);
}
*/

var datastat = [[1074099.39, 825126.26, 787929.16, 326395.46 ]];





$(function () {
			$('#graph').graphify({
				//options: true,
				start: 'bar',
				obj: {
					id: 'ggg',
					width: '1350',
					height: 375,
					xGrid: false,
					legend: true,
					
					title: 'Kutipan Bagi Tahun ' + 2018,
					x: ['Jan', 'Feb', 'Mac', 'Apr', 'Mei', 'Jun', 'Jul', 'Aug', 'Sep', 'Okt', 'Nov', 'Dis'],
					points: 
						
						datastat
					,
					pointRadius: 3,
					scale: getCei(datastat),
					colors: ['#428bca', '#1caf9a'],
					xDist: 100,
				   	dataNames: ['Jumlah'],
					xName: 'Bulan',
					tooltipWidth: 15,
					animations: true,
					pointAnimation: true,
					averagePointRadius: 5,
					design: {
						tooltipColor: '#fff',
						gridColor: '#f3f1f1',
						tooltipBoxColor: '#d9534f',
						averageLineColor: '#d9534f',
						pointColor: '#d9534f',
						lineStrokeColor: 'grey',
					}
				}
			});



			 function getCei(data) {
			     //Math.max(...points.map(e => Array.isArray(e) ? getMax(e): e));
			     //(Math.ceil((Math.max(...points) + 1) / 10) * 10) / 10;
			     //var points = data;
			     //data.join().split(",");
			     var cel = (Math.ceil((Math.max(...data.join().split(",")) + 1) / 10) * 10) / 10;//(Math.ceil(Math.max(points) + 1) / 5) * 5;
			     //console.log("math max" + Math.max(...data.map(e => Array.isArray(e) ? getCei(e): e)));
			    // console.log(cel);

			    return cel;

			}


		});


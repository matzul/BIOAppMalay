var canvas = document.getElementById("barChart");
var ctx = canvas.getContext('2d');
// We are only changing the chart type, so let's make that a global variable along with the chart object:
var chartType = 'bar';
var myBarChart;

// Global Options:
Chart.defaults.global.defaultFontColor = 'grey';
Chart.defaults.global.defaultFontSize = 16;

var data = {
  labels: ["Januari", "Februari", "Mac", "April", "Mei", "Jun", "Julai", "Ogos", "September", "Oktober", "November", "Disember"],
  datasets: [{
    label: "Kutipan",
    fill: true,
    lineTension: 0.1,
    backgroundColor: "rgba(0,255,0,0.4)",
    borderColor: "blue", // The main line color
    borderCapStyle: 'square',
    pointBorderColor: "white",
    pointBackgroundColor: "green",
    pointBorderWidth: 1,
    pointHoverRadius: 8,
    pointHoverBackgroundColor: "yellow",
    pointHoverBorderColor: "green",
    pointHoverBorderWidth: 2,
    pointRadius: 4,
    pointHitRadius: 10,
    data: [10, 13, 17, 12, 30, 47, 60, 120, 230, 466, 310, 400],
    spanGaps: true,
  }]
};

// Notice the scaleLabel at the same level as Ticks
var options = {
  scales: {
    yAxes: [{
      ticks: {
        beginAtZero: true
      }
    }]
  },
  title: {
    fontSize: 18,
    display: true,
    text: 'I want to believe !',
    position: 'bottom'
  }
};


// We add an init function down here after the chart options are declared.

init();

function init() {
  // Chart declaration:
  myBarChart = new Chart(ctx, {
    type: chartType,
    data: data,
    options: options
  });
}

function toggleChart() {
  //destroy chart:
  myBarChart.destroy();
  //change chart type: 
  this.chartType = (this.chartType == 'bar') ? 'line' : 'bar';
  //restart chart:
  init();
}
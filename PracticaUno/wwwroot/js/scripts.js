document.addEventListener('DOMContentLoaded', function () {


    function initializeChart() {
        var ctx = document.getElementById("sales-chart").getContext("2d");

        var salesData = {
            labels: ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio"],
            datasets: [{
                label: 'Este año',
                data: [2000, 4000, 6000, 7000, 7500, 8000],
                backgroundColor: 'rgba(0, 123, 255, 0.6)',
                borderColor: 'rgba(0, 123, 255,1)',
                borderWidth: 1
            }, {
                label: 'El año pasado',
                data: [500, 1000, 2000, 4000, 6000, 10000],
                backgroundColor: 'rgba(109, 117, 125, 0.6)',
                borderColor: 'rgba(109, 117, 125)',
                borderWidth: 1
            }]
        };

        var salesOptions = {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        };
        var salesChart = new Chart(ctx, {
            type: 'bar',
            data: salesData,
            options: salesOptions
        });
    }
    var menuState = {};

    
});

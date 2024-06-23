$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Home/ChartsViewStudent",
        contentType: "application/json",
        dataType: "json",
        success: function (data) {
            if (typeof data !== 'object' || data === null) {
                console.error("Invalid JSON response:", data);
                return;
            }
            if (!isFinite(data.malePercentage) || !isFinite(data.femalePercentage) || !isFinite(data.otherPercentage)) {
                console.error("Invalid percentage values:", data);
                return;
            }
            updatePieChart(data);
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", status, error);
        }
    });

    function updatePieChart(data) {
        if (!data || typeof data !== 'object') {
            console.error("Invalid data:", data);
            return;
        }

        try {
            Highcharts.chart('container', {
                chart: {
                    type: 'pie'
                },
                title: {
                    text: 'Students Ratio',
                    align: 'Center'
                },
                subtitle: {
                    text: 'Student Data',
                    align: 'Center'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                accessibility: {
                    point: {
                        valueSuffix: '%'
                    }
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        borderWidth: 2,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b><br>{point.percentage:.1f}%',
                            distance: 20
                        }
                    }
                },
                series: [{
                    enableMouseTracking: false,
                    animation: {
                        duration: 2000
                    },
                    colorByPoint: true,
                    data: [{
                        name: 'Girls',
                        y: data.femalePercentage
                    }, {
                        name: 'Boys',
                        y: data.malePercentage
                    }, {
                        name: 'Others',
                        y: data.otherPercentage
                    }]
                }]
            });
        } catch (err) {
            console.error("Chart Error:", err);
        }
    }
});

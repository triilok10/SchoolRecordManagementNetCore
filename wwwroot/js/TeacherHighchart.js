

$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "/Home/DashboardTeacher",
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
            Highcharts.chart('containerChart', {
                chart: {
                    type: 'pie',
                    height: 500
                },
                title: {
                    text: 'Teacher Ratio',
                    align: 'Center',
                    style: {
                        fontSize: '18px',
                        fontWeight: 'bold'
                    }
                },
                subtitle: {
                    text: 'Teacher Data',
                    align: 'Center',
                    style: {
                        fontSize: '14px',
                        color: '#666666'
                    }
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>',
                    backgroundColor: 'rgba(255, 255, 255, 0.85)',
                    borderColor: '#666666',
                    borderRadius: 8,
                    borderWidth: 1,
                    shadow: false,
                    style: {
                        color: '#333333',
                        fontSize: '12px'
                    }
                },
                accessibility: {
                    point: {
                        valueSuffix: '%'
                    }
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        borderWidth: 1,
                        borderColor: '#ffffff',
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b><br>{point.percentage:.1f}%',
                            distance: 20,
                            style: {
                                fontSize: '12px',
                                fontWeight: 'normal',
                                color: 'contrast'
                            }
                        }
                    }
                },
                credits: {
                    enabled: false
                },
                series: [{
                    enableMouseTracking: false,
                    animation: {
                        duration: 1000
                    },
                    colorByPoint: true,
                    data: [{
                        name: 'Female Teacher',
                        y: data.femalePercentage
                    }, {
                        name: 'Male Teacher',
                        y: data.malePercentage
                    }, {
                        name: 'Other',
                        y: data.otherPercentage
                    }]
                }]
            });

        } catch (err) {
            console.error("Chart Error:", err);
        }
    }
});



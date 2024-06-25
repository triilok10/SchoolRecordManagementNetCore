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
                    type: 'pie',
                    height: 500
                },
                title: {
                    text: 'Students Ratio',
                    align: 'Center',
                    style: {
                        fontSize: '18px',
                        fontWeight: 'bold'
                    }
                },
                subtitle: {
                    text: 'Student Data',
                    align: 'Center',
                    style: {
                        fontSize: '14px',
                        color: '#666666'
                    }
                },
                s: {
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
                        name: 'Girls',
                        y: data.femalePercentage
                    }, {
                        name: 'Boys',
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

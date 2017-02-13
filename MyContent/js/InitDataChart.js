$(function () {

    $(document).ready(function () {

        //<!----------------------数据表的主题开始----------------------------------->

        Highcharts.createElement('link', {
            href: 'http://fonts.googleapis.com/css?family=Signika:400,700',
            rel: 'stylesheet',
            type: 'text/css'
        }, null, document.getElementsByTagName('head')[0]);

        // Add the background image to the container
        Highcharts.wrap(Highcharts.Chart.prototype, 'getContainer', function (proceed) {
            proceed.call(this);
            this.container.style.background = 'url(http://www.highcharts.com/samples/graphics/sand.png)';
            //this.container.style.background = "#CCFFCC";
        });

        //Highcharts.theme = {
        //    colors: ["#f45b5b", "#8085e9", "#8d4654", "#7798BF", "#aaeeee", "#ff0066", "#eeaaee",
        //        "#55BF3B", "#DF5353", "#7798BF", "#aaeeee"],
        //    chart: {
        //        backgroundColor: null,
        //        style: {
        //            fontFamily: "Signika, serif"
        //        }
        //    },
        //    title: {
        //        style: {
        //            color: 'black',
        //            fontSize: '16px',
        //            fontWeight: 'bold'
        //        }
        //    },
        //    subtitle: {
        //        style: {
        //            color: 'black'
        //        }
        //    },
        //    tooltip: {
        //        borderWidth: 0
        //    },
        //    legend: {
        //        itemStyle: {
        //            fontWeight: 'bold',
        //            fontSize: '13px'
        //        }
        //    },
        //    xAxis: {
        //        labels: {
        //            style: {
        //                color: '#6e6e70'
        //            }
        //        }
        //    },
        //    yAxis: {
        //        labels: {
        //            style: {
        //                color: '#6e6e70'
        //            }
        //        }
        //    },
        //    plotOptions: {
        //        series: {
        //            shadow: true
        //        },
        //        candlestick: {
        //            lineColor: '#404048'
        //        },
        //        map: {
        //            shadow: false
        //        }
        //    },

        //    // Highstock specific
        //    navigator: {
        //        xAxis: {
        //            gridLineColor: '#D0D0D8'
        //        }
        //    },
        //    rangeSelector: {
        //        buttonTheme: {
        //            fill: 'white',
        //            stroke: '#C0C0C8',
        //            'stroke-width': 1,
        //            states: {
        //                select: {
        //                    fill: '#D0D0D8'
        //                }
        //            }
        //        }
        //    },
        //    scrollbar: {
        //        trackBorderColor: '#C0C0C8'
        //    },

        //    // General
        //    background2: '#E0E0E8'

        //};


        //// Apply the theme
        //Highcharts.setOptions(Highcharts.theme);
        /**
 * Dark blue theme for Highcharts JS
 * @author Torstein Honsi
 */

        Highcharts.theme = {
            colors: ["#DDDF0D", "#55BF3B", "#DF5353", "#7798BF", "#aaeeee", "#ff0066", "#eeaaee",
                "#55BF3B", "#DF5353", "#7798BF", "#aaeeee"],
            chart: {
                backgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 1, y2: 1 },
                    stops: [
                        [0, 'rgb(48, 48, 96)'],
                        [1, 'rgb(0, 0, 0)']
                    ]
                },
                borderColor: '#000000',
                borderWidth: 2,
                className: 'dark-container',
                plotBackgroundColor: 'rgba(255, 255, 255, .1)',
                plotBorderColor: '#CCCCCC',
                plotBorderWidth: 1
            },
            title: {
                style: {
                    color: '#C0C0C0',
                    font: 'bold 16px "Trebuchet MS", Verdana, sans-serif'
                }
            },
            subtitle: {
                style: {
                    color: '#666666',
                    font: 'bold 12px "Trebuchet MS", Verdana, sans-serif'
                }
            },
            xAxis: {
                gridLineColor: '#333333',
                gridLineWidth: 1,
                labels: {
                    style: {
                        color: '#A0A0A0'
                    }
                },
                lineColor: '#A0A0A0',
                tickColor: '#A0A0A0',
                title: {
                    style: {
                        color: '#CCC',
                        fontWeight: 'bold',
                        fontSize: '12px',
                        fontFamily: 'Trebuchet MS, Verdana, sans-serif'

                    }
                }
            },
            yAxis: {
                gridLineColor: '#333333',
                labels: {
                    style: {
                        color: '#A0A0A0'
                    }
                },
                lineColor: '#A0A0A0',
                minorTickInterval: null,
                tickColor: '#A0A0A0',
                tickWidth: 1,
                title: {
                    style: {
                        color: '#CCC',
                        fontWeight: 'bold',
                        fontSize: '12px',
                        fontFamily: 'Trebuchet MS, Verdana, sans-serif'
                    }
                }
            },
            tooltip: {
                backgroundColor: 'rgba(0, 0, 0, 0.75)',
                style: {
                    color: '#F0F0F0'
                }
            },
            toolbar: {
                itemStyle: {
                    color: 'silver'
                }
            },
            plotOptions: {
                line: {
                    dataLabels: {
                        color: '#CCC'
                    },
                    marker: {
                        lineColor: '#333'
                    }
                },
                spline: {
                    marker: {
                        lineColor: '#333'
                    }
                },
                scatter: {
                    marker: {
                        lineColor: '#333'
                    }
                },
                candlestick: {
                    lineColor: 'white'
                }
            },
            legend: {
                itemStyle: {
                    font: '9pt Trebuchet MS, Verdana, sans-serif',
                    color: '#A0A0A0'
                },
                itemHoverStyle: {
                    color: '#FFF'
                },
                itemHiddenStyle: {
                    color: '#444'
                }
            },
            credits: {
                style: {
                    color: '#666'
                }
            },
            labels: {
                style: {
                    color: '#CCC'
                }
            },

            navigation: {
                buttonOptions: {
                    symbolStroke: '#DDDDDD',
                    hoverSymbolStroke: '#FFFFFF',
                    theme: {
                        fill: {
                            linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                            stops: [
                                [0.4, '#606060'],
                                [0.6, '#333333']
                            ]
                        },
                        stroke: '#000000'
                    }
                }
            },

            // scroll charts
            rangeSelector: {
                buttonTheme: {
                    fill: {
                        linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                        stops: [
                            [0.4, '#888'],
                            [0.6, '#555']
                        ]
                    },
                    stroke: '#000000',
                    style: {
                        color: '#CCC',
                        fontWeight: 'bold'
                    },
                    states: {
                        hover: {
                            fill: {
                                linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                                stops: [
                                    [0.4, '#BBB'],
                                    [0.6, '#888']
                                ]
                            },
                            stroke: '#000000',
                            style: {
                                color: 'white'
                            }
                        },
                        select: {
                            fill: {
                                linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                                stops: [
                                    [0.1, '#000'],
                                    [0.3, '#333']
                                ]
                            },
                            stroke: '#000000',
                            style: {
                                color: 'yellow'
                            }
                        }
                    }
                },
                inputStyle: {
                    backgroundColor: '#333',
                    color: 'silver'
                },
                labelStyle: {
                    color: 'silver'
                }
            },

            navigator: {
                handles: {
                    backgroundColor: '#666',
                    borderColor: '#AAA'
                },
                outlineColor: '#CCC',
                maskFill: 'rgba(16, 16, 16, 0.5)',
                series: {
                    color: '#7798BF',
                    lineColor: '#A6C7ED'
                }
            },

            scrollbar: {
                barBackgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0.4, '#888'],
                        [0.6, '#555']
                    ]
                },
                barBorderColor: '#CCC',
                buttonArrowColor: '#CCC',
                buttonBackgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0.4, '#888'],
                        [0.6, '#555']
                    ]
                },
                buttonBorderColor: '#CCC',
                rifleColor: '#FFF',
                trackBackgroundColor: {
                    linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                    stops: [
                        [0, '#000'],
                        [1, '#333']
                    ]
                },
                trackBorderColor: '#666'
            },

            // special colors for some of the
            legendBackgroundColor: 'rgba(0, 0, 0, 0.5)',
            background2: 'rgb(35, 35, 70)',
            dataLabelsColor: '#444',
            textColor: '#C0C0C0',
            maskColor: 'rgba(255,255,255,0.3)'
        };

        // Apply the theme
        var highchartsOptions = Highcharts.setOptions(Highcharts.theme);

        //<!----------------------数据表的主题结束----------------------------------->


        Highcharts.setOptions({
            global: {
                useUTC: false
            }
        });

        //<!----------------------------（1）动态图开始（空气温度）--------------------------------->
        var chart;
        $('#container1').highcharts({
            chart: {
                type: 'spline',
                animation: Highcharts.svg, // don't animate in old IE               
                marginRight: 10,
                events: {
                    load: function () {

                        // set up the updating of the chart each second             
                        var series = this.series[0];
                        setInterval(function () {
                            $.post("/Home/GetMyDataPoint", { sensor: "心率" }, function (result) {
                                // var x = (new Date()).getTime(), // current time         
                                //y = Math.random();
                                var json = JSON.parse(result);
                                var x = (new Date()).getTime();
                                
                                if (json["Successful"] == "true") {
                                    series.addPoint([x, json["Data"][0].value], true, true);
                                    $("#s1").children("span").eq(1).html(json["Data"][0].value);
                                } else {
                                    var y = $("#s1").children("span").eq(1).html();
                                    //y = 72;
                                    series.addPoint([x, parseInt(y)], true, true);
                                }
                            });
                        }, 1000);
                    }
                }
            },
            title: {
                text: '心率(次 / 分)'
            },
            xAxis: {
                type: 'datetime',
                tickPixelInterval: 150
            },
            yAxis: {
                title: {
                    text: ''
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br>' +
                    Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br>' +
                    Highcharts.numberFormat(this.y, 2);
                }
            },
            legend: {
                enabled: false
            },
            exporting: {
                enabled: false
            },
            series: [{
                name: '心率',
                data: (function () {
                    // generate an array of random data                             
                    var data = [],
                        time = (new Date()).getTime(),
                        i;

                    for (i = -19; i <= 0; i++) {
                        data.push({
                            x: time + i * 1000,
                            //y: Math.random()
                            y: 0
                        });
                    }
                    return data;
                })()
            }]
        });
        //<!----------------------------（1）动态图结束（空气温度）--------------------------------->



        //<!----------------------------（2）动态图开始（土壤湿度）--------------------------------->
        var chart;
        $('#container2').highcharts({
            chart: {
                type: 'spline',
                animation: Highcharts.svg, // don't animate in old IE               
                marginRight: 10,
                events: {
                    load: function () {

                        // set up the updating of the chart each second             
                        var series = this.series[0];
                        setInterval(function () {
                            $.post("/Home/GetMyDataPoint", { sensor: "体温" }, function (result) {
                                // var x = (new Date()).getTime(), // current time         
                                //y = Math.random();
                                var json = JSON.parse(result);
                                var x = (new Date()).getTime();

                                if (json["Successful"] == "true") {
                                    series.addPoint([x, json["Data"][0].value], true, true);
                                    $("#s2").children("span").eq(1).html(json["Data"][0].value);
                                } else {
                                    var y = $("#s2").children("span").eq(1).html();
                                    series.addPoint([x, parseInt(y)], true, true);
                                }
                            });
                        }, 1000);
                    }
                }
            },
            title: {
                text: '体温(℃)'
            },
            xAxis: {
                type: 'datetime',
                tickPixelInterval: 150
            },
            yAxis: {
                title: {
                    text: ''
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#996699'
                }]
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br>' +
                    Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br>' +
                    Highcharts.numberFormat(this.y, 2);
                }
            },
            legend: {
                enabled: false
            },
            exporting: {
                enabled: false
            },
            series: [{
                name: '体温',
                data: (function () {
                    // generate an array of random data                             
                    var data = [],
                        time = (new Date()).getTime(),
                        i;

                    for (i = -19; i <= 0; i++) {
                        data.push({
                            x: time + i * 1000,
                            //y: Math.random()
                            y: 0
                        });
                    }
                    return data;
                })()
            }]
        });
        //<!----------------------------（2）动态图结束（土壤湿度）--------------------------------->


        //<!----------------------------（3）动态图开始（光照强度）--------------------------------->
        var chart;
        $('#container3').highcharts({
            chart: {
                type: 'spline',
                animation: Highcharts.svg, // don't animate in old IE               
                marginRight: 10,
                events: {
                    load: function () {

                        // set up the updating of the chart each second             
                        var series = this.series[0];
                        setInterval(function () {
                            $.post("/Home/GetMyDataPoint", { sensor: "体重" }, function (result) {
                                // var x = (new Date()).getTime(), // current time         
                                //y = Math.random();
                                var json = JSON.parse(result);
                                var x = (new Date()).getTime();

                                if (json["Successful"] == "true") {
                                    series.addPoint([x, json["Data"][0].value], true, true);
                                    $("#s3").children("span").eq(1).html(json["Data"][0].value);
                                } else {
                                    var y = $("#s3").children("span").eq(1).html();
                                    series.addPoint([x, parseInt(y)], true, true);
                                }
                            });
                        }, 1000);
                    }
                }
            },
            title: {
                text: '体重(Kg)'
            },
            xAxis: {
                type: 'datetime',
                tickPixelInterval: 150
            },
            yAxis: {
                title: {
                    text: ''
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br>' +
                    Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br>' +
                    Highcharts.numberFormat(this.y, 2);
                }
            },
            legend: {
                enabled: false
            },
            exporting: {
                enabled: false
            },
            series: [{
                name: '体重',
                data: (function () {
                    // generate an array of random data                             
                    var data = [],
                        time = (new Date()).getTime(),
                        i;

                    for (i = -19; i <= 0; i++) {
                        data.push({
                            x: time + i * 1000,
                            //y: Math.random()
                            y: 0
                        });
                    }
                    return data;
                })()
            }]
        });
        //<!----------------------------（3）动态图结束（光照强度）--------------------------------->



        //<!----------------------------（4）动态图开始（空气湿度）--------------------------------->
        var chart;
        $('#container4').highcharts({
            chart: {
                type: 'spline',
                animation: Highcharts.svg, // don't animate in old IE               
                marginRight: 10,
                events: {
                    load: function () {

                        // set up the updating of the chart each second             
                        var series = this.series[0];
                        setInterval(function () {
                            $.post("/Home/GetMyDataPoint", { sensor: "收缩压" }, function (result) {
                                // var x = (new Date()).getTime(), // current time         
                                //y = Math.random();
                                var json = JSON.parse(result);
                                var x = (new Date()).getTime();
                               
                                if (json["Successful"] == "true") {
                                    series.addPoint([x, json["Data"][0].value], true, true);
                                    $("#s4").children("span").eq(1).html(json["Data"][0].value);
                                } else {
                                    var y = $("#s4").children("span").eq(1).html();
                                    series.addPoint([x, parseInt(y)], true, true);
                                }
                            });
                        }, 1000);
                    }
                }
            },
            title: {
                text: '收缩压(mmHg)'
            },
            xAxis: {
                type: 'datetime',
                tickPixelInterval: 150
            },
            yAxis: {
                title: {
                    text: ''
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br>' +
                    Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br>' +
                    Highcharts.numberFormat(this.y, 2);
                }
            },
            legend: {
                enabled: false
            },
            exporting: {
                enabled: false
            },
            series: [{
                name: '收缩压',
                data: (function () {
                    // generate an array of random data                             
                    var data = [],
                        time = (new Date()).getTime(),
                        i;

                    for (i = -19; i <= 0; i++) {
                        data.push({
                            x: time + i * 1000,
                            //y: Math.random()
                            y: 0
                        });
                    }
                    return data;
                })()
            }]
        });
        //<!----------------------------（4）动态图结束（空气湿度）--------------------------------->



        //<!----------------------------（5）动态图开始（PM2.5）--------------------------------->
        var chart;
        $('#container5').highcharts({
            chart: {
                type: 'spline',
                animation: Highcharts.svg, // don't animate in old IE               
                marginRight: 10,
                events: {
                    load: function () {

                        // set up the updating of the chart each second             
                        var series = this.series[0];
                        setInterval(function () {
                            $.post("/Home/GetMyDataPoint", { sensor: "舒张压" }, function (result) {
                                // var x = (new Date()).getTime(), // current time         
                                //y = Math.random();
                                var json = JSON.parse(result);
                                var x = (new Date()).getTime();
                               
                                if (json["Successful"] == "true") {
                                    series.addPoint([x, json["Data"][0].value], true, true);
                                    $("#s5").children("span").eq(1).html(json["Data"][0].value);
                                } else {
                                    var y = $("#s5").children("span").eq(1).html();
                                    series.addPoint([x, parseInt(y)], true, true);
                                }
                            });
                        }, 1000);
                    }
                }
            },
            title: {
                text: '舒张压(mmHg)'
            },
            xAxis: {
                type: 'datetime',
                tickPixelInterval: 150
            },
            yAxis: {
                title: {
                    text: ''
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br>' +
                    Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br>' +
                    Highcharts.numberFormat(this.y, 2);
                }
            },
            legend: {
                enabled: false
            },
            exporting: {
                enabled: false
            },
            series: [{
                name: '舒张压',
                data: (function () {
                    // generate an array of random data                             
                    var data = [],
                        time = (new Date()).getTime(),
                        i;

                    for (i = -19; i <= 0; i++) {
                        data.push({
                            x: time + i * 1000,
                            //y: Math.random()
                            y: 0
                        });
                    }
                    return data;
                })()
            }]
        });
        //<!----------------------------（5）动态图结束（PM2.5）--------------------------------->


        //<!----------------------------（6）动态图开始（CO浓度）--------------------------------->
        var chart;
        $('#container6').highcharts({
            chart: {
                type: 'spline',
                animation: Highcharts.svg, // don't animate in old IE               
                marginRight: 10,
                events: {
                    load: function () {

                        // set up the updating of the chart each second             
                        var series = this.series[0];
                        setInterval(function () {
                            $.post("/Home/GetDataPoint", { sensor: "CO浓度" }, function (result) {
                                // var x = (new Date()).getTime(), // current time         
                                //y = Math.random();
                                var json = JSON.parse(result);
                                var x = (new Date()).getTime(),
                                    y = result.value;

                                if (json["Successful"] == "true") {
                                    series.addPoint([x, json["Data"][0].value], true, true);
                                    $("#s6").children("span").eq(1).html(json["Data"][0].value);
                                } else {
                                    var y = $("#s6").children("span").eq(1).html();
                                    series.addPoint([x, parseInt(y)], true, true);
                                }
                            });
                        }, 1000);
                    }
                }
            },
            title: {
                text: 'CO浓度(毫克每立方米 / mg/m³)'
            },
            xAxis: {
                type: 'datetime',
                tickPixelInterval: 150
            },
            yAxis: {
                title: {
                    text: ''
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }]
            },
            tooltip: {
                formatter: function () {
                    return '<b>' + this.series.name + '</b><br>' +
                    Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br>' +
                    Highcharts.numberFormat(this.y, 2);
                }
            },
            legend: {
                enabled: false
            },
            exporting: {
                enabled: false
            },
            series: [{
                name: 'CO浓度',
                data: (function () {
                    // generate an array of random data                             
                    var data = [],
                        time = (new Date()).getTime(),
                        i;

                    for (i = -19; i <= 0; i++) {
                        data.push({
                            x: time + i * 1000,
                            //y: Math.random()
                            y: 0
                        });
                    }
                    return data;
                })()
            }]
        });
        //<!----------------------------（6）动态图结束（CO浓度）--------------------------------->


    });//ready  function--->end

});//function----->end
/*
Template Name: AdminKit
Author: UXLiner
*/
$(function() {
    "use strict";

// Use Morris.Area instead of Morris.Line

// ======
// Yearly Earning Starts
// ======

    //var day_data= variable4;//
//var day_data = [
//  {"elapsed": "2018", "Sales": 60, "Earning": 80},
//  {"elapsed": "2019", "Sales": 120, "Earning": 130},
//  {"elapsed": "2020", "Sales": 70, "Earning": 50},
//  {"elapsed": "2021", "Sales": 200, "Earning": 155},
//  {"elapsed": "2022", "Sales": 60, "Earning": 105},
//  {"elapsed": "2023", "Sales": 180, "Earning": 80},
//  {"elapsed": "2026", "Sales": 100, "Earning": 180}
    //];
    var day_data = [variable4];
   
    Morris.Line({
        element: 'earning',
        data: $.parseJSON(day_data),
        xkey: 'label',
        ykeys: ['value'],
        //xkey: 'elapsed',
        //  //ykeys: ['Sales', 'Earning'],
        //ykeys: ['Sales'],
        labels: ['Retirement'],
        fillOpacity: 0,
        //pointStrokeColors: ['#1976d2', '#00a65a'],
        pointStrokeColors: ['#1976d2'],
        behaveLikeLine: true,
        gridLineColor: '#e0e0e0',
        lineWidth: 3,
        hideHover: 'auto',
        //lineColors: ['#0077d3', '#00a65a'],
        lineColors: ['#0077d3'],
        parseTime: false,
        resize: true
    });

// ======
// Yearly Earning Ending
// ======

// ======
// Donut Chart Starts
    // ======

    var day_data = [variable5];
 
    Morris.Line({
        element: 'tender',
        data: $.parseJSON(day_data),
        xkey: 'label',
        ykeys: ['value'],
        //xkey: 'elapsed',
        //  //ykeys: ['Sales', 'Earning'],
        //ykeys: ['Sales'],
        labels: ['Tender'],
        fillOpacity: 0,
        //pointStrokeColors: ['#1976d2', '#00a65a'],
        pointStrokeColors: ['#1976d2'],
        behaveLikeLine: true,
        gridLineColor: '#e0e0e0',
        lineWidth: 3,
        hideHover: 'auto',
        //lineColors: ['#0077d3', '#00a65a'],
        lineColors: ['#0077d3'],
        parseTime: false,
        resize: true
    });


Morris.Donut({
      element: 'donut',
      data: [
        {value: variable1, label: 'On-Leave'},
        { value: variable2, label: 'Due for- Leave' },
        { value: variable3, label: 'On-Duty' }
      ],
      backgroundColor: '#fff',
      labelColor: '#404e67',
      colors: [
        '#ff4558',
        '#ff7d4d',
        '#00a5a8'
      ],
      formatter: function (x) { return x }
});
Morris.Donut({
    element: 'donut1',
    data: [
      { value: varrialeJrStaff, label: 'Jr-Staff' },
      { value: varrialeSrStaff, label: 'Sr-Staff' },
      { value: varrialeNyscStaff, label: 'NYSC-Members' },
            { value: varrialeOthersStaff, label: 'Others' }

    ],
    backgroundColor: '#fff',
    labelColor: '#404e67',
    colors: [
      '#ff4558',
      '#ff7d4d',
      '#00a5a8'
    ],
    formatter: function (x) { return x }
});
Morris.Donut({
    element: 'donut2',
    data: [
      { value: varrialeCentral, label: 'Central' },
      { value: varrialeStationary, label: 'Stationary' },
      { value: varrialeChemical, label: 'Chemical' },
      { value: varrialeFertilizer, label: 'Fertilizer' }
    ],
    backgroundColor: '#fff',
    labelColor: '#404e67',
    colors: [
      '#ff4558',
      '#ff7d4d',
      '#00a5a8'
    ],
    formatter: function (x) { return x }
});

// ======
// Donut chart End
// ======

})(jQuery);
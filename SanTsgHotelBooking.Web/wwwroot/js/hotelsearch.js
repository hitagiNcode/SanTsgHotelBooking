//Does the hotel ajaxcall when document lodaded
var hotelDataTable;
var cityId;

$(document).ready(function () {
    cityId = $("#cityId").text();
    loadDataTable();
});

function loadDataTable() {
    hotelDataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Search/GetHotelsPrices/",
            "data": { term: cityId },
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "15%" },
            { "data": "name", "width": "15%" },
            { "data": "stars", "width": "15%" },
            { "data": "thumbnail", "width": "15%" }

        ]
    });
}
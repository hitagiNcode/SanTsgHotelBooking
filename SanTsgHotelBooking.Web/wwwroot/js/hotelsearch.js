//Does the hotel ajaxcall when document lodaded
var hotelDataTable;
var cityId;

$(document).ready(function () {
    cityId = $("#cityId").text();
    loadDataTable();
});

function loadDataTable() {
    hotelDataTable = $('#tblData').DataTable({
        "processing": true,
        "language": {
            processing: "Searching for best prices and hotels!",
            emptyTable: "We couldn't find any hotels at this area for your search.",
            loadingRecords: "Please wait while your search is loading..."
        },
        "order": [[2, "desc"]],
        "info": true,
        "stateSave": true,
        "ajax": {
            "url": "/Search/GetHotelsPrices/",
            "data": { term: cityId },
            "headers": { RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() },
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
            {
                "width": "20%",
                "orderable": false,
                "data": "thumbnailFull",
                "render": function (data) { return '<img style="max-height:150px;width:100%;" class="img-fluid" src="' + data + '" alt="HotelPicture" />' }
            },
            {
                "orderable": false,
                data: "name",
                render: function (data, type, row) {
                    return row.name + '<br>(' + row.address + ')';
                },
                autoWidth: true
            },
            { data: "stars", width: "5%" },
            {
                data: "offers[0].price.amount",
                render: function (data, type, row) {
                    return row.offers[0].price.amount + '<br>(' + row.offers[0].price.currency + ')';
                },
                width: "5%"
            },
            {
                render: function (data, type, row) {
                    return '<div class="" role="group">' + '<a href="/Search/HotelDetails?id='+ row.id +'"  class="btn btn-info"> <i class="bi bi-book"></i>Details</a>' +
                        '</br>' + '<a href="/Booking?offerId=' + row.offers[0].offerId + '"  class="btn btn-primary mt-1"> <i class="bi bi-book"></i>Book Now</a>' + ' </div>';
                },
                width: "5%"
            }
        ],
        "columnDefs": [
            { "className": "dt-center", "targets": "_all" }
        ],
    });
}
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $("#searchBox").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Search/AutoComplete/',
                headers: {
                    "RequestVerificationToken":
                        $('input[name="__RequestVerificationToken"]').val()
                },
                data: { "term": request.term },
                type: "POST",
                dataType: "json",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (xhr, textStatus, error) {
                    alert(xhr.statusText);
                },
                failure: function (response) {
                    alert("failure " + response.responseText);
                }
            });

        },
        select: function (e, i) {
            //$("#box").val(i.item.val);
        },
        minLength: 3
    });
});
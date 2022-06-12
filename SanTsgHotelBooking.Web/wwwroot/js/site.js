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
                    $("#searchBox").val(data[0]);
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
            $("#searchBox").val(i.item.val);
            /* When selected starts search ---  I will add this later----
            $.ajax({
                type: 'GET',
                url: '/Search/Hotels?SearchString='+i.item.val,
                contentType: 'json',
                success: function (result) {
                    console.log('Data received: ');
                    console.log(result);
                }
            });*/
        },
        minLength: 3
    });
});


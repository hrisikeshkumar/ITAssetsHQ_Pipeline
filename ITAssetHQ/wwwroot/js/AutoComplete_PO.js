$(function(){
    
    $('#PO_No').autocomplete({

        //var URL = $(this).attr('url');
        source: function (request, response) {
            //var URL = '@Url.Action("AutoComplete_FindPO", "Computer")';
            $.ajax({
                url: "@Url.Action("AutoComplete_FindPO", "Computer")",
                url: URL,
                data: { input: request.term },
                dataType: "json",
                type: "POST",
                // contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item.value + '***' + item.text;

                    }))
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    console.log('some error occured', textStatus, errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {

            $('#PO_Id').val(ui.item.label.split('***')[0]);
            $('#PO_No').val(ui.item.value.split('***')[1]);

            return false;
        }
    });



});
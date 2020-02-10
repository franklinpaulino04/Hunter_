var count = 0;
$(document).ready(function() {

    if ($("#actor-table tbody tr.row-item").length === 0) {
        getRow(0);
    } 

    //Add a new row
    $(document).on('click', '.trigger_add_row', function(e) {
        var rowTable = $('#row-table-hidden .row-item').clone(),
            count_row = $("#actor-table tbody tr.row-item").length;
        rowTable.find(".ActorId").attr("name", "Items[" + count_row + "].ActorId");

        $('.last_row').before(rowTable).promise().done(function() {
            countRow();
        });
        count++;
    });

    //Remove row
    $(document).on('click', '.trigger_remove_row', function() {
        var item = $(this).closest('.row-item'),
            count = $('#actor-table tr.row-item').length,
            id = parseInt($(this).data('id'));

        if (count > 1) {
            if (id === 'undefined' || id == 0) {
                item.remove();

            } else {
               
                var res = confirm("Esta seguro que quiere borrar este registro?");
                if (res == true) {
                    $.ajax({ type: 'get', url: "https://localhost:44356/api/movie/delete_Item/" + id }).done(function (data) { item.remove();});
                }
            }

            countRow();
        } else {
            alert("No puede borrar todos los registros");
        }

        count--;
    });

});


var getRow = function(index) {
    var row = $('#row-table-hidden tr.row-item').clone();
        row.find(".ActorId").attr("name", "Items[" + count + "].ActorId");

    if (index == 0) {
        $('#actor-table tr.last_row').before(row).promise().done(function () {
            countRow();
        });
    } 

    count++;
};

var countRow = function() {
    $('table tr.row-item .number-count').each(function(i) {
        $(this).text(i + 1);
    });
};
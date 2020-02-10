var table = $('#dataTable-actor').DataTable({
    "ajax": "https://localhost:44391/api/Actor/Datatable",
    "language": {
        "sProcessing": "Procesando...",
        "sLengthMenu": "Mostrar _MENU_ registros",
        "sZeroRecords": "No se encontraron resultados",
        "sEmptyTable": "Ningun dato disponible en esta tabla =(",
        "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
        "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
        "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
        "sInfoPostFix": "",
        "sSearch": "Buscar:",
        "sUrl": "",
        "sInfoThousands": ",",
        "sLoadingRecords": "Cargando...",
        "oPaginate": {
            "sFirst": "Primero",
            "sLast": "Último",
            "sNext": "Siguiente",
            "sPrevious": "Anterior"
        },
        "oAria": {
            "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
            "sSortDescending": ": Activar para ordenar la columna de manera descendente"
        },
        "buttons": {
            "copy": "Copiar",
            "colvis": "Visibilidad"
        }
    },
    "columns": [
        { "data": 'ActorId',    "sClass": "dt-ActorId",     "width": "0",   "defaultContent": "<i class='na'>N/A</i>" },
        { "data": 'Name',       "sClass": "dt-Name",        "width": "25%", "defaultContent": "<i class='na'>N/A</i>" },
        { "data": 'Birth_date', "sClass": "dt-Birth_date",  "width": "15%", "defaultContent": "<i class='na'>N/A</i>" },
        { "data": 'Sexo',       "sClass": "dt-Sexo",        "width": "20%", "defaultContent": "<i class='na'>N/A</i>" },
        { "data": 'Photo',      "sClass": "dt-Photo",       "width": "20%", "defaultContent": "<i class='na'>N/A</i>" },
        { "data": 'action',     "sClass": "dt-action",      "width": "10%", "defaultContent": "<i class='na'>N/A</i>" }
    ],
    'createdRow': function (row, data, index) {
        $('.dt-action', row).html(actionLinks(data));
        $('.dt-Sexo', row).html(Sexo(data));
        //$('.dt-Photo', row).html(Image(data));
    }

});

table.columns([0]).visible(false, false);

$(document).ready(function () {

  
});

var Sexo = function (data) {
    return (parseInt(data.Sexo) == 1) ? ' <div class="badge badge-success" role="alert">Masculino</div>' : ' <div class="badge badge-danger" role="alert">Femenino</div>';
};


var actionLinks = function (data) {
    var html = '',
        id = data.ActorId;

    html += '<div class="dropdown">';
    html += '<a class="btn btn-secondary dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">';
    html += 'Opciones';
    html += '</a>';
    html += '<div class="dropdown-menu" aria-labelledby="dropdownMenuLink">';
    html += '<a class="dropdown-item" href="/Actor/edit/' + id + '"><i class="fa fa-trash-alt"> Editar</i></a>';
    html += '<a class="dropdown-item modal_trigger_delete" href="javascript:void(0)" data-url="cp_contacts/hide/' + id + '"><i class="fa fa-trash-alt"> Eliminar</i></a>';
    html += '</div>';
    html += '</div>';

    return html;
};
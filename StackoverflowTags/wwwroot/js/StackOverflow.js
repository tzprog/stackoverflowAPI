var TableStackOverflow;

$(document).ready(function () {
    loadTableStackOverflowAPI();
    loadTableStackOverflow(1000);
});

function loadTableStackOverflowAPI() {
    TableStackOverflow = $('#DT_load_API').DataTable({
        "ajax": {
            "url": "/api/GetAllAPI",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "lp", "width": "10%" },
            { "data": "name", "width": "50%" },
            { "data": "count", "width": "30%" },
            { "data": "percentPopularity", "width": "10%" }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "paging": false,
        "searching": false,
        "width": "100%"
    })
}

function loadTableStackOverflow(r) {
    TableStackOverflow = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/GetAllWWW/"+r,
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "tagName", "width": "50%" },
            { "data": "tagQuestionsCounter", "width": "30%" },
            { "data": "percentPopularity", "width": "10%" }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "paging": false,
        "searching": false,
        "width": "100%"
    })
}
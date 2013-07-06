function objectToRow(item) {
    var res = '';
    for (var key in item) {
        if (Cebritas.tableMapping.hasOwnProperty(key)) {
            res += '<td>' + item[key] + '</td>';
        }
    }
    return res;
}
function getRow(item, id) {
    var res = '<tr class="table-row" id="' + id + '">' + objectToRow(item) + '</tr>';
    return res;
}
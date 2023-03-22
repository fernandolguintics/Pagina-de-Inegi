function SearchEmployees(txtSearch, cblMateriales) {
    if ($(txtSearch).val() != "") {
        var count = 0;
        $(cblMateriales).children('tbody').children('tr').each(function () {
            var match = false;
            $(this).children('td').children('label').each(function () {
                if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                    match = true;
            });
            if (match) {
                $(this).show();
                count++;
            }
            else { $(this).hide(); }
        });
        $('#spnCount').html((count) + ' match');
    }
    else {
        $(cblMateriales).children('tbody').children('tr').each(function () {
            $(this).show();
        });
        $('#spnCount').html('');
    }
}
﻿$(document).ready(function () {
    loadDataTable();
});

//function loadDataTable() {
//    $.ajax({
//        url: "/Admin/Product/GetAll"
//    }).done(function (data) {
//        $('#tblData').html(data);
//        $('#tblProduct').DataTable();
//    });
//}

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Admin/Product/GetAll' },
        "columns": [
            { data: 'title',"width":"15%" },
            { data: 'isbn', "width": "15%" },
            { data: 'listPrice', "width": "15%" },
            { data: 'author', "width": "15%" },
            { data: 'category.name', "width": "15%" },
            { data: 'id',
                "render": function (data) {
                    return ` <div class="w-75 btn-group" role="group">

                    <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2">
                            <i class="bi bi-pencil-square"></i>Edit
                        </a>
                
                    <a href=/admin/product/delete?id=${data} " class ="btn btn-danger mx-2">
                                <i class="bi bi-trash"></i>Delete
                    </a>
                    </div>`
                }
            , "width": "15%" }
        ]
    });
}
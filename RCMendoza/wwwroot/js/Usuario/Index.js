$(document).ready(function () {

    let TablePersonal = $("#TablePersonal");
    let btnAddUsuario = $("#btnAddUsuario");

    btnAddUsuario.on("click", function (e) {
        e.preventDefault();
        InvocarModal();
    });
    // Funcion Invocar un Modal
    function InvocarModal(id) {
        AbrirModal(`/Usuario/MantenimientoUsuario/${id ? id : ""}`);
    }
    // Funcion para Abrir un Modal
    function AbrirModal(url) {

        $.ajax({
            type: 'GET',
            url: url,
            dataType: "html",
            cache: false,
            success: function (data) {
                $('.modal-container').html(data).find('.modal').modal({
                    show: true
                });
            }
        });
    }
    // Listado de Personal Consumiendo Datataables
    let DataTablePersonal = TablePersonal.DataTable({
        scrollY: true,
        scrollX: true,
        paging: false,
        ordering:false,
        ajax: {
            url: '/Usuario/GetAllUsuarios',
        },
        columnDefs: [
            { targets: 0, width: 100},
            { targets: 1, width: 110},
            { targets: 2, width: 180 },
            { targets: 3, width: 180 },
            { targets: 4, width: 210 },
            { targets: 5, width: 100 },
            { targets: 6, width: 210 },
            { targets: 7, width: 150 },
            { targets: 8, width: 100 },
            { targets: 9, width: 100 },
            { targets: 10, width: 100 }
        ],
        columns: [
            { data: "fkRolesNavigation.descripcion", title: "Tipo Doc" },
            { data: "numerodoc", title: "Dni" },
            { data: "nombres", title: "Nombre" },
            { data: "apellidopaterno", title: "Apellido Paterno" },
            { data: "apellidomaterno", title: "Apellido Materno" },
            { data: "telefono", title:"Celular" },
            { data: "direccion", title: "Direccion" },
            { data: "email", title: "Email" },
            { data: "fecharegistro", title: "Fecha de Registro" },
            { data: "fkTipodocumentoNavigation.descripcion", title: "Cargo" },
            {
                data: null,
                defaultContent: "<button type='button' id='btnEditar' class='btn btn-primary'><i class='fas fa-pen-square'></i></i></button>",
                orderable: false,
                searchable: false,
                width: "26px",
                title: "Editar" 
            },
            { data: null, defaultContent: "<button type='button' id='btnEliminar' class='btn btn-danger'><i class='fas fa-trash-alt'></i></i></button>", title: "Eliminar"},
        ]
    });
    // Agregar Personal Administrativo
    $(".modal-container").on("click", "#btnSave", function (e) {
        e.preventDefault();
        let Personal = {
            
            "FkTipodocumento": $("#FkTipodocumento").val(),
            "Numerodoc": $("#Numerodoc").val(),
            "Nombres": $("#Nombres").val(),
            "Apellidopaterno": $("#Apellidopaterno").val(),
            "Apellidomaterno": $("#Apellidomaterno").val(),
            "Telefono": $("#Telefono").val(),
            "Direccion": $("#Direccion").val(),
            "Email": $("#Email").val(),
            "fkRoles": $("#FkRoles").val(),
            "Contrasenia": $("#Contrasenia").val()
        }
        Swal.fire({
            title: 'Desea Registrar a este Usuario?',
            showDenyButton: true,
            confirmButtonText: 'Registrar',
            denyButtonText: `Cancelar`,
            denyButtonClass: 'button-cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Usuario/MantenimientoUsuario/CreateUsuario',
                    data: JSON.stringify(Personal),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if (data.success === true) {
                            console.log(data);
                            Swal.fire('Saved!', '', 'success')
                            $('#modal-default').modal('hide');
                             DataTablePersonal.ajax.reload();
                        }
                        else if (data.success === false) {
                            console.log(data);
                            Swal.fire(`Upss! ${data.message}`, '', 'info')
                            $('#modal-default').modal('hide');
                        }
                    },
                    error: function (error) {
                        if (error.status === 400) {
                            Swal.fire('Upss! Ocurrio Algo.', '', 'info')
                        }
                    }
                });
            }
            else if (result.isDenied) {
                Swal.fire('Cambios no aplicado', '', 'info')
                $('#modal-default').modal('hide');
            }
        })
    });

    // Ingresando Update
        TablePersonal.on("click", "#btnEditar", function () {
            let id = DataTablePersonal.row($(this).parents("tr")).data().idUsuario;
            console.log(id);
            InvocarModal(id); 
        });
        $(".modal-container").on("click", "#btnUpdate", function (e) {
            e.preventDefault();
            let Personal = {
                "FkTipodocumento": $("#FkTipodocumento").val(),
                "IdUsuario": $("#IdUsuario").val(),
                "Numerodoc": $("#Numerodoc").val(),
                "Nombres": $("#Nombres").val(),
                "Apellidopaterno": $("#Apellidopaterno").val(),
                "Apellidomaterno": $("#Apellidomaterno").val(),
                "Telefono": $("#Telefono").val(),
                "Direccion": $("#Direccion").val(),
                "Email": $("#Email").val(),
                "fkRoles": $("#FkRoles").val(),
                "Contrasenia": $("#Contrasenia").val()
            }
    
            Swal.fire({
                title: 'Desea actualizar a este Usuario?',
                showDenyButton: true,
                confirmButtonText: 'Actualizar',
                denyButtonText: `Cancelar`,
                denyButtonClass: 'button-cancel'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: '/Usuario/MantenimientoUsuario/UpdateUsuario?id=${id}',
                        data: JSON.stringify(Personal),
                        type: 'POST',
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            console.log(data);
                            if (data.success === true) {
                                console.log(data);
                                Swal.fire('Update!', '', 'success')
                                $('#modal-default').modal('hide');
                                DataTablePersonal.ajax.reload();
                            }
                            else if (data.success === false) {
                                console.log(data);
                                Swal.fire(`Error! ${data.message}`, '', 'info')
                                $('#modal-default').modal('hide');
                            }
                        },
                        error: function (error) {
                            if (error.status === 400) {
                                Swal.fire('Error! .', '', 'info')
                            }
                        }
                    });
                }
                else if (result.isDenied) {
                    Swal.fire('Cambios no aplicados', '', 'info')
                    $('#modal-default').modal('hide');
                }
            })
        });
    // Desactivar un Personal.
    TablePersonal.on("click", "#btnEliminar", function () {
        let id = DataTablePersonal.row($(this).parents("tr")).data().idUsuario;
        let nombre = DataTablePersonal.row($(this).parents("tr")).data().nombres;
        console.log(id); console.log(nombre);
        // Ajax consumir rest metodo delete personal.
        Swal.fire({
            title: 'Estas Seguro?',
            text: `que desea eliminar a este usuario : ${nombre} `,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok, Eliminar esto!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Usuario/DeleteUsuario?id=${id}`,
                    data: JSON.stringify(id),                 
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if (data.success === true) {
                            Swal.fire(
                                'Eliminado!',
                                `El Usuario ${nombre} a sido eliminado.`,
                                'success'
                            )
                            DataTablePersonal.ajax.reload();
                        }
                    },
                    error: function (error) {
                        if (error.status === 400) {
                            Swal.fire('Error! .', '', 'info')
                        }
                    }
                });
            }
        });
    });

});
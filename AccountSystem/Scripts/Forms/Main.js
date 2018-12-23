$(function () {
    $('#form').validate({
        rules: {
            Name: "required",
            LastName: "required",
            PhoneNumber: "required"
        },
        messages: {
            Name: ' <span class="text-danger"> Porfavor ingrese un Nombre </span> ',
            LastName: '<span class="text-danger"> Porfavor ingrese un Apellido </span>',
            PhoneNumber: '<span class="text-danger"> Porfavor ingrese un Numero Telefonico </span>'
        }
    });
    $('#formAddAccount').validate({
        rules: {
            Name: "required"
        },
        messages: {
            Name: '<span class="text-danger"> Porfavor ingrese un Nombre para la cuenta </span><br />'
        }

    });
    $('#Debs').validate({

        rules: {
            Money: "required",
        },
        messages: {
            Money: "<span class='text-danger'> Porfavor ingrese un valor</span>"
        }

    });
    $('#pay').validate({
        rules: {
            Amount: "required"
        },
        messages: {
            Amount: "<span class='text-danger'> Porfavor ingrese un valor</span>"
        }
    })
    
});


$(document).ready(function () {

    $("#PhoneNumber").keyup(function () {
        let phone = $("#PhoneNumber").val();

        if (phone.length < 10) {
            $("#MinNumber").text('Porfavor Ingrese Un Numero Valido').css("color", "red");
        } else {
            $("#MinNumber").text('');
        }
    })
});



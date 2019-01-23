$('.input-daterange').datepicker({
    format: "dd/mm/yyyy"
})
//Get Debs byId
function GetById(id) {
    fetch(`/Deb/GetById/${id}`)
        .then(r => {
            r.json().then(response => {
                document.getElementById("Money").value = response.Deb.Money;
                response.Deb.Descripcion = response.Deb.Descripcion == undefined ? 'Ninguna' : response.Deb.Descripcion;
                document.getElementById("Description").innerHTML = response.Deb.Descripcion;
                document.getElementById("idDeb").value = response.Deb.Id;
            }).catch(e => {
                console.log("error ");
                window.location.reload()
            })
        })
        .catch(e => {
            console.log("error");
        });
}
//Get Payment byId
function GetByIdPayment(id) {
    try {

        fetch(`/Payment/GetById/${id}`).then(r => {
            r.json().then(response => {
                document.getElementById("accountIdPayment").value = response.payment.AccountId;
                document.getElementById("idPaymentUpdate").value = response.payment.Id;
                document.getElementById("quantityPayment").value = response.payment.Quantity;
            }).catch(error => {
                console.log("error json");
            })
        }).catch(e => {
            console.log("error");
        })

    } catch (e) {
        console.log("error connection");
    }
}

//Get Payment byId
function GetById(id) {
    fetch('/Payment/GetById/' + id).then(r => {
        r.json().then(response => {
            document.getElementById('debIdP').value = response.payment.DebId;
            document.getElementById('quantityP').value = response.payment.Quantity;
            document.getElementById('idP').value = response.payment.Id;
            document.getElementById('aId').value = response.payment;

            console.log(response)

        })
            .catch(e => {
            })
    }).catch(e => {
        console.log('error ' + e);
    })
}
//sallout Payments
function PayDeb() {
    swal({
        title: "Esta seguro?",
        text: "Saldara esta deuda!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                fetch('/Payment/PayAll/' + document.getElementById('DebId').value, {
                    method: 'POST',
                    body: JSON.stringify(document.getElementById('DebId').value)
                }).then(r => {
                    r.json().then(res => {
                        swal("Deuda Saldada!", {
                            icon: "success",
                        }).then(r => {
                            location.reload();
                            }).catch (e => {
                                location.reload();

                        })
                    }).catch(e => {
                        swal("Operacion cancelada!");
                    })
                })
            }
        }).catch(e => {
                swal("Operacion cancelada!");
        });
             
}

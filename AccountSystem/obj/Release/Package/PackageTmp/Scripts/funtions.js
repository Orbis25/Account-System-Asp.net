$('.input-daterange').datepicker({
    format: "dd/mm/yyyy"
});
//Get Debs byId
function GetByIdDeb(id) {
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

//detail
function downloadPdf(name , tomado , restante , cliente , cuenta) {
    var doc = new jsPDF();
    //html = $('#' + contentId).html();
    //specialElementHandlers = {};
    doc.line(10, 12, 191, 12)// horizontal line
    doc.setFontSize(13)
    doc.text(60, 10, 'Cuentas por cobrar comprobante (deudas)')

    doc.setTextColor(100)
    doc.setFontSize(11)
    doc.text(20, 20, 'Alias Cuenta:')
    doc.setTextColor(100)
    doc.text(20, 30, 'Cliente:')
    doc.setTextColor(100)
    doc.text(20, 40, 'Total tomado:')
    doc.setTextColor(100)
    doc.text(20, 50, 'Total restante:')

    doc.setTextColor(100)
    doc.text(45, 20, `${cuenta}`)
    doc.setTextColor(100)
    doc.text(35, 30, `${cliente}`)
    doc.setTextColor(100)
    doc.text(50, 40, `$${tomado}`)
    doc.setTextColor(100)
    doc.text(50, 50, `$${restante}`)

    doc.setTextColor(100);
    let f = new Date();
    doc.text(140, 20, 'Fecha: ' + f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear());
    doc.autoTable({ html: '#table', margin: { top: 70 } });
    doc.setTextColor(100);
    doc.text(140, 30, 'No. Factura: '+'Fact - ' + Math.floor(Math.random() * (10 - 50) + 15025));
    doc.save(name + Date.now());
}

function downloadPdfPayments(name, tomado, restante, cliente, cuenta) {
    var doc = new jsPDF();
    doc.line(10, 12, 191, 12)
    doc.setFontSize(13)
    doc.text(60, 10, 'Cuentas por cobrar comprobante (Pagos)')

    doc.setTextColor(100)
    doc.setFontSize(11)
    doc.text(20, 20, 'Alias Cuenta:')
    doc.setTextColor(100)
    doc.text(20, 30, 'Cliente:')
    doc.setTextColor(100)
    doc.text(20, 40, 'Total tomado:')
    doc.setTextColor(100)
    doc.text(20, 50, 'Total restante:')

    doc.setTextColor(100)
    doc.text(45, 20, `${cuenta}`)
    doc.setTextColor(100)
    doc.text(35, 30, `${cliente}`)
    doc.setTextColor(100)
    doc.text(50, 40, `$${tomado}`)
    doc.setTextColor(100)
    doc.text(50, 50, `$${restante}`)

    doc.setTextColor(100);
    let f = new Date();
    doc.text(140, 20, 'Fecha: ' + f.getDate() + "/" + (f.getMonth() + 1) + "/" + f.getFullYear());
    doc.autoTable({ html: '#bady-table1', margin: { top: 70 } });
    doc.setTextColor(100);
    doc.text(140, 30, 'No. Factura: ' + 'Fact - ' + Math.floor(Math.random() * (10 - 50) + 15025));
    doc.save(name + Date.now());
}

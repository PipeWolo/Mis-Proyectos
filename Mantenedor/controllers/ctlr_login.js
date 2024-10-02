var ctlr_login = new ctlr_login();

function ctlr_login() {

    this.Init = function () {
        sessionStorage.clear();
    }

    this.SetListeners = function SetListeners() {

    }

    this.Mensaje = function Mensaje(title, text, type) {
        try {
            swal.fire(title, text, type);
        }
        catch (err) {
            alert(title + ': ' + text);
        }
    }
}
function alterProfile() {
    var xhr = new XMLHttpRequest();
    var data = {
        "account": document.getElementById("account").value,
        "username": document.getElementById("username").value,
        "address": document.getElementById("address").value,
        "phone": document.getElementById("phone").value,
        "email": document.getElementById("email").value
    };

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(this.responseText);
            if (response.message) {
                document.getElementById("message").innerHTML = response.message;
                setTimeout(() => { document.getElementById("message").innerHTML = ""; }, 3000);
            }
            if (response.redirectTo) window.location.href = response.redirectTo;
        }
    };

    xhr.open("POST", "/member/alterprofile", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(JSON.stringify(data));

    return false;
}
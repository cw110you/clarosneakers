function headerOnload() {
    if (Cookies.get("account")) {
        document.getElementById("login-signup").classList.add("d-none");
    }
    else {
        document.getElementById("memberInfo").classList.add("d-none");
    }
}

function check() {
    var input = document.getElementById("keyword");
    var keyword = input.value.trim().substring(0, 50);

    if (keyword) {
        var urlParams = new URLSearchParams();

        urlParams.set("keyword", keyword);
        window.location.href = "/product/search?" + urlParams.toString();
    }
    else {
        input.value = "";
    }

    return false;
}

function getKeyword() {
    var input = document.getElementById("keyword");
    var urlParams = new URLSearchParams(window.location.search);

    if (urlParams.has("keyword")) {
        input.value = urlParams.get("keyword");
    }
}

function redirectTo(url) {
    var urlParams = new URLSearchParams(window.location.search);

    if (window.location.pathname == "/login" || window.location.pathname == "/login/signup") {
        if (urlParams.get("next") == null) urlParams.set("next", encodeURI(window.location.origin));
    }
    else urlParams.set("next", encodeURI(window.location.origin + window.location.pathname));

    window.location.href = url + "?" + urlParams.toString();
}
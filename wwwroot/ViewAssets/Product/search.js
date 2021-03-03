function redirectToPage(dataPage) {
    var urlParams = new URLSearchParams(window.location.search);
    var newPage = 0;

    if (Number.isInteger(parseInt(dataPage))) {
        newPage = dataPage;
    }
    else {
        var currentPage = document.querySelector("#pagination ul li.active span.page-link").getAttribute("data-page");

        switch (dataPage) {
            case "prev":
                newPage = parseInt(currentPage) - 1;
                break;
            case "next":
                newPage = parseInt(currentPage) + 1;
                break;
        }
    }

    urlParams.set("page", newPage);

    window.location.href = "/product/search?" + urlParams.toString();
}

function sort(orderby) {
    var urlParams = new URLSearchParams(window.location.search);

    if (urlParams.get("orderby") == orderby && urlParams.get("order") == "asc")
        urlParams.set("order", "desc");
    else
        urlParams.set("order", "asc");

    urlParams.set("orderby", orderby);
    urlParams.set("page", 1);

    window.location.href = "/product/search?" + urlParams.toString();

}

function redirectToDetail(productId) {
    window.location.href = "/product/detail/" + productId;
}
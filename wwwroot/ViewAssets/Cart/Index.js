function updateQuantity(select) {
    if (Cookies.get("account")) {
        var xhr = new XMLHttpRequest();
        var data = { "productId": parseInt(select.parentNode.getAttribute("data-product-id")), "quantity": parseInt(select.value) };

        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var response = JSON.parse(this.responseText);

                if (response.isSuccess) recalculateSubTotal(document.querySelector('div[data-product-id="' + response.productId + '"]'));
            }
        };

        xhr.open("POST", "/cart/alter", true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.send(JSON.stringify(data));
    }
    else {
        var changedItem = { "productId": parseInt(select.parentNode.getAttribute("data-product-id")), "quantity": parseInt(select.value) };
        var cart = Cookies.get("cart");

        if (cart) {
            cart = JSON.parse(decodeURIComponent(cart));
            for (var i = 0; i < cart.length; i++) {
                if (cart[i].productId == changedItem.productId) {
                    cart[i].quantity = changedItem.quantity;
                    break;
                }
            }
            cart = encodeURIComponent(JSON.stringify(cart));
        }

        Cookies.set("cart", cart);
        recalculateSubTotal(select.parentNode);
    }
}

function recalculateSubTotal(target) {
    var price = target.querySelector("span.price").innerText.replace("$", "");
    var quantity = target.querySelector('select[name="cartQuantity"]').value;
    var subTotalSpan = target.querySelector("span.sub-total");

    subTotalSpan.innerText = "$" + price * quantity;
    recalculateTotal();
}

function recalculateTotal() {
    var totalQuantitySpan = document.querySelector("span.total-quantity");
    var totalPriceSpan = document.querySelector("span.total-price");
    var carts = document.querySelectorAll("div[data-product-id]");
    var totalQuantity = totalPrice = 0;

    carts.forEach(cart => {
        totalQuantity += parseInt(cart.querySelector('select[name="cartQuantity"]').value);
        totalPrice += parseInt(cart.querySelector("span.sub-total").innerText.replace("$", ""));
    });

    totalQuantitySpan.innerText = "(共 " + totalQuantity + " 件)";
    totalPriceSpan.innerText = "$" + totalPrice;
}

function checkCart(checkbox) {
    var checkboxes = document.getElementsByName("cartCheckbox");
    var isAllChecked = true;

    checkboxes.forEach(ckb => {
        isAllChecked = isAllChecked && ckb.checked;
    });

    document.querySelector('form input[type="checkbox"').checked = isAllChecked;
}

function selectAll(checkboxAll) {
    var checkboxes = document.getElementsByName("cartCheckbox");

    checkboxes.forEach(ckb => {
        ckb.checked = checkboxAll.checked;
    });
}

function deleteCart(delBtn) {
    var data = [{ "productId": parseInt(delBtn.parentNode.getAttribute("data-product-id")) }];

    if (Cookies.get("account"))
        deletePOST(data);
    else
        deleteSetCookie(data);
}

function deleteSelectedCart() {
    var checkboxes = document.getElementsByName("cartCheckbox");
    var data = [];

    checkboxes.forEach(ckb => {
        if (ckb.checked) data.push({ "productId": parseInt(ckb.parentNode.getAttribute("data-product-id")) })
    });

    if (data.length > 0) {
        if (Cookies.get("account"))
            deletePOST(data);
        else
            deleteSetCookie(data);
    }
}

function deletePOST(data) {
    var xhr = new XMLHttpRequest();

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            var response = JSON.parse(this.responseText);
            var message = document.getElementById("message");
            console.log(response, response.productIds, message);

            if (response.isSuccess) {
                response.productIds.forEach(productId => {
                    var deletedCartDiv = document.querySelector('div[data-product-id="' + productId + '"]');

                    deletedCartDiv.parentNode.removeChild(deletedCartDiv);
                });

                recalculateTotal();
                message.innerHTML = "商品已刪除";

                if (document.querySelector("div[data-product-id]") == null) {
                    document.getElementById("cart-form").classList.add("d-none");
                    document.getElementById("no-cart").classList.remove("d-none");
                }
            }
            else
                message.innerHTML = "刪除商品失敗";

            message.classList.remove("d-none");
            setTimeout(() => { document.getElementById("message").classList.add("d-none"); }, 3000);
        }
    };

    xhr.open("POST", "/cart/delete", true);
    xhr.setRequestHeader("Content-Type", "application/json");
    xhr.send(JSON.stringify(data));
}

function deleteSetCookie(data) {
    data.forEach(data => {
        var cartRow = document.querySelector('div[data-product-id="' + data.productId + '"]');
        var cartCookie = Cookies.get("cart");

        cartRow.parentNode.removeChild(cartRow);

        if (cartCookie) {
            cartCookie = JSON.parse(decodeURIComponent(cartCookie));

            for (var i = 0; i < cartCookie.length; i++) {
                if (cartCookie[i].productId == data.productId) {
                    cartCookie.splice(i, 1);
                    break;
                }
            }

            if (cartCookie.length > 0) {
                cartCookie = encodeURIComponent(JSON.stringify(cartCookie));
                Cookies.set("cart", cartCookie);
            }
            else {
                document.getElementById("cart-form").classList.add("d-none");
                document.getElementById("no-cart").classList.remove("d-none");
                Cookies.remove("cart");
            }
        }
    });

    recalculateTotal();
    message.innerHTML = "商品已刪除";
    message.classList.remove("d-none");
    setTimeout(() => { document.getElementById("message").classList.add("d-none"); }, 3000);
}
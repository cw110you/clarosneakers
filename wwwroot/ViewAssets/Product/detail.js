function detailOnload() {
    var thumbnailList = document.querySelector(".thumbnail-list");
    var previewDiv = document.querySelector("div.preview");

    if (thumbnailList) thumbnailList.style.width = thumbnailList.childElementCount * 85 + "px";
    thumbnailSlider(0);

    document.body.onresize = () => { thumbnailSlider(0); };

    previewDiv.addEventListener("mousedown", mouseDownPreview);
    previewDiv.addEventListener("touchstart", touchStartPreview);
}

function preview(index) {
    var previewImage = document.querySelector(".preview img");
    var image = document.querySelector("[data-image-index='" + index + "']") ? document.querySelector("[data-image-index='" + index + "']").firstElementChild : null;

    if (image) {
        previewImage.src = image.src;
        document.querySelector("ul.thumbnail-list li.active").classList.remove("active");
        image.parentElement.classList.add("active");
        previewImage.parentElement.setAttribute("data-previewing-index", index);
    }
}

function thumbnailSlider(direction) {
    var thumbnailList = document.querySelector(".thumbnail-list");
    var listIndex = parseInt(thumbnailList.getAttribute("data-index"));
    var imageWidth = thumbnailList.firstElementChild.offsetWidth;
    var containerSize = parseInt(document.querySelector(".thumbnail").offsetWidth / imageWidth);
    var maxIndex = thumbnailList.childElementCount - containerSize;
    var calcIndex = listIndex + direction;

    if (maxIndex < 1) {
        calcIndex = 0;
        document.querySelector("i.btn-prev").style.display = "none";
        document.querySelector("i.btn-next").style.display = "none";
    }
    else {
        if (calcIndex < 0) calcIndex = 0;
        else if (calcIndex > maxIndex) calcIndex = maxIndex;
        document.querySelector("i.btn-prev").style.display = "";
        document.querySelector("i.btn-next").style.display = "";
    }

    thumbnailList.style.left = calcIndex * -imageWidth + "px";
    thumbnailList.setAttribute("data-index", calcIndex);
}

function changeQuantity(variation) {
    var quantityInput = document.getElementById("quantity");
    var quantity = isNaN(quantityInput.value) ? 1 : parseInt(quantityInput.value) + variation;
    var stock = parseInt(quantityInput.getAttribute("data-stock"));

    if (quantity > stock) quantity = stock;
    else if (quantity < 1) quantity = 1;

    quantityInput.value = quantity;
}

function mouseDownPreview(event) {
    var previewDiv = document.querySelector("div.preview");
    previewDiv.dataset.downX = event.offsetX;
    previewDiv.addEventListener("mouseup", mouseUpPreview);
}

function mouseUpPreview(event) {
    var previewDiv = document.querySelector("div.preview");
    var variation = event.offsetX - previewDiv.dataset.downX;
    var previewingIndex = parseInt(previewDiv.getAttribute("data-previewing-index"));

    if (variation > (previewDiv.clientWidth / 5)) preview(previewingIndex - 1);
    else if (variation < (-previewDiv.clientWidth / 5)) preview(previewingIndex + 1);

    previewDiv.removeEventListener("mouseup", mouseUpPreview);
}

function touchStartPreview(event) {
    var previewDiv = document.querySelector("div.preview");
    if (event.touches.length == 1) {
        event.offsetX = event.touches[0].pageX - event.target.x;
        mouseDownPreview(event);
    }

    previewDiv.addEventListener("touchend", touchEndPreview);

}

function touchEndPreview(event) {
    var previewDiv = document.querySelector("div.preview");

    event.offsetX = event.changedTouches[0].pageX - event.target.x;
    mouseUpPreview(event);
    previewDiv.removeEventListener("touchend", touchEndPreview);
}

function addToCart(redirect) {
    var message = document.getElementById("message");

    if (Cookies.get("account")) {
        var xhr = new XMLHttpRequest();
        var data = { "productId": parseInt(document.getElementById("productId").value), "quantity": parseInt(document.getElementById("quantity").value) };

        xhr.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                var response = JSON.parse(this.responseText);

                if (response.isSuccess) {
                    message.innerHTML = "已加入購物車";
                    if (redirect) window.location.href = redirect;
                }
                else
                    message.innerHTML = "加入購物車失敗";

                message.classList.remove("d-none");
            }
        };

        xhr.open("POST", "/cart/add", true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.send(JSON.stringify(data));
    }
    else {
        var newItem = { "productId": parseInt(document.getElementById("productId").value), "quantity": parseInt(document.getElementById("quantity").value) };
        var cart = Cookies.get("cart");

        if (cart) {
            cart = JSON.parse(decodeURIComponent(cart));
            if (!cart.some((item) => {
                if (item.productId == newItem.productId) {
                    item.quantity += newItem.quantity;
                    return true;
                }
                else return false;
            })) cart.push(newItem);
            cart = encodeURIComponent(JSON.stringify(cart));
        }
        else {
            cart = [newItem];
            cart = encodeURIComponent(JSON.stringify(cart));
        }

        Cookies.set("cart", cart);
        message.innerHTML = "已加入購物車";
        message.classList.remove("d-none");

        if (redirect) window.location.href = redirect;
    }

    setTimeout(() => { document.getElementById("message").classList.add("d-none"); }, 3000);
}
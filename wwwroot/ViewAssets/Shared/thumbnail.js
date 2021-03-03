function imgScaler(event, width) {
    var imgTemp = new Image();

    imgTemp.style.display = "none";
    imgTemp.src = event.target.getAttribute("data-src");
    if (width) imgTemp.setAttribute("data-width", width);

    imgTemp.onload = (e) => {
        var parent = e.target.parentNode;
        var imgTemp = e.target;

        if (imgTemp.width == imgTemp.height) {
            parent.removeChild(imgTemp.previousElementSibling);
            imgTemp.style.display = "";
            imgTemp.onload = "";
        }
        else {
            var canvas = document.createElement("canvas");
            var context = canvas.getContext("2d");
            var beginX = beginY = drawWidth = drawHeight = offset = 0;

            canvas.width = canvas.height = drawWidth = drawHeight = imgTemp.getAttribute("data-width") ? imgTemp.getAttribute("data-width") : parent.clientWidth;

            if (imgTemp.width > imgTemp.height) {
                offset = (imgTemp.width - imgTemp.height) / imgTemp.width * canvas.height;
                beginY += offset / 2;
                drawHeight -= offset;
            }
            else {
                offset = (imgTemp.height - imgTemp.width) / imgTemp.height * canvas.width;
                beginX += offset / 2;
                drawWidth -= offset;
            }

            context.rect(0, 0, canvas.width, canvas.height);
            context.fillStyle = "#F9F9F9";
            context.fill();
            context.drawImage(imgTemp, beginX, beginY, drawWidth, drawHeight);
            imgTemp.src = canvas.toDataURL();
        }
    };

    event.target.parentNode.appendChild(imgTemp, event.target.nextSibling);
}    
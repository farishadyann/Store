function AddToCart(d) {
    var pProductID = d.getAttribute("data-dproductid");
    var pUserID = d.getAttribute("data-duserid");
    var pUserName = d.getAttribute("data-dusername");
    var pQuantity = 1;
    var Url = d.getAttribute("data-durl")
    $.ajax({
        type: "POST",
        url: Url,
        data: {
            ProductID_FK : pProductID,
            UserID_FK : pUserID,
            Quantity : pQuantity,
            CreatedBy : pUserName
        },
        success: function (data) {
            var Quantity = parseInt(pQuantity);
            var CartCount = parseInt($("#lblCartCount").text());
            var hasil = CartCount + Quantity;
            $("#lblCartCount").text(hasil);
        },
        error: function (err) {
            console.log("There was an error: " + JSON.stringify(err));
        }
    })
}
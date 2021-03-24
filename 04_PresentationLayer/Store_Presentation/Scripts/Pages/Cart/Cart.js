$(document).ready(function () {
    
    $.ajax({
        type: "POST",
        url: URLUserCart,
        success: function (data) {
            $("#lblCartCount").text(data);
        },
        error: function (err) {
            console.log("There was an error: " + JSON.stringify(err));
        }
    })
})

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
            UserName : pUserName
        },
        success: function (data) {
            $("#lblCartCount").text(data);
        },
        error: function (err) {
            console.log("There was an error: " + JSON.stringify(err));
        }
    })
}

function DeleteCart(d) {
    var pCartID = d.getAttribute("data-cartID");
    var pUserName = d.getAttribute("data-userName");
    var pUrl = d.getAttribute("data-url");
    $.ajax({
        type: "POST",
        url: pUrl,
        data: {
            CartID_PK: pCartID,
            UserName: pUserName
        },
        success: function (data) {
            alert("Success Delete");
            window.locaton.reload;
        },
        error: function (err) {
            console.log("There was an error: " + JSON.stringify(err));
        }
    })
}
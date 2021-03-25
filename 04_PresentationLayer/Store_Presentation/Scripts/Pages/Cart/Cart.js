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

function AddQuantity(d) {
    var Price = parseInt(d.getAttribute("data-price"));
    var pCartID = d.getAttribute("data-cartid");
    var pUserName = d.getAttribute("data-username");
    var pUrl = d.getAttribute("data-url");
    var pQuantity = parseInt(d.value);
    var NewTotal = Price * pQuantity;
    var strTotal = new Intl.NumberFormat('id-ID', { style: 'currency', currency: 'IDR' }).format(NewTotal);
    
    if ($('form[name="ItemCart"]').find('input[name="CheckCart"]:checked').length != 0) {
        var TotalPayment = parseInt($('#lblTotalPayment').text());
        var PriceItem1 = parseInt($(d).closest('form[name="ItemCart"]').find('input[name="TotalPrice"]').val().replace(/[Rp.]+/g, ""));
        TotalPayment = TotalPayment - PriceItem1;
        TotalPayment = TotalPayment + NewTotal;
        $('#lblTotalPayment').text(TotalPayment);
    }
    


    $.ajax({
        type: "POST",
        url: pUrl,
        data: {
            CartID_PK: pCartID,
            Quantity: pQuantity,
            UserName: pUserName
        },
        success: function (data) {
            $(d).closest('form[name="ItemCart"]').find('input[name="TotalPrice"]').val(strTotal.substring(0, strTotal.length - 3));
            
        },
        error: function (err) {
            console.log("There was an error: " + JSON.stringify(err));
        }
    })

    
}

function changeCheckbox(d) {
    AddTotalPayment()
}

function AddTotalPayment() {
    var TotalPayment = 0;
    var formCart = $('form[name="ItemCart"]');
    for (var i = 0; i < formCart.length; i++) {
        if (formCart[i][0].checked) {
            var strTotalPrice = formCart[i][2].value;
            var tot = strTotalPrice.replace(/[Rp.]+/g, "");
            TotalPayment = parseInt(TotalPayment) + parseInt(tot);
            $('#lblTotalPayment').text(TotalPayment);
        }
    }
    if (formCart.find('input[name="CheckCart"]:checked').length == 0) {
        $('#lblTotalPayment').text("");
    }
}
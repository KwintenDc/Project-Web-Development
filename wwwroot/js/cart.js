document.addEventListener("DOMContentLoaded", function () {

    var quantityInputs = document.querySelectorAll('input[type="number"]');
    quantityInputs.forEach(function (input) {
        input.addEventListener("change", function () {
            updateTotal(input);
            updateSubtotal();
            updateTotalPrice();

            var orderId = input.id.replace('quantity ', '');
            console.log(orderId);

            var xhr = new XMLHttpRequest();
            xhr.open('POST', '/Home/UpdateQuantity');
            xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            xhr.onreadystatechange = function () {
                if (xhr.readyState === XMLHttpRequest.DONE) {
                    if (xhr.status === 200) {
                        //...
                    } else {
                        console.error('Error:', xhr.responseText);
                    }
                }
            };
            xhr.send('orderDetailId=' + encodeURIComponent(orderId) + '&quantity=' + encodeURIComponent(input.value));
        });
    });

    var removeButtons = document.querySelectorAll('.btn.btn-sm.btn-danger');
    removeButtons.forEach(function (button) {
        button.addEventListener("click", function () {
            removeItem(button);
            updateSubtotal();
            updateTotalPrice();
        });
    });

    // Function to update total price when quantity changes
    function updateTotal(input) {
        var quantity = parseInt(input.value);
        var priceString = input.parentElement.previousElementSibling.textContent.replace(/[^\d,.]/g, '');
        var price = parseFloat(priceString.replace(',', '.')); 
        var totalPriceElement = input.parentElement.nextElementSibling;
        var totalPrice = quantity * price;
        totalPriceElement.textContent = '€' + totalPrice.toFixed(2).replace('.', ',');
    }

    // Function to update subtotal
    function updateSubtotal() {
        var totalPrices = document.querySelectorAll('td:nth-child(4)');
        var subtotal = 0;
        totalPrices.forEach(function (totalPrice) {
            var priceString = totalPrice.textContent.replace(/[^\d,.]/g, '');
            var price = parseFloat(priceString.replace(',', '.'));
            subtotal += price;
        });
        var subtotalElement = document.getElementById('subtotal');
        subtotalElement.textContent = '€' + subtotal.toFixed(2).replace('.', ',');
    }

    // Function to update total price (subtotal + shipping)
    function updateTotalPrice() {
        var subtotalElement = document.getElementById('subtotal');
        var shippingCostsString = document.getElementById('shipping').textContent.replace(/[^\d,.]/g, '');
        var shippingCosts = parseFloat(shippingCostsString.replace(',', '.'));
        var total = parseFloat(subtotalElement.textContent.replace(/[^\d,.]/g, '').replace(',', '.')) + shippingCosts;
        var totalElement = document.getElementById('total');
        totalElement.textContent = '€' + total.toFixed(2).replace('.', ',');
    }

    // Function to remove item from cart
    function removeItem(button) {
        var row = button.closest('tr');
        var itemId = button.dataset.id; 
        row.remove();

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Home/RemoveFromCart');
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    //...
                } else {
                    console.error('Error:', xhr.responseText);
                }
            }
        };
        xhr.send('orderDetailId=' + encodeURIComponent(itemId));
    }

    var chkBtn = document.getElementById("checkoutBtn");
    chkBtn.addEventListener('click', function () {
        console.log("click")
        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Home/Checkout');
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    window.location.reload();
                } else {
                    console.error('Error:', xhr.responseText);
                }
            }
        };
        xhr.send();
    });
});

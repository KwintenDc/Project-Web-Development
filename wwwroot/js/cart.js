document.addEventListener("DOMContentLoaded", function () {

    var quantityInputs = document.querySelectorAll('input[type="number"]');
    quantityInputs.forEach(function (input) {
        input.addEventListener("change", function () {
            updateTotal(input);
            updateSubtotal();
            updateTotalPrice();
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
        var price = parseFloat(priceString.replace(',', '.')); // Replace comma with dot
        var totalPriceElement = input.parentElement.nextElementSibling;
        var totalPrice = quantity * price;
        totalPriceElement.textContent = '€' + totalPrice.toFixed(2).replace('.', ','); 
    }

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
        console.log(shippingCostsString);
        var shippingCosts = parseFloat(shippingCostsString.replace(',', '.'));
        console.log(shippingCosts);
        var total = parseFloat(subtotalElement.textContent.replace(/[^\d,.]/g, '').replace(',', '.')) + shippingCosts;
        var totalElement = document.getElementById('total');
        totalElement.textContent = '€' + total.toFixed(2).replace('.', ','); 
    }


    function removeItem(button) {
        var row = button.closest('tr');
        row.remove();
    }
});

$(document).ready(function () {
    $('.update-price-btn').click(function () {
        var itemId = $(this).data('item-id');
        var newPrice = $(this).closest('tr').find('.price-input').val().toString("0.00");

        console.log("Price : " + newPrice + " ID : " + itemId);

        $.ajax({
            url: '/Home/UpdateItemPrice',
            method: 'POST',
            data: {
                itemId: itemId,
                newPrice: newPrice
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        });
    });
});
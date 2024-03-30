$(document).ready(function () {
    $('.edit-btn').click(function () {
        $(this).closest('tr').next('.order-details').toggle();
    });

    $('.quantity-input').change(function () {
        var quantity = $(this).val();
        var orderDetailId = $(this).data('orderdetail-id');
        var totalPriceElement = $(this).closest('.order-details').find('#' + orderDetailId);
        $.ajax({
            url: '/Home/UpdateQuantityAdmin', 
            method: 'POST',
            data: {
                orderDetailId: orderDetailId,
                quantity: quantity
            },
            success: function (response) {
                totalPriceElement.text(response.totalPrice.toFixed(2));

                var orderId = totalPriceElement.closest('.order-details').attr('id').split('-')[1];
                var totalAmount = 0;
                $('#OrderId-' + orderId).find('span').each(function () {
                    totalAmount += parseFloat($(this).text().replace('€', ''));
                });

                $('#total-amount-' + orderId).text('€' + totalAmount.toFixed(2));
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        });
    });

    // Delete single order
    $('.delete-btn').click(function () {
        var orderId = $(this).closest('tr').attr('id').split('-')[1];
        deleteOrder([orderId]);
    });

    // Delete selected orders
    $('#delete-selected-btn').click(function () {
        var selectedOrderIds = [];
        $('.order-checkbox:checked').each(function () {
            selectedOrderIds.push($(this).data('order-id'));
        });

        // Check if any orders are selected before sending the request
        if (selectedOrderIds.length > 0) {
            deleteOrder(selectedOrderIds);
        } else {
            // No orders selected, handle accordingly (e.g., display a message)
            console.log('No orders selected for deletion.');
        }
    });

    // Function to delete orders
    function deleteOrder(orderIds) {
        // Send POST request with order IDs to delete
        $.ajax({
            url: '/Home/DeleteSelectedOrders',
            method: 'POST',
            data: {
                selectedOrderIds: orderIds
            },
            success: function (response) {
                // Remove the selected rows from the table
                $.each(orderIds, function (index, orderId) {
                    $('#OrderIdTable-' + orderId).remove();
                    $('#OrderId-' + orderId).remove();
                });
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        });
    }
});

$(document).ready(function () {

    $('.btn-save').click(function () {
        var row = $(this).closest('tr');
        var orderId = row.data('order-id'); // Only for existing orders
        if (!orderId) return; // Ignore new order save button

        // Get UserId
        var userId = row.find('input[data-field="UserId"]').val() || row.find('td').first().text().trim();

        var updatedOrder = {
            Id: orderId,
            UserId: userId,
            Address: {
                Street: row.find('input[data-field="Street"]').val(),
                City: row.find('input[data-field="City"]').val(),
                PostalCode: row.find('input[data-field="PostalCode"]').val(),
                Country: row.find('input[data-field="Country"]').val()
            },
            OrderItems: []
        };

        // Collect order items if any are shown
        row.next('.order-items-row').find('.order-item-row').each(function () {
            updatedOrder.OrderItems.push({
                ShopItemId: $(this).find('select[data-field="ShopItemId"]').val(),
                Count: $(this).find('input[data-field="Count"]').val()
            });
        });

        // Make AJAX call to update the order
        $.ajax({
            url: '/Admin/Orders/Edit',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(updatedOrder),
            success: function (response) {
                if (response.success) {
                    alert('Order updated successfully!');
                } else {
                    alert('Error updating order.');
                }
            },
            error: function () {
                alert('Failed to save changes.');
            }
        });
    });


    // Show Order Items button functionality for new and existing orders
    $(document).on('click', '.btn-toggle-items', function () {
        var row = $(this).closest('tr');
        var orderRow = row.next('.order-items-row');
        orderRow.toggle(); // Toggle the visibility of order items
    });

    // Add New Order button functionality
    $('#addNewOrderButton').click(function () {
        // Hide the "Add New Order" button and show the new order row
        $('#addNewOrderButton').hide();
        $('#newOrderRow').show();

        // Add an empty item list row
        var newItemRow = `
                    <tr class="order-items-row">
                        <td colspan="7">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>Shop Item</th>
                                        <th>Count</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <!-- Empty initially, items will be added dynamically -->
                                </tbody>
                            </table>
                            <button class="btn btn-primary btn-add-item">Add Item</button>
                        </td>
                    </tr>
                `;
        $('#newOrderRow').after(newItemRow);
    });

    // Cancel button functionality for adding a new order
    $(document).on('click', '.btn-cancel-new-order', function () {
        // Find the new order row and its associated item list row
        var newOrderRow = $(this).closest('tr'); // The new order row
        var orderItemsRow = newOrderRow.next('.order-items-row'); // The associated item list row

        // Remove both the new order row and the item list row
        newOrderRow.hide();
        orderItemsRow.hide();

        // Show the "Add New Order" button again
        $('#addNewOrderButton').show();
    });

    // Save New Order button functionality
    $(document).on('click', '.btn-save-new-order', function () {
        var row = $(this).closest('tr');

        // Collect new order data
        var newOrder = {
            UserId: row.find('input[data-field="UserId"]').val()?.trim(),
            Address: {
                Street: row.find('input[data-field="Street"]').val()?.trim(),
                City: row.find('input[data-field="City"]').val()?.trim(),
                PostalCode: row.find('input[data-field="PostalCode"]').val()?.trim(),
                Country: row.find('input[data-field="Country"]').val()?.trim()
            },
            OrderItems: []
        };

        // Collect order items if present
        row.next('.order-items-row').find('.order-item-row').each(function () {
            var shopItemId = $(this).find('select[data-field="ShopItemId"]').val();
            var count = $(this).find('input[data-field="Count"]').val();
            if (shopItemId && count) {
                newOrder.OrderItems.push({
                    ShopItemId: parseInt(shopItemId, 10),
                    Count: parseInt(count, 10)
                });
            }
        });

        // Validate required fields
        if (!newOrder.UserId || !newOrder.Address.Street || !newOrder.Address.City ||
            !newOrder.Address.PostalCode || !newOrder.Address.Country) {
            alert('Please fill out all required fields.');
            return;
        }

        // Make AJAX call to create the new order
        $.ajax({
            url: '/Admin/Orders/Create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(newOrder),
            success: function (response) {
                if (response.success) {
                    location.reload(); // Reload the page to show the new order in the list
                    alert('Order created successfully!');
                } else {
                    alert('Error adding new order: ' + response.message);
                }
            },
            error: function () {
                alert('Failed to add new order.');
            }
        });
    });

    // Add Item button functionality (add new item row)
    $(document).on('click', '.btn-add-item', function () {
        var row = $(this).closest('.order-items-row');
        var newItemRow = `
                    <tr class="order-item-row">
                        <td>
                            <select class="form-control" data-field="ShopItemId">
                                @foreach (var shopItem in Model.ShopItems)
                                {
                                    <option value="@shopItem.Id">@shopItem.BookTitle</option>
                                }
                            </select>
                        </td>
                        <td>
                            <input type="number" class="form-control" data-field="Count" value="1" />
                        </td>
                        <td>
                            <button class="btn btn-danger btn-delete-item">Remove</button>
                        </td>
                    </tr>
                `;
        row.find('tbody').append(newItemRow); // Append the new item row to the order items table
    });

    // Remove Item functionality
    $(document).on('click', '.btn-delete-item', function () {
        $(this).closest('tr').remove(); // Remove the item row
    });

    // Delete button functionality for orders
    $('.btn-delete').click(function () {
        var row = $(this).closest('tr');
        var orderId = row.data('order-id');

        // Make AJAX call to delete the order
        $.ajax({
            url: '/Admin/Orders/Delete/' + orderId,
            type: 'POST',
            success: function (response) {
                if (response.success) {
                    row.remove();
                    alert('Order deleted successfully!');
                } else {
                    alert('Error deleting order.');
                }
            },
            error: function () {
                alert('Failed to delete order.');
            }
        });
    });

});
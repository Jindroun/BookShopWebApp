$(document).ready(function () {
    // Save button functionality for existing books
    $('.btn-save').click(function () {
        var row = $(this).closest('tr');
        var bookId = row.data('book-id'); // Only for existing books
        if (!bookId) return; // Ignore new book save button

        var updatedBook = {
            Id: bookId,
            Title: row.find('input[data-field="Title"]').val(),
            AuthorId: row.find('select[data-field="AuthorId"]').val(),
            PublisherId: row.find('select[data-field="PublisherId"]').val(),
            GenreId: row.find('select[data-field="GenreId"]').val(),
            Isbn: row.find('input[data-field="Isbn"]').val(),
            Description: row.find('input[data-field="Description"]').val(),
        };

        // Make AJAX call to update the book
        $.ajax({
            url: '/Admin/Books/Edit',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(updatedBook),
            success: function (response) {
                if (response.success) {
                    alert('Book updated successfully!');
                } else {
                    alert('Error updating book.');
                }
            },
            error: function () {
                alert('Failed to save changes.');
            }
        });
    });

    // Delete button functionality for books
    $('.btn-delete').click(function () {
        var row = $(this).closest('tr');
        var bookId = row.data('book-id');

        // Make AJAX call to delete the book
        $.ajax({
            url: '/Admin/Books/Delete/' + bookId,
            type: 'POST',
            success: function (response) {
                if (response.success) {
                    row.remove();
                    alert('Book deleted successfully!');
                } else {
                    alert('Error deleting book.');
                }
            },
            error: function () {
                alert('Failed to delete book.');
            }
        });
    });

    // Add New Book button functionality
    $('#addNewBookButton').click(function () {
        $('#newBookRow').show();
        $('#addNewBookButton').hide();
    });

    // Cancel button functionality for adding a new book
    $('.btn-cancel-new').click(function () {
        $('#newBookRow').hide();
        $('#addNewBookButton').show();
    });

    // Save New Book button functionality
    $('#newBookRow .btn-save-new').click(function () {
        var row = $('#newBookRow');
        var newBook = {
            Title: row.find('input[data-field="Title"]').val(),
            AuthorId: row.find('select[data-field="AuthorId"]').val(),
            PublisherId: row.find('select[data-field="PublisherId"]').val(),
            GenreId: row.find('select[data-field="GenreId"]').val(),
            Isbn: row.find('input[data-field="Isbn"]').val(),
            Description: row.find('input[data-field="Description"]').val(),
        };

        // Make AJAX call to create the new book
        $.ajax({
            url: '/Admin/Books/Create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(newBook),
            success: function (response) {
                if (response.success) {
                    location.reload(); // Reload the page to show the new book in the list
                    alert('Book created successfully!');
                } else {
                    alert('Error adding new book.');
                }
            },
            error: function () {
                alert('Failed to add new book.');
            }
        });
    });
});
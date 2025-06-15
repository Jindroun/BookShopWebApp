$(document).ready(function () {
    // Save button functionality for existing authors
    $('.btn-save').click(function () {
        var row = $(this).closest('tr');
        var authorId = row.data('author-id'); // Only for existing authors

        var updatedAuthor = {
            Id: authorId,
            FirstName: row.find('input[data-field="FirstName"]').val(),
            LastName: row.find('input[data-field="LastName"]').val()
        };

        // Make AJAX call to update the author
        $.ajax({
            url: '/Admin/Authors/Edit',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(updatedAuthor),
            success: function (response) {
                if (response.success) {
                    alert('Author updated successfully!');
                } else {
                    alert('Error updating author: ' + response.message);
                }
            },
            error: function () {
                alert('Failed to save changes.');
            }
        });
    });

    // Delete button functionality for authors
    $('.btn-delete').click(function () {
        var row = $(this).closest('tr');
        var authorId = row.data('author-id');

        // Make AJAX call to delete the author
        $.ajax({
            url: '/Admin/Authors/Delete/' + authorId,
            type: 'POST',
            success: function (response) {
                if (response.success) {
                    row.remove();
                    alert('Author deleted successfully!');
                } else {
                    alert('Error deleting author: ' + response.message);
                }
            },
            error: function () {
                alert('Failed to delete author.');
            }
        });
    });

    // Add New Author button functionality
    $('#addNewAuthorButton').click(function () {
        $('#newAuthorRow').show();
        $('#addNewAuthorButton').hide();
    });

    // Cancel button functionality for adding a new author
    $('.btn-cancel-new').click(function () {
        $('#newAuthorRow').hide();
        $('#addNewAuthorButton').show();
    });

    // Save New Author button functionality
    $('#newAuthorRow .btn-save-new').click(function () {
        var row = $('#newAuthorRow');
        var newAuthor = {
            FirstName: row.find('input[data-field="FirstName"]').val(),
            LastName: row.find('input[data-field="LastName"]').val()
        };

        // Make AJAX call to create the new author
        $.ajax({
            url: '/Admin/Authors/Create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(newAuthor),
            success: function (response) {
                if (response.success) {
                    location.reload(); // Reload the page to show the new author in the list
                    alert('Author added successfully!');
                } else {
                    alert('Error adding new author: ' + response.message);
                }
            },
            error: function () {
                alert('Failed to add new author.');
            }
        });
    });
});
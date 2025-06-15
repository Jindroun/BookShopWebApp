$(document).ready(function () {
    // Save button functionality for existing genres
    $('.btn-save').click(function () {
        var row = $(this).closest('tr');
        var genreId = row.data('genre-id'); // Only for existing genres
        if (!genreId) return; // Ignore new genre save button

        var updatedGenre = {
            Id: genreId,
            Name: row.find('input[data-field="Name"]').val()
        };

        // Make AJAX call to update the genre
        $.ajax({
            url: '/Admin/Genres/Edit',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(updatedGenre),
            success: function (response) {
                if (response.success) {
                    alert('Genre updated successfully!');
                } else {
                    alert('Error updating genre.');
                }
            },
            error: function () {
                alert('Failed to save changes.');
            }
        });
    });

    // Delete button functionality for genres
    $('.btn-delete').click(function () {
        var row = $(this).closest('tr');
        var genreId = row.data('genre-id');

        // Make AJAX call to delete the genre
        $.ajax({
            url: '/Admin/Genres/Delete/' + genreId,
            type: 'POST',
            success: function (response) {
                if (response.success) {
                    row.remove();
                    alert('Genre deleted successfully!');
                } else {
                    alert('Error deleting genre.');
                }
            },
            error: function () {
                alert('Failed to delete genre.');
            }
        });
    });

    // Add New Genre button functionality
    $('#addNewGenreButton').click(function () {
        $('#newGenreRow').show();
        $('#addNewGenreButton').hide();
    });

    // Cancel button functionality for adding a new genre
    $('.btn-cancel-new').click(function () {
        $('#newGenreRow').hide();
        $('#addNewGenreButton').show();
    });

    // Save New Genre button functionality
    $('#newGenreRow .btn-save-new').click(function () {
        var row = $('#newGenreRow');
        var newGenre = {
            Name: row.find('input[data-field="Name"]').val()
        };

        // Make AJAX call to create the new genre
        $.ajax({
            url: '/Admin/Genres/Create',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(newGenre),
            success: function (response) {
                if (response.success) {
                    location.reload(); // Reload the page to show the new genre in the list
                    alert('Genre created successfully!');
                } else {
                    alert('Error adding new genre.');
                }
            },
            error: function () {
                alert('Failed to add new genre.');
            }
        });
    });
});
document.addEventListener("DOMContentLoaded", () => {
    let debounceTimeout;
    const searchInput = document.getElementById("searchInput");
    const dropdown = document.getElementById("searchDropdown");

    if (!searchInput || !dropdown) return; // defensive fallback

    searchInput.addEventListener("keyup", (event) => {
        const query = event.target.value.trim();

        clearTimeout(debounceTimeout);

        if (query.length > 0) {
            debounceTimeout = setTimeout(() => {
                fetch(`/Home/SearchBooks?query=${encodeURIComponent(query)}`)
                    .then(response => {
                        if (!response.ok) {
                            throw new Error(`HTTP error! Status: ${response.status}`);
                        }
                        return response.json();
                    })
                    .then(data => {
                        const { books, authors, genres } = data;

                        let booksHtml = '<div class="px-3 mb-1 border-bottom"><strong>Books</strong></div>';
                        booksHtml += books.length > 0
                            ? books.map(book => `
                                <a href="/ItemDetail/Index/${book.id}" class="dropdown-item">
                                    <strong>${book.bookTitle}</strong><br />
                                    <small>by ${book.authorFirstName} ${book.authorLastName}</small>
                                </a>
                            `).join('')
                            : '<span class="dropdown-item text-muted">No books found</span>';

                        let authorsHtml = '<div class="px-3 mb-1 border-bottom"><strong>Authors</strong></div>';
                        authorsHtml += authors.length > 0
                            ? authors.map(author => `
                                <span class="dropdown-item">${author.firstName} ${author.lastName}</span>
                            `).join('')
                            : '<span class="dropdown-item text-muted">No authors found</span>';

                        let genresHtml = '<div class="px-3 mb-1 border-bottom"><strong>Genres</strong></div>';
                        genresHtml += genres.length > 0
                            ? genres.map(genre => `
                                <span class="dropdown-item">${genre.name}</span>
                            `).join('')
                            : '<span class="dropdown-item text-muted">No genres found</span>';

                        dropdown.innerHTML = booksHtml + authorsHtml + genresHtml;
                        dropdown.style.display = "block";
                    })
                    .catch(error => {
                        console.error("Error fetching search results:", error);
                        dropdown.style.display = "none";
                    });
            }, 300);
        } else {
            dropdown.style.display = "none";
        }
    });
});

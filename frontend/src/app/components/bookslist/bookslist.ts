import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faPenToSquare, faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { environment } from '../../../environments/environment';

interface Book {
  id: number | null;
  title: string;
  author: string;
}

@Component({
  selector: 'app-bookslist',
  imports: [CommonModule, FormsModule, FontAwesomeModule],
  templateUrl: './bookslist.html',
  styleUrl: './bookslist.css',
})
export class Bookslist implements OnInit {
  faPenToSquare = faPenToSquare;
  faTrash = faTrashCan;

  books: Book[] = [];

  newBookTitle: string = '';
  newBookAuthor: string = '';
  errorMessage: string = '';

  showErrorMessage(message: string) {
    this.errorMessage = message;
    setTimeout(() => {
      this.errorMessage = '';
    }, 3000);
  }

  isBookModalOpen: boolean = false;
  isBookEditModalOpen: boolean = false;
  editingBookIndex: number | null = null;

  toggleBookModal() {
    if (!this.isBookEditModalOpen) {
      this.isBookModalOpen = !this.isBookModalOpen;
    } else {
      this.showErrorMessage('Close the edit book modal before adding a book.');
    }
  }

  toggleBookEditModal(index: number | null) {
    if (index === null) {
      this.isBookEditModalOpen = false;
      this.editingBookIndex = null;
      this.newBookTitle = '';
      this.newBookAuthor = '';
      return;
    }

    if (!this.isBookModalOpen && index >= 0 && index < this.books.length) {
      this.isBookEditModalOpen = true;
      this.editingBookIndex = index;
      this.newBookTitle = this.books[index].title;
      this.newBookAuthor = this.books[index].author;
    } else if (this.isBookModalOpen) {
      this.showErrorMessage('Close the add book modal before editing a book.');
    }
  }

  listClassName = 'list';

  currentDate: Date = new Date();

  http = inject(HttpClient);

  ngOnInit(): void {
    this.getBooks();
  }

  getBooks() {
    this.http.get<Book[]>(`${environment.apiBaseUrl}/api/books`).subscribe({
      next: (data: Book[]) => {
        this.books = data;
      },
      error: (error) => {
        console.error('Error fetching books:', error);
        this.showErrorMessage('Failed to fetch books from API.');
      },
    });
  }

  addNewBook() {
    this.http
      .post<Book>(`${environment.apiBaseUrl}/api/books`, {
        title: this.newBookTitle,
        author: this.newBookAuthor,
      })
      .subscribe({
        next: (book: Book) => {
          this.books.push(book);
          this.newBookTitle = '';
          this.newBookAuthor = '';
          this.isBookModalOpen = false;
        },
        error: (error) => {
          console.error('Error adding book:', error);
          this.showErrorMessage('Failed to add book to API.');
        },
      });
  }

  removeBook(index: number) {
    const book = this.books[index];
    if (!book || book.id === null) {
      this.showErrorMessage('Invalid book ID for deletion.');
      return;
    }
    this.http.delete(`${environment.apiBaseUrl}/api/books/${book.id}`).subscribe({
      next: () => {
        this.books.splice(index, 1);
      },
      error: (error) => {
        console.error('Error deleting book:', error);
        this.showErrorMessage('Failed to delete book from API.');
      },
    });
  }

  editBook(index: number) {
    if (index < 0 || index >= this.books.length) {
      this.showErrorMessage('Invalid book index for editing.');
      return;
    }

    const book = this.books[index];
    if (!book || book.id === null) {
      this.showErrorMessage('Invalid book ID for editing.');
      return;
    }
    this.http
      .put<Book>(`${environment.apiBaseUrl}/api/books/${book.id}`, {
        title: this.newBookTitle,
        author: this.newBookAuthor,
      })
      .subscribe({
        next: (updatedBook: Book) => {
          if (updatedBook && updatedBook.title && updatedBook.author) {
            this.books[index] = updatedBook;
            this.isBookEditModalOpen = false;
            this.editingBookIndex = null;
            this.newBookTitle = '';
            this.newBookAuthor = '';
          } else {
            this.showErrorMessage('Invalid updated book returned from API.');
          }
        },
        error: (error) => {
          console.error('Error updating book:', error);
          this.showErrorMessage('Failed to update book in API.');
        },
      });
  }
}

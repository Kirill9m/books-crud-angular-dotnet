import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faPenToSquare, faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { environment } from '../../../environments/environment';
import { ErrorMessageService } from '../../service/ErrorMessageService';

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
  constructor(public errorMessageService: ErrorMessageService) {}

  newBookTitle: string = '';
  newBookAuthor: string = '';

  isBookModalOpen: boolean = false;
  isBookEditModalOpen: boolean = false;
  editingBookIndex: number | null = null;

  toggleBookModal() {
    if (!this.isBookEditModalOpen) {
      this.isBookModalOpen = !this.isBookModalOpen;
    } else {
      this.errorMessageService.showErrorMessage('Close the edit book modal before adding a book.');
    }
  }

  toggleBookEditModal(bookId: number | null) {
    if (bookId === null) {
      this.isBookEditModalOpen = false;
      this.editingBookIndex = null;
      this.newBookTitle = '';
      this.newBookAuthor = '';
      return;
    }
    const index = this.books.findIndex((b) => b.id === bookId);
    if (index >= 0 && !this.isBookModalOpen) {
      this.isBookEditModalOpen = true;
      this.editingBookIndex = index;
      this.newBookTitle = this.books[index].title;
      this.newBookAuthor = this.books[index].author;
    } else if (this.isBookModalOpen) {
      this.errorMessageService.showErrorMessage('Close the add book modal before editing a book.');
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
        this.errorMessageService.showErrorMessage('Failed to fetch books from API.');
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
          this.errorMessageService.showErrorMessage('Failed to add book to API.');
        },
      });
  }

  removeBook(bookId: number | null) {
    if (bookId === null) {
      this.errorMessageService.showErrorMessage('Invalid book ID for deletion.');
      return;
    }
    const bookIndex = this.books.findIndex((b) => b.id === bookId);
    if (bookIndex === -1) {
      this.errorMessageService.showErrorMessage('Invalid book ID for deletion.');
      return;
    }
    this.http.delete(`${environment.apiBaseUrl}/api/books/${bookId}`).subscribe({
      next: () => {
        this.books.splice(bookIndex, 1);
      },
      error: (error) => {
        console.error('Error deleting book:', error);
        this.errorMessageService.showErrorMessage('Failed to delete book from API.');
      },
    });
  }

  editBook() {
    if (
      this.editingBookIndex === null ||
      this.editingBookIndex < 0 ||
      this.editingBookIndex >= this.books.length
    ) {
      this.errorMessageService.showErrorMessage('Invalid book index for editing.');
      return;
    }

    const book = this.books[this.editingBookIndex];
    if (!book || book.id === null) {
      this.errorMessageService.showErrorMessage('Invalid book ID for editing.');
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
            this.books[this.editingBookIndex!] = updatedBook;
            this.isBookEditModalOpen = false;
            this.editingBookIndex = null;
            this.newBookTitle = '';
            this.newBookAuthor = '';
          } else {
            this.errorMessageService.showErrorMessage('Invalid updated book returned from API.');
          }
        },
        error: (error) => {
          console.error('Error updating book:', error);
          this.errorMessageService.showErrorMessage('Failed to update book in API.');
        },
      });
  }
}

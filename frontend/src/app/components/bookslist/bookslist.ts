import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { faPenToSquare, faTrashCan } from '@fortawesome/free-solid-svg-icons';

interface Book {
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

  addNewBook() {
    this.books.push({ title: this.newBookTitle, author: this.newBookAuthor });
    this.newBookTitle = '';
    this.newBookAuthor = '';
    this.isBookModalOpen = false;
  }

  removeBook(index: number) {
    this.books.splice(index, 1);
  }

  editBook(newTitle: string, newAuthor: string) {
    if (this.editingBookIndex !== null) {
      this.books[this.editingBookIndex] = { title: newTitle, author: newAuthor };
      this.isBookEditModalOpen = false;
      this.editingBookIndex = null;
      this.newBookTitle = '';
      this.newBookAuthor = '';
    }
  }

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
    this.getBooksFromApi();
  }

  getBooksFromApi() {
    this.http.get<Book[]>('http://localhost:5201/books').subscribe({
      next: (data: Book[]) => {
        this.books = data;
      },
      error: (error) => {
        console.error('Error fetching books:', error);
        this.showErrorMessage('Failed to fetch books from API.');
      },
    });
  }
}

import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

interface Book {
  title: string;
  author: string;
}

@Component({
  selector: 'app-bookslist',
  imports: [CommonModule, FormsModule],
  templateUrl: './bookslist.html',
  styleUrl: './bookslist.css',
})
export class Bookslist {
  books: Book[] = [
    {title: 'The Great Gatsby', author: 'F. Scott Fitzgerald'},
    {title: 'To Kill a Mockingbird', author: 'Harper Lee'},
    {title: '1984', author: 'George Orwell'},
  ];

  newBookTitle: string = '';
  newBookAuthor: string = '';

  addNewBook() {
    this.books.push({title: this.newBookTitle, author: this.newBookAuthor});
    this.newBookTitle = '';
    this.newBookAuthor = '';
  }

  //Property binding example
  listClassName = 'list';
}
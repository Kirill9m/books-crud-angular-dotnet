import { Component, inject } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { ErrorMessageService } from '../../service/ErrorMessageService';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../auth/auth.service';
import { faTrashCan } from '@fortawesome/free-solid-svg-icons/faTrashCan';
import { faPenToSquare } from '@fortawesome/free-solid-svg-icons/faPenToSquare';
import { FaIconComponent } from '@fortawesome/angular-fontawesome';
import { faEllipsis } from '@fortawesome/free-solid-svg-icons/faEllipsis';
import { faEyeSlash } from '@fortawesome/free-solid-svg-icons';

interface Quote {
  id: number | null;
  text: string;
}

@Component({
  selector: 'app-quotes-list',
  imports: [FormsModule, FaIconComponent],
  templateUrl: './quotes-list.html',
  styleUrl: './quotes-list.css',
})
export class QuotesList {
  http = inject(HttpClient);
  errorMessageService = inject(ErrorMessageService);
  faPenToSquare = faPenToSquare;
  faTrash = faTrashCan;
  faEllipsis = faEllipsis;
  faEye = faEyeSlash;
  selectedQuoteId: number | null = null;
  editingQuoteIndex: number | null = null;
  quoteEditMode: boolean = false;

  toggleBtnHidden(quoteId: number | null) {
    if (this.selectedQuoteId !== quoteId) {
      this.selectedQuoteId = quoteId;
    }
  }

  newQuoteText: string = '';
  quotes: Quote[] = [];

  ngOnInit(): void {
    this.getQuotes();
  }

  editQuote(quoteId: number | null) {
    if (quoteId !== null) {
      this.toggleBtnHidden(null);
      this.quoteEditMode = true;
      this.editingQuoteIndex = this.quotes.findIndex((q) => q.id === quoteId);
      if (this.editingQuoteIndex !== -1) {
        const quote = this.quotes[this.editingQuoteIndex];
        this.newQuoteText = quote.text;
      } else {
        this.quoteEditMode = false;
        this.editingQuoteIndex = null;
        this.newQuoteText = '';
      }
    } else {
      this.quoteEditMode = false;
      this.editingQuoteIndex = null;
      this.newQuoteText = '';
    }
  }

  getQuotes() {
    this.http.get<Quote[]>(`${environment.apiBaseUrl}/api/quotes`).subscribe({
      next: (quotes: Quote[]) => {
        this.quotes = quotes;
      },
      error: (error) => {
        if (error.status === 401) {
          this.errorMessageService.showErrorMessage('Logga in för att se citat.');
        } else {
          this.errorMessageService.showErrorMessage('Failed to fetch quotes from API.');
        }
      },
    });
  }

  addNewQuote() {
    this.http
      .post<Quote>(`${environment.apiBaseUrl}/api/quotes`, {
        text: this.newQuoteText,
      })
      .subscribe({
        next: (quote: Quote) => {
          this.quotes.push(quote);
          this.newQuoteText = '';
        },
        error: (error) => {
          if (error.status === 401) {
            this.errorMessageService.showErrorMessage('Logga in för att lägga till ett citat.');
          } else {
            this.errorMessageService.showErrorMessage(error.error || 'Failed to add new quote.');
          }
        },
      });
  }

  updateQuote() {
    if (this.editingQuoteIndex === null) {
      this.errorMessageService.showErrorMessage('No quote selected for editing.');
      return;
    }
    const quote = this.quotes[this.editingQuoteIndex];
    if (quote.id === null) {
      this.errorMessageService.showErrorMessage('Invalid quote ID for update.');
      return;
    }
    this.http
      .put<Quote>(`${environment.apiBaseUrl}/api/quotes/${quote.id}`, {
        text: this.newQuoteText,
      })
      .subscribe({
        next: (updatedQuote: Quote) => {
          this.quotes[this.editingQuoteIndex!] = updatedQuote;
          this.newQuoteText = '';
          this.quoteEditMode = false;
          this.editingQuoteIndex = null;
        },
        error: (error) => {
          if (error.status === 401) {
            this.errorMessageService.showErrorMessage('Logga in för att redigera ett citat.');
          } else {
            this.errorMessageService.showErrorMessage(error.error || 'Failed to update quote.');
          }
        },
      });
  }

  removeQuote(quoteId: number | null) {
    if (quoteId === null) {
      this.errorMessageService.showErrorMessage('Invalid quote ID for deletion.');
      return;
    }
    const quoteIndex = this.quotes.findIndex((q) => q.id === quoteId);
    if (quoteIndex === -1) {
      this.errorMessageService.showErrorMessage('Invalid quote ID for deletion.');
      return;
    }
    this.http.delete(`${environment.apiBaseUrl}/api/quotes/${quoteId}`).subscribe({
      next: () => {
        this.quotes.splice(quoteIndex, 1);
      },
      error: (error) => {
        if (error.status === 401) {
          this.errorMessageService.showErrorMessage('Logga in för att ta bort ett citat.');
        } else {
          this.errorMessageService.showErrorMessage('Failed to delete quote from API.');
        }
      },
    });
  }
}

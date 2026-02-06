import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuotesList } from './quotes-list';

describe('QuotesList', () => {
  let component: QuotesList;
  let fixture: ComponentFixture<QuotesList>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [QuotesList],
    }).compileComponents();

    fixture = TestBed.createComponent(QuotesList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

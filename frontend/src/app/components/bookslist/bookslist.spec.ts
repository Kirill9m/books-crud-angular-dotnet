import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Bookslist } from './bookslist';

describe('Bookslist', () => {
  let component: Bookslist;
  let fixture: ComponentFixture<Bookslist>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Bookslist],
    }).compileComponents();

    fixture = TestBed.createComponent(Bookslist);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

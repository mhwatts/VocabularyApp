import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WordLookupComponent } from './word-lookup.component';

describe('WordLookupComponent', () => {
  let component: WordLookupComponent;
  let fixture: ComponentFixture<WordLookupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WordLookupComponent]
    })
      .compileComponents();

    fixture = TestBed.createComponent(WordLookupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should clear current word when user starts typing', () => {
    // Setup: Set up a current word and sorted groups
    component.currentWord = {
      word: 'test',
      phonetic: '/test/',
      partOfSpeechGroups: [],
      source: 'external'
    };
    component.sortedGroups = [
      {
        partOfSpeech: 'noun',
        priority: 1,
        definitions: [{ definition: 'test definition' }],
        isExpanded: false,
        primaryDefinitions: [{ definition: 'test definition' }]
      }
    ];
    component.errorMessage = 'some error';

    // Action: Start typing in search
    component.searchTerm = 'new search';
    component.onSearchInput();

    // Assert: Current word and related data should be cleared
    expect(component.currentWord).toBeNull();
    expect(component.sortedGroups).toEqual([]);
    expect(component.errorMessage).toBe('');
  });
});

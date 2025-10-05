import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { WordLookupResult, PartOfSpeechGroup, SearchSuggestion, POS_PRIORITY } from '../../models/word-lookup.model';

@Component({
  selector: 'app-word-lookup',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './word-lookup.component.html',
  styleUrl: './word-lookup.component.scss'
})
export class WordLookupComponent implements OnInit {
  searchTerm = '';
  suggestions: SearchSuggestion[] = [];
  selectedSuggestionIndex = -1;
  isLoading = false;
  errorMessage = '';
  
  currentWord: WordLookupResult | null = null;
  sortedGroups: PartOfSpeechGroup[] = [];

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {}

  onSearchInput(): void {
    if (this.searchTerm.length >= 2) {
      this.searchUserVocabulary(this.searchTerm);
    } else {
      this.suggestions = [];
    }
  }

  searchUserVocabulary(term: string): void {
    // TODO: Implement user vocabulary search for autocomplete
    // For now, mock some suggestions
    this.suggestions = [
      {
        word: 'hello',
        type: 'existing',
        partOfSpeech: 'noun, interjection',
        preview: 'A greeting or expression of goodwill',
        action: 'Review word'
      },
      {
        word: term,
        type: 'new-search',
        action: 'Search dictionary'
      }
    ];
  }

  selectSuggestion(suggestion: SearchSuggestion): void {
    if (suggestion.type === 'existing') {
      this.viewExistingWord(suggestion.word);
    } else {
      this.searchNewWord(suggestion.word);
    }
    this.suggestions = [];
  }

  viewExistingWord(word: string): void {
    // TODO: Get word from user's vocabulary
    console.log('Viewing existing word:', word);
  }

  searchNewWord(word: string): void {
    this.isLoading = true;
    this.errorMessage = '';
    
    // TODO: Implement the search hierarchy:
    // 1. Check canonical dictionary
    // 2. Call external API if not found
    // 3. Show spelling suggestions if API fails
    
    console.log('Searching for new word:', word);
    this.isLoading = false;
  }

  onSearchSubmit(): void {
    if (this.searchTerm.trim()) {
      this.searchNewWord(this.searchTerm.trim());
    }
  }

  toggleExpandGroup(group: PartOfSpeechGroup): void {
    group.isExpanded = !group.isExpanded;
  }

  hasExistingSuggestions(): boolean {
    return this.suggestions.some(s => s.type === 'existing');
  }

  onKeyUp(event: KeyboardEvent): void {
    if (event.key === 'Enter') {
      this.onSearchSubmit();
    }
  }

  private processWordResult(result: any): void {
    // Process and sort the word definition groups by priority
    this.sortedGroups = result.partOfSpeechGroups
      .sort((a: PartOfSpeechGroup, b: PartOfSpeechGroup) => {
        const priorityA = POS_PRIORITY[a.partOfSpeech as keyof typeof POS_PRIORITY] || 99;
        const priorityB = POS_PRIORITY[b.partOfSpeech as keyof typeof POS_PRIORITY] || 99;
        return priorityA - priorityB;
      });
  }

  private prioritizeDefinitions(definitions: any[]): any[] {
    return definitions
      .sort((a, b) => {
        // Prioritize definitions with examples
        if (a.example && !b.example) return -1;
        if (!a.example && b.example) return 1;
        
        // Then by length (shorter = more common)
        return a.definition.length - b.definition.length;
      })
      .slice(0, 2); // Show top 2 initially
  }
}

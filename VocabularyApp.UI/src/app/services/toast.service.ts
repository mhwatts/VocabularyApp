import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

export interface Toast {
  id: string;
  type: 'success' | 'error' | 'info' | 'warning';
  message: string;
  duration?: number;
}

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  private toasts$ = new BehaviorSubject<Toast[]>([]);

  constructor() { }

  get toasts() {
    return this.toasts$.asObservable();
  }

  show(toast: Omit<Toast, 'id'>): void {
    const id = this.generateId();
    const newToast: Toast = {
      ...toast,
      id,
      duration: toast.duration || 5000
    };

    const currentToasts = this.toasts$.value;
    this.toasts$.next([...currentToasts, newToast]);

    // Auto remove after duration
    if (newToast.duration && newToast.duration > 0) {
      setTimeout(() => {
        this.remove(id);
      }, newToast.duration);
    }
  }

  success(message: string, duration?: number): void {
    this.show({
      type: 'success',
      message,
      duration
    });
  }

  error(message: string, duration?: number): void {
    this.show({
      type: 'error',
      message,
      duration: duration || 7000 // Errors stay longer
    });
  }

  info(message: string, duration?: number): void {
    this.show({
      type: 'info',
      message,
      duration
    });
  }

  warning(message: string, duration?: number): void {
    this.show({
      type: 'warning',
      message,
      duration
    });
  }

  remove(id: string): void {
    const currentToasts = this.toasts$.value;
    this.toasts$.next(currentToasts.filter(toast => toast.id !== id));
  }

  clear(): void {
    this.toasts$.next([]);
  }

  private generateId(): string {
    return Math.random().toString(36).substring(2) + Date.now().toString(36);
  }
}
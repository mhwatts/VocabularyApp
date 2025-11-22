import { TestBed } from '@angular/core/testing';
import { ToastService } from './toast.service';

describe('ToastService', () => {
  let service: ToastService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ToastService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should show success toast', (done) => {
    service.success('Test success message');

    service.toasts.subscribe(toasts => {
      expect(toasts.length).toBe(1);
      expect(toasts[0].type).toBe('success');
      expect(toasts[0].message).toBe('Test success message');
      done();
    });
  });

  it('should show error toast', (done) => {
    service.error('Test error message');

    service.toasts.subscribe(toasts => {
      expect(toasts.length).toBe(1);
      expect(toasts[0].type).toBe('error');
      expect(toasts[0].message).toBe('Test error message');
      done();
    });
  });

  it('should remove toast', () => {
    service.success('Test message');

    service.toasts.subscribe(toasts => {
      if (toasts.length > 0) {
        const toastId = toasts[0].id;
        service.remove(toastId);

        service.toasts.subscribe(updatedToasts => {
          expect(updatedToasts.length).toBe(0);
        });
      }
    });
  });

  it('should clear all toasts', () => {
    service.success('Test 1');
    service.error('Test 2');

    service.clear();

    service.toasts.subscribe(toasts => {
      expect(toasts.length).toBe(0);
    });
  });
});
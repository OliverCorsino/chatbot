import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  @BlockUI() blockUI: NgBlockUI;
  constructor(private toastr: ToastrService) { }

  showSuccessMessage(title: string, message: string): void {
    this.toastr.success(message, title);
  }

  showErrorMessage(title: string, message: string): void {
    this.toastr.error(message, title);
  }

  showWarningMessage(title: string, message: string): void {
    this.toastr.warning(message, title);
  }

  showInfoMessage(title: string, message: string): void {
    this.toastr.info(message, title);
  }

  showErrorMessages(errors: string[], title: string): void {
    const formatedErrors = this.formatErrorMessage(errors);

    this.toastr.info(formatedErrors, title);
  }

  showLoading(): void {
    this.blockUI.start('Loading...');
  }

  hideLoading(): void {
    this.blockUI.stop();
  }

  private formatErrorMessage(errors: string[]): string {
    let response = '<ul>';

    errors.forEach(message => response += `<li class="text-danger">${message}</li>`);

    return response + '</ul>';
  }
}

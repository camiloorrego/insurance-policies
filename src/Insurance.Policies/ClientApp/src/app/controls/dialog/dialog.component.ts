import { MatDialogRef } from '@angular/material';
import { Component } from '@angular/core';

@Component({
  selector: 'app-st-dialog',
  styleUrls: ['dialog.component.scss'],
  templateUrl: 'dialog.component.html',
})
export class DialogComponent {
  public message: any;
  public list: any;
  public message2: string;
  public title: string;
  public isConfirm: boolean;
  public isList: boolean;
  public isMessage2: boolean;

  constructor(public dialogRef: MatDialogRef<DialogComponent>) { }

  selectText(containerid: any) {
    const container = document.getElementById(containerid);
    const range = document.createRange();
    range.selectNode(container);
    window.getSelection().removeAllRanges();
    window.getSelection().addRange(range);
    document.execCommand('copy');
  }
}

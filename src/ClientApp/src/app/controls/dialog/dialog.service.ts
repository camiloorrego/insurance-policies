
import { DialogComponent } from './dialog.component';
import { MatDialogRef, MatDialog, MatDialogConfig } from '@angular/material';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class DialogService {

    constructor(private dialog: MatDialog) { }

    public show(message: string, title: string): Observable<boolean> {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        let dialogRef: MatDialogRef<DialogComponent>;
        dialogRef = this.dialog.open(DialogComponent, dialogConfig);
        dialogRef.componentInstance.message = message;
        dialogRef.componentInstance.title = title;
        dialogRef.componentInstance.isConfirm = false;
        dialogRef.componentInstance.isMessage2 = false;
        return dialogRef.afterClosed();
    }

    public showList(list: any, title: string): Observable<boolean> {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        let dialogRef: MatDialogRef<DialogComponent>;
        dialogRef = this.dialog.open(DialogComponent, dialogConfig);
        dialogRef.componentInstance.list = list;
        dialogRef.componentInstance.title = title;
        dialogRef.componentInstance.isList = true;
        return dialogRef.afterClosed();
    }

    public showWithDetail(message: string, message2: string, title: string): Observable<boolean> {
        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        let dialogRef: MatDialogRef<DialogComponent>;
        dialogRef = this.dialog.open(DialogComponent, dialogConfig);
        dialogRef.componentInstance.message = message;
        dialogRef.componentInstance.message2 = message2;
        dialogRef.componentInstance.title = title;
        dialogRef.componentInstance.isMessage2 = true;
        return dialogRef.afterClosed();
    }

    public confirm(message: string, event: any, title: string): any {

        const dialogConfig = new MatDialogConfig();
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = true;

        let dialogRef: MatDialogRef<DialogComponent>;
        dialogRef = this.dialog.open(DialogComponent, dialogConfig);
        dialogRef.componentInstance.message = message;
        dialogRef.componentInstance.title = title;

        dialogRef.componentInstance.isConfirm = true;
        return dialogRef.afterClosed().subscribe((result) => {
            if (result) {
                event();
            }
        });
    }
}

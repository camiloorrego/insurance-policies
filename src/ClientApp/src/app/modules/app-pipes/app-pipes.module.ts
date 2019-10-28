import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SizeTextPipe } from './size-text.pipe';

@NgModule({
  declarations: [SizeTextPipe],
  imports: [
    CommonModule
  ],
  exports: [SizeTextPipe]
})
export class AppPipesModule { }

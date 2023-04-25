import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ConvertText } from 'src/services/converttext.service';

const routes: Routes = [];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [ConvertText]
})
export class AppRoutingModule { }

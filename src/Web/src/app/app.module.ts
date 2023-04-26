import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// custom classes
import { ConvertTextService } from 'src/services/converttext.service';
import { PatientService } from 'src/services/patient.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [ConvertTextService, PatientService],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavBarComponent } from './components/common/nav-bar/nav-bar.component';
import { LayoutModule } from '@angular/cdk/layout';

import { HomeComponent } from './components/home/home.component';
import { CandidateViewComponent } from './components/candidate/candidate-view/candidate-view.component';
import { CandidateCreateComponent } from './components/candidate/candidate-create/candidate-create.component';
import { CandidateEditComponent } from './components/candidate/candidate-edit/candidate-edit.component';
import { MatImportModule } from './modules/mat-import/mat-import.module';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { CandidateService } from './services/data/candidate.service';
import { DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { QualifcationViewComponent } from './components/candidate/qualifcation-view/qualifcation-view.component';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    HomeComponent,
    CandidateViewComponent,
    CandidateCreateComponent,
    CandidateEditComponent,
    QualifcationViewComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatImportModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    MatNativeDateModule
  ],
  providers: [HttpClient, DatePipe, CandidateService],
  bootstrap: [AppComponent]
})
export class AppModule { }

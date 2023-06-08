import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CandidateCreateComponent } from './components/candidate/candidate-create/candidate-create.component';
import { CandidateEditComponent } from './components/candidate/candidate-edit/candidate-edit.component';
import { CandidateViewComponent } from './components/candidate/candidate-view/candidate-view.component';
import { QualifcationViewComponent } from './components/candidate/qualifcation-view/qualifcation-view.component';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'candidates', component: CandidateViewComponent },
  { path: 'candidate-create', component: CandidateCreateComponent },
  { path: 'candidate-edit/:id', component: CandidateEditComponent },
  { path: 'qualifications/:id', component: QualifcationViewComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './views/home/home.component';
import { LogCrudComponent } from './views/log-crud/log-crud.component';
import { LogCreateComponent } from './components/log/log-create/log-create.component';
import { LogUpdateComponent } from './components/log/log-update/log-update.component';


const routes: Routes = [
  {
    path: "",
    component: HomeComponent
  },
  {
    path: "logs",
    component: LogCrudComponent
  },
  {
    path: "logs/create",
    component: LogCreateComponent
  },
  {
    path: "logs/update/:id",
    component: LogUpdateComponent
  }];

  
  
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

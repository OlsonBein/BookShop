import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DeleteComponent } from 'src/app/shared/components/delete/delete.component';


const routes: Routes = [
  {path: 'delete', component: DeleteComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SharedRoutingModule { }

import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';
//import { AuthGuard } from "./auth/auth.guard";

import { LoginComponent } from "./auth/login/login.component";
import { TodosComponent } from "./todos/todos.component";


const routes: Routes  = [
    {path: '', component: TodosComponent},
    {path: 'login', component: LoginComponent},
    {path: 'todos', component: TodosComponent}
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
    //providers: [AuthGuard]
})
export class AppRoutingModule{}
import { Subject } from 'rxjs';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router'
import { User } from './user.model';
import { AuthData } from './auth-data.model';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';


@Injectable()
export class AuthService {
     authChange = new Subject<boolean>();
     private user: User;

     constructor(private router: Router, private http: HttpClient){}


     login(model: AuthData){
        this.http.post('https://localhost:5001/api/account/login', model).pipe(
            map((response: any) => {
                const user = response;
                if(user){
                    this.user = user;
                    this.authSuccessfully();
                }
            })
        )
        .subscribe();
     }


     getUser(){
         return {...this.user};
     }

     logout(){
        this.user = null;
        this.authChange.next(false);
        this.router.navigate(['/login']);
    }

    isAuth(){
        return this.user != null;
    }

    private authSuccessfully(){
        this.authChange.next(true);
        this.router.navigate(['/todos']);
     }

}
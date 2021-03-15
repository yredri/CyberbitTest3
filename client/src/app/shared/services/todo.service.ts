import { Subject } from 'rxjs';
import { TodoItem } from "../../todos/todo-item.model";
import { Injectable } from '@angular/core';
import { discardPeriodicTasks } from '@angular/core/testing';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable()
export class TodoService{
    baseUrl: string = "https://localhost:5001/api";
    
    todosChanged = new Subject<TodoItem[]>(); 
    private todoList: TodoItem[] = [];

    constructor(private http: HttpClient){}

    fetchTodos(){
        this.http.get(this.baseUrl + '/task').pipe(
            map((response: any) => {
                this.todoList = response;
                this.todosChanged.next([...this.todoList]);
                return [...this.todoList];
            })
        )
        .subscribe();
    }

    addNewTodoTask(task: any){
        this.http.post(this.baseUrl + '/task/addNew', task).pipe(
            map((response: any) => {
                this.todoList.push(response);
                this.todosChanged.next([...this.todoList]);
            })
        ).subscribe();
    }

    completeTask(task: any){
        this.http.post(this.baseUrl + '/task/complete',task ).pipe(
            map((response: any) => {
                let completedTaskIdx = this.todoList.map(item => item.id).indexOf(task.id);
                this.todoList[completedTaskIdx] = response;

                this.todosChanged.next([...this.todoList]);
            })
        ).subscribe();
    }

    deleteProduct(task: any){
        this.http.post(this.baseUrl + '/task/delete', task).pipe(
            map((response: any) => {
                let taskIdx = this.todoList.map(item => item.id).indexOf(task.id);
                this.todoList.splice(taskIdx , 1);
                this.todosChanged.next([...this.todoList]);
            })
        ).subscribe();
    }
}
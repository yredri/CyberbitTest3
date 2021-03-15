import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { TodoService } from 'src/app/shared/services/todo.service';

@Component({
  selector: 'app-new-task',
  templateUrl: './new-task.component.html',
  styleUrls: ['./new-task.component.less']
})
export class NewTaskComponent implements OnInit {

  constructor(private todoService: TodoService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm){
    let todoItem = {
      name: form.value.name,
      dueDate: form.value.dueDate
    };
    console.log(todoItem);
    this.todoService.addNewTodoTask(todoItem);
  }

}

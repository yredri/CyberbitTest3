import { Component, OnInit, ViewChild, AfterViewInit, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Subscription } from 'rxjs';
import { DeleteDialogComponent } from 'src/app/shared/components/delete-dialog/delete-dialog.component';
import { TodoService } from 'src/app/shared/services/todo.service';
import { TodoItem } from '../todo-item.model';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.less']
})
export class TodoListComponent implements OnInit, AfterViewInit, OnDestroy {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  displayedColumns = ['name', 'date', 'status', 'actions']; 
  dataSource = new MatTableDataSource<TodoItem>();
  todosSubscription: Subscription;

  constructor(private dialog: MatDialog, private todoService: TodoService) { }

  ngOnInit(): void {
    this.todosSubscription = this.todoService.todosChanged.subscribe((items: TodoItem[]) => {
      this.dataSource.data = items;
    });
    this.todoService.fetchTodos();
  }

  ngAfterViewInit(){
    this.dataSource.paginator = this.paginator;
  }


  onDeleteTask(item: any){
    const dialogRef = this.dialog.open(DeleteDialogComponent, {data: {
      name: item.name
    }});

    dialogRef.afterClosed().subscribe(result => {
       if(result){
        this.todoService.deleteProduct(item)
       }
    });
    
  }

  onCompleteTask(task: any){
    this.todoService.completeTask(task);
  }

  ngOnDestroy(){
    this.todosSubscription.unsubscribe()
  }

}

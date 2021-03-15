using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Entities;
using API.Enums;
using API.Interfaces;

namespace API.Controllers
{
    public class TaskController : BaseApiController
    {
        //private readonly DataContext _context;
        private readonly ITaskRepository _taskRepository;
        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;

        }

        [HttpPost("addNew")]
        [AllowAnonymous]
        public async Task<ActionResult<TaskDto>> AddNewTask(NewTaskDto task)
        {
            try
            {
                var newTask = new TodoTask
                {
                    Name = task.Name,
                    DueDate = task.DueDate,
                    Completed = false,
                    StatusId = (int)TodoAppEnums.TaskStatus.InProgress
                };

                _taskRepository.Insert(newTask);
                _taskRepository.Save();

                return new TaskDto
                {
                    Id = newTask.Id,
                    Name = newTask.Name,
                    DueDate = task.DueDate,
                    Completed = false,
                    Status = "inprogress"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<TaskDto>> GetTasks()
        {
            try
            {
                var tasks = await _taskRepository.GetTasksAsync();
                var tasksToDisplay = new List<TaskDto>();
                //TODO map from Task to TaskDto
                foreach (var task in tasks)
                {
                    var newTask = new TaskDto
                    {
                        Id = task.Id,
                        Name = task.Name,
                        DueDate = task.DueDate,
                        Completed = task.Completed,
                        Status = GetStatusDescription(task.StatusId)
                    };
                    tasksToDisplay.Add(newTask);
                }
                return tasksToDisplay;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("delete")]
        [AllowAnonymous]
        public async Task<bool> DeleteTask(TaskDto task)
        {
            var taskToRemove = await _taskRepository.GetTaskByIdAsync(task.Id);
            if (taskToRemove != null)
            {
                _taskRepository.DeleteTask(taskToRemove.Id);
                _taskRepository.Save();

                return true;
            }
            return false;
        }

        [HttpPost("complete")]
        [AllowAnonymous]
        public async Task<TaskDto> CompleteTask(TaskDto task)
        {
            try
            {
                var existingTask = await _taskRepository.GetTaskByIdAsync(task.Id);

                if (existingTask != null)
                {
                    DateTime nowTime = DateTime.Now;
                    existingTask.Completed = true;
                    existingTask.StatusId = existingTask.DueDate >= nowTime ? (int)TodoAppEnums.TaskStatus.OnTime : (int)TodoAppEnums.TaskStatus.Late;

                    _taskRepository.Update(existingTask);
                    _taskRepository.Save();

                    return new TaskDto
                    {
                        Id = existingTask.Id,
                        Name = existingTask.Name,
                        DueDate = existingTask.DueDate,
                        Completed = true,
                        Status = GetStatusDescription(existingTask.StatusId)
                    };

                }
                return task;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetStatusDescription(int? statusId)
        {
            var status = string.Empty;

            switch (statusId)
            {
                case 0:
                    status = "inprogress";
                    break;
                case 1:
                    status = "ontime";
                    break;
                case 2:
                    status = "late";
                    break;
            }

            return status;
        }


    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    public interface IToDoItemService
    {
        Task<ToDoItem[]> GetIncompleteItemsAsync();

        Task<bool> AddItemAsync(ToDoItem newItem);

        Task<bool> MarkDoneAsync(Guid id);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Models;
using Microsoft.AspNetCore.Identity;

namespace ToDoApp.Services
{
    public interface IToDoItemService
    {
        Task<ToDoItem[]> GetIncompleteItemsAsync(IdentityUser user);

        Task<bool> AddItemAsync(ToDoItem newItem, IdentityUser user);

        Task<bool> MarkDoneAsync(Guid id, IdentityUser user);
    }
}
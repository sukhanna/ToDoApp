using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ToDoApp.Models;
using ToDoApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ToDoApp.Services
{
    public class ToDoItemsService : IToDoItemService
    {
        private readonly ApplicationDbContext _context;

        public ToDoItemsService(ApplicationDbContext context)
        {
            _context = context;
        }

        async Task<ToDoItem[]> IToDoItemService.GetIncompleteItemsAsync(IdentityUser user) => await _context.Items
            .Where(x => !x.IsDone && x.UserId == user.Id)
            .ToArrayAsync();

        public async Task<bool> AddItemAsync(ToDoItem newItem, IdentityUser user)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(3);
            newItem.UserId = user.Id;
            _context.Items.Add(newItem);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id, IdentityUser user)
        {
            var item = await _context.Items
            .Where(x => x.Id == id && x.UserId == user.Id)
            .SingleOrDefaultAsync();
            if (item == null) return false;
            item.IsDone = true;
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }
    }
}
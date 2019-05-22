using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ToDoApp.Models;
using ToDoApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Services
{
    public class ToDoItemsService : IToDoItemService
    {
        private readonly ApplicationDbContext _context;

        public ToDoItemsService(ApplicationDbContext context)
        {
            _context = context;
        }

        async Task<ToDoItem[]> IToDoItemService.GetIncompleteItemsAsync() => await _context.Items
            .Where(x => !x.IsDone)
            .ToArrayAsync();

        public async Task<bool> AddItemAsync(ToDoItem newItem)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(3);
            _context.Items.Add(newItem);
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> MarkDoneAsync(Guid id)
        {
            var item = await _context.Items
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync();
            if (item == null) return false;
            item.IsDone = true;
            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1; // One entity should have been updated
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entity;
using SystemDAO;
using SystemService.Implementation;

namespace BusManagementSystem.Pages.ViewUsers
{
    public class IndexModel : PageModel
    {
        private readonly SystemDAO.BusManagementSystemContext _context;

        public IndexModel(SystemDAO.BusManagementSystemContext context)
        {
            _context = context;
        }

        public IList<User> User { get; set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.Users
                .Include(u => u.Role).ToListAsync();
        }
    }
}

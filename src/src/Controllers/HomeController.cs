using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Data;
using src.Models;

namespace src.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Todo()
        {
            return View();
        }

        public IActionResult AddEditTodo(int id = 0)
        {
            if (id == 0)
            {
                return View(new Todo());
            }
            else
            {
                return View(_context.Todo.Where(x => x.todoId.Equals(id)).FirstOrDefault());
            }
            
        }


    }
}

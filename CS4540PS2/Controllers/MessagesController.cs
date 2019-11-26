using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CS4540PS2.Models;

namespace CS4540PS2.Controllers
{
    public class MessagesController : Controller
    {
        private readonly LOTDBContext _context;

        public MessagesController(LOTDBContext context)
        {
            _context = context;
        }


        public JsonResult SendMessage(string text, int sender, int receiver)
        {
            /*
            if (user == null)
            {
                return Json(new { success = false, reason = "The user could not be found." });
            }
            */
            Messages message = new Messages();
            message.Text = text;
            message.Sender = sender;
            message.Receiver = receiver;
            try
            {
                _context.Messages.AddAsync(message).Wait();
                _context.SaveChangesAsync();
            } 
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            

          
            return Json(new { success = true});
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            var lOTDBContext = _context.Messages.Include(m => m.ReceiverNavigation).Include(m => m.SenderNavigation);
            return View(await lOTDBContext.ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .Include(m => m.ReceiverNavigation)
                .Include(m => m.SenderNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // GET: Messages/Create
        public IActionResult Create()
        {
            ViewData["Receiver"] = new SelectList(_context.UserLocator, "Id", "UserLoginEmail");
            ViewData["Sender"] = new SelectList(_context.UserLocator, "Id", "UserLoginEmail");
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,Date,Sender,Receiver")] Messages messages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Receiver"] = new SelectList(_context.UserLocator, "Id", "UserLoginEmail", messages.Receiver);
            ViewData["Sender"] = new SelectList(_context.UserLocator, "Id", "UserLoginEmail", messages.Sender);
            return View(messages);
        }

        // GET: Messages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages.FindAsync(id);
            if (messages == null)
            {
                return NotFound();
            }
            ViewData["Receiver"] = new SelectList(_context.UserLocator, "Id", "UserLoginEmail", messages.Receiver);
            ViewData["Sender"] = new SelectList(_context.UserLocator, "Id", "UserLoginEmail", messages.Sender);
            return View(messages);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,Date,Sender,Receiver")] Messages messages)
        {
            if (id != messages.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessagesExists(messages.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Receiver"] = new SelectList(_context.UserLocator, "Id", "UserLoginEmail", messages.Receiver);
            ViewData["Sender"] = new SelectList(_context.UserLocator, "Id", "UserLoginEmail", messages.Sender);
            return View(messages);
        }

        // GET: Messages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var messages = await _context.Messages
                .Include(m => m.ReceiverNavigation)
                .Include(m => m.SenderNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messages == null)
            {
                return NotFound();
            }

            return View(messages);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var messages = await _context.Messages.FindAsync(id);
            _context.Messages.Remove(messages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessagesExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}

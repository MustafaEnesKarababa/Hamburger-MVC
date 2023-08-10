using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentitySonProje.Data;
using IdentitySonProje.Entities;
using IdentitySonProje.Models.ViewModels;

namespace IdentitySonProje.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExtrasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExtrasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Extras
        public async Task<IActionResult> Index()
        {
            return _context.Extras != null ?
                        View(await _context.Extras.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Extras'  is null.");
        }

        // GET: Admin/Extras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Extras == null)
            {
                return NotFound();
            }

            var extra = await _context.Extras
                .FirstOrDefaultAsync(m => m.ExtraId == id);

            if (extra == null)
            {
                return NotFound();
            }

            return View(extra);
        }

        // GET: Admin/Extras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Extras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExtraId,Name,Price,ImageName")] Extra extra, IFormFile ImageName)
        {
            if (ModelState.IsValid)
            {
                _context.Add(extra);
                Guid guid = Guid.NewGuid();
                string newFileName = guid.ToString() + "_" + ImageName.FileName;
                extra.ImageName = newFileName;
                FileStream fs = new FileStream("wwwroot/ExtraImages/" + newFileName, FileMode.Create);

                await ImageName.CopyToAsync(fs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(extra);
        }

        // GET: Admin/Extras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Extras == null)
            {
                return NotFound();
            }

            var extra = await _context.Extras.FindAsync(id);
            if (extra == null)
            {
                return NotFound();
            }
            return View(extra);
        }

        // POST: Admin/Extras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ExtraId,Name,Price,ImageName")] Extra extras, IFormFile ImageName)
        {

            Extra extra = _context.Extras.Find(id);
            extra.Name = extras.Name;
            extra.Price = extras.Price;
            extra.ImageName = extra.ImageName;

            if (!ModelState.IsValid)
            {
                try
                {
                    if (extras.ImageName != null)
                    {
                        Guid guid = Guid.NewGuid();
                        string newFileName = guid.ToString() + "_" + ImageName.FileName;
                        extra.ImageName = newFileName;
                        FileStream fs = new FileStream("wwwroot/ExtraImages/" + newFileName, FileMode.Create);
                        await ImageName.CopyToAsync(fs);
                    }

                    //if (ImageName != null)
                    //{
                    //}

                    _context.Update(extra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExtraExists(extra.ExtraId))
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
            return View(extra);
        }

        // GET: Admin/Extras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Extras == null)
            {
                return NotFound();
            }

            var extra = await _context.Extras
                .FirstOrDefaultAsync(m => m.ExtraId == id);
            if (extra == null)
            {
                return NotFound();
            }

            return View(extra);
        }

        // POST: Admin/Extras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Extras == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Extras'  is null.");
            }
            var extra = await _context.Extras.FindAsync(id);
            if (extra != null)
            {
                _context.Extras.Remove(extra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExtraExists(int id)
        {
            return (_context.Extras?.Any(e => e.ExtraId == id)).GetValueOrDefault();
        }
    }
}

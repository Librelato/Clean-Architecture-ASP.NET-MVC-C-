using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService) {
            _categoryService=categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetCategories();
            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var category = await _categoryService.GetById(id);
            if (category == null) return NotFound();    
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var category = await _categoryService.GetById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var category = await _categoryService.GetById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Add(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.Update(category);
                } catch(Exception ex)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int? id)
        {
            await _categoryService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

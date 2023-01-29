using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(IProductService productService
                                , ICategoryService categoryService
                                , IWebHostEnvironment environment)
        {
            _productService=productService;
            _categoryService=categoryService;
            _environment=environment;           
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetProducts();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetCategories(), "Id", "Name"); //vai ser usado lá na combo da tela de create.
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.Add(productDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id==null) { return NotFound(); }

            var productDto = await _productService.GetById(id);
            if (productDto==null) { return NotFound(); }

            var categories = await _categoryService.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.Update(productDTO);
                return RedirectToAction(nameof(Index));
            }

            return View(productDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id==null) { return NotFound(); }
            
            var productDto = await _productService.GetById(id);
            if (productDto==null) { return NotFound(); }

            return View(productDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id==null) { return NotFound(); }
            await _productService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id==null) { return NotFound(); }

            var productDto = await _productService.GetById(id);
            if (productDto==null) { return NotFound(); }

            //tenho que verificar se a imagem existe!!
            var imagePath = Path.Combine(_environment.WebRootPath, "images\\", productDto.Image);
            ViewBag.ImageExist = System.IO.File.Exists(imagePath);

            return View(productDto);
        }

    }
}

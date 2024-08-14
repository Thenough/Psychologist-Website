using AutoMapper;
using Core.DTOs;
using Core.Models.Concrete;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PsikoWeb.WebApp.Services;

namespace PsikoWeb.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;
        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productApiService.GetProductWithCategoriesAsync());
        }

        public async Task<IActionResult> Save()
        {
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO productDTO)
        {
            
            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(productDTO);
                return RedirectToAction(nameof(Index));
            }
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiService.GetByIdAsync(id);
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name",product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(productDTO);
                return RedirectToAction(nameof(Index));
            }
            var categoriesDto = await _categoryApiService.GetAllAsync();
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productDTO.CategoryId);
            return View(productDTO);
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _productApiService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

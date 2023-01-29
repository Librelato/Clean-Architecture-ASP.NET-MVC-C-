using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService=productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var produtos = await _productService.GetProducts();
            if (produtos== null) { return NotFound(); }
            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name ="GetProduct")]
        public async Task<ActionResult<ProductDTO>>get(int id)
        {
            var produto = await _productService.GetById(id);
            if (produto==null) { return NotFound(); }
            return Ok(produto);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductDTO productDTO)
        {
            if (productDTO==null) { return BadRequest(); }
            await _productService.Add(productDTO);
            return new CreatedAtRouteResult(  "GetProduct"
                                            , new {id=productDTO.Id}
                                            , productDTO);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id) { return BadRequest(id); }
            if (productDTO == null) { return BadRequest(); }
            await _productService.Update(productDTO);
            return Ok(productDTO);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>>Delete(int id)
        {
            var produto = await _productService.GetById(id);
            if (produto==null) { return NotFound();}
            await _productService.Remove(id);
            return Ok(produto);
        }
    }
}

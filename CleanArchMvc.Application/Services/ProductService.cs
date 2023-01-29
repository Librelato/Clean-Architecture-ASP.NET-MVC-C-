using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        //private readonly IProductRepository _productRepository;

        //public ProductService(IProductRepository productRepository, IMapper mapper)
        //{
        //    ArgumentNullException.ThrowIfNull(productRepository);
        //    _productRepository=productRepository;
        //    _mapper=mapper;
        //}

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProductService(IMapper mapper, IMediator mediator)
        {
            _mapper=mapper;
            _mediator=mediator;
        }

        public async Task<IEnumerable<ProductDTO>> GetProducts()
        {
            //var productsEntity = await _productRepository.GetProductsAsync();
            //return _mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
            var result = await _mediator.Send(new GetProductsQuery());
            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }
        public async Task<ProductDTO> GetById(int? id)
        {
            //var productEntity = await _productRepository.GetByIdAsync(id);
            //return _mapper.Map<ProductDTO>(productEntity);
            var result = await _mediator.Send(new GetProductByIdQuery(id.GetValueOrDefault()));
            return _mapper.Map<ProductDTO>(result);
        }
        //public async Task<ProductDTO> GetProductCategory(int? id)
        //{
        //    var productEntity = await _productRepository.GetProductCategoryAsync(id);
        //    return _mapper.Map<ProductDTO>(productEntity);
        //}
        public async Task Add(ProductDTO productDTO)
        {
            //var productEntity = _mapper.Map<Product>(productDTO);
            //await _productRepository.CreateAsync(productEntity);
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
            await _mediator.Send(productCreateCommand);
        }
        public async Task Update(ProductDTO productDTO)
        {
            //var productEntity = _mapper.Map<Product>(productDTO);
            //await _productRepository.UpdateAsync(productEntity);
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
            await _mediator.Send(productUpdateCommand);
        }
        public async Task Remove(int? id)
        {
            //var productEntity = await _productRepository.GetByIdAsync(id);
            //await _productRepository.DeleteAsync(productEntity);
            //throw new NotImplementedException();
            await _mediator.Send(new ProductRemoveCommand(id.GetValueOrDefault()));
        }

    }
}

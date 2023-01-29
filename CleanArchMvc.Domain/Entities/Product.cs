﻿using CleanArchMvc.Domain.Entities.Abstracts;
using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Product : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string Image { get; private set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Product(string name, string description, decimal price, int stock, string image)
        {
            ValidateDomain(name, description, price, stock, image);
        }
        public Product(int id, string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(id<0, "Invalid Id value!");
            Id=id;
            ValidateDomain(name, description, price, stock, image);
        }

        public void Update(string name, string description, decimal price, int stock, string image, int categoryId)
        {
            ValidateDomain(name, description, price, stock, image);
            this.CategoryId = categoryId;
        }

        private void ValidateDomain(string name, string description, decimal price, int stock, string image)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name), "Invalid Name. Name is required!");
            DomainExceptionValidation.When(name.Length<3, "Invalid name. Too short, minimum 3 characters!");
            DomainExceptionValidation.When(string.IsNullOrEmpty(description), "Invalid description. Description is required!");
            DomainExceptionValidation.When(description.Length<5, "Invalid description. Too short, mininum 5 characters!");
            DomainExceptionValidation.When(price < 0, "Invalid price value!");
            DomainExceptionValidation.When(stock < 0, "Invalid stock value!");
            DomainExceptionValidation.When(image?.Length>250, "Ivalid image name. Too long, maximium 250 characters!");

            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Stock = stock;
            this.Image = image;
        }

    }
}

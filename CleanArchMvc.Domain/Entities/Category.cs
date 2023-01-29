using CleanArchMvc.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Entities.Abstracts;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class Category : EntityBase
    {
        public Category(string name)
        {
            //this.Name = name;
            this.Name = ValidateDomain(name);
        }

        public Category(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value");
            this.Id=id;
            //this.Name=name;
            this.Name = ValidateDomain(name);
        }

        public string Name { get; private set; }

        public ICollection<Product> Products { get; set; } = null;

        public void Update(string Name)
        {
            this.Name = ValidateDomain(Name);
        }

        private string ValidateDomain(string name)
        {
            DomainExceptionValidation.When( string.IsNullOrEmpty(name)
                                           ,"Invalid name. Name is required!");

            DomainExceptionValidation.When(name.Length < 3
                                           , "Invalid name. Name too short, minimum 3 characters!");

            return name;
        }
    }
}

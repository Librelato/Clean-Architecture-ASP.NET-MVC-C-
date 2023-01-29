using CleanArchMvc.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Nome")]
        public string Name { get; set; }
        [Required(ErrorMessage="Descrição é requerido!!!")]
        [MinLength(5)]
        [MaxLength(200)]
        [DisplayName("Descrição")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName="decimal(18,2)")]
        [DisplayFormat(DataFormatString="{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Preço")]
        public decimal Price { get; set; }
        [Required]
        [Range(1,9999)]
        [DisplayName("Estoque")]
        public int Stock { get; set; }
        [MaxLength(250)]
        [DisplayName("Imagem do Produto")]
        public string Image { get; set; }
        [ValidateNever]
        [JsonIgnore]
        public Category Category { get; set; }
        [DisplayName("Categorias")]
        public int CategoryId { get; set; }

    }
}

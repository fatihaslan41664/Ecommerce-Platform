using EticaretAPI.Application.Features.Commands.Product.CreateProduct;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Lütfen ürün adını boş bırakmayınız")
                .MaximumLength(150)
                .WithMessage("Ürün adı en fazla 150 karakter olabilir")
                .MinimumLength(1)
                .WithMessage("Ürün adı en az 1 karakter olmalıdır");

            RuleFor(p => p.Series)
                .NotEmpty()
                .WithMessage("Lütfen seri/evren bilgisini boş bırakmayınız")
                .MaximumLength(150)
                .WithMessage("Seri/evren en fazla 150 karakter olabilir");

            RuleFor(p => p.Category)
                .NotEmpty()
                .WithMessage("Lütfen kategori bilgisini boş bırakmayınız")
                .MaximumLength(100)
                .WithMessage("Kategori en fazla 100 karakter olabilir");

            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithMessage("Fiyat sıfırdan büyük olmalıdır");

            RuleFor(p => p.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stok 0 veya daha büyük olmalıdır");

            RuleFor(p => p.Height)
                .GreaterThan(0)
                .WithMessage("Boyut sıfırdan büyük olmalıdır");

            RuleFor(p => p.Color)
                .MaximumLength(50)
                .WithMessage("Renk en fazla 50 karakter olabilir");

            RuleFor(p => p.Description)
                .MaximumLength(1000)
                .WithMessage("Açıklama en fazla 1000 karakter olabilir");

        }
    }
}

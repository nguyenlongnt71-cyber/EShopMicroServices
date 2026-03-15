using Catalog.API.Products.CreateProduct;
using System.Data;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id,string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;
    
    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {

            RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(c => c.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(c => c.Price).NotNull().GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand cmd, CancellationToken cancellationToken)
        {
            //logger.LogInformation("UpdateProductHandler. Handle called with {@cmd}", cmd);

            var product = await session.LoadAsync<Product>(cmd.Id, cancellationToken);

            if(product is null)
            {
                throw new ProductNotFoundException(cmd.Id);
            }
            product.Name = cmd.Name;
            product.Category = cmd.Category;
            product.Description = cmd.Description;
            product.ImageFile = cmd.ImageFile;
            product.Price = cmd.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}

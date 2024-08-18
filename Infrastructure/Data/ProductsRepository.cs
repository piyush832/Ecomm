using Core.Entities;
using Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductsRepository : IProductRepository
    {
        private readonly StoreContext context;

        public ProductsRepository(StoreContext context)
        {
            this.context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await context.ProductBrands.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await context.products
                    .Include(p => p.ProductType) //Include will give the Eagar loading ( it will call explicitily the Product Type and Brand)
                    .Include(p => p.ProductBrand)
                    .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await context.products
                    .Include(p => p.ProductType)
                    .Include(p => p.ProductBrand)
                    .ToListAsync(); ;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await context.ProductTypes.ToListAsync();
        }
    }
}
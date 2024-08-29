using Core.Entities;
using Core.Interface;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProductsController : ControllerBase
    {
        //Private Variable
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<ProductBrand> productBrandRepo;
        private readonly IGenericRepository<ProductType> productTypeRepo;

        //Constructor
        public ProductsController(IGenericRepository<Product> productRepo, IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo)
        {
            this.productRepo = productRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(); // Specification will include the Product type and Product Brands 

            var products = await productRepo.ListAsync(specification);
            return Ok(products);
            
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductsById(int id)
        {
            var specification = new ProductsWithTypesAndBrandsSpecification(id);
            return await productRepo.GetEntityWithSpecification(specification);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductBrandsAsync()
        {
            return Ok(await productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypesAsync()
        {
            return Ok(await productTypeRepo.ListAllAsync());
        }
    }
}
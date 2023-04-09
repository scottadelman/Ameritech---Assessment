using ADS.Blazor.Assessment.Server.Models;
using ADS.Blazor.Assessment.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ADS.Blazor.Assessment.Server.Controllers
{
    [ApiController]
    //   [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ModelContext _context;
        public ProductController(ModelContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("/product")]
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {

            var result = _context.Products;

            var productDtoList = new List<ProductDto>();

            foreach (var item in result)
            {
                ProductDto productDto = new ProductDto();

                productDto.Id = item.Id;
                productDto.Name = item.Name;
                productDto.Size = item.Size;
                productDto.Price = item.Price;
                productDto.Inventory = item.Inventory;

                CategoryDto categoryDto = new CategoryDto();

                categoryDto.Id = item.CategoryId;
                categoryDto.Name = item.Category.Name;

                productDto.Category = categoryDto;

                productDtoList.Add(productDto);

            }

            return productDtoList;

        }

        [HttpPost]
        [Route("/Add")]
        public void Post([FromBody] ProductDto addAProduct)
        {
            Product product = new Product();

            product.Name = addAProduct.Name;

            product.Size = addAProduct.Size;

            product.Price = addAProduct.Price;

            product.Inventory = addAProduct.Inventory;

            Category category = _context.Categories.Where(x => x.Id == addAProduct.Category.Id).First();

            product.Category = category;

            _context.Products.Add(product);

            _context.SaveChanges();
        }


        [HttpPost]
        [Route("/Category")]
        public void Post([FromBody] CategoryDto addACategory)
        {
            Category category = new Category();

            category.Id = addACategory.Id;

            category.Name = addACategory.Name;

            _context.Categories.Add(category);

            _context.SaveChanges();
        }


        [HttpDelete]
        [Route("/Delete/{id}")]
        public void Delete(int id)
        {
            var deleteProduct = _context.Products.Where(x => x.Id == id).First();

            if (deleteProduct != null)
            {
                _context.Products.Remove(deleteProduct);
                _context.SaveChanges();

            }
        }

        [HttpPut]
        [Route("/Edit")]
        public void EditProduct([FromBody] ProductDto newProductInventory)
        {
            var editProduct = _context.Products.Where(x => x.Id == newProductInventory.Id).First();

            if (editProduct != null)
            {
                editProduct.Inventory = newProductInventory.Inventory;
                _context.SaveChanges();

            }
        }

    }
}
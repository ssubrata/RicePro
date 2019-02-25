using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiceShop.Api.ViewModel;
using RiceShop.Clb;
using RiceShop.Clb.Entity;
using RiceShop.Clb.Interface;

namespace RiceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        public ProductController()
        {
            unitOfWork = new UnitOfWork(new DatadbContext());
        }
        [HttpGet]
        public IActionResult Get() => Ok(unitOfWork.Product.GetProductWithCategory());

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok();


        [HttpPost]
        public IActionResult Post([FromBody] VmProduct product)
        {
            unitOfWork.Product.Add(new Product { CategoryId=product.CategoryId,Description=product.Description,ProductName=product.ProductName,ProductId=product.ProductId});
            unitOfWork.Complete();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody]VmProduct product, int id)
        {
            var find = unitOfWork.Product.Get(id);
            if (find == null) return NotFound();
            find.CategoryId = product.CategoryId;
            find.ProductName = product.ProductName;
            find.Description = product.Description;
            unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var find = unitOfWork.Product.Get(id);
            if (find == null) return NotFound();
            unitOfWork.Product.Remove(find);
            unitOfWork.Complete();
            return Ok();
        }
    }
}
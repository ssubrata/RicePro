using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiceShop.Clb;
using RiceShop.Clb.Entity;
using RiceShop.Clb.Interface;

namespace RiceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockController()
        {
            _unitOfWork = new UnitOfWork(new DatadbContext());
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Stock.GetStockWithProduct());
        }
        [HttpPost]
        public IActionResult Post([FromBody]Stock stock)
        {
            _unitOfWork.Stock.Add(stock);
            _unitOfWork.Complete();
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody]Stock stock, int id)
        {
            var find = _unitOfWork.Stock.Get(id);
            if (find == null) return NotFound();
            find.ProductId = stock.ProductId;
            find.UnitPrice = stock.UnitPrice;
            find.StockNo = stock.StockNo;
            find.Quantity = stock.Quantity;
            find.StockInDate = stock.StockInDate;
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var find = _unitOfWork.Stock.Get(id);
            if (find == null) return NotFound();
            _unitOfWork.Stock.Remove(find);
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CategoryController(
            IMapper mapper
            )
        {
            _mapper = mapper;
            _unitOfWork = new UnitOfWork(new DatadbContext());
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_unitOfWork.Category.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_unitOfWork.Category.Get(id));
        }
        [HttpPost]
        public IActionResult Post([FromBody] VmCategory vmCategory)
        {
            _unitOfWork.Category.Add(_mapper.Map<Category>(vmCategory));
            _unitOfWork.Complete();
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] VmCategory vmCategory, int id)
        {
            var find = _unitOfWork.Category.Get(vmCategory.CategoryId);
            if (find != null)
            {
                find.CategoryName = vmCategory.CategoryName;
                find.Description = vmCategory.Description;
                _unitOfWork.Complete();
                return Ok();
            }

            return NotFound();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var find = _unitOfWork.Category.Get(id);
            if (find != null)
            {
                _unitOfWork.Category.Remove(find);
                _unitOfWork.Complete();
                return Ok();
            }
            return NotFound();
        }
    }
}
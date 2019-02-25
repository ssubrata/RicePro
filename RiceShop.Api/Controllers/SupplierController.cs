using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class SupplierController : ControllerBase
    {

        private readonly IUnitOfWork unitOfWork;
        private IMapper _mapper;
        public SupplierController(IMapper mapper)
        {
            _mapper = mapper;
            unitOfWork = new UnitOfWork(new DatadbContext());
        }
        
        [Authorize("ReadPolicy")]
        [HttpGet]
        public IActionResult Get() => Ok(unitOfWork.Supplier.GetAll());

        [Authorize("ReadPolicy")]
        [HttpGet("{id}")]

        public IActionResult Get(int id) => Ok(unitOfWork.Supplier.Get(id));

        [Authorize("AddPolicy")]
        [HttpPost]

        public IActionResult Post([FromBody] VmSupplier vmSupllier)
        {
            unitOfWork.Supplier.Add(_mapper.Map<Supplier>(vmSupllier));
            unitOfWork.Complete();
            return Ok();
        }

        [Authorize("Updatepolicy")]
        [HttpPut("{id}")]

        public IActionResult Put([FromBody]VmSupplier vmSupplier, int id)
        {

            var find = unitOfWork.Supplier.Get(id);
            if (find == null) return NotFound();
            var supplier=_mapper.Map<Supplier>(vmSupplier);
            find.Address = supplier.Address;
            find.CompanyName = supplier.CompanyName;
            find.ContactName = supplier.ContactName;
            find.OwnerName = supplier.OwnerName;
         
            //   unitOfWork.Supplier.Add(find);
            unitOfWork.Complete();
            return Ok();
        }

        [Authorize("Deletepolicy")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var find = unitOfWork.Supplier.Get(id);
            if (find == null) return NotFound();
            unitOfWork.Supplier.Remove(find);
            unitOfWork.Complete();
            return Ok();
        }
    }
}
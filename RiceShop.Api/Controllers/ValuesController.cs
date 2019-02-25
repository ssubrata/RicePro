using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RiceShop.Clb;
using RiceShop.Clb.Entity;

namespace RiceShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DatadbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ValuesController(DatadbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        // GET api/values
        [Authorize(Roles ="Manager")]
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            var u = await _userManager.FindByIdAsync("383427FA-FD9F-48CA-BEBA-F4BD238691D2");
            return Ok(u);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace CoreApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        // GET api/values
        public ValuesController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetValues()
        {
            var values=_context.values.ToList();
            return Ok(values);
        }
       

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetValue(int id)
        {
            var values=_context.values.FirstOrDefault(x=>x.Id==id);
            return Ok(values);
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

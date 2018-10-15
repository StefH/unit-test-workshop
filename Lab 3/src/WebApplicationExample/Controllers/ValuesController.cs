using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ProductService;
using TaxService;

namespace WebApplicationExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IProductHelper _productHelper;

        public ValuesController(IProductHelper productHelper)
        {
            _productHelper = productHelper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] { "value1", _productHelper.GetTotalValue(3, 27.9, TaxType.Food).ToString(CultureInfo.CurrentUICulture) };
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

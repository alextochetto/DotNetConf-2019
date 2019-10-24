using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarApi.Controllers
{
    //[Authorize(Roles = "Admin,Identity")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("get-admin")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<string>> GetAdmin()
        {
            return new string[] { "Admin" };
        }
        [HttpGet("get-identity")]
        [Authorize(Roles = "Identity")]
        public ActionResult<IEnumerable<string>> GetIdentity()
        {
            return new string[] { "Identity" };
        }
        [HttpGet("get-both")]
        [Authorize(Roles = "Admin,Identity")]
        public ActionResult<IEnumerable<string>> GetBoth()
        {
            return new string[] { "Admin", "Identity" };
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
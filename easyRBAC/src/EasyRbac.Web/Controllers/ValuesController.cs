using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyRbac.Reponsitory.Helper;
using Microsoft.AspNetCore.Mvc;

namespace EasyRbac.Web.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IKeyedIdGenerate idg;

        public ValuesController(IKeyedIdGenerate idg)
        {
            this.idg = idg;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public int Get(int id)
        {
            return this.idg.NewId(id.ToString());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

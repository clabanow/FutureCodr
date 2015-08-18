namespace FutureCodr.UI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    [Authorize]
    public class ValuesController : ApiController
    {
        public void Delete(int id)
        {
        }

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        public string Get(int id)
        {
            return "value";
        }

        public void Post([FromBody] string value)
        {
        }

        public void Put(int id, [FromBody] string value)
        {
        }
    }
}

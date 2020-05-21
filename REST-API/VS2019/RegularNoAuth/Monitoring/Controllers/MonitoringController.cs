using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Monitoring.Model;
using Microsoft.Extensions.Configuration;

namespace Monitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoringController : ControllerBase
    {
        private IConfiguration _configuration;

        public MonitoringController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        // GET: api/Monitoring
        [HttpGet]
        public IEnumerable<MonitorRecord> Get()
        {
            IEnumerable<MonitorRecord> monitorRecords = Monitoring.Persistance.SqlCommandUtility.GetAll(_configuration);
            return monitorRecords;
        }

        // GET: api/Monitoring/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Monitoring
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Monitoring/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

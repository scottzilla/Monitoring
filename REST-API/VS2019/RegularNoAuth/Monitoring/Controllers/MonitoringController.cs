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
            monitorRecords = monitorRecords.OrderByDescending(x => x.ModifiedDate).ToList();
            return monitorRecords;
        }

        // GET: api/Monitoring/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{id:int}", Name = "Get")]

        public IActionResult GetById(int id)
        {
            List<MonitorRecord> records = Monitoring.Persistance.SqlCommandUtility.GetEntriesByAppId(id, _configuration);
            records = records.OrderByDescending(x => x.ModifiedDate).ToList();
            return Ok(records);

        }

        // POST: api/Monitoring
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Post([FromBody] MonitorRecord monitorRecord)
        {

            Monitoring.Persistance.SqlCommandUtility.AddMonitorRecordEntry(monitorRecord, _configuration);
            return Ok();
        }

        // PUT: api/Monitoring/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id:int}")]
        public void Delete(int id)
        {
            Monitoring.Persistance.SqlCommandUtility.DeleteMonitorRecordsBasedOnAppId(id, _configuration);
        }
    }

}

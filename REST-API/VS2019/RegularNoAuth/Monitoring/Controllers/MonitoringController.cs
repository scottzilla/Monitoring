using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Monitoring.Model;
using System.Collections.Generic;
using System.Linq;

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

        public ActionResult GetById(int id)
        {
            //add validation
            List<MonitorRecord> records = Monitoring.Persistance.SqlCommandUtility.GetEntriesByAppId(id, _configuration);
            records = records.OrderByDescending(x => x.ModifiedDate).ToList();
            return Ok(records);

        }

        // POST: api/Monitoring
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult Post([FromBody] MonitorRecord monitorRecord)
        {
            //add validation
            Monitoring.Persistance.SqlCommandUtility.AddMonitorRecordEntry(monitorRecord, _configuration);
            return Ok();
        }
                

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id:int}",Name ="delete")]
        public ActionResult  Delete(int id)
        {
            //add validation
            Monitoring.Persistance.SqlCommandUtility.DeleteMonitorRecordsBasedOnAppId(id, _configuration);
            return Ok();
        }
    }

}

using BrevardMapper.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BrevardMapper.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public RequestsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        public async Task<IEnumerable<ServiceRequests>> Get()
        {
            using var conn = new SqlConnection(_config.GetConnectionString("Default"));

            return await conn.QueryAsync<ServiceRequests>(
                "sp_GetRequests",
                commandType: CommandType.StoredProcedure
            );

        }

        [HttpGet("map")]
        public IActionResult GetMapPoints()
        {
            var data = new[]
            {
                new {
                    id = 1,
                    title = "Palm Bay Road Issue",
                    description = "Pothole reported",
                    latitude = 28.0345,
                    longitude = -80.5887,
                    status = "Open"
                },
                new {
                    id = 2,
                    title = "Melbourne Drainage",
                    description = "Flooding after storm",
                    latitude = 28.0836,
                    longitude = -80.6081,
                    status = "Open"
                },
                new {
                    id = 3,
                    title = "Cocoa Traffic Signal",
                    description = "Signal malfunction",
                    latitude = 28.3861,
                    longitude = -80.7415,
                    status = "Closed"
                }
            };

            return Ok(data);
        }
    }
}

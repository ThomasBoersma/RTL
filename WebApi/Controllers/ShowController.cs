using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Services;
using Services.Models;
using Common.Paging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowController : ControllerBase
    {
        private readonly IShowService showService;

        public ShowController(IShowService showService)
        {
            this.showService = showService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<ShowDto>>> GetAsync([FromQuery] PagingOptions pagingOptions)
        {
            var result = await showService.GetAsync(pagingOptions);

            return Ok(result);
        }
    }
}

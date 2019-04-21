using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QuestEngine.Models;
using QuestEngine.PlayerQuestData;
using QuestEngine.Service;

namespace QuestEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private IQuestEngineService service;

        public ProgressController(IQuestEngineService engineService)
        {
            service = engineService;
        }

        // POST /api/progress
        [HttpPost]
        public ActionResult<ProgressResult> Post([FromBody] ProgressInput input) 
        {
            return service.Progress(input);
        }
    }
}
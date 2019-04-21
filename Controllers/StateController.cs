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
    public class StateController : ControllerBase
    {
        private IQuestEngineService service;
        
        public StateController(IQuestEngineService engineService)
        {
            service = engineService;
        }

        // GET api/state/{playerId}
        [HttpGet("{playerId}")]
        public ActionResult<StateOutput> Get(string playerId)
        {
            return service.GetState(playerId);
        }
    }
}
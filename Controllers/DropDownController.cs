using Microsoft.AspNetCore.Mvc;
using ScopoERP.Services;
using ScopoERP.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ScopoERP.Models;

namespace ScopoERP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DropDownController : ControllerBase
    {
        private readonly ILogger<DropDownController> _logger;
        private DropDownService _dropDownService;

        public DropDownController(ILogger<DropDownController> logger, DropDownService dropDownService)
        {
            _logger = logger;
            _dropDownService = dropDownService;
        }

        [HttpGet("GetDropDownList")]
        public async Task<ActionResult<IEnumerable<DropDownViewModel>>> GetDropDownList(string type, int? accountTypeId = null)
        {
            return Ok(await _dropDownService.GetDropDownList(type,  accountTypeId));
        }
    }
}

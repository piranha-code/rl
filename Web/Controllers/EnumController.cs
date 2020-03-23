using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Attributes;

namespace Web.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [GenericController]
    public class EnumController<TEnum> : ControllerBase
    where TEnum : System.Enum
    {
        readonly Dictionary<string, TEnum> _enum;
        public EnumController()
        {
            _enum = Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .ToDictionary(m => m.ToString(), m => (TEnum)m);
        }

        [Authorize(Roles = "guest")]
        [HttpGet]
        public virtual IActionResult Get()
        {
            return Ok(_enum.Keys);
        }
    }
}
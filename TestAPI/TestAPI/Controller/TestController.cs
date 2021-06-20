using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAPI.Controller
{
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 111
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return Ok();
        }
    }
}

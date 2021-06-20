using APITest1.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITest1.Configuration
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            ResponseResult response;
            switch (context.Exception.GetType().Name)
            {

                default:
                    _logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);
                    response = new ResponseResult("500", context.Exception.ToString(), null);
                    break;
            }

            context.Result = new ObjectResult(response);
            context.ExceptionHandled = true;
        }
    }
}

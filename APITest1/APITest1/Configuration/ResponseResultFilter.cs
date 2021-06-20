using APITest1.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITest1.Configuration
{
    public class ResponseResultFilter : ActionFilterAttribute
    {

        private readonly ILogger<ResponseResultFilter> _logger;
        private readonly IHostingEnvironment _host;

        public ResponseResultFilter(ILogger<ResponseResultFilter> logger, IHostingEnvironment host)
        {
            _logger = logger;
            _host = host;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var paraList = context.ActionDescriptor.Parameters;
            //_logger.LogDebug($"访问方法{actionName},参数:{string.Join(",", paraList.Select(a => (a.Name ?? "").ToString()).ToArray())}");
            if (_host.IsProduction())
            {
                if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {

                    base.OnActionExecuting(context);
                }
                else
                {
                    //不存在
                    context.Result = new ObjectResult(new ResponseResult("401", "Not Authorization Info", null));
                    return;
                }
            }
            else
                base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
            {
                if (context.Result is ObjectResult)
                {
                    ObjectResult result = context.Result as ObjectResult;
                    if (result.Value != null)
                        if (result.Value is ResponseResult)
                            context.Result = context.Result;
                        else
                            context.Result = new ObjectResult(new ResponseResult("200", "", result.Value));
                    //else
                    //    context.Result = new ObjectResult(new ResponseResult("404", "未找到资源", null));
                }
                else if (context.Result is EmptyResult)
                {
                    context.Result = new ObjectResult(new ResponseResult("404", "未找到资源", null));
                }
                else if (context.Result is JsonResult)
                {
                    JsonResult result = context.Result as JsonResult;
                    if (result.Value != null)
                        if (result.Value is ResponseResult)
                            context.Result = context.Result;
                        else
                            context.Result = new ObjectResult(new ResponseResult("200", "", result.Value));
                    //else
                    //    context.Result = new ObjectResult(new ResponseResult("404", "未找到资源", null));
                }
                else if (context.Result is ContentResult)
                {
                    ContentResult result = context.Result as ContentResult;
                    context.Result = new ObjectResult(new ResponseResult("200", "", result.Content));
                }
            }
            base.OnActionExecuted(context);
        }
    }
}

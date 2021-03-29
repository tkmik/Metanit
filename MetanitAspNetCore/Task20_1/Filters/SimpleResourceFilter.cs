using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task20_1.Filters
{
    public class SimpleResourceFilter : Attribute, IResourceFilter
    {
      //  private int _id;
      //  private string _token;
        private ILogger _logger;
        public SimpleResourceFilter(ILoggerFactory loggerFactory)
        {
          //  _id = id;
          //  _token = token;
            _logger = loggerFactory.CreateLogger("SimpleResourceFilter");
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
          //  context.HttpContext.Response.Cookies.Append("LastVisit", DateTime.Now.ToString("dd/MM/yyyy hh-mm-ss"));
            _logger.LogInformation($"Executed {DateTime.Now}");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
          //  context.HttpContext.Response.Headers.Add("Id", _id.ToString());
          //  context.HttpContext.Response.Headers.Add("Token", _token.ToString());
            _logger.LogInformation($"Executing {DateTime.Now}");
        }
    }
}

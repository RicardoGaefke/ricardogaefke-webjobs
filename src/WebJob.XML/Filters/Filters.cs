using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace RicardoGaefke.WebJob.XML.Filters
{
  /// <summary>
  /// Sample exception filter that shows how declarative error handling logic
  /// can be integrated into the execution pipeline.
  /// </summary>

  public class ErrorHandlerAttribute : FunctionExceptionFilterAttribute
  {
    public override Task OnExceptionAsync(FunctionExceptionContext exceptionContext, CancellationToken cancellationToken)
    {
      exceptionContext.Logger.LogError($"ErrorHandler called. Function '{exceptionContext.FunctionName}:{exceptionContext.FunctionInstanceId} failed.");
      return Task.CompletedTask;
    }
  }
}

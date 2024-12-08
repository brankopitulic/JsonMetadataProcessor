using Application.Common.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Presentation.Requests;

namespace Presentation.Filters;

public class ValidateFileSizeAttribute : ActionFilterAttribute
{
    private readonly int _maxFileSizeBytes;

    public ValidateFileSizeAttribute(IOptions<FileUploadSettings> fileUploadSettings)
    {
        _maxFileSizeBytes = fileUploadSettings.Value.MaxFileSizeMB * 1024 * 1024;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue("request", out var value) && value is UploadJsonFileRequest request)
        {
            if (request.File == null || request.File.Length > _maxFileSizeBytes)
            {
                context.Result = new BadRequestObjectResult($"File size exceeds the limit of {_maxFileSizeBytes / (1024 * 1024)} MB.");
            }
        }

        base.OnActionExecuting(context);
    }
}

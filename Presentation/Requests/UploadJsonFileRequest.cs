using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Requests;
public class UploadJsonFileRequest
{
    [FromForm(Name = "file")]
    public IFormFile File { get; set; }
}

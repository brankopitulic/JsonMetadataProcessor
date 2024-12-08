using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.Process;
public class ProcessClinicalTrialCommand : IRequest<int>
{
    public IFormFile File { get; set; }
}

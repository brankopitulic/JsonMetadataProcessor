using FluentValidation;
using MediatR;
using Application.Commands.Create;
using Application.Commands.Validation;
using AutoMapper;
using Newtonsoft.Json.Linq;

namespace Application.Commands.Process;
public class ProcessClinicalTrialCommandHandler : IRequestHandler<ProcessClinicalTrialCommand, int>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProcessClinicalTrialCommandHandler(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<int> Handle(ProcessClinicalTrialCommand request, CancellationToken cancellationToken)
    {
        using var stream = new StreamReader(request.File.OpenReadStream());
        var jsonContent = await stream.ReadToEndAsync();

        var validationResult = await _mediator.Send(new ValidateJsonSchemaCommand { JsonContent = jsonContent });
        if (!validationResult.IsValid)
        {
            throw new ValidationException(string.Join(", ", validationResult.Errors));
        }

        var jsonObject = JObject.Parse(jsonContent);
        var command = _mapper.Map<CreateClinicalTrialCommand>(jsonObject);

        return await _mediator.Send(command);
    }
}

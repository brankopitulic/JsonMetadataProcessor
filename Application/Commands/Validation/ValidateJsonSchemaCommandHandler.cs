using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using MediatR;

namespace Application.Commands.Validation;
public class ValidateJsonSchemaCommandHandler : IRequestHandler<ValidateJsonSchemaCommand, ValidationResult>
{
    private readonly JSchema _schema;

    public ValidateJsonSchemaCommandHandler()
    {
        // Load schema
        _schema = JSchema.Parse(@"{
                '$schema': 'http://json-schema.org/draft-07/schema#',
                'title': 'ClinicalTrialMetadata',
                'type': 'object',
                'properties': {
                    'trialId': { 'type': 'string' },
                    'title': { 'type': 'string' },
                    'startDate': { 'type': 'string', 'format': 'date' },
                    'endDate': { 'type': 'string', 'format': 'date' },
                    'participants': { 'type': 'integer', 'minimum': 1 },
                    'status': { 'type': 'string', 'enum': ['NotStarted', 'Ongoing', 'Completed'] }
                },
                'required': ['trialId', 'title', 'startDate', 'status'],
                'additionalProperties': false
            }");
    }

    public Task<ValidationResult> Handle(ValidateJsonSchemaCommand request, CancellationToken cancellationToken)
    {
        var jsonObject = JObject.Parse(request.JsonContent);

        var isValid = jsonObject.IsValid(_schema, out IList<string> errors);

        return Task.FromResult(new ValidationResult
        {
            IsValid = isValid,
            Errors = errors.ToList()
        });
    }
}

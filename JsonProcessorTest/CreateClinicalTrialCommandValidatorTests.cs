using Application.Commands.Create;
using Domain.Enums;
using FluentValidation.TestHelper;
using Xunit;

public class CreateClinicalTrialCommandValidatorTests
{
    private readonly CreateClinicalTrialCommandValidator _validator;

    public CreateClinicalTrialCommandValidatorTests()
    {
        _validator = new CreateClinicalTrialCommandValidator();
    }

    [Fact]
    public void Should_Pass_For_Valid_Command()
    {
        var command = new CreateClinicalTrialCommand
        {
            TrialId = "CT-001",
            Title = "Clinical Trial Study",
            StartDate = DateTime.UtcNow.AddDays(-10),
            Participants = 50,
            Status = TrialStatus.NotStarted
        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Fail_For_Empty_TrialId()
    {
        var command = new CreateClinicalTrialCommand { TrialId = "" };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.TrialId);
    }

    [Fact]
    public void Should_Fail_For_Invalid_Status()
    {
        var command = new CreateClinicalTrialCommand
        {
            TrialId = "CT-001",
            Title = "Clinical Trial Study",
            StartDate = DateTime.UtcNow.AddDays(-10),
            Participants = 50,
            Status = (TrialStatus)999 // Invalid enum value
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Status);
    }
}
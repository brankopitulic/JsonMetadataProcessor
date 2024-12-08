using System;
using Application.Dto;
using MediatR;

namespace Application.Queries.Get;
public class GetClinicalTrialByIdQuery : IRequest<ClinicalTrialDto>
{
    public int Id { get; set; }
}

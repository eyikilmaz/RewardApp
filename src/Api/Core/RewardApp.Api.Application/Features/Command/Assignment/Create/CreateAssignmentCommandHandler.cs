using AutoMapper;
using MediatR;
using RewardApp.Api.Application.Interfaces.Repositories;
using RewardApp.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Command.Assignment.Create;

public class CreateAssignmentCommandHandler : IRequestHandler<CreateAssignmentCommand, Guid>
{
    private readonly IAssignmentRepository assignmentRepository;
    private readonly IMapper mapper;

    public CreateAssignmentCommandHandler(IAssignmentRepository assignmentRepository, IMapper mapper)
    {
        this.assignmentRepository = assignmentRepository;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var dbAssignment = mapper.Map<Domain.Models.Assignment>(request);

        await assignmentRepository.AddAsync(dbAssignment);

        return dbAssignment.Id;
    }
}

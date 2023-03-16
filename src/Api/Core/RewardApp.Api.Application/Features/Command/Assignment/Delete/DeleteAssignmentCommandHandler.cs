using AutoMapper;
using MediatR;
using RewardApp.Api.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Command.Assignment.Delete;

public class DeleteAssignmentCommandHandler : IRequestHandler<DeleteAssignmentCommand, bool>
{
    private readonly IAssignmentRepository assignmentRepository;
    private readonly IMapper mapper;

    public DeleteAssignmentCommandHandler(IAssignmentRepository assignmentRepository, IMapper mapper)
    {
        this.assignmentRepository = assignmentRepository;
        this.mapper = mapper;
    }

    public async Task<bool> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId.HasValue)
        {
            await assignmentRepository.BulkDelete(i => i.UserId == request.UserId);
        }
        else
        {
            var dbAssignment =await assignmentRepository.GetByIdAsync(request.AssignmentId);
            await assignmentRepository.DeleteAsync(dbAssignment);
        }

        return true;
    }
}

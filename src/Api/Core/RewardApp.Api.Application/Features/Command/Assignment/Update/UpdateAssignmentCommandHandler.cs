using AutoMapper;
using MediatR;
using RewardApp.Api.Application.Interfaces.Repositories;
using RewardApp.Common.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardApp.Api.Application.Features.Command.Assignment.Update;

public class UpdateAssignmentCommandHandler : IRequestHandler<UpdateAssignmentCommand, Guid>
{
    private readonly IAssignmentRepository assignmentRepository;
    private readonly IMapper mapper;

    public UpdateAssignmentCommandHandler(IAssignmentRepository assignmentRepository, IMapper mapper)
    {
        this.assignmentRepository = assignmentRepository;
        this.mapper = mapper;
    }

    public async Task<Guid> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var dbAssignment =await assignmentRepository.GetByIdAsync(request.Id);

        if (dbAssignment is null)
            throw new Exception("Görev bulunamadı!");

        await assignmentRepository.UpdateAsync(dbAssignment);

        return dbAssignment.Id;
    }
}

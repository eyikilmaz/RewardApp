using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RewarApp.Api.Application.Interfaces.Repositories;
using RewardApp.Api.Application.Interfaces.Repositories;
using RewardApp.Common.Models.Queries.Assignment;
using RewardApp.Common.Models.Queries.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RewardApp.Api.Application.Features.Queries.Assignment;

public class GetAssignmentDetailQueryHandler
{
    private readonly IAssignmentRepository assignmentRepository;
    private readonly IMapper mapper;

    public GetAssignmentDetailQueryHandler(IAssignmentRepository assignmentRepository, IMapper mapper)
    {
        this.assignmentRepository = assignmentRepository;
        this.mapper = mapper;
    }

    public async Task<List<AssignmentDetailViewModel>> Handle(GetAssignmentDetailQuery request, CancellationToken cancellationToken)
    {
        var query = assignmentRepository.AsQueryable();

        if (request.UserId != Guid.Empty)
        {
            query = query.Where(i => i.UserId == request.UserId);
        }

        query = query.Include(i => i.Reward);

        var list = query.Select(i => new AssignmentDetailViewModel()
        {
            Id = i.Id,
            CreateDate = i.CreateDate,
            AssignmentName = i.AssignmentName,
            RewardName = i.Reward.RewardName,
            State = i.State
        });

        return list.ToList();
    }
}

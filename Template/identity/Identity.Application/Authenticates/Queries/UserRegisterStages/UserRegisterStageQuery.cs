using Identity.Application;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Authenticates;
public class UserRegisterStageQuery : IRequest<GenericResponse<List<UserRegisterStageQueryResponse>>>
{
    public string ParameterTypeId {  get; set; }    
}

public class GetRegisterStageQueryHandler : IRequestHandler<UserRegisterStageQuery, GenericResponse<List<UserRegisterStageQueryResponse>>>
{
    private readonly IApplicationDbContext _context;

    public GetRegisterStageQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GenericResponse<List<UserRegisterStageQueryResponse>>> Handle(UserRegisterStageQuery request, CancellationToken cancellationToken)
    {
        var response = _context.Parameters
            .Include(x => x.ParameterStages)
            .Where(x => x.ParameterTypeId == request.ParameterTypeId && x.IsActive)
            .Select(x => new UserRegisterStageQueryResponse
            {                
                ParameterId = x.ParameterId,
                ParameterName = x.ParameterName,
                Description = x.Description,
                ParameterSections = x.ParameterStages.Select(y => new GetRegisterParameterStage
                {
                    Description = y.Description,
                    Name = y.Name,
                    ParameterStageId = y.ParameterStageId
                }),
            }).ToList();
        return GenericResponse<List<UserRegisterStageQueryResponse>>.Success(response, 200);
    }
}


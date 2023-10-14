
namespace TemplateDapr.Application;

public class GetParameterQuery: IRequest<GenericResponse<List<CreateParameterModel>>>
{
    public string Name { get; set; }
}

public class GetParameterQueryHandler : IRequestHandler<GetParameterQuery, GenericResponse<List<CreateParameterModel>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetParameterQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GenericResponse<List<CreateParameterModel>>> Handle(GetParameterQuery request, CancellationToken cancellationToken)
    {
        var response = _context.Parameters.Where(x=> x.IsActive).ToList();
        var parameters = _mapper.Map<List<CreateParameterModel>>(response);
        return GenericResponse<List<CreateParameterModel>>.Success(parameters, 200);
    }
}

namespace TemplateDapr.Application;

public class GetParameterQuery: IRequest<GenericResponse<List<ParameterModel>>>
{
    public string Name { get; set; }
}

public class GetParameterQueryHandler : IRequestHandler<GetParameterQuery, GenericResponse<List<ParameterModel>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetParameterQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GenericResponse<List<ParameterModel>>> Handle(GetParameterQuery request, CancellationToken cancellationToken)
    {
        var response = _context.Parameters.Where(x=> x.IsActive).ToList();
        var parameters = _mapper.Map<List<ParameterModel>>(response);
        return GenericResponse<List<ParameterModel>>.Success(parameters, 200);
    }
}

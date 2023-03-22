namespace EventDrivenDesign.Rest2.Data;

public class InitializerDatabaseRest2
{
    private readonly Rest2DbContext _context;
    private readonly ILogger<InitializerDatabaseRest2> _logger;

    public InitializerDatabaseRest2(Rest2DbContext context, ILogger<InitializerDatabaseRest2> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void Run()
    {
#if DEBUG
        _context.Database.EnsureDeleted();
#endif
        _context.Database.EnsureCreated();
    }
}


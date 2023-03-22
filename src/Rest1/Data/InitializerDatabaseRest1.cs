namespace EventDrivenDesign.Rest1.Data;

public class InitializerDatabaseRest1
{
    private readonly Rest1DbContext _context;
    private readonly ILogger<InitializerDatabaseRest1> _logger;

    public InitializerDatabaseRest1(Rest1DbContext context, ILogger<InitializerDatabaseRest1> logger)
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


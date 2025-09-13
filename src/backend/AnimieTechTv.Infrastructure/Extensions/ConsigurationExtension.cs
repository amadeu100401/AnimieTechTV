using Microsoft.Extensions.Configuration;

namespace AnimieTechTv.Infrastructure.Extensions;

public static class ConsigurationExtension
{
    public static string ConnectionString(this IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        return configuration.GetConnectionString("DefaultConnection")!;
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GEC.Infrastructure.Data;

namespace GEC.Runtime.Connections
{
    public static class SqlServerConnection
    {
        public static void AddSqlConnection(this IServiceCollection services, string connectionString){
            
            services.AddDbContext<ApplicationDBContext>(optionsAction => {
                optionsAction.UseSqlServer(connectionString);
            });
        }
    }
}
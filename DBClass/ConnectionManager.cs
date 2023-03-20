using Microsoft.EntityFrameworkCore;
using SignalR_Asp.netCoreWebAppDemo.Context;
using SignalR_Asp.netCoreWebAppDemo.Model;

namespace SignalR_Asp.netCoreWebAppDemo.DBClass
{
    public class ConnectionManager
    {
        private readonly SignalRDbContext _dbContext;

        public ConnectionManager(SignalRDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUserConnection(string macAddress, string connectionId)
        {
            var connectionName = $"{connectionId}";
            await _dbContext.UserConnections.AddAsync(new ClientInfo
            {
                MacAddress = macAddress,
                ConnectionId = connectionName
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveUserConnection(string connectionId)
        {
            var connectionName = $"{connectionId}";
            var userConnection = await _dbContext.UserConnections
                .FirstOrDefaultAsync(uc => uc.ConnectionId == connectionName);

            if (userConnection != null)
            {
                _dbContext.UserConnections.Remove(userConnection);
                await _dbContext.SaveChangesAsync();
            }
        }

    }

}

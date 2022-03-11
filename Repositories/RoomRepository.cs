using Dapper;
using Trial.Models;
using Trial.Repositories;
using Trial.Utilities;

namespace Tral.Repositories;

public interface IRoomRepository
{
    
    Task<Room> GetById(long RoomId);

    Task Delete(long Id);
}

public class RoomRepository : BaseRepository, IRoomRepository
{
    public RoomRepository(IConfiguration config) : base(config)

    {


    
    }

    public async Task Delete(long Id)
    {
        var query = $@"DELETE FROM {TableNames.room} WHERE id = @Id";

        using (var con = NewConnection)
            await con.ExecuteAsync(query, new { Id });
    }

    public async Task<Room> GetById(long RoomId)
    {
        var query = $@"SELECT * FROM ""{TableNames.room}""
        WHERE room_id = @roomId";

        using (var con = NewConnection)
           return await con.QuerySingleOrDefaultAsync<Room>(query, new 
           {
               roomId = RoomId

               });

    }


}


using Trial.Models;
using Dapper;
using Trial.Utilities;

namespace Trial.Repositories;

public interface IScheduleRepository
{
    Task<Schedule> Create(int RoomId);

    Task<bool> Update(Schedule data, long RoomId);

    Task<bool> Delete(int RoomId);

    Task<Schedule> GetById(long RoomId);

    Task<List<Schedule>> GetList(long RoomId);
}

public class ScheduleRepository : BaseRepository, IScheduleRepository
{
    public ScheduleRepository(IConfiguration config) : base(config)
    {

    }



    public async Task<Schedule> Create(int RoomId)
    {
        var MaxGetIdQuery = $"SELECT MAX(Schedule_id) FROM {TableNames.schedule}";

        using (var con = NewConnection)
        {
            int maxvalue = con.ExecuteScalar<int>(MaxGetIdQuery);

            var query = $@"INSERT INTO ""{TableNames.schedule}""
         (room_id, guest_id, room_type_id, booking_date, incoming_date, staying_days, vecant_time)
	    VALUES (@RoomId, @GuestId, @RoomTypeId, @BookingDate, @IncomingDate, @StayingDays, @VecantTime)
        RETURNING *";

            var res = await con.QuerySingleOrDefaultAsync(query, RoomId);
            return res;
        }
    }



    public async Task<bool> Delete(int RoomId)
    {
        var query = $@"DELETE FROM ""{TableNames.schedule}""
        WHERE Room_id = @RoomId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new { RoomId });
            return res > 0;
        }
    }



    // public async Task<Schedule> GetById(int RoomId)
    // {
    //     try
    //     {
    //     var query = $@"SELECT * FROM ""{TableNames.schedule}""
    //     WHERE Room_id = @RoomId";

    //     using (var con = NewConnection)
    //        return await con.QuerySingleOrDefaultAsync<Schedule>(query, new {RoomId = RoomId});
    //     }
    //     catch (Exception ex){

    //     }
    //      return default;
    // }

    public async Task<Schedule> GetById(long RoomId)
    {
        try
        {
            var query = $@"SELECT * FROM ""{TableNames.schedule}""
        WHERE Room_id = @RoomId";

            using (var con = NewConnection)
                return await con.QuerySingleOrDefaultAsync<Schedule>(query, new { RoomId = RoomId });
        }
        catch (Exception ex)
        {

        }
        return default;
    }

    public async Task<List<Schedule>> GetList(long RoomId)
    {
        var query = "SELECT * FROM schedule";


        List<Schedule> res;
        using (var con = NewConnection)

            res = (await con.QueryAsync<Schedule>(query)).AsList();


        return res;
    }


    public async Task<bool> Update(Schedule data, long RoomId)
    {
        try
        {
            var query = $@"UPDATE ""{TableNames.schedule}"" SET guest_id = @GuestId, 
        staying_days = @StayingDays, vecant_time = @VecantTime WHERE room_id={RoomId}";

            using (var con = NewConnection)
            {
                var rowCount = await con.ExecuteAsync(query, RoomId);
                return rowCount == 1;
            }
        }
        catch (Exception ex)
        {

        }
        return default;
    }
}


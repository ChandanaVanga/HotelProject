
using Trial.Models;
using Dapper;
using Trial.Utilities;

namespace Trial.Repositories;

public interface IGuestRepository
     {
         Task<Guest> Create(Guest Id);

         Task<bool> Update(Guest Id);

         Task<bool> Delete(int GuestId);

         Task<Guest> GetById(int GuestId);

         Task<List<Guest>>GetList();
     }

     public class GuestRepository : BaseRepository, IGuestRepository
    {
     public GuestRepository(IConfiguration config) : base(config)
       {

       }

    public async Task<Guest> Create(Guest Id)
    {
        var MaxGetIdQuery = $"SELECT MAX(guest_id) FROM {TableNames.guest}";        

        using (var con = NewConnection)
        {
            int maxvalue = con.ExecuteScalar<int>(MaxGetIdQuery);

            var query = $@"INSERT INTO ""{TableNames.guest}""
         (guest_id, guest_name, guest_id_proof, address, date_of_birth, mobile_number, gender, room_number)
	    VALUES ({maxvalue+1}, @GuestName, @GuestIdProof, @Address, @DateOfBirth, @MobileNumber, @Gender, @RoomNumber)
        RETURNING *";
  
            var res = await con.QuerySingleOrDefaultAsync(query, Id);
            return res;
        }
    }

    public async Task<bool> Delete(int GuestId)
    {
        var query = $@"DELETE FROM ""{TableNames.guest}""
        WHERE guest_id = @GuestId";

        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new {GuestId});
            return res > 0;
        }
    }

    public async Task<Guest> GetById(int GuestId)
    {
        try
        {
        var query = $@"SELECT * FROM ""{TableNames.guest}""
        WHERE guest_id = @guestId";

        using (var con = NewConnection)
           return await con.QuerySingleOrDefaultAsync<Guest>(query, new {guestId = GuestId});
        }
        catch (Exception ex){

        }
         return default;
    }

    public async Task<List<Guest>> GetList()
    {
       var query = "SELECT * FROM guest"; 


     List<Guest> res;
     using (var con = NewConnection)

       res = (await con.QueryAsync<Guest>(query)).AsList();  


       return res;
    }

    public async Task<bool> Update(Guest Id)
    {
        var query = $@"UPDATE ""{TableNames.guest}"" SET guest_name = @GuestName, guest_id_proof = @GuestIdProof, address = @Address, date_of_birth = @DateOfBirth, mobile_number = @MobileNumber, gender = @Gender, room_number = @RoomNumber"; 

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Id);
            return rowCount == 1;
        }
    }
}

 
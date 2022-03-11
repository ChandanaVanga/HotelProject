
using Trial.Models;
using Dapper;
using Trial.Utilities;

namespace Trial.Repositories;

public interface IStaffRepository
     {
         Task<Staff> Create(Staff Item);

         Task<bool> Update(Staff Id);

         Task<bool> Delete(int StaffId);

         Task<Staff> GetById(int StaffId);

         Task<List<Staff>> GetList();
     }

     public class StaffRepository : BaseRepository, IStaffRepository
    {
     public StaffRepository(IConfiguration config) : base(config)
       {

       }

    public async Task<Staff> Create(Staff Id)
    {
        var MaxGetIdQuery = $"SELECT MAX(Staff_id) FROM {TableNames.staff}";        

        using (var con = NewConnection)
        {
            int maxvalue = con.ExecuteScalar<int>(MaxGetIdQuery);

            var query = $@"INSERT INTO ""{TableNames.staff}""
         (staff_id, staff_name, staff_type, clock_in, clock_out, shift)
	    VALUES (@StaffId, @StaffName, @StaffType, @ClockIn, @ClockOut, @Shift)
        RETURNING *";
  
            var res = await con.QuerySingleOrDefaultAsync(query, Id);
            return res;
        }
    }

    public async Task<bool> Delete(int StaffId)
    {
        var query = $@"DELETE FROM ""{TableNames.staff}"" WHERE Staff_id = @StaffId";
        using (var con = NewConnection)
        {
            var res = await con.ExecuteAsync(query, new {StaffId});
            return res > 0;
        }
    }

    public async Task<Staff> GetById(int StaffId)
    {
        try
        {
        var query = $@"SELECT * FROM ""{TableNames.staff}""
        WHERE staff_id = @StaffId";

        using (var con = NewConnection)
           return await con.QuerySingleOrDefaultAsync<Staff>(query, new {StaffId = StaffId});
        }
        catch (Exception ex)
        {

        }
         return default;
    }

    public async Task<List<Staff>> GetList()
    {
       var query = "SELECT * FROM staff"; 


     List<Staff> res;
     using (var con = NewConnection)

       res = (await con.QueryAsync<Staff>(query)).AsList(); 

       return res;
    }

    public async Task<bool> Update(Staff Id)
    {
        var query = $@"UPDATE ""{TableNames.staff}"" SET staff_id = @StaffId, 
        staff_name = @StaffName, staff_type = @StaffType, clock_in = @ClockIn, 
        clock_out = @ClockOut, shift = @Shift WHERE staff_id = @StaffId"; 

        using (var con = NewConnection)
        {
            var rowCount = await con.ExecuteAsync(query, Id);
            return rowCount == 1;
        }
    }
}

 
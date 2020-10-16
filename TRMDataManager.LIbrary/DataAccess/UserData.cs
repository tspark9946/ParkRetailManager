using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.LIbrary.Internal.DataAccess;
using TRMDataManager.LIbrary.Models;

namespace TRMDataManager.LIbrary.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var param = new { Id = Id };

            var ret = sql.LoadData<UserModel, dynamic>("dbo.spUserLookup", param, "TRMData");
            return ret;
        }
    }
}

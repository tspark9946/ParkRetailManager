using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.LIbrary.Internal.DataAccess;
using TRMDataManager.LIbrary.Models;

namespace TRMDataManager.LIbrary.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();
            
            var ret = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { } , "TRMData");

            return ret;
        }

        public ProductModel GetProductById(int productId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var ret = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetById", new { Id = productId }, "TRMData").FirstOrDefault();

            return ret;
        }
    }
}

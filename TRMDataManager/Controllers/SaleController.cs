using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TRMDataManager.LIbrary.DataAccess;
using TRMDataManager.LIbrary.Models;

namespace TRMDataManager.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {
        public void Post(SaleModel sale)
        {
            SaleData saleData = new SaleData();
            string userId = RequestContext.Principal.Identity.GetUserId();
            saleData.SaveSale(sale, userId);
        }
        
    }
}

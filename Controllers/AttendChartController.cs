using Base.Models;
using Base.Services;
using BaseApi.Controllers;
using BaoCust.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BaseWeb.Attributes;

namespace BaoCust.Controllers
{
    public class AttendChartController : ApiCtrl
    {
        //[XgProgAuth]
        public ActionResult Index()
        {
            return View();
        }

        //id: Bao.Id
        [HttpPost]
        public async Task<List<IdNumDto>> GetData()
        {
            //var userId = _Fun.GetBaseUser().UserId;
            var sql = $@"
select 
	Id=b.Name,
	Num=(
		select count(*)
		from dbo.Attend  
        where BaoId=b.Id
	)
from dbo.Bao b
/*where b.Creator='{_Fun.UserId()}'*/
";
            return await _Db.GetModelsAsync<IdNumDto>(sql);
        }

    }//class
}
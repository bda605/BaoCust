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
    public class MonthChartController : ApiCtrl
    {
        //[XgProgAuth]
        public async Task<ActionResult> Index()
        {
			ViewBag.Baos = await _XpCode.GetBaos();
            return View();
        }

        //id: Bao.Id
        [HttpPost]
        public async Task<List<IdNumDto>> GetData(string id)
        {
            //return await new MonthChart().GetData(id);
            var sql = @"
--declare @StartDate date, @EndDate date, @BaoId varchar
--select @StartDate = '2021-11-16'
--select @EndDate = '2021-12-15'
--select @BaoId = 'B001'

-- get range dates
;with result(rowDate) as (
	select @StartDate
    union all
    select dateAdd(day, 1, rowDate)
    from result 
    where rowDate < @EndDate)

-- get data
select 
	Id=convert(char(5), a.rowDate, 1),
	Num=(
		select count(*)
		from dbo.Attend  
        where BaoId=@BaoId
		and convert(date, Created)=a.rowDate
	)
from result a
";
            var today = DateTime.Today;
            var args = new List<object>() {
                "BaoId", id,
                "StartDate", today.AddMonths(-1).AddDays(1),
                "EndDate", today,
            };
            return await _Db.GetModelsAsync<IdNumDto>(sql, args);
        }

    }//class
}
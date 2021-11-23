using Base.Enums;
using Base.Models;
using Base.Services;
using BaseWeb.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaoCust.Services
{
    public class BaoEdit : XpEdit
    {
        public BaoEdit(string ctrl) : base(ctrl) { }

        override public EditDto GetDto()
        {
            return new EditDto
            {
				Table = "dbo.[Bao]",
                PkeyFid = "Id",
                Col4 = null,
                Items = new EitemDto[] 
				{
					new() { Fid = "Id" },
					new() { Fid = "Name", Required = true },
					new() { Fid = "StartTime", Required = true },
					new() { Fid = "EndTime", Required = true },
                    new() { Fid = "Status" },
                    new() { Fid = "IsBatch" },
					new() { Fid = "IsMove" },
					new() { Fid = "GiftType", Required = true },
					new() { Fid = "GiftName", Required = true },
					new() { Fid = "Note" },
					new() { Fid = "LevelCount" },
					new() { Fid = "Created" },
                },
                Childs = new EditDto[]
                {
                    new EditDto
                    {
                        Table = "dbo.[Stage]",
                        PkeyFid = "Id",
                        FkeyFid = "BaoId",
						OrderBy = "Sort",
                        Col4 = null,
                        Items = new EitemDto[] 
						{
							new() { Fid = "Id" },
							new() { Fid = "BaoId" },
							new() { Fid = "FileName", Required = true },
                            new() { Fid = "Hint" },
                            new() { Fid = "Answer", Required = true },
							new() { Fid = "Sort", Required = true },
                        },
                    },
                },
            };
        }

        //only changed data sent to server side !!
        private void Md5Answer(JObject json)
        {
            var stages = _Json.GetChildRows(json, 0);
            if (stages != null)
            {
                var fid = "Answer";
                foreach(var stage in stages)
                {
                    if (!_Object.IsEmpty(stage[fid]))
                        stage[fid] = _Str.Md5(stage[fid].ToString());
                }
            }
        }

        //TODO: add your code
        //t03_FileName: t + table serial _ + fid
        public async Task<ResultDto> CreateAsnyc(JObject json, List<IFormFile> t00_FileName)
        {
            var service = EditService();
            Md5Answer(json);
            var result = await service.CreateAsync(json);
            if (_Valid.ResultStatus(result))
                await _WebFile.SaveCrudFilesAsnyc(json, service.GetNewKeyJson(), _Xp.DirStage, t00_FileName, nameof(t00_FileName));
            return result;
        }

        //TODO: add your code
        //t03_FileName: t + table serial _ + fid
        public async Task<ResultDto> UpdateAsnyc(string key, JObject json, List<IFormFile> t00_FileName)
        {
            var service = EditService();
            Md5Answer(json);
            var result = await service.UpdateAsync(key, json);
            if (_Valid.ResultStatus(result))
                await _WebFile.SaveCrudFilesAsnyc(json, service.GetNewKeyJson(), _Xp.DirStage, t00_FileName, nameof(t00_FileName));
            return result;
        }
    } //class
}

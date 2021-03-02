using Microsoft.AspNetCore.Mvc;
using StackoverflowTags.Data;
using StackoverflowTags.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackoverflowTags.Controllers
{
    [ApiController]
    public class ApiController : Controller
    {
        private readonly IStackoverflowData _StackOverflowData;
        public ApiController(IStackoverflowData StackOverflowData)
        {
            _StackOverflowData = StackOverflowData;
        }

        [HttpGet]
        [Route("api/GetAllWWW/{res:int}")]
        public async Task<JsonResult> GetAll(int res)
        {
            List<StackoverflowTypeWWW> tagsAll = new List<StackoverflowTypeWWW>();

            try
            {
                var r = await _StackOverflowData.GetAll(res);

                for (int i = 0; i < r.Count; i++)
                {
                    StackoverflowTypeWWW tag = new StackoverflowTypeWWW();
                    tag.Id = i + 1;
                    tag.TagName = r[i].TagName;
                    tag.TagQuestionsCounter = r[i].TagQuestionsCounter;
                    tag.PercentPopularity = r[i].PercentPopularity;
                    tagsAll.Add(tag);
                }
            }catch(Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }

            return Json(new { data = tagsAll.ToList() });
        }

        [HttpGet]
        [Route("api/GetAllAPI")]
        public async Task<JsonResult> GetData()
        {
            List<StackoverflowAPI> tagsAll = new List<StackoverflowAPI>();

            try
            {
                var r = await _StackOverflowData.GetData();

                for (int i = 0; i < r.Count; i++)
                {
                    StackoverflowAPI tag = new StackoverflowAPI();
                    tag.lp = i + 1;
                    tag.count = r[i].count;
                    tag.name = r[i].name;
                    tag.PercentPopularity = r[i].PercentPopularity;
                    tagsAll.Add(tag);
                }
            }catch(Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }
            return Json(new { data = tagsAll.ToList() });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using StackoverflowTags.Helpers;
using StackoverflowTags.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StackoverflowTags.Controllers
{
    [ApiController]
    public class ApiController : Controller
    {
        #region First result

        [HttpGet]
        [Route("api/GetAllAPI")]
        public async Task<IActionResult> GetData()
        {
            List<Item> tagsAll = new List<Item>();
            long sum = 0;
            int tagsCounter = 1;
            string url = "https://api.stackexchange.com/2.2/tags?page={num}&pagesize=100&order=desc&sort=popular&site=stackoverflow";
            PageHelper ph = new PageHelper();

            for (int i = 1; i <= 10; i++)
            {
                string urlTemp = url.Replace("{num}", i.ToString());
                var repositories = await ProcessRepositories(urlTemp);

                if (repositories != null)
                {
                    List<Item> tags = new List<Item>();

                    for (int j = 0; j < repositories.items.Count; j++)
                    {
                        Item tag = new Item();
                        tag.lp = tagsCounter;
                        tag.name = repositories.items[j].name;
                        tag.count = repositories.items[j].count;
                        tags.Add(tag);
                        sum += Convert.ToInt32(tag.count);
                        tagsCounter++;
                    }
                   
                    tagsAll.AddRange(tags);
                }
            }

            for (int k = 0; k < tagsAll.Count; k++)
            {
                double a1 = (100 * Convert.ToDouble(tagsAll[k].count)) / sum;
                tagsAll[k].PercentPopularity = Math.Round(a1, 4);
            }

            return Json(new { data = tagsAll.ToList() });
        }

        private static readonly HttpClient client = new HttpClient();

        private static async Task<StackoverflowModelAPI> ProcessRepositories(string url)
        {
            var handler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip };
            var client = new HttpClient(handler);
            var streamTask = client.GetStreamAsync(url);

            var repositories = await JsonSerializer.DeserializeAsync<StackoverflowModelAPI>(await streamTask);
            return repositories;
        }

        private static byte[] Decompress(byte[] compressed)
        {
            using var from = new MemoryStream(compressed);
            using var to = new MemoryStream();
            using var gZipStream = new GZipStream(from, CompressionMode.Decompress);
            gZipStream.CopyTo(to);
            return to.ToArray();
        }
        #endregion First result

        #region Second result
        [HttpGet]
        [Route("api/GetAllWWW/{res:int}")]
        public async Task<IActionResult> GetAll(int res)
        {
            string url = "https://stackoverflow.com/tags?page={num}&tab=popular";
            int tagsCounter = 0;
            bool flagPrint = false;
            long sum = 0;
            List<StackoverflowModel> tagsAll = new List<StackoverflowModel>();
            PageHelper ph = new PageHelper();

            for (int i = 1; ; i++)
            {
                string urlTemp = url.Replace("{num}", i.ToString());
                string source = await ph.pageSource(urlTemp);

                if (source != null)
                {
                    List<string> tagNameList = new List<string>();
                    List<string> tagCounterList = new List<string>();
                    Match m = Regex.Match(source, "rel=\"tag\">(.*?)</a>", RegexOptions.IgnoreCase);
                    string tagName = null;
                    if (m.Groups.Count > 0 && m.Groups[1] != null && m.Groups[1].Length > 0)
                    {
                        tagName = m.Groups[1].ToString();
                        tagNameList.Add(tagName);
                    }
                    while (m.Success)
                    {
                        m = m.NextMatch();
                        if (m.Groups.Count > 0 && m.Groups[1] != null && m.Groups[1].Length > 0)
                        {
                            tagName = m.Groups[1].ToString();
                            tagNameList.Add(tagName);
                        }
                    }

                    Match a = Regex.Match(source, "grid--cell\">(.[0-9]*)? questions</div>", RegexOptions.IgnoreCase);
                    string tagCounter = null;
                    if (a.Groups.Count > 0 && a.Groups[1] != null && a.Groups[1].Length > 0)
                    {
                        tagCounter = a.Groups[1].ToString();
                        tagCounterList.Add(tagCounter);
                    }
                    while (a.Success)
                    {
                        a = a.NextMatch();
                        if (a.Groups.Count > 0 && a.Groups[1] != null && a.Groups[1].Length > 0)
                        {
                            tagCounter = a.Groups[1].ToString();
                            tagCounterList.Add(tagCounter);
                        }
                    }

                    List<StackoverflowModel> tags = new List<StackoverflowModel>();
                    if (tagNameList.Count > 0 && tagCounterList.Count == tagNameList.Count)
                    {
                        for (int j = 0; j < tagNameList.Count; j++)
                        {
                            StackoverflowModel tag = new StackoverflowModel();
                            tag.Id = tagsCounter + 1;
                            tag.TagName = tagNameList[j];
                            tag.TagQuestionsCounter = tagCounterList[j];
                            tags.Add(tag);
                            tagsCounter++;
                            sum += Convert.ToInt32(tagCounterList[j]);
                            if (tagsCounter == res)
                            {
                                flagPrint = true;
                                break;
                            }
                        }
                    }

                    tagsAll.AddRange(tags);
                }

                if (flagPrint)
                    break;
            }

            for(int k = 0; k < tagsCounter; k++)
            {
                double a1 = (100 * Convert.ToDouble(tagsAll[k].TagQuestionsCounter))/sum;

                tagsAll[k].PercentPopularity =  Math.Round(a1, 4);
            }

            return Json(new { data = tagsAll.ToList() });
        }

        #endregion Second result
    }
}

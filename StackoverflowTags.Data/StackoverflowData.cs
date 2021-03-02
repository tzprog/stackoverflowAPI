using StackoverflowTags.Data.Helpers;
using StackoverflowTags.Types;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StackoverflowTags.Data
{
    public class StackoverflowData : IStackoverflowData
    {
        #region First result

        public async Task<List<StackoverflowAPI>> GetData()
        {
            List<StackoverflowAPI> tagsAll = new List<StackoverflowAPI>();
            long sum = 0;
            int tagsCounter = 1;
            string url = "https://api.stackexchange.com/2.2/tags?page={num}&pagesize=100&order=desc&sort=popular&site=stackoverflow";
            DeserializeHelper dh = new DeserializeHelper();

            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    string urlTemp = url.Replace("{num}", i.ToString());
                    var repositories = await dh.ProcessDeserialize(urlTemp);

                    if (repositories != null)
                    {
                        List<StackoverflowAPI> tags = new List<StackoverflowAPI>();

                        for (int j = 0; j < repositories.items.Count; j++)
                        {
                            StackoverflowAPI tag = new StackoverflowAPI();
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

            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }

            return tagsAll;
        }
        #endregion First result

        #region Second result
        public async Task<List<StackoverflowTypeWWW>> GetAll(int res)
        {
            if (res > 1000)
                res = 1000;
            if (res < 0)
                res = 0;
            string url = "https://stackoverflow.com/tags?page={num}&tab=popular";
            int tagsCounter = 0;
            bool flagPrint = false;
            long sum = 0;
            List<StackoverflowTypeWWW> tagsAll = new List<StackoverflowTypeWWW>();
            PageHelper ph = new PageHelper();

            try
            {

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

                        List<StackoverflowTypeWWW> tags = new List<StackoverflowTypeWWW>();
                        if (tagNameList.Count > 0 && tagCounterList.Count == tagNameList.Count)
                        {
                            for (int j = 0; j < tagNameList.Count; j++)
                            {
                                StackoverflowTypeWWW tag = new StackoverflowTypeWWW();
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

                for (int k = 0; k < tagsCounter; k++)
                {
                    double a1 = (100 * Convert.ToDouble(tagsAll[k].TagQuestionsCounter)) / sum;

                    tagsAll[k].PercentPopularity = Math.Round(a1, 4);
                }

            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not implemented.");
            }

            return tagsAll;
        }

        #endregion Second result
    }
}

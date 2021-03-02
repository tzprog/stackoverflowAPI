using StackoverflowTags.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace StackoverflowTags.Data.Helpers
{
    public class DeserializeHelper
    {
        public async Task<StackoverflowTypeAPI> ProcessDeserialize(string url)
        {
            var handler = new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip };
            var client = new HttpClient(handler);
            var streamTask = client.GetStreamAsync(url);

            var repositories = await JsonSerializer.DeserializeAsync<StackoverflowTypeAPI>(await streamTask);
            return repositories;
        }
    }
}

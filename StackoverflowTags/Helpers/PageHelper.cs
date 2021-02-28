using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StackoverflowTags.Helpers
{
    public class PageHelper
    {
        public async Task<string> pageSource(string urlAddress)
        {
            string data = null;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (String.IsNullOrWhiteSpace(response.CharacterSet))
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                    if (readStream != null)
                        data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();
                }
            }
            catch (HttpRequestException e)
            {
            }

            return data;
        }
    }
}

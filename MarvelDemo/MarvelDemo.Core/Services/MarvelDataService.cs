using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MarvelDemo.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MarvelDemo.Core.Services
{
    public class MarvelDataService : IMarvelDataService
    {
        // TODO: Add your Marvel Developer Account keys
        const string _API_PRIVATE_KEY = "16b3d100a5a7842a938aa1f4e4c08d75dc3d1e2d";
        const string _API_PUBLIC_KEY = "f01237e29a0de9ebe010ea15ae70a9f7";

        readonly IHashService _hashService;

        public MarvelDataService(IHashService hashService)
        {
            _hashService = hashService;
        }

        public async Task<IEnumerable<Comic>> GetComicsBySeries(int seriesId)
        {
            var ts = Guid.NewGuid().ToString();
            var hash = _hashService.CreateMd5Hash(ts + _API_PRIVATE_KEY + _API_PUBLIC_KEY);

            var url =
                $@"http://gateway.marvel.com/v1/public/series/{seriesId}/comics?orderBy=issueNumber&apikey={_API_PUBLIC_KEY}&hash={hash}&ts={ts}";
            
            var client = new HttpClient();
            var response = await client.GetStringAsync(url);
            
            var responseObject = JObject.Parse(response);

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IEnumerable<Comic>>(responseObject["data"]["results"].ToString()));
        }
    }
}

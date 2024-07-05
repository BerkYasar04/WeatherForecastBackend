using Newtonsoft.Json;
using System.Web;
using WebAPI.Business.Service;
using WebAPI.Const;
using WebAPI.Models.MGM;
using WebAPI.Models.Response;
using WebAPI.Models.Result;

namespace WebAPI.Business.Manager
{
    public class MGMManager : IMGMService
    {
        private readonly HttpClient _httpClient;

        public MGMManager()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://servis.mgm.gov.tr")
            };
            _httpClient.DefaultRequestHeaders.Referrer = new Uri("https://www.mgm.gov.tr/");
            _httpClient.DefaultRequestHeaders.Add("Origin", "https://www.mgm.gov.tr");
            _httpClient.DefaultRequestHeaders.Host = "servis.mgm.gov.tr";
        }

        public async Task<IDataResult<List<RProvince>>> GetProvinces(string? filter)
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(MGMUrl.ProvinceUrl);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TProvince>>(jsonResponse);
            result = [.. result!.OrderBy(x => x.ilPlaka)];

            if (filter != null && filter != "")
            {
                var filteredList = new List<TProvince>();
                foreach (var item in result)
                {
                    if (item.il.Contains(filter)) filteredList.Add(item);
                }
                result = filteredList;
            }
            var mappedList = new List<RProvince>();
            foreach (var province in result)
            {
                var latestEvent = (await GetLastestEvent(province.merkezId)).SuccessData[0];
                var extEvent = ConvertEventCode(latestEvent.hadiseKodu);
                var mappedProvince = new RProvince
                {
                    Province = province.il,
                    District = province.ilce,
                    MerkezId = province.merkezId,
                    CurrentPressure = latestEvent.aktuelBasinc,
                    Event = extEvent,
                    EventCode = latestEvent.hadiseKodu,
                    Humidity = latestEvent.nem,
                    SeaLevelPressure = latestEvent.denizeIndirgenmisBasinc,
                    Temperature = latestEvent.sicaklik,
                    WindSpeed = latestEvent.ruzgarHiz,
                    Precipitation = latestEvent.yagis00Now
                };
                mappedList.Add(mappedProvince);
            }

            return new SuccessDataResult<List<RProvince>>(mappedList);
        }

        public async Task<IDataResult<List<TLatestEvent>>> GetLastestEvent(int merkezid)
        {
            var dic = new Dictionary<string, string>
            {
                { "merkezid", merkezid.ToString() }
            };
            var url = BuildUrlWithQueryString(MGMUrl.LatestEventsUrl, dic);
            using HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                //return 
            }
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TLatestEvent>>(jsonResponse);
            return new SuccessDataResult<List<TLatestEvent>>(result!);
        }

        private static string ConvertEventCode(string eventCode)
        {
            return eventCode switch
            {
                "AB" => "Az Bulutlu",
                "PB" => "Parçalı Bulutlu",
                "CB" => "Çok Bulutlu",
                "Y" => "Yağmurlu",
                "SY" => "Sağanak Yağışlı",
                "A" => "Açık",
                "PUS" => "Puslu",
                "HY" => "Hafif Yağmurlu",
                "GSY" => "Gökgürültülü Sağanak Yağışlı",
                "KGY" => "Kuvvetli Gökgürültülü Sağanak Yağışlı",
                _ => eventCode,
            };
        }

        private static string BuildUrlWithQueryString(string basePath, Dictionary<string, string> queryParams)
        {
            var queryString = string.Join("&",
                queryParams.Select(kvp => $"{HttpUtility.UrlEncode(kvp.Key)}={HttpUtility.UrlEncode(kvp.Value)}"));

            return $"{basePath}?{queryString}";
        }
    }
}

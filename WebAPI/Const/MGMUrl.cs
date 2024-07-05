namespace WebAPI.Const
{
    public static class MGMUrl
    {
        public const string BaseURL = "http://servis.mgm.gov.tr";
        public const string BasePath = "/web";
        public const string ProvinceUrl = BasePath + "/merkezler/iller";
        public const string DistrictUrl = BasePath + "/merkezler/ililcesi";
        public const string CenterInfoUrl = BasePath + "/merkezler";
        public const string DailyForecastUrl = BasePath + "/tahminler/gunluk";
        public const string HourlyForecastUrl = BasePath + "/tahminler/saatlik";
        public const string LatestEventsUrl = BasePath + "/sondurumlar";
    }
}

﻿namespace WebAPI.Models.Response
{
    public class RDistrict
    {
        public string Province { get; set; }
        public string District { get; set; }
        public int MerkezId { get; set; }
        public string Event { get; set; }
        public string EventCode { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double CurrentPressure { get; set; }
        public double SeaLevelPressure { get; set; }
        public double Precipitation { get; set; }
    }
}

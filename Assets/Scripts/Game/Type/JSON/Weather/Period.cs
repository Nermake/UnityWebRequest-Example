using System;

namespace Game.Type.JSON
{
    [Serializable]
    public class Period
    {
        public int number;
        public string name;
        public string startTime;
        public string endTime;
        public bool isDaytime;
        public int temperature;
        public string temperatureUnit;
        public string temperatureTrend;
        public ProbabilityOfPrecipitation probabilityOfPrecipitation;
        public string windSpeed;
        public string windDirection;
        public string icon;
        public string shortForecast;
        public string detailedForecast;
    }
}
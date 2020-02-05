using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net.Http;

namespace InternetData
{
    public class Weather
    {
        protected static readonly string key = "4814ff966292830a43761b77aefe1545";
        protected static readonly double longitude = -71.1071909;
        protected static readonly double latitude = 42.340993;

        public static WeatherForcast GetWeatherForcast()
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                string.Format("https://api.darksky.net/forecast/{0}/{1},{2}", key, latitude, longitude));

            HttpResponseMessage response = client.SendAsync(request).Result;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(WeatherForcast));

            if(!response.IsSuccessStatusCode)
            {
                return new WeatherForcast();
            }

            return (WeatherForcast)serializer.ReadObject(response.Content.ReadAsStreamAsync().Result);
        }
    }

    [DataContract]
    public class WeatherForcast
    {
        [DataMember]
        public double latitude;

        [DataMember]
        public double longitude;

        [DataMember]
        public string timezone;

        [DataMember]
        public ForecastData currently;

        [DataMember]
        public ForecastCollection minutely;

        [DataMember]
        public ForecastCollection hourly;

        [DataMember]
        public ForecastCollection daily;

        [DataMember]
        public List<WeatherAlert> alerts;
    }

    [DataContract]
    public class WeatherAlert
    {
        [DataMember]
        public string title;

        [DataMember]
        public List<string> regions;

        [DataMember]
        public string severity;

        [DataMember]
        public long time;

        [DataMember]
        public long expires;

        [DataMember]
        public string description;

        [DataMember]
        public string uri;

        [IgnoreDataMember]
        public DateTime Time => new DateTime(time);

        [IgnoreDataMember]
        public DateTime Expires => new DateTime(expires);
    }

    [DataContract]
    public class ForecastCollection
    {
        [DataMember]
        public string summary;

        [DataMember]
        public string icon;

        [DataMember]
        public List<ForecastData> data;
    }

    [DataContract]
    public class ForecastData
    {
        [DataMember(IsRequired = true)]
        public long time;

        [DataMember]
        public string summary;

        [DataMember]
        public string icon;

        [DataMember]
        public double percipIntensity;

        [DataMember]
        public double pecipProbability;

        [DataMember]
        public double temperatue;

        [DataMember]
        public double apparentTemperature;

        [DataMember]
        public double dewPoint;

        [DataMember]
        public double humidity;

        [DataMember]
        public double pressure;

        [DataMember]
        public double windSpeed;

        [DataMember]
        public double windGust;

        [DataMember]
        public double windBearing;

        [DataMember]
        public double cloudCover;

        [DataMember]
        public double uvIndex;

        [DataMember]
        public double visibility;

        [DataMember]
        public double ozone;

        [DataMember]
        public double moonPhase;

        [DataMember]
        public double temperatureHigh;

        [DataMember]
        public long temperatureHighTime;

        [IgnoreDataMember]
        public DateTime HighTemperatureTime => new DateTime(temperatureHighTime);

        [DataMember]
        public double temperatureLow;

        [DataMember]
        public long temperatureLowTime;

        [IgnoreDataMember]
        public DateTime LowTemperatureTime => new DateTime(temperatureLowTime);

        // There are more things we could add.......
    }
        
}

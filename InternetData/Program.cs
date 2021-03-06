﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Net.Http;

namespace RussianCal
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            HttpClient client = new HttpClient();

            /*Console.Write("How many quotes do you want?  ");
            int count = Convert.ToInt32(Console.ReadLine()); */

            // Create an HTTP GET request for the API Endpoint.
            HttpRequestMessage request =
new HttpRequestMessage(HttpMethod.Get, string.Format("https://datazen.katren.ru/calendar/day/"));// This is the URL for the API endpoint, the last {0} is where you insert the number /count/ into the string.

            // Use the HttpClient to send the request message to the remote server.
            // The result, is a Response message which contains the data you requested.
            HttpResponseMessage response = client.SendAsync(request).Result;

            // Since the data is in JSON format, we use a DataContractJsonSerializer to pull the data out of that object.
            // the JsonSerializer takes a DataType as its input.  in this case, we expect the response to be a List of Strings.
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(RusCalClass));

            // Make sure the response is actually there and not a failed request.
            if (response.IsSuccessStatusCode)
            {
                RusCalClass stuff = (RusCalClass)serializer.ReadObject(response.Content.ReadAsStreamAsync().Result);

                if (stuff.holiday){
                    Console.WriteLine("Yes!");
                }else
                {
                    Console.WriteLine("Sorry nope");
                }
            }
            // Read the data from the response message.  The result is an Object that is converted into a List<string> type.
            // Print each of the quotes!
            //foreach (string quote in quotes)
            //Console.WriteLine(quote)

            else
            {
                // Make the text red
                Console.ForegroundColor = ConsoleColor.Red;

                // print the status code for the failure.  see https://http.cat for interpretations
                Console.WriteLine("Failed!  Status Code: {0}", response.StatusCode);
            }

            Console.WriteLine("\nDone!");
        }
    }
    //this class represents a boolian, true if its a holiday, false if it isnt, while checking the day

        /// <summary>
        /// Is today a Russian Holiday?
        /// </summary>

    [DataContract]
    public class RusCalClass
    {
        [DataMember]
        public bool holiday;
        [DataMember]
        public string date;

    }


}
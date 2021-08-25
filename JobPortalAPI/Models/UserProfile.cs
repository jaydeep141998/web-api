using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPortalAPI.Models
{
    public class UserProfile
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("dob")]
        public DateTime DOB { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; } = new Address();

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("highestQualification")]
        public Qualification HighestQualification { get; set; } = new Qualification();


    }


    public class Address
    {
        [JsonProperty("houseNo")]
        public int HouseNo { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("pincode")]
        public int Pincode { get; set; }
    }

    public class Qualification
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("university")]
        public string University { get; set; }

        [JsonProperty("yearOfPassing")]
        public int YearOfPassing { get; set; }

        [JsonProperty("grade")]
        public char Grade { get; set; }
    }
}

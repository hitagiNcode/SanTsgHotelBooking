namespace SanTsgHotelBooking.Application.Models.SetReservationInfo.Request
{
    public class SetReservationInfoRequest
    {
        public string transactionId { get; set; } = null!;
        public List<Traveller> travellers { get; set; } = null!;
        public CustomerInfo customerInfo { get; set; }
        public string reservationNote { get; set; } = "Reservation note";
        public string agencyReservationNumber { get; set; } = "Agency reservation number text";
    }

    public class Address
    {
        public ContactPhone contactPhone { get; set; } = new ContactPhone();
        public string email { get; set; } = null!;
        public string address { get; set; }
        public string zipCode { get; set; }
        public City city { get; set; }
        public Country country { get; set; }
        public string phone { get; set; } 
    }

    public class City
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class ContactPhone
    {
        public string countryCode { get; set; } = "90";
        public string areaCode { get; set; } = "555";
        public string phoneNumber { get; set; } = "5555555";
    }

    public class Country
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class CustomerInfo
    {
        public bool isCompany { get; set; }
        public PassportInfo passportInfo { get; set; }
        public Address address { get; set; }
        public TaxInfo taxInfo { get; set; }
        public int title { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string birthDate { get; set; }
        public string identityNumber { get; set; }
    }

    public class DestinationAddress
    {
    }

    public class Nationality
    {
        public string twoLetterCode { get; set; } = "DE";
    }

    public class PassportInfo
    {
        public string serial { get; set; } = "a";
        public string number { get; set; } = "13";
        public DateTime expireDate { get; set; } = DateTime.Now;
        public DateTime issueDate { get; set; } = DateTime.Now;
        public string citizenshipCountryCode { get; set; } = "";
    }
    public class TaxInfo
    {
    }

    public class Traveller
    {
        public string travellerId { get; set; } = "1";
        public int type { get; set; } = 1;
        public int title { get; set; } = 1;
        public int passengerType { get; set; } = 1;
        public string name { get; set; } = null!;
        public string surname { get; set; } = null!;
        public bool isLeader { get; set; } = true;
        public DateTime birthDate { get; set; } = DateTime.Now;
        public Nationality nationality { get; set; } = new Nationality();
        public string identityNumber { get; set; } = "";
        public PassportInfo passportInfo { get; set; } = new PassportInfo();
        public Address address { get; set; } = new Address();
        public DestinationAddress destinationAddress { get; set; }
        public int orderNumber { get; set; } = 1;
        public List<object> documents { get; set; }
        public List<object> insertFields { get; set; }
        public int status { get; set; } = 0;
    }
}

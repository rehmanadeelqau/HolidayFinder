namespace OTB.HolidaySearch.Domain.Contracts
{
    public class Recommendation
    {
        public decimal TotalPrice => (Flight.Price) + (Hotel != null ? Hotel.PricePerNight * Hotel.AvailableNights : 0);
        public Flight Flight { get; set; }

        public Hotel Hotel { get; set; }

    }
}

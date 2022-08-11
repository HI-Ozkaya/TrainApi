using TrainApi.Models;

namespace TrainApi.CommandResponse
{
    public class TrainReservationCommandResponse
    {
        public TrainReservationCommandResponse()
        {
            this.YerlesimAyrinti = new List<SeatDetail>();
        }
        public bool RezervasyonYapilabilir { get; set; }

        public List<SeatDetail> YerlesimAyrinti { get; set; }
    }
}

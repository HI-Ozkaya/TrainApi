using TrainApi.Models;

namespace TrainApi.Command
{
    public class TrainReservationCommand
    {
        public Tren Tren { get; set; }

        public int RezervasyonYapilacakKisiSayisi { get; set; }

        public bool KisilerFarkliVagonlaraYerlestirilebilir { get; set; }
    }
}

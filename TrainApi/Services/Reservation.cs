using TrainApi.Command;
using TrainApi.CommandResponse;
using TrainApi.IServices;
using TrainApi.Models;

namespace TrainApi.Services
{
    public class Reservation : IReservation
    {
        public async Task<TrainReservationCommandResponse> MakeReservation(TrainReservationCommand command)
        {
            TrainReservationCommandResponse response = new TrainReservationCommandResponse();
            bool flex = command.KisilerFarkliVagonlaraYerlestirilebilir;
            int number = command.RezervasyonYapilacakKisiSayisi;
            foreach (var item in command.Tren.Vagonlar)
            {
                int onlineLimit = item.Kapasite * 70 / 100;
                if (item.DoluKoltukAdet>=onlineLimit)
                {
                    continue;
                }
                else
                {
                    int bosYer = onlineLimit - item.DoluKoltukAdet;
                    if (bosYer > number)
                    {
                        SeatDetail vagonSeat = new SeatDetail();
                        vagonSeat.KisiSayisi = number;
                        vagonSeat.VagonAdi = item.Ad;
                        response.YerlesimAyrinti.Add(vagonSeat);
                        number = 0;
                    }
                    else if (flex)
                    {
                        int oldNumber = number;
                        number = number - bosYer;
                        SeatDetail vagonSeat = new SeatDetail();
                        vagonSeat.KisiSayisi = oldNumber-number;
                        vagonSeat.VagonAdi = item.Ad;
                        response.YerlesimAyrinti.Add(vagonSeat);
                    }
                }
                if (number==0)
                {
                    response.RezervasyonYapilabilir = true;
                    break;
                }
            }
            if (number>0)
            {
                response.YerlesimAyrinti.Clear();
            }
            return response;
        }
    }
}

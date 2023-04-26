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
            var response = new TrainReservationCommandResponse();
            bool flex = command.KisilerFarkliVagonlaraYerlestirilebilir;
            int number = command.RezervasyonYapilacakKisiSayisi;

            if (flex)
            {
                int emptySeatsForOnline = 0;
                foreach (var item in command.Tren.Vagonlar)
                {
                    int onlineLimit = item.Kapasite * 70 / 100;
                    if (item.DoluKoltukAdet < onlineLimit)
                    {
                        emptySeatsForOnline += onlineLimit - item.DoluKoltukAdet;
                    }
                }

                if (emptySeatsForOnline>=number)
                {
                    response.RezervasyonYapilabilir= true;
                    foreach (var item in command.Tren.Vagonlar)
                    {
                        int onlineLimit = item.Kapasite * 70 / 100;
                        int avaliableOnlineSeat = onlineLimit - item.DoluKoltukAdet;
                        if (avaliableOnlineSeat > 0)
                        {
                            if (avaliableOnlineSeat>number)
                            {
                                SeatDetail vagonSeat = new SeatDetail
                                {
                                    KisiSayisi = avaliableOnlineSeat - number + item.DoluKoltukAdet,
                                    VagonAdi = item.Ad
                                };
                                response.YerlesimAyrinti.Add(vagonSeat);
                                break;
                            }
                            else
                            {
                                SeatDetail vagonSeat = new SeatDetail
                                {
                                    KisiSayisi = avaliableOnlineSeat + item.DoluKoltukAdet,
                                    VagonAdi = item.Ad
                                };
                                response.YerlesimAyrinti.Add(vagonSeat);
                                number -= number - avaliableOnlineSeat;
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var item in command.Tren.Vagonlar)
                {
                    int onlineLimit = item.Kapasite * 70 / 100;
                    if (item.DoluKoltukAdet+number < onlineLimit)
                    {
                        response.RezervasyonYapilabilir = true;
                        SeatDetail vagonSeat = new SeatDetail
                        {
                            KisiSayisi = item.DoluKoltukAdet + number + item.DoluKoltukAdet,
                            VagonAdi = item.Ad
                        };
                        response.YerlesimAyrinti.Add(vagonSeat);
                        break;
                    }
                }
            }
            return response;
        }
    }
}

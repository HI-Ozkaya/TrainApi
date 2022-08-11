using TrainApi.CommandResponse;
using TrainApi.Command;

namespace TrainApi.IServices
{
    public interface IReservation
    {
        Task<TrainReservationCommandResponse> MakeReservation (TrainReservationCommand command);
    }
}

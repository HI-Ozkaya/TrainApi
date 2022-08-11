using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainApi.Command;
using TrainApi.CommandResponse;
using TrainApi.IServices;

namespace TrainApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservation reservation;
        public ReservationController(IReservation reservation)
        {
            this.reservation = reservation;
        }

        [HttpPost("trainReservation")]
        public async Task<TrainReservationCommandResponse> TrainReservation([FromBody] TrainReservationCommand command)
        {
            return await this.reservation.MakeReservation(command);
        }
    }
}

namespace TrainApi.Models
{
    public class Tren
    {
        public Tren()
        {
            this.Vagonlar = new List<Vagon>();
        }
        public string Ad { get; set; }
        public List<Vagon> Vagonlar { get; set; }
    }
}

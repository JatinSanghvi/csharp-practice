namespace CsharpPractice.Source.ClassDesign.ParkingLot
{
    public class ParkingSpot
    {
        public SpotType SpotType { get; }

        public bool IsFree { get; private set; }

        public ParkingSpot(SpotType spotType)
        {
            this.SpotType = spotType;
            this.IsFree = true;
        }

        public void Park()
        {
            this.IsFree = false;
        }

        public void Unpark()
        {
            this.IsFree = true;
        }
    }
}
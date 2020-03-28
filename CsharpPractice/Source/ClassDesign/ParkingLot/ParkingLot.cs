namespace CsharpPractice.Source.ClassDesign.ParkingLot
{
    public sealed class ParkingLot
    {
        private readonly ParkingLevel[] parkingLevels;

        public ParkingLot(ParkingLevel[] parkingLevels)
        {
            this.parkingLevels = parkingLevels;
        }

        public ParkingSpot[] Park(VehicleType vehicleType)
        {
            foreach (ParkingLevel parkingLevel in this.parkingLevels)
            {
                ParkingSpot[] parkingSpots = parkingLevel.Park(vehicleType);
                if (parkingSpots != null)
                {
                    return parkingSpots;
                }
            }

            return null;
        }

        public void Unpark(ParkingSpot parkingSpot)
        {
            parkingSpot.Unpark();
        }
    }
}

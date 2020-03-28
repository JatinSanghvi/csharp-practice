namespace CsharpPractice.Source.ClassDesign.ParkingLot
{
    public sealed class ParkingLevel
    {
        private readonly ParkingRow[] parkingRows;

        public ParkingLevel(ParkingRow[] parkingRows)
        {
            this.parkingRows = parkingRows;
        }

        public ParkingSpot[] Park(VehicleType vehicleType)
        {
            foreach (ParkingRow parkingRow in this.parkingRows)
            {
                ParkingSpot[] parkingSpots = parkingRow.Park(vehicleType);
                if (parkingSpots != null)
                {
                    return parkingSpots;
                }
            }

            return null;
        }
    }
}

using System;
using System.Linq;

namespace CsharpPractice.Source.ClassDesign.ParkingLot
{
    public sealed class ParkingRow
    {
        private readonly ParkingSpot[] parkingSpots;

        public ParkingRow(ParkingSpot[] parkingSpots)
        {
            this.parkingSpots = parkingSpots;
        }

        public ParkingSpot[] Park(VehicleType vehicleType)
        {
            ParkingSpot[] parkedSpots = null;

            switch (vehicleType)
            {
                case VehicleType.Motorcycle:
                    ParkingSpot parkedSpot = this.parkingSpots.FirstOrDefault(spot => spot.SpotType == SpotType.Motorcycle && spot.IsFree)
                        ?? this.parkingSpots.FirstOrDefault(spot => spot.SpotType == SpotType.Compact && spot.IsFree)
                        ?? this.parkingSpots.FirstOrDefault(spot => spot.SpotType == SpotType.Large && spot.IsFree);
                    parkedSpots = new[] { parkedSpot };
                    break;

                case VehicleType.Car:
                    parkedSpot = this.parkingSpots.FirstOrDefault(spot => spot.SpotType == SpotType.Compact && spot.IsFree)
                        ?? this.parkingSpots.FirstOrDefault(spot => spot.SpotType == SpotType.Large && spot.IsFree);
                    parkedSpots = new[] { parkedSpot };
                    break;

                case VehicleType.Bus:

                    int freeSpots = 0;
                    foreach (int index in Enumerable.Range(0, this.parkingSpots.Length))
                    {
                        freeSpots = this.parkingSpots[index].IsFree ? freeSpots + 1 : 0;

                        if (freeSpots == 5)
                        {
                            parkedSpots = new ParkingSpot[5];
                            Array.Copy(this.parkingSpots, index - 4, parkedSpots, 0, 5);
                            break;
                        }
                    }

                    break;
            }

            foreach (var parkedSpot in parkedSpots)
            {
                parkedSpot.Park(); // Not thread safe.
            }

            return parkedSpots;
        }
    }
}

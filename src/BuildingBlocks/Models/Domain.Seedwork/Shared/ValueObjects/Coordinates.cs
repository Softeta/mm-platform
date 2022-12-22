using Domain.Seedwork.Consts;
using NetTopologySuite.Geometries;

namespace Domain.Seedwork.Shared.ValueObjects
{
    public class Coordinates : ValueObject<Coordinates>
    {
        public double Longitude { get; init; }
        public double Latitude { get; init; }
        public Point Point { get; init; }

        public Coordinates(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
            Point = new Point(Longitude, Latitude) { SRID = SpatialReferenceIds.Gps };
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Longitude;
            yield return Latitude;
            yield return Point;
        }
    }
}

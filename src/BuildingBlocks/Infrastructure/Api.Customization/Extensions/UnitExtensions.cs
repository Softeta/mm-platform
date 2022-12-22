namespace API.Customization.Extensions
{
    public static class UnitExtensions
    {
        public static int? ToMeters(this int? kilometers)  => 
            kilometers.HasValue ? kilometers * 1000 : null;
    }
}

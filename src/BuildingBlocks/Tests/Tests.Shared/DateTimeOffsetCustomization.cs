using AutoFixture;

namespace Tests.Shared
{
    public class DateTimeOffsetCustomization : ICustomization
    {
        private readonly Random _random = new();
        private DateTimeOffset _current = DateTimeOffset.MinValue;

        public void Customize(IFixture fixture)
        {
            fixture.Register(GenerateIncreasingDates);
        }

        private DateTimeOffset GenerateIncreasingDates()
        {
            if (_current >= DateTimeOffset.MaxValue)
            {
                _current = DateTimeOffset.MinValue;
            }

            TimeSpan offset = DateTimeOffset.MaxValue - _current;
            var range = offset.Milliseconds;
            _current = _current.AddMilliseconds(_random.Next(range) + 1);

            return _current;
        }

    }
}

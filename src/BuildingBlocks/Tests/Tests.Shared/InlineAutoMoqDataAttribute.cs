using AutoFixture.Xunit2;

namespace Tests.Shared
{
    public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object?[]? objects) : base(new AutoMoqDataAttribute(), objects) { }
    }
}

using Xunit;

namespace Domain.UnitTests;

public class OutcomeTests
{
    public class TheWinnersProperty
    {
        [Fact]
        public void ShouldReturnCorrectWinners()
        {
            var sut = new Outcome(new[] { new Player("123", "Cleetus") });

            Assert.Equal(1, sut.Winners.Count);
        }
    }
}
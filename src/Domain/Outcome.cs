using System.Collections.Generic;

namespace Domain
{
    public record Outcome(IReadOnlyCollection<Player> Winners);
}
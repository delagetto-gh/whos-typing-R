namespace Application.Events;

public abstract record GameEvent
{
    public string Name => this.GetType().Name;
}
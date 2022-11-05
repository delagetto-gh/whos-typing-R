using System;

namespace Domain.Exceptions;

public class UnchosenPlayerTypedException : Exception
{
    public UnchosenPlayerTypedException() : base("The wrong player typed.") { }
}
namespace AspNetDependencyInjectionVS
{
    using System;

    public class MyOtherService : IOtherService
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
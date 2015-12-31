namespace AspNetDependencyInjectionVS
{
    using System;
    
    public interface IService
    {
        int Id { get; }

        DateTime Created { get; }
        
        IOtherService OtherService { get; }
    }
}
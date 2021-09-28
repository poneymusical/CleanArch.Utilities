using System.Reflection;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.DependencyInjection;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Test.CleanArch.Utilities.DependencyInjection._TestUtils.PipelineBehaviors;
using Test.CleanArch.Utilities.DependencyInjection._TestUtils.Requests;
using Xunit;

namespace Test.CleanArch.Utilities.DependencyInjection
{
    public class TestServiceCollectionExtensions
    {
        [Fact]
        public void TestAddServicePipelineBehavior_TypeDoesNotImplementAnythingRelevant()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServicePipelineBehavior<object>(Assembly.GetExecutingAssembly());
            serviceCollection.Should().BeEmpty();
        }

        [Fact]
        public void TestAddServicePipelineBehavior_TypeImplementsClosedVersion_TypeIsNotGeneric()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServicePipelineBehavior(Assembly.GetExecutingAssembly(), typeof(MockGenericPipelineBehaviorForClosedRequest<>));
            serviceCollection.Should().HaveCount(2);
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockGenericPipelineBehaviorForClosedRequest<MockClosedServiceRequest>)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<MockClosedServiceRequest, ServiceResponse>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockGenericPipelineBehaviorForClosedRequest<AnotherMockClosedServiceRequest>)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<AnotherMockClosedServiceRequest, ServiceResponse>));
        }
        
        [Fact]
        public void TestAddServicePipelineBehavior_TypeImplementsClosedVersion_TypeIsGeneric()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServicePipelineBehavior<MockClosedPipelineBehaviorForClosedRequest>(Assembly.GetExecutingAssembly());
            serviceCollection.Should().HaveCount(2);
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockClosedPipelineBehaviorForClosedRequest)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<MockClosedServiceRequest, ServiceResponse>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockClosedPipelineBehaviorForClosedRequest)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<AnotherMockClosedServiceRequest, ServiceResponse>));
        }
    }
}
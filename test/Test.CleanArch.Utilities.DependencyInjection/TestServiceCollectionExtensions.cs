using System.Reflection;
using CleanArch.Utilities.Core.Service;
using CleanArch.Utilities.Core.Validation;
using CleanArch.Utilities.DependencyInjection;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Test.CleanArch.Utilities.DependencyInjection._TestUtils.PipelineBehaviors;
using Test.CleanArch.Utilities.DependencyInjection._TestUtils.RequestHandlers;
using Test.CleanArch.Utilities.DependencyInjection._TestUtils.Requests;
using Test.CleanArch.Utilities.DependencyInjection._TestUtils.Validators;
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
        public void TestAddServicePipelineBehavior_ClosedPipelineForClosedRequests()
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
        
        [Fact]
        public void TestAddServicePipelineBehavior_GenericPipelineForClosedRequests()
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
        public void TestAddServicePipelineBehavior_ClosedPipelineForGenericRequest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServicePipelineBehavior<MockClosedPipelineBehaviorForGenericRequest>(Assembly.GetExecutingAssembly());
            serviceCollection.Should().HaveCount(2);
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockClosedPipelineBehaviorForGenericRequest)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<MockGenericServiceRequest, ServiceResponse<int>>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockClosedPipelineBehaviorForGenericRequest)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<AnotherMockGenericServiceRequest, ServiceResponse<long>>));
        }
        
        [Fact]
        public void TestAddServicePipelineBehavior_GenericPipelineForGenericRequest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServicePipelineBehavior(Assembly.GetExecutingAssembly(), typeof(MockGenericPipelineBehaviorForGenericRequest<,>));
            serviceCollection.Should().HaveCount(2);
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockGenericPipelineBehaviorForGenericRequest<MockGenericServiceRequest, int>)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<MockGenericServiceRequest, ServiceResponse<int>>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockGenericPipelineBehaviorForGenericRequest<AnotherMockGenericServiceRequest, long>)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<AnotherMockGenericServiceRequest, ServiceResponse<long>>));
        }

        [Fact]
        public void TestAddServices_ExplicitPipelineBehaviors()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServices(Assembly.GetExecutingAssembly(),
                typeof(MockGenericPipelineBehaviorForClosedRequest<>), typeof(MockGenericPipelineBehaviorForGenericRequest<,>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Scoped 
                                                     && sd.ImplementationType == typeof(MockValidator)
                                                     && sd.ServiceType == typeof(IValidator<MockClosedServiceRequest>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockRequestHandlerForClosedRequest)
                                                     && sd.ServiceType == typeof(IRequestHandler<MockClosedServiceRequest, ServiceResponse>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockRequestHandlerForGenericRequest)
                                                     && sd.ServiceType == typeof(IRequestHandler<MockGenericServiceRequest, ServiceResponse<int>>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockGenericPipelineBehaviorForClosedRequest<MockClosedServiceRequest>)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<MockClosedServiceRequest, ServiceResponse>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockGenericPipelineBehaviorForClosedRequest<AnotherMockClosedServiceRequest>)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<AnotherMockClosedServiceRequest, ServiceResponse>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockGenericPipelineBehaviorForGenericRequest<MockGenericServiceRequest, int>)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<MockGenericServiceRequest, ServiceResponse<int>>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockGenericPipelineBehaviorForGenericRequest<AnotherMockGenericServiceRequest, long>)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<AnotherMockGenericServiceRequest, ServiceResponse<long>>));
        }
        
        [Fact]
        public void TestAddServices_DefaultBehaviors()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServices(Assembly.GetExecutingAssembly());
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Scoped 
                                                     && sd.ImplementationType == typeof(MockValidator)
                                                     && sd.ServiceType == typeof(IValidator<MockClosedServiceRequest>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockRequestHandlerForClosedRequest)
                                                     && sd.ServiceType == typeof(IRequestHandler<MockClosedServiceRequest, ServiceResponse>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(MockRequestHandlerForGenericRequest)
                                                     && sd.ServiceType == typeof(IRequestHandler<MockGenericServiceRequest, ServiceResponse<int>>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(ValidationPipelineBehavior<MockClosedServiceRequest>)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<MockClosedServiceRequest, ServiceResponse>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(ValidationPipelineBehavior<AnotherMockClosedServiceRequest>)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<AnotherMockClosedServiceRequest, ServiceResponse>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(ValidationPipelineBehavior<MockGenericServiceRequest, int>)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<MockGenericServiceRequest, ServiceResponse<int>>));
            
            serviceCollection.Should().Contain(sd => sd.Lifetime == ServiceLifetime.Transient 
                                                     && sd.ImplementationType == typeof(ValidationPipelineBehavior<AnotherMockGenericServiceRequest, long>)
                                                     && sd.ServiceType == typeof(IPipelineBehavior<AnotherMockGenericServiceRequest, ServiceResponse<long>>));
        }
    }
}
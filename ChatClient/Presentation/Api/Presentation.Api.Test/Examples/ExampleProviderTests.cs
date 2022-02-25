using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Presentation.Api.Test.Examples
{
    public class ExampleProviderTests
    {
        private static IEnumerable<Type> GetTypesThatImplementOpenGenericInterface(Type genericInterface)
        {
            return typeof(Program).Assembly.GetTypes()
                .SelectMany(type => type.GetInterfaces(), (type, @interface) => new {type, @interface})
                .Select(t => new {t, baseType = t.type.BaseType})
                .Where(t =>
                    t.baseType != null && t.baseType.IsGenericType &&
                    genericInterface.IsAssignableFrom(t.baseType.GetGenericTypeDefinition()) ||

                    t.t.@interface.IsGenericType &&
                    genericInterface.IsAssignableFrom(t.t.@interface.GetGenericTypeDefinition()))
                .Select(t => t.t.type);
        }

        [Fact]
        public void SingleExampleProviders_ShouldProduceValidExamples()
        {
            // Arrange
            Type openProviderType = typeof(IExamplesProvider<>);

            IEnumerable<Type> types = GetTypesThatImplementOpenGenericInterface(openProviderType);

            Assert.NotEmpty(types);

            IEnumerable<object> providerInstances = types.Select(Activator.CreateInstance);

            // Act + Assert
            foreach (object instance in providerInstances)
            {
                Assert.NotNull(instance);

                const string methodName = nameof(IExamplesProvider<object>.GetExamples);

                MethodInfo methodInfo = instance.GetType().GetMethod(methodName);

                Assert.NotNull(methodInfo);

                object exampleValue = methodInfo.Invoke(instance, null);

                Assert.NotNull(exampleValue);
            }
        }

        [Fact]
        public void MultipleExampleProviders_ShouldProduceValidExamples()
        {
            // Arrage
            Type openProviderType = typeof(IMultipleExamplesProvider<>);

            IEnumerable<Type> types = GetTypesThatImplementOpenGenericInterface(openProviderType);

            Assert.NotEmpty(types);

            IEnumerable<object> providerInstances = types.Select(Activator.CreateInstance);

            // Act + Assert
            foreach (object instance in providerInstances)
            {
                Assert.NotNull(instance);

                const string methodName = nameof(IMultipleExamplesProvider<object>.GetExamples);
                MethodInfo methodInfo = instance.GetType().GetMethod(methodName);

                Assert.NotNull(methodInfo);

                object wrapperCollectionValue = methodInfo.Invoke(instance, null);

                Assert.NotNull(wrapperCollectionValue);

                // Iterate through example wrappers and their values
                IEnumerable wrapperCollection = (IEnumerable) wrapperCollectionValue;

                foreach (object exampleWrapper in wrapperCollection)
                {
                    const string valueName = nameof(SwaggerExample<object>.Value);
                    PropertyInfo valuePropertyInfo = exampleWrapper.GetType().GetProperty(valueName);

                    Assert.NotNull(valuePropertyInfo);

                    object exampleValue = valuePropertyInfo.GetValue(exampleWrapper);

                    Assert.NotNull(exampleValue);
                }
            }
        }
    }
}

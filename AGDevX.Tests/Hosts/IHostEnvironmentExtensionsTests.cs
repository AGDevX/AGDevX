﻿using AGDevX.Exceptions;
using AGDevX.Hosts;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace AGDevX.Tests.Hosts;
public class IHostEnvironmentExtensionsTests
{
    private static IHostEnvironment _prodHostEnvironment = new HostEnvironment
    {
        ApplicationName = "AGDevX",
        EnvironmentName = "Prod",
        ContentRootPath = ""
    };

    public class When_calling_IsOneOf
    {
        [Fact]
        public void And_the_host_environment_is_null_with_a_null_array_of_environments_then_throw_exception()
        {
            //-- Arrange
            var hostEnvironment = _prodHostEnvironment;
            string[]? environments = null;

            //-- Act & Assert
            Assert.Throws<ExtensionMethodParameterNullException>(() => hostEnvironment.IsOneOf(environments));
        }

        [Fact]
        public void And_the_host_environment_is_a_prod_environment_with_a_prod_environment_then_return_true()
        {
            //-- Arrange
            var hostEnvironment = _prodHostEnvironment;
            var environments = new string[1] { "Prod" };

            //-- Act
            var isOneOf = hostEnvironment.IsOneOf(environments);

            //-- Assert
            Assert.True(isOneOf);
        }

        [Fact]
        public void And_the_host_environment_is_a_prod_environment_with_a_prod_and_non_prod_environment_then_return_true()
        {
            //-- Arrange
            var hostEnvironment = _prodHostEnvironment;
            var environments = new string[2] { "QA", "Prod" };

            //-- Act
            var isOneOf = hostEnvironment.IsOneOf(environments);

            //-- Assert
            Assert.True(isOneOf);
        }

        [Fact]
        public void And_the_host_environment_is_a_prod_environment_with_a_non_prod_environment_then_return_false()
        {
            //-- Arrange
            var hostEnvironment = _prodHostEnvironment;
            var environments = new string[2] { "Local", "Dev" };

            //-- Act
            var isOneOf = hostEnvironment.IsOneOf(environments);

            //-- Assert
            Assert.False(isOneOf);
        }
    }

    public class HostEnvironment : IHostEnvironment
    {
        public required string EnvironmentName { get; set; }
        public required string ApplicationName { get; set; }
        public required string ContentRootPath { get; set; }
#pragma warning disable CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
        public IFileProvider? ContentRootFileProvider { get; set; }
#pragma warning restore CS8766 // Nullability of reference types in return type doesn't match implicitly implemented member (possibly because of nullability attributes).
    }
}
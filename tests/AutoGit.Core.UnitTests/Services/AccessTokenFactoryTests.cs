using AutoGit.Core.Interfaces;
using AutoGit.Core.Services;
using FluentAssertions;
using NSubstitute;
using System;
using Xunit;

namespace AutoGit.Core.UnitTests.Services
{
    public class AccessTokenFactoryTests
    {
        private readonly IDateTimeProvider _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        private readonly IAccessTokenFactory _sut;

        public AccessTokenFactoryTests()
        {
            _sut = new AccessTokenFactory(_dateTimeProvider);
        }

        [Fact(Skip = "Broken")]
        public void Create_ShouldCreateToken_WhenValidDates()
        {
            // Arrange
            var appIdentifier = "123";
            var privateKey =
                "MIIEowIBAAKCAQB6W0gmlz6/PtSCwq5KCIUXe/nsSg14sdgzkyqa3t6EvY7x/DNC0ImUxFiwHUYvDKspvhdBKu47Qw+d/wFm2UNhFnhVVKfqzK0/0QGIXZdLTOMMw7XFJCsKshH2rxrYININpHKqZ3YywbsRb/WBG0rf68hHdlSotX9oYl8ox/g8ueWI43d3xRW43cH1SY9MuysL5BxppIpbSrZa0txPp4ns+fIIlzUeUsd0U66Z66AONMCJThwYLYxtsf5vSH1mlG9ieh9nB96tCaaAVAMoNvfNpbViGkbg1hKq/w+wC38CgC+7U//E1KytUQs6g+XcbJWBHjEiX1xMfPEanjsn6NmPAgMBAAECggEAKpGxk4ORWBYy919mJem67EW82QGWmEQ/tQnhi8o4XrRYlEYrS3akNzbsqDE3Js1gi4BQNOMLyWB2gYCj6zVxpMidiwqN9TnKmOZNgzUUmyUf5WP9zJ3dv7XeXBXl4AXjLs3k+vZOADC4tcNfBdEKBsfQgVRg9eVXc2iuNNEFw5Jo7Av1SHooz7kAQOQNjcNeakxg5+Al9MOdtMCbQiz+hdWZeDD5RCWMISHYbK754Cn7kPPVeOiJtDdYecztF8kaGqHdeB/wAsSudKyWNB4NQ4+OAhO/m8XAUFTUZZvcAvkws+vZaMsqt2dSzMRWhmRGHxbp8y7ZxpleT05rHhRSIQKBgQDO1g8ZrNO8cs2GeaDlK2Md3a5JIXXHr7NFrl5O+/E15ch0IlAhg/Pz7IaKxh0Eo2Iv238ZKAExUMc6fQQ+PL3Y+MpJIH+BkBBmCM1/GpcsFS2Eee6UtUDuPi6CGxMkBB8HQlXX6YAuzTAAkKSQv1WOpuoxv83oVSmqGkB7Oa2tfwKBgQCXcKfSp9zjium7tuRhCnDIqY8JkH+2kWQls/aImkZTRxxNTDU1VyzqptLK2qbBD9A4u+rOYoDqR0/UC4kM7zpkBsWyKvEjUSES0qMDRrasXcZw3QNhm0U+8ZRDllZHRMkPzexYVIlYbNUm9L9gIYQQMIr0aiNG+N+CxPyR/jP78QKBgQCQefGeS5yzG7X4YulCauYXLIvbRWYSD+a3m68AmPfKYDF+/aDfaF51Whg4w13tqXqiVz4Wgl0i53vinQTjvGYYMuCJ6AtoowrxWuDAEgDaqAhdaxZ+BYXPZNvzwZnDJKXP2kRARzhkeKs5Gc/508ydknYwPfxEd4hc621zJbztXQKBgHSN+BxhpdhbKdmXwd4OMw+9sKEj3aed7v41rCEm28lsiZPUe386QQ+4pbQK5RziFtgTiXyew6mMiRwiZLDXzpD9xVqDQmR0JIhgJaZ33lB7PCfRcrDE2F7Bf+Mk6fKgNj87thOlvK+Z09PPuxs5sHLlaY0Dw3L0SNJU2i9i/o+BAoGBAMXrH9CUdIaMb+uSFpI8OrREYG03qKvI0YGUCKH808j32AJHKXkRQFoWh4LJsUhJIIQJBdgKsJEWK4fexBIoqINP6aHeIKDkQew7iQm1RXySRw/gOfJ4fywQ+Pn5wl2z4OaHZzTxjyxrR0wzW8/5zBI5DGnEZfc4wdagKtwHMSZQ";
            _dateTimeProvider.UtcNow.Returns(new DateTime(2022, 1, 1, 1, 10, 0));

            // Act
            var accessToken = _sut.Create(appIdentifier, privateKey);

            // Assert
            accessToken.Should().Be(
                "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE2NDEwMTc0MDAsImV4cCI6MTY0MTAxODAwMCwiaWF0IjoxNjQxMDE3NDAwLCJpc3MiOiIxMjMifQ.AjsXSeWFEIa16hQ1T8pkM-3tnDlJ087PeYHZCBKjeiEwMEoFsvlsR3i4uorB7yBEvuGWjSD6kvtl4WD3FyIosqA4VoGQgJk34L5SzIE7e7CkrZvX3UcQyAJp_J7me6rZ813Z3qSkWvLMfagplJdanFN42e4OgRM0hYWXQoUEWP9QGwiANsFL764q8mw1KEC5glJF6wOHDCps1X_MqXrNzs1_whBmLbD6vSqTkfKvVNp0W4V_IgSbqfKd8UePcBpGUZXpwuzifZXcFswoXnaFRgZ7of6C1cs54R_cPtgjLWT7qkZVMeiVdY9-UZWVgp5rYrzLG4t5xGqyfSxIVMmYhQ");
        }
    }
}
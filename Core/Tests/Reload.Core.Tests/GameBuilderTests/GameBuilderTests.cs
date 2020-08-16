using FluentAssertions;
using NSubstitute;
using Reload.Core.Exceptions;
using Reload.Core.Game;
using Reload.Core.Tests.Fakes;
using System;
using Xunit;

namespace Reload.Core.Tests.GameBuilderTests
{
    public class GameBuilderTests
    {
        [Fact]
        public void WithWindow_OSNotCompatible_ThrowsReloadWindowBackendNotSupportedException()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            GameBuilder<GameSystemFake> gameBuilder = new GameBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckWindowCompatability<GameWindowFake>().Returns(false);

            //Act
            Func<GameBuilder<GameSystemFake>> act = () => gameBuilder.WithWindow<GameWindowFake>();

            //Assert
            act.Should().Throw<ReloadWindowBackendNotSupportedException>();
        }

        [Fact]
        public void WithWindow_OSCompatible_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            GameBuilder<GameSystemFake> gameBuilder = new GameBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckWindowCompatability<GameWindowFake>().Returns(true);

            //Act
            Func<GameBuilder<GameSystemFake>> act = () => gameBuilder.WithWindow<GameWindowFake>();

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<GameBuilder<GameSystemFake>>();
        }

        [Fact]
        public void WithGraphicsBackend_OSNotCompatible_ThrowsReloadGraphicsBackendNotSupportedException()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            GameBuilder<GameSystemFake> gameBuilder = new GameBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckGraphicsBackendCompatability<GraphicsBackendFake>().Returns(false);

            //Act
            Func<GameBuilder<GameSystemFake>> act = () => gameBuilder.WithGraphicsBackend<GraphicsBackendFake>();

            //Assert
            act.Should().Throw<ReloadGraphicsBackendNotSupportedException>();
        }

        [Fact]
        public void WithGraphicsBackend_OSCompatible_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            GameBuilder<GameSystemFake> gameBuilder = new GameBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckGraphicsBackendCompatability<GraphicsBackendFake>().Returns(true);

            //Act
            Func<GameBuilder<GameSystemFake>> act = () => gameBuilder.WithGraphicsBackend<GraphicsBackendFake>();

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<GameBuilder<GameSystemFake>>();
        }

        [Fact]
        public void WithAudioBackend_OSNotCompatible_ThrowsReloadAudioBackendNotSupportedException()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            GameBuilder<GameSystemFake> gameBuilder = new GameBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckAudioBackendCompatability<AudioBackendFake>().Returns(false);

            //Act
            Func<GameBuilder<GameSystemFake>> act = () => gameBuilder.WithAudioBackend<AudioBackendFake>();

            //Assert
            act.Should().Throw<ReloadAudioBackendNotSupportedException>();
        }

        [Fact]
        public void WithAudioBackend_OSCompatible_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            GameBuilder<GameSystemFake> gameBuilder = new GameBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckAudioBackendCompatability<AudioBackendFake>().Returns(true);

            //Act
            Func<GameBuilder<GameSystemFake>> act = () => gameBuilder.WithAudioBackend<AudioBackendFake>();

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<GameBuilder<GameSystemFake>>();
        }

        [Fact]
        public void WithInput_OSNotCompatible_ThrowsReloadInputNotSupportedException()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            GameBuilder<GameSystemFake> gameBuilder = new GameBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckInputCompatability<InputSystemFake>().Returns(false);

            //Act
            Func<GameBuilder<GameSystemFake>> act = () => gameBuilder.WithInput<InputSystemFake>();

            //Assert
            act.Should().Throw<ReloadInputNotSupportedException>();
        }

        [Fact]
        public void WithInput_OSCompatible_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            GameBuilder<GameSystemFake> gameBuilder = new GameBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckInputCompatability<InputSystemFake>().Returns(true);

            //Act
            Func<GameBuilder<GameSystemFake>> act = () => gameBuilder.WithInput<InputSystemFake>();

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<GameBuilder<GameSystemFake>>();
        }

        [Fact]
        public void WithSubSystem_InvalidLifetime_ThrowsReloadInvalidEnumValueException()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            GameBuilder<GameSystemFake> gameBuilder = new GameBuilder<GameSystemFake>(osPlatform);

            //Act
            Func<GameBuilder<GameSystemFake>> act = () => gameBuilder.WithSubSystem<SubSystemFake>(0);

            //Assert
            act.Should().Throw<ReloadInvalidEnumValueException>();
        }

        [Fact]
        public void WithSubSystem_SingletonLifetime_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            GameBuilder<GameSystemFake> gameBuilder = new GameBuilder<GameSystemFake>(osPlatform);

            //Act
            Func<GameBuilder<GameSystemFake>> act = () => gameBuilder.WithSubSystem<SubSystemFake>(SubSystemLifetime.Singleton);

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<GameBuilder<GameSystemFake>>();
        }

        [Fact]
        public void WithSubSystem_TransientLifetime_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            GameBuilder<GameSystemFake> gameBuilder = new GameBuilder<GameSystemFake>(osPlatform);

            //Act
            Func<GameBuilder<GameSystemFake>> act = () => gameBuilder.WithSubSystem<SubSystemFake>(SubSystemLifetime.Transient);

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<GameBuilder<GameSystemFake>>();
        }

        [Fact]
        public void WithSubSystem_ScopedLifetime_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            GameBuilder<GameSystemFake> gameBuilder = new GameBuilder<GameSystemFake>(osPlatform);

            //Act
            Func<GameBuilder<GameSystemFake>> act = () => gameBuilder.WithSubSystem<SubSystemFake>(SubSystemLifetime.Scoped);

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<GameBuilder<GameSystemFake>>();
        }

        [Fact]
        public void BuildForPlatform_WithSubSystems_ReturnsGameSystem()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            GameBuilder<GameSystemFake> gameBuilder = new GameBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckGraphicsBackendCompatability<GraphicsBackendFake>().Returns(true);
            osPlatform.CheckAudioBackendCompatability<AudioBackendFake>().Returns(true);
            osPlatform.CheckWindowCompatability<GameWindowFake>().Returns(true);
            osPlatform.CheckInputCompatability<InputSystemFake>().Returns(true);

            gameBuilder
                .WithGraphicsBackend<GraphicsBackendFake>()
                .WithAudioBackend<AudioBackendFake>()
                .WithWindow<GameWindowFake>()
                .WithInput<InputSystemFake>()
                .WithSubSystem<SubSystemFake>(SubSystemLifetime.Singleton);
            //Act
            Func<GameSystemFake> build = () => gameBuilder.BuildForPlatform();
            GameSystemFake gameFake = build();

            //Assert
            build.Should().NotThrow();           
        }
    }
}

using FluentAssertions;
using NSubstitute;
using Reload.Core.Exceptions;
using Reload.Core.Game;
using Reload.Core.Tests.Fakes;
using System;
using Xunit;

namespace Reload.Core.Tests
{
    public class GameBuilderTests
    {
        [Fact]
        public void WithWindow_OSNotCompatible_ThrowsReloadWindowBackendNotSupportedException()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            ProgramBuilder<GameSystemFake> gameBuilder = new ProgramBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckWindowCompatability<GameWindowFake>().Returns(false);

            //Act
            Func<ProgramBuilder<GameSystemFake>> act = () => gameBuilder.WithWindow<GameWindowFake>();

            //Assert
            act.Should().Throw<ReloadWindowBackendNotSupportedException>();
        }

        [Fact]
        public void WithWindow_OSCompatible_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            ProgramBuilder<GameSystemFake> gameBuilder = new ProgramBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckWindowCompatability<GameWindowFake>().Returns(true);

            //Act
            Func<ProgramBuilder<GameSystemFake>> act = () => gameBuilder.WithWindow<GameWindowFake>();

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<ProgramBuilder<GameSystemFake>>();
        }

        [Fact]
        public void WithGraphicsBackend_OSNotCompatible_ThrowsReloadGraphicsBackendNotSupportedException()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            ProgramBuilder<GameSystemFake> gameBuilder = new ProgramBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckGraphicsBackendCompatability<GraphicsAPIFake>().Returns(false);

            //Act
            Func<ProgramBuilder<GameSystemFake>> act = () => gameBuilder.WithGraphicsAPI<GraphicsAPIFake>();

            //Assert
            act.Should().Throw<ReloadGraphicsBackendNotSupportedException>();
        }

        [Fact]
        public void WithGraphicsBackend_OSCompatible_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            ProgramBuilder<GameSystemFake> gameBuilder = new ProgramBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckGraphicsBackendCompatability<GraphicsAPIFake>().Returns(true);

            //Act
            Func<ProgramBuilder<GameSystemFake>> act = () => gameBuilder.WithGraphicsAPI<GraphicsAPIFake>();

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<ProgramBuilder<GameSystemFake>>();
        }

        [Fact]
        public void WithAudioBackend_OSNotCompatible_ThrowsReloadAudioBackendNotSupportedException()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            ProgramBuilder<GameSystemFake> gameBuilder = new ProgramBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckAudioBackendCompatability<AudioAPIFake>().Returns(false);

            //Act
            Func<ProgramBuilder<GameSystemFake>> act = () => gameBuilder.WithAudioAPI<AudioAPIFake>();

            //Assert
            act.Should().Throw<ReloadAudioBackendNotSupportedException>();
        }

        [Fact]
        public void WithAudioBackend_OSCompatible_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            ProgramBuilder<GameSystemFake> gameBuilder = new ProgramBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckAudioBackendCompatability<AudioAPIFake>().Returns(true);

            //Act
            Func<ProgramBuilder<GameSystemFake>> act = () => gameBuilder.WithAudioAPI<AudioAPIFake>();

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<ProgramBuilder<GameSystemFake>>();
        }

        [Fact]
        public void WithInput_OSNotCompatible_ThrowsReloadInputNotSupportedException()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            ProgramBuilder<GameSystemFake> gameBuilder = new ProgramBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckInputCompatability<InputSystemFake>().Returns(false);

            //Act
            Func<ProgramBuilder<GameSystemFake>> act = () => gameBuilder.WithInput<InputSystemFake>();

            //Assert
            act.Should().Throw<ReloadInputNotSupportedException>();
        }

        [Fact]
        public void WithInput_OSCompatible_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            ProgramBuilder<GameSystemFake> gameBuilder = new ProgramBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckInputCompatability<InputSystemFake>().Returns(true);

            //Act
            Func<ProgramBuilder<GameSystemFake>> act = () => gameBuilder.WithInput<InputSystemFake>();

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<ProgramBuilder<GameSystemFake>>();
        }

        [Fact]
        public void WithSubSystem_InvalidLifetime_ThrowsReloadInvalidEnumValueException()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            ProgramBuilder<GameSystemFake> gameBuilder = new ProgramBuilder<GameSystemFake>(osPlatform);

            //Act
            Func<ProgramBuilder<GameSystemFake>> act = () => gameBuilder.WithSubSystem<SubSystemFake>(0);

            //Assert
            act.Should().Throw<ReloadInvalidEnumArgumentException>();
        }

        [Fact]
        public void WithSubSystem_SingletonLifetime_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            ProgramBuilder<GameSystemFake> gameBuilder = new ProgramBuilder<GameSystemFake>(osPlatform);

            //Act
            Func<ProgramBuilder<GameSystemFake>> act = () => gameBuilder.WithSubSystem<SubSystemFake>(Lifetime.Singleton);

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<ProgramBuilder<GameSystemFake>>();
        }

        [Fact]
        public void WithSubSystem_TransientLifetime_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            ProgramBuilder<GameSystemFake> gameBuilder = new ProgramBuilder<GameSystemFake>(osPlatform);

            //Act
            Func<ProgramBuilder<GameSystemFake>> act = () => gameBuilder.WithSubSystem<SubSystemFake>(Lifetime.Transient);

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<ProgramBuilder<GameSystemFake>>();
        }

        [Fact]
        public void WithSubSystem_ScopedLifetime_ReturnsGameBuilder()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            ProgramBuilder<GameSystemFake> gameBuilder = new ProgramBuilder<GameSystemFake>(osPlatform);

            //Act
            Func<ProgramBuilder<GameSystemFake>> act = () => gameBuilder.WithSubSystem<SubSystemFake>(Lifetime.Scoped);

            //Assert
            act.Should().NotThrow<Exception>();
            act().Should().BeOfType<ProgramBuilder<GameSystemFake>>();
        }

        [Fact]
        public void BuildForPlatform_WithSubSystems_ReturnsGameSystem()
        {
            // Arrange
            PlatformOS osPlatform = Substitute.For<PlatformOS>();
            ProgramBuilder<GameSystemFake> gameBuilder = new ProgramBuilder<GameSystemFake>(osPlatform);

            osPlatform.CheckGraphicsBackendCompatability<GraphicsAPIFake>().Returns(true);
            osPlatform.CheckAudioBackendCompatability<AudioAPIFake>().Returns(true);
            osPlatform.CheckWindowCompatability<GameWindowFake>().Returns(true);
            osPlatform.CheckInputCompatability<InputSystemFake>().Returns(true);

            gameBuilder
                .WithGraphicsAPI<GraphicsAPIFake>()
                .WithAudioAPI<AudioAPIFake>()
                .WithWindow<GameWindowFake>()
                .WithInput<InputSystemFake>()
                .WithSubSystem<SubSystemFake>(Lifetime.Singleton);
            //Act
            Func<GameSystemFake> build = () => gameBuilder.BuildForPlatform();
            GameSystemFake gameFake = build();

            //Assert
            build.Should().NotThrow();           
        }
    }
}

namespace DotNet.Testcontainers.Tests.Unit
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using DotNet.Testcontainers.Builders;
  using DotNet.Testcontainers.Commons;
  using DotNet.Testcontainers.Configurations.WaitStrategies;
  using DotNet.Testcontainers.Containers;
  using Xunit;

  public sealed class WaitStrategyOptionsTests
  {
    [Fact]
    public async Task ContainerIsRunning()
    {
      // Given
      await using var container = new ContainerBuilder()
        .WithImage(CommonImages.Alpine)
        .WithEntrypoint("/bin/sh", "-c")
        .WithCommand("echo \"Started\" | tee /dev/stderr && trap : TERM INT; sleep infinity & wait")
        .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged(
          "Started",
          options => options.WithRetries(3).WithTimeout(TimeSpan.FromSeconds(10))))
        .Build();

      using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(1));

      // When
      await container.StartAsync(cts.Token);

      // Then
      Assert.Equal(TestcontainersStates.Running, container.State);
    }

    [Fact]
    public async Task ContainerIsRunningAfterSuccessfulRetry()
    {
      // Given
      await using var container = new ContainerBuilder()
        .WithImage(CommonImages.Alpine)
        .WithEntrypoint("/bin/sh", "-c")
        .WithCommand("sleep 1.2 && echo \"Started\" | tee /dev/stderr && trap : TERM INT; sleep infinity & wait")
        .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged(
          "Started",
          options => options.WithRetries(3).WithTimeout(TimeSpan.FromSeconds(1)).WithDelayStrategy(DelayStrategy.Linear)))
        .Build();

      using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(1));

      // When
      await container.StartAsync(cts.Token);

      // Then
      Assert.Equal(TestcontainersStates.Running, container.State);
    }
  }
}

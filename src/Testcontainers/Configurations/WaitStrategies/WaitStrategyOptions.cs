namespace DotNet.Testcontainers.Configurations.WaitStrategies
{
  using System;

  public sealed class WaitStrategyOptions
  {
    internal int Retries { get; private set; }

    internal TimeSpan Timeout { get; private set; }

    internal BackoffStrategy BackoffStrategy { get; private set; }

    public WaitStrategyOptions WithRetries(int retries)
    {
      this.Retries = retries;
      return this;
    }

    public WaitStrategyOptions WithTimeout(TimeSpan timeout)
    {
      this.Timeout = timeout;
      return this;
    }

    public WaitStrategyOptions WithBackoffStrategy(BackoffStrategy backoffStrategy)
    {
      this.BackoffStrategy = backoffStrategy;
      return this;
    }
  }
}

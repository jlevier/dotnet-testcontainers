namespace DotNet.Testcontainers.Configurations.WaitStrategies
{
  using System;

  public sealed class WaitStrategyOptions
  {
    internal int? Retries { get; private set; }

    internal TimeSpan? BackoffPeriod { get; private set; }

    internal DelayStrategy DelayStrategy { get; private set; } = DelayStrategy.None;

    private TimeSpan? Timeout { get; set; }

    /// <summary>
    /// Set the number of retry attempts on the wait condition before a timeout occurs.
    /// </summary>
    /// <param name="retries">Number of retries.</param>
    /// <returns>Current wait strategy options builder.</returns>
    public WaitStrategyOptions WithRetries(int retries)
    {
      this.Retries = retries;
      return this;
    }

    /// <summary>
    /// Set the length of time before a timeout occurs and the container start attempt is abandoned.
    /// </summary>
    /// <param name="timeout">Timeout period.</param>
    /// <returns>Current wait strategy options builder.</returns>
    public WaitStrategyOptions WithTimeout(TimeSpan timeout)
    {
      this.Timeout = timeout;
      return this;
    }

    /// <summary>
    /// Sets the length of time the system will wait in between each retry attempt.
    /// </summary>
    /// <param name="backoffPeriod">Backoff period.</param>
    /// <returns>Current wait strategy options builder.</returns>
    public WaitStrategyOptions WithBackoffPeriod(TimeSpan backoffPeriod)
    {
      this.BackoffPeriod = backoffPeriod;
      return this;
    }

    /// <summary>
    /// Sets the delay strategy which will be used in conjunction with <see cref="WithRetries(int)"/> and <see cref="WithBackoffPeriod(TimeSpan)"/>.
    /// </summary>
    /// <param name="delayStrategy">Selected delay stragety to use.  Default is None.</param>
    /// <returns>Current wait strategy options builder.</returns>
    public WaitStrategyOptions WithDelayStrategy(DelayStrategy delayStrategy)
    {
      this.DelayStrategy = delayStrategy;
      return this;
    }

    internal int GetBackoff(int attempt)
    {
      if (this.DelayStrategy == DelayStrategy.None)
      {
        return 0;
      }

      var delayPeriod = this.BackoffPeriod ?? TimeSpan.FromSeconds(1);

      return delayPeriod.Milliseconds * (this.DelayStrategy == DelayStrategy.Linear ? 1 : attempt);
    }

    internal TimeSpan GetTimeout(int attempt)
    {
      if (this.Timeout == null)
      {
        return System.Threading.Timeout.InfiniteTimeSpan;
      }

      var delayMilliseconds = this.Timeout.Value.Seconds * 1000 * (this.DelayStrategy == DelayStrategy.Linear ? 1 : attempt);

      return TimeSpan.FromMilliseconds(delayMilliseconds);
    }
  }
}

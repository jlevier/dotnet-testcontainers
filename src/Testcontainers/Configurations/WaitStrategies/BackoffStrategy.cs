namespace DotNet.Testcontainers.Configurations.WaitStrategies
{
  /// <summary>
  /// Backoff strategy for throttling retry attempts in a <see cref="IWaitForContainerOS" />.
  /// </summary>
  public enum BackoffStrategy
  {
    /// <summary>
    /// Do not use a backoff strategy.
    /// </summary>
    None,

    /// <summary>
    /// Linear backoff strategy will wait the same amount of times in between retries.
    /// </summary>
    Linear,

    /// <summary>
    /// Exponential retry strategy will increase the amount of time awaited between retries.
    /// </summary>
    Exponential,
  }
}

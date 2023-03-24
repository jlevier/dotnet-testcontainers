namespace DotNet.Testcontainers.Configurations.WaitStrategies
{
  /// <summary>
  /// Backoff strategy for throttling retry attempts in a <see cref="WaitStrategyOptions" />.
  /// </summary>
  public enum DelayStrategy
  {
    /// <summary>
    /// Do not use a delay strategy.
    /// </summary>
    None,

    /// <summary>
    /// Linear delay strategy will wait the same amount of times in between attempts.
    /// </summary>
    Linear,

    /// <summary>
    /// Exponential delay strategy will increase the amount of time awaited between attempts.
    /// </summary>
    Exponential,
  }
}

namespace DotNet.Testcontainers.Configurations
{
  using DotNet.Testcontainers.Configurations.WaitStrategies;

  internal abstract class WaitUntilBase
  {
    public WaitStrategyOptions WaitStrategyOptions { get; private set; }

    protected void SetWaitStrategyOptions(WaitStrategyOptions options)
    {
      this.WaitStrategyOptions = options;
    }
  }
}

namespace DotNet.Testcontainers.Configurations
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Text.RegularExpressions;
  using DotNet.Testcontainers.Configurations.WaitStrategies;

  /// <inheritdoc cref="IWaitForContainerOS" />
  internal abstract class WaitForContainerOS : IWaitForContainerOS
  {
    private readonly ICollection<IWaitUntil> _waitStrategies = new List<IWaitUntil>();

    /// <summary>
    /// Initializes a new instance of the <see cref="WaitForContainerOS" /> class.
    /// </summary>
    protected WaitForContainerOS()
    {
      _waitStrategies.Add(new UntilContainerIsRunning());
    }

    /// <inheritdoc />
    public abstract IWaitForContainerOS UntilCommandIsCompleted(string command);

    /// <inheritdoc />
    public abstract IWaitForContainerOS UntilCommandIsCompleted(params string[] command);

    /// <inheritdoc />
    public abstract IWaitForContainerOS UntilPortIsAvailable(int port);

    /// <inheritdoc />
    public virtual IWaitForContainerOS AddCustomWaitStrategy(IWaitUntil waitStrategy)
    {
      _waitStrategies.Add(waitStrategy);
      return this;
    }

    /// <inheritdoc />
    public virtual IWaitForContainerOS UntilFileExists(string file)
    {
      return AddCustomWaitStrategy(new UntilFilesExists(file));
    }

    /// <inheritdoc />
    public IWaitForContainerOS UntilMessageIsLogged(string pattern)
    {
      return AddCustomWaitStrategy(new UntilMessageIsLogged(pattern));
    }

    /// <inheritdoc />
    public IWaitForContainerOS UntilMessageIsLogged(string pattern, Action<WaitStrategyOptions> options)
    {
      var waitStrategyOptions = new WaitStrategyOptions();
      options.Invoke(waitStrategyOptions);
      return this.AddCustomWaitStrategy(new UntilMessageIsLogged(pattern, waitStrategyOptions));
    }

    /// <inheritdoc />
    public IWaitForContainerOS UntilMessageIsLogged(Regex pattern)
    {
      return AddCustomWaitStrategy(new UntilMessageIsLogged(pattern));
    }

    /// <inheritdoc />
    public virtual IWaitForContainerOS UntilMessageIsLogged(Stream stream, string message)
    {
      return AddCustomWaitStrategy(new UntilMessageIsLogged(message));
    }

    /// <inheritdoc />
    public virtual IWaitForContainerOS UntilOperationIsSucceeded(Func<bool> operation, int maxCallCount)
    {
      return AddCustomWaitStrategy(new UntilOperationIsSucceeded(operation, maxCallCount));
    }

    /// <inheritdoc />
    public virtual IWaitForContainerOS UntilHttpRequestIsSucceeded(Func<HttpWaitStrategy, HttpWaitStrategy> request)
    {
      return AddCustomWaitStrategy(request.Invoke(new HttpWaitStrategy()));
    }

    /// <inheritdoc />
    public virtual IWaitForContainerOS UntilContainerIsHealthy(long failingStreak = 3)
    {
      return AddCustomWaitStrategy(new UntilContainerIsHealthy(failingStreak));
    }

    /// <inheritdoc />
    public IEnumerable<IWaitUntil> Build()
    {
      return _waitStrategies;
    }
  }
}

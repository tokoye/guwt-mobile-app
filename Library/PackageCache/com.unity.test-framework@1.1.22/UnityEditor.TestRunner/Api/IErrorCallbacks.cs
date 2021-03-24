<<<<<<< HEAD:Library/PackageCache/com.unity.test-framework@1.1.22/UnityEditor.TestRunner/Api/IErrorCallbacks.cs
namespace UnityEditor.TestTools.TestRunner.Api
{
    /// <summary>
    /// An extended version of the <see cref="ICallbacks"/>, which get invoked if the test run fails due to a build error or if any <see cref="UnityEngine.TestTools.IPrebuildSetup"/> has a failure.
    /// </summary>
    public interface IErrorCallbacks : ICallbacks
    {
        /// <summary>
        /// Method invoked on failure.
        /// </summary>
        /// <param name="message">
        /// The error message detailing the reason for the run to fail.
        /// </param>
        void OnError(string message);
    }
}
=======
namespace UnityEditor.TestTools.TestRunner.Api
{
    /// <summary>
    /// An extended version of the <see cref="ICallbacks"/>, which get invoked if the test run fails due to a build error or if any <see cref="UnityEngine.TestTools.IPrebuildSetup"/> has a failure.
    /// </summary>
    public interface IErrorCallbacks : ICallbacks
    {
        /// <summary>
        /// Method invoked on failure.
        /// </summary>
        /// <param name="message">
        /// The error message detailing the reason for the run to fail.
        /// </param>
        void OnError(string message);
    }
}
>>>>>>> 311c70c6976bc5ea11bafc9e95d3e0e4ee456de5:Library/PackageCache/com.unity.test-framework@1.1.20/UnityEditor.TestRunner/Api/IErrorCallbacks.cs

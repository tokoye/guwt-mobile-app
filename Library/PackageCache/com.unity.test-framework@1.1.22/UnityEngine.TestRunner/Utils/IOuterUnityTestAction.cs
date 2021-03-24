<<<<<<< HEAD:Library/PackageCache/com.unity.test-framework@1.1.22/UnityEngine.TestRunner/Utils/IOuterUnityTestAction.cs
using System.Collections;
using NUnit.Framework.Interfaces;

namespace UnityEngine.TestTools
{
    /// <summary>
    /// When implemented by an attribute, this interface implemented to provide actions to execute before setup and after teardown of tests.
    /// </summary>
    public interface IOuterUnityTestAction
    {
        /// <summary>Executed before each test is run</summary>
        /// <param name="test">The test that is going to be run.</param>
        /// <returns>Enumerable collection of actions to perform before test setup.</returns>
        IEnumerator BeforeTest(ITest test);

        /// <summary>Executed after each test is run</summary>
        /// <param name="test">The test that has just been run.</param>
        /// <returns>Enumerable collection of actions to perform after test teardown.</returns>
        IEnumerator AfterTest(ITest test);
    }
}
=======
using System.Collections;
using NUnit.Framework.Interfaces;

namespace UnityEngine.TestTools
{
    /// <summary>
    /// When implemented by an attribute, this interface implemented to provide actions to execute before setup and after teardown of tests.
    /// </summary>
    public interface IOuterUnityTestAction
    {
        /// <summary>Executed before each test is run</summary>
        /// <param name="test">The test that is going to be run.</param>
        /// <returns>Enumerable collection of actions to perform before test setup.</returns>
        IEnumerator BeforeTest(ITest test);

        /// <summary>Executed after each test is run</summary>
        /// <param name="test">The test that has just been run.</param>
        /// <returns>Enumerable collection of actions to perform after test teardown.</returns>
        IEnumerator AfterTest(ITest test);
    }
}
>>>>>>> 311c70c6976bc5ea11bafc9e95d3e0e4ee456de5:Library/PackageCache/com.unity.test-framework@1.1.20/UnityEngine.TestRunner/Utils/IOuterUnityTestAction.cs

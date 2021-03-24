<<<<<<< HEAD:Library/PackageCache/com.unity.test-framework@1.1.22/UnityEditor.TestRunner/UnityTestProtocol/IUtpMessageReporter.cs
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEditor.TestTools.TestRunner.Api;

namespace UnityEditor.TestTools.TestRunner.UnityTestProtocol
{
    internal interface IUtpMessageReporter
    {
        void ReportTestFinished(ITestResultAdaptor result);
        void ReportTestRunStarted(ITestAdaptor testsToRun);
        void ReportTestStarted(ITestAdaptor test);
    }
}
=======
using System.Collections.Generic;
using UnityEditor.Compilation;
using UnityEditor.TestTools.TestRunner.Api;

namespace UnityEditor.TestTools.TestRunner.UnityTestProtocol
{
    internal interface IUtpMessageReporter
    {
        void ReportTestFinished(ITestResultAdaptor result);
        void ReportTestRunStarted(ITestAdaptor testsToRun);
        void ReportTestStarted(ITestAdaptor test);
    }
}
>>>>>>> 311c70c6976bc5ea11bafc9e95d3e0e4ee456de5:Library/PackageCache/com.unity.test-framework@1.1.20/UnityEditor.TestRunner/UnityTestProtocol/IUtpMessageReporter.cs

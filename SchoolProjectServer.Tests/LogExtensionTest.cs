// <copyright file="LogExtensionTest.cs">Copyright ©  2017</copyright>
using System;
using CustomLog;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CustomLog.Tests
{
    /// <summary>This class contains parameterized unit tests for LogExtension</summary>
    [PexClass(typeof(LogExtension))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class LogExtensionTest
    {
        /// <summary>Test stub for Log(Object, String)</summary>
        [PexMethod]
        public void LogTest(object source, string message)
        {
            LogExtension.Log(source, message);
            // TODO: add assertions to method LogExtensionTest.LogTest(Object, String)
        }

        /// <summary>Test stub for Log(Object, String, LogLevels)</summary>
        [PexMethod]
        public void LogTest01(
            object source,
            string message,
            LogExtension.LogLevels level
        )
        {
            LogExtension.Log(source, message, level);
            // TODO: add assertions to method LogExtensionTest.LogTest01(Object, String, LogLevels)
        }

        /// <summary>Test stub for Log(Object, String, LogLevels, String)</summary>
        [PexMethod]
        public void LogTest02(
            object source,
            string message,
            LogExtension.LogLevels level,
            string logWindowID
        )
        {
            LogExtension.Log(source, message, level, logWindowID);
            // TODO: add assertions to method LogExtensionTest.LogTest02(Object, String, LogLevels, String)
        }
    }
}

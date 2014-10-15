// Copyright (c) 2014. Evgeny Nazarov
// http://guardian.codeplex.com/
// All rights reserved.
// 
// Redistribution and use in source and binary forms, 
// with or without modification, are permitted provided 
// that the following conditions are met:
// 
//     * Redistributions of source code must retain the 
//     above copyright notice, this list of conditions and 
//     the following disclaimer.
//     
//     * Redistributions in binary form must reproduce 
//     the above copyright notice, this list of conditions 
//     and the following disclaimer in the documentation 
//     and/or other materials provided with the distribution.
//     
//     * Neither the name of contributors may be used to endorse 
//     or promote products derived from this software 
//     without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
// CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
// MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR 
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF 
// SUCH DAMAGE.
// 
// [This is the BSD license, see http://www.opensource.org/licenses/bsd-license.php]
using NUnit.Framework;

namespace Guardian.Tests
{
    using System;
    using Guardian;
    using Guardian.Tests.Helpers;
    using Shouldly;

    [TestFixture]
    internal sealed class IsFixture
    {
        [Test, TestCaseSource(typeof(TestData), "GetNotNullObjects")]
        public void ShouldSucceedForArgumentIsNotNull(object referenceArg, string message)
        {
            // Given
            var argument = TestUtility.CreateArgument(() => referenceArg);

            // When
            var action = new TestDelegate(() => Is.NotNull(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetNullObjects")]
        public void ShoulFailForArgumentIsNotNull(object referenceArg, string message, string format)
        {
            // Given
            var expected = string.Format(format, "Provided parameter should not be null", "referenceArg");
            var argument = TestUtility.CreateArgument(() => referenceArg);

            // When
            var action = new TestDelegate(() => Is.NotNull(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentNullException>(action);
            exception.ParamName.ShouldBe("referenceArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetNotNullObjects")]
        public void ShouldSucceedForArgumentIsNotNullOrEmpty(string stringArg, string message)
        {
            // Given
            var argument = TestUtility.CreateArgument(() => stringArg);

            // When
            var action = new TestDelegate(() => Is.NotNullOrEmpty(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetNullObjects")]
        public void ShoulFailForArgumentIsNotNullOrEmptyWhenNull(string stringArg, string message, string format)
        {
            // Given
            var expected = string.Format(format, "Provided string should not be null", "stringArg");
            var argument = TestUtility.CreateArgument(() => stringArg);

            // When
            var action = new TestDelegate(() => Is.NotNullOrEmpty(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentNullException>(action);
            exception.ParamName.ShouldBe("stringArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetEmptyObjects")]
        public void ShoulFailForArgumentIsNotNullOrEmptyWhenEmpty(string stringArg, string message, string format)
        {
            // Given
            var expected = string.Format(format, "Provided string should not be empty", "stringArg") + Environment.NewLine + "Actual value was .";
            var argument = TestUtility.CreateArgument(() => stringArg);

            // When
            var action = new TestDelegate(() => Is.NotNullOrEmpty(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentOutOfRangeException>(action);
            exception.ParamName.ShouldBe("stringArg");
            exception.Message.ShouldBe(expected);
            exception.ActualValue.ShouldBe(string.Empty);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetNullObjects")]
        public void ShoulFailForArgumentIsNotNullOrWhiteSpaceWhenNull(string stringArg, string message, string format)
        {
            // Given
            var expected = string.Format(format, "Provided string should not be null", "stringArg");
            var argument = TestUtility.CreateArgument(() => stringArg);

            // When
            var action = new TestDelegate(() => Is.NotNullOrWhiteSpace(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentNullException>(action);
            exception.ParamName.ShouldBe("stringArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetEmptyObjects")]
        public void ShoulFailForArgumentIsNotNullOrWhiteSpaceWhenEmpty(string stringArg, string message, string format)
        {
            // Given
            var expected = string.Format(format, "Provided string should not be empty or white space", "stringArg") + Environment.NewLine + "Actual value was .";
            var argument = TestUtility.CreateArgument(() => stringArg);

            // When
            var action = new TestDelegate(() => Is.NotNullOrWhiteSpace(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentOutOfRangeException>(action);
            exception.ParamName.ShouldBe("stringArg");
            exception.Message.ShouldBe(expected);
            exception.ActualValue.ShouldBe(string.Empty);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetWhiteSpaceObjects")]
        public void ShoulFailForArgumentIsNotNullOrWhiteSpaceWhenWhiteSpace(string stringArg, string message, string format)
        {
            // Given
            var expected = string.Format(format, "Provided string should not be empty or white space", "stringArg") + Environment.NewLine + "Actual value was " + TestData.WhiteSpace + ".";
            var argument = TestUtility.CreateArgument(() => stringArg);

            // When
            var action = new TestDelegate(() => Is.NotNullOrWhiteSpace(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentOutOfRangeException>(action);
            exception.ParamName.ShouldBe("stringArg");
            exception.Message.ShouldBe(expected);
            exception.ActualValue.ShouldBe(TestData.WhiteSpace);
            exception.InnerException.ShouldBe(null);
        }
    }
}

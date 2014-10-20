// Copyright (c) 2014. Evgeny Nazarov
// http://guardly.codeplex.com/
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

namespace Guardly.Tests.Helpers
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    internal static class TestData
    {
        public const string Generic = "AB";
        public const string WhiteSpace = " ";

        private static IEnumerable<string> GetExtendedMessages()
        {
            yield return null;
            yield return string.Empty;
            yield return WhiteSpace;
            yield return "Extended error message";
        }

        public static IEnumerable<TestCaseData> GetNotNullObjects()
        {
            return GetExtendedMessages().Select(extendedMessage => new TestCaseData(Generic, extendedMessage));
        }

        public static IEnumerable<TestCaseData> GetNullObjects()
        {
            return GetExtendedMessages().Select(extendedMessage => new TestCaseData(null, extendedMessage, TestUtility.GetExpectedArgumentMessage(extendedMessage)));
        }

        public static IEnumerable<TestCaseData> GetEmptyObjects()
        {
            return GetExtendedMessages().Select(extendedMessage => new TestCaseData(string.Empty, extendedMessage, TestUtility.GetExpectedArgumentMessage(extendedMessage)));
        }

        public static IEnumerable<TestCaseData> GetWhiteSpaceObjects()
        {
            return GetExtendedMessages().Select(extendedMessage => new TestCaseData(WhiteSpace, extendedMessage, TestUtility.GetExpectedArgumentMessage(extendedMessage)));
        }

        public static IEnumerable<TestCaseData> GetRefArray()
        {
            return GetExtendedMessages().Select(extendedMessage => new TestCaseData(new object[0], extendedMessage));
        }

        public static IEnumerable<TestCaseData> GetRefArrayExt()
        {
            return GetExtendedMessages().Select(extendedMessage => new TestCaseData(new object[0], extendedMessage, TestUtility.GetExpectedArgumentMessage(extendedMessage)));
        }

        public static IEnumerable<TestCaseData> GetArrayNoError()
        {
            return GetExtendedMessages().Select(extendedMessage => new TestCaseData(new int[0], extendedMessage));
        }
        
        public static IEnumerable<TestCaseData> GetArrayWithError()
        {
            return GetExtendedMessages().Select(extendedMessage => new TestCaseData(new int[0], extendedMessage, TestUtility.GetExpectedArgumentMessage(extendedMessage)));
        }
    }
}
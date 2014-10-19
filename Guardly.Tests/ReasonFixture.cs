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
using NUnit.Framework;

namespace Guardly.Tests
{
    using Shouldly;

    [TestFixture]
    internal sealed class ReasonFixture
    {
        private const string Empty = "";
        private const string White = " ";
        private const string Single = "Single";
        private const string Plural = "Plural";

        [TestCase(-2, null, null, null)]
        [TestCase(-2, Empty, null, null)]
        [TestCase(-2, White, null, null)]
        [TestCase(-2, null, null, null)]
        [TestCase(-2, null, Empty, Empty)]
        [TestCase(-2, null, White, White)]
        [TestCase(-2, Single, Plural, Plural)]
        [TestCase(-1, null, null, null)]
        [TestCase(-1, Empty, null, null)]
        [TestCase(-1, White, null, null)]
        [TestCase(-1, null, null, null)]
        [TestCase(-1, null, Empty, Empty)]
        [TestCase(-1, null, White, White)]
        [TestCase(-1, Single, Plural, Plural)]
        [TestCase(0, null, null, null)]
        [TestCase(0, Empty, null, null)]
        [TestCase(0, White, null, null)]
        [TestCase(0, null, null, null)]
        [TestCase(0, null, Empty, Empty)]
        [TestCase(0, null, White, White)]
        [TestCase(0, Single, Plural, Plural)]
        [TestCase(1, null, null, null)]
        [TestCase(1, Empty, null, Empty)]
        [TestCase(1, White, null, White)]
        [TestCase(1, Single, Plural, Single)]
        [TestCase(1, null, null, null)]
        [TestCase(1, null, Empty, null)]
        [TestCase(1, null, White, null)]
        [TestCase(1, null, Single, null)]
        [TestCase(2, null, null, null)]
        [TestCase(2, Empty, null, null)]
        [TestCase(2, White, null, null)]
        [TestCase(2, null, null, null)]
        [TestCase(2, null, Empty, Empty)]
        [TestCase(2, null, White, White)]
        [TestCase(2, Single, Plural, Plural)]
        public void ShouldPluralize(int count, string singular, string plural, string expected)
        {
            // When
            var result = count.Pluralize(singular, expected);

            // Then
            result.ShouldBe(expected);
        }

        [TestCase(null, null, ".")]
        [TestCase(null, Empty, ".")]
        [TestCase(null, White, ".")]
        [TestCase(null, "DEF", "DEF.\r\n.")]
        [TestCase(Empty, null, ".")]
        [TestCase(Empty, Empty, ".")]
        [TestCase(Empty, White, ".")]
        [TestCase(Empty, "DEF", "DEF.\r\n.")]
        [TestCase(White, null, " .")]
        [TestCase(White, Empty, " .")]
        [TestCase(White, White, " .")]
        [TestCase(White, "DEF", "DEF.\r\n .")]
        [TestCase("ABC", null, "ABC.")]
        [TestCase("ABC", Empty, "ABC.")]
        [TestCase("ABC", White, "ABC.")]
        [TestCase("ABC", "DEF", "DEF.\r\nABC.")]
        public void ShouldCompose(string baseMessage, string extendedMessage, string expected)
        {
            // When
            var result = Reason.Compose(baseMessage, extendedMessage);

            // Then
            result.ShouldNotBe(null);
            result.ToString().ShouldBe(expected);
        }
    }
}

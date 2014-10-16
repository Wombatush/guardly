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
    using System;
    using System.Collections.Generic;
    using Moq;
    using Shouldly;

    [TestFixture]
    internal sealed class GuardBaseFixture
    {
        private const int HashCodeOne = 123456;
        private const int HashCodeTwo = 654321;

        [Test]
        [TestCase(HashCodeOne)]
        [TestCase(HashCodeTwo)]
        public void ShouldGetHashCode(int hashCode)
        {
            // Given
            var mock = new  Mock<GuardBase>(hashCode);
            var instance = mock.Object;

            // When
            var result = instance.GetHashCode();

            // Then
            result.ShouldBe(hashCode);
        }

        [Test]
        public void ShouldNotBeEqualToNotGuardBase()
        {
            // Given
            var mock = new Mock<GuardBase>(HashCodeOne);
            var instance = mock.Object;
            var other = new object();

            // When
            var result = instance.Equals(other);

            // Then
            result.ShouldBe(false);
        }

        [Test]
        public void ShouldNotBeEqualToNull()
        {
            // Given
            var mock = new Mock<GuardBase>(HashCodeOne);
            var instance = mock.Object;

            // When
            var result = instance.Equals(null);

            // Then
            result.ShouldBe(false);
        }

        [Test]
        public void ShouldBeEqualBySameHashCode()
        {
            // Given
            var mockOne = new Mock<GuardBase>(HashCodeOne);
            var mockTwo = new Mock<GuardBase>(HashCodeOne);
            var instanceOne = mockOne.Object;
            var instanceTwo = mockTwo.Object;

            // When
            var result = instanceOne.Equals(instanceTwo);

            // Then
            result.ShouldBe(true);
        }

        [Test]
        public void ShouldBeEqualToSelf()
        {
            // Given
            var mock = new Mock<GuardBase>(HashCodeOne);
            var instance = mock.Object;
            
            // When
            var result = instance.Equals(instance);

            // Then
            result.ShouldBe(true);
        }

        [Test]
        public void ShouldNotBeEqualWheDifferentHashCode()
        {
            // Given
            var mockOne = new Mock<GuardBase>(HashCodeOne);
            var mockTwo = new Mock<GuardBase>(HashCodeTwo);
            var instanceOne = mockOne.Object;
            var instanceTwo = mockTwo.Object;

            // When
            var result = instanceOne.Equals(instanceTwo);

            // Then
            result.ShouldBe(false);
        }

        [Test]
        public void ShouldNotBeEqualWhenDifferentTypes()
        {
            // Given
            var mockOne = new Mock<GuardBase<float>>(HashCodeOne, new Func<float>(() => default(float)));
            var mockTwo = new Mock<GuardBase<double>>(HashCodeOne, new Func<double>(() => default(double)));
            var instanceOne = mockOne.Object;
            var instanceTwo = mockTwo.Object;

            // When
            var result = instanceOne.Equals(instanceTwo);

            // Then
            result.ShouldBe(false);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void ShouldUseProvidedGetterToRetrieveValue(int count)
        {
            // Given
            var stub = new List<int>();
            var invocations = 0;
            var mock = new Mock<GuardBase<int>>(HashCodeOne, new Func<int>(() => ++invocations));
            var instance = mock.Object;
            
            // When
            for (var i = 0; i < count; i++)
            {
                stub.Add(instance.Value);
            }
            
            // Then
            invocations.ShouldBe(count);
        }
    }
}

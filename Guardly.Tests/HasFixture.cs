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
    using Guardly.Tests.Helpers;
    using Shouldly;

    [TestFixture]
    internal sealed class HasFixture
    {
        #region Has.NoNulls

        [Test, TestCaseSource(typeof(TestData), "GetRefArray")]
        public void ShouldSucceedForArgumentHasNoNullsWhenSequenceHasNoItems(object[] arrayArg, string message)
        {
            // Given
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.NoNulls(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetRefArray")]
        public void ShouldSucceedForArgumentHasNoNullsWhenSequenceHasOneItem(object[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { new object() };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.NoNulls(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetRefArray")]
        public void ShouldSucceedForArgumentHasNoNullsWhenSequenceHasNoDuplicates(object[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { new object(), new object(), new object() };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.NoNulls(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetRefArrayExt")]
        public void ShoulFailForArgumentHasNoNullsWhenSequenceHasNulls(object[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { new object(), null, new object() };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided enumerable parameter should not have null(s), but had null at index 1", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.NoNulls(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        #endregion Has.NoNulls

        #region Has.NoDuplicates

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasNoDuplicatesWhenSequenceHasNoItems(int[] arrayArg, string message)
        {
            // Given
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.NoDuplicates(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasNoDuplicatesWhenSequenceHasOneItem(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.NoDuplicates(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasNoDuplicatesWhenSequenceHasNoDuplicates(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1, 2, 3 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.NoDuplicates(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasNoDuplicatesWhenSequenceHasDuplicates(int[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { 1, 2, 3, 1, 4, 5 };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided enumerable parameter should not have duplicate elements, but has same elements at index 0 and index 3", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.NoDuplicates(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        #endregion Has.NoDuplicates

        #region Has.LengthEqualTo

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthEqualToWhenArrayHasNoItems(int[] arrayArg, string message)
        {
            // Given
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthEqualTo<int>(() => 0)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthEqualToWhenArrayHasOneItem(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthEqualTo<int>(() => 1)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthEqualToWhenArrayHasManyItems(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1, 2, 3 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthEqualTo<int>(() => 3)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthEqualToWhenArrayHasMoreItems(int[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { 1, 2, 3, 1, 4, 5 };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have 1 element, but has 6 elements", "arrayArg");
            
            // When
            var action = new TestDelegate(() => Has.LengthEqualTo<int>(() => 1)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthEqualToWhenArrayHasLessItems(int[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { 1 };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have 6 elements, but has 1 element", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthEqualTo<int>(() => 6)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        #endregion Has.LengthEqualTo

        #region Has.LengthLessThan

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthLessThanWhenArrayHasNoItems(int[] arrayArg, string message)
        {
            // Given
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthLessThan<int>(() => 1)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthLessThanWhenArrayHasOneItem(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthLessThan<int>(() => 2)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthLessThanWhenArrayHasManyItems(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1, 2, 3 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthLessThan<int>(() => 4)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthLessThanWhenArrayHasMoreItemsSingular(int[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { 1, 2, 3, 1, 4, 5 };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have less than 1 element, but has 6 elements", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthLessThan<int>(() => 1)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthLessThanWhenArrayHasMoreItemsPlural(int[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { 1, 2, 3, 1, 4, 5 };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have less than 2 elements, but has 6 elements", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthLessThan<int>(() => 2)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthLessThanWhenArrayHasEqualNumberOfItemsPlural(int[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { 1, 2, 3, 1, 4, 5 };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have less than 6 elements, but has 6 elements", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthLessThan<int>(() => 6)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthLessThanWhenArrayHasEqualNumberOfItemsSingular(int[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { 1 };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have less than 1 element, but has 1 element", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthLessThan<int>(() => 1)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        #endregion Has.LengthLessThan

        #region Has.LengthGreaterThan

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthGreaterThanWhenArrayHasOneItem(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthGreaterThan<int>(() => 0)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthGreaterThanWhenArrayHasManyItems(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1, 2, 3 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthGreaterThan<int>(() => 2)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthGreaterThanWhenArrayHasNoItems(int[] arrayArg, string message, string format)
        {
            // Given
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have greater than 0 elements, but has 0 elements", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthGreaterThan<int>(() => 0)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthGreaterThanWhenArrayHasLessItemsSingular(int[] arrayArg, string message, string format)
        {
            // Given
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have greater than 1 element, but has 0 elements", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthGreaterThan<int>(() => 1)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthGreaterThanWhenArrayHasLessItemsPlural(int[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { 1 };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have greater than 2 elements, but has 1 element", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthGreaterThan<int>(() => 2)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthGreaterThanWhenArrayHasEqualNumberOfItemsPlural(int[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { 1, 2, 3, 1, 4, 5 };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have greater than 6 elements, but has 6 elements", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthGreaterThan<int>(() => 6)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthGreaterThanWhenArrayHasEqualNumberOfItemsSingular(int[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { 1 };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have greater than 1 element, but has 1 element", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthGreaterThan<int>(() => 1)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        #endregion Has.LengthGreaterThan

        #region Has.LengthLessThanOrEqualTo

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthLessThanOrEqualToWhenArrayHasNoItems(int[] arrayArg, string message)
        {
            // Given
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthLessThanOrEqualTo<int>(() => 1)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthLessThanOrEqualToWhenArrayHasOneItem(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthLessThanOrEqualTo<int>(() => 2)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthLessThanOrEqualToWhenArrayHasManyItems(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1, 2, 3 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthLessThanOrEqualTo<int>(() => 3)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthLessThanOrEqualToWhenArrayHasEqualNumberOfItemsl(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1, 2, 3, 1, 4, 5 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthLessThanOrEqualTo<int>(() => 6)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthLessThanOrEqualToWhenArrayHasMoreItemsSingular(int[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { 1, 2, 3, 1, 4, 5 };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have less than or equal to 1 element, but has 6 elements", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthLessThanOrEqualTo<int>(() => 1)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthLessThanOrEqualToWhenArrayHasMoreItemsPlural(int[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { 1, 2, 3, 1, 4, 5 };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have less than or equal to 2 elements, but has 6 elements", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthLessThanOrEqualTo<int>(() => 2)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        #endregion Has.LengthLessThanOrEqualTo

        #region Has.LengthGreaterThanOrEqualTo

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthGreaterThanOrEqualToWhenArrayHasOneItem(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthGreaterThanOrEqualTo<int>(() => 0)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthGreaterThanOrEqualToWhenArrayHasManyItems(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1, 2, 3 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthGreaterThanOrEqualTo<int>(() => 2)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayNoError")]
        public void ShouldSucceedForArgumentHasLengthGreaterThanOrEqualToWhenArrayHasEqualNumberOfItemsPlural(int[] arrayArg, string message)
        {
            // Given
            arrayArg = new[] { 1, 2, 3, 1, 4, 5 };
            var argument = TestUtility.CreateArgument(() => arrayArg);

            // When
            var action = new TestDelegate(() => Has.LengthGreaterThanOrEqualTo<int>(() => 6)(argument, message));

            // Then
            Assert.DoesNotThrow(action);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthGreaterThanOrEqualToWhenArrayHasNoItems(int[] arrayArg, string message, string format)
        {
            // Given
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have greater than or equal to 1 element, but has 0 elements", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthGreaterThanOrEqualTo<int>(() => 1)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthGreaterThanOrEqualToWhenArrayHasLessItemsSingular(int[] arrayArg, string message, string format)
        {
            // Given
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have greater than or equal to 1 element, but has 0 elements", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthGreaterThanOrEqualTo<int>(() => 1)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        [Test, TestCaseSource(typeof(TestData), "GetArrayWithError")]
        public void ShouldFailForArgumentHasLengthGreaterThanOrEqualToWhenArrayHasLessItemsPlural(int[] arrayArg, string message, string format)
        {
            // Given
            arrayArg = new[] { 1 };
            var argument = TestUtility.CreateArgument(() => arrayArg);
            var expected = string.Format(format, "Provided array parameter should have greater than or equal to 2 elements, but has 1 element", "arrayArg");

            // When
            var action = new TestDelegate(() => Has.LengthGreaterThanOrEqualTo<int>(() => 2)(argument, message));

            // Then
            var exception = Assert.Throws<ArgumentException>(action);
            exception.ParamName.ShouldBe("arrayArg");
            exception.Message.ShouldBe(expected);
            exception.InnerException.ShouldBe(null);
        }

        #endregion Has.LengthGreaterThanOrEqualTo
    }
}

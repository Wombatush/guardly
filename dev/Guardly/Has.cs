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

namespace Guardly
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a class whose assessments start with "Has".
    /// </summary>
#if !DEBUG
    [System.Diagnostics.DebuggerNonUserCode]
#endif
    public static class Has
    {
        #region Arguments
        
        /// <summary>
        /// Perform assessment against nulls in sequence for the argument of enumerable type.
        /// </summary>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <param name="argument">Argument wrapper.</param>
        /// <param name="message">Custom message to be displayed when assessment fails.</param>
        /// <exception cref="ArgumentException">Argument sequence contains null.</exception>
        /// <remarks>This methods uses <see cref="Object.ReferenceEquals"/> for assessment.</remarks>
        public static void NoNulls<T>(Argument<T> argument, string message)
            where T : IEnumerable<object>
        {
            var index = 0;
            var value = argument.Value;

            foreach (var item in value)
            {
                if (ReferenceEquals(item, null))
                {
                    var formatted = string.Format("Provided enumerable parameter should not have null(s), but had null at index {0}", index);
                    var reason = Reason.Compose(formatted, message).ToString();

                    throw new ArgumentException(reason, argument.Name);
                }

                ++index;
            }
        }

        /// <summary>
        /// Perform assessment against duplicates in sequence for the argument of enumerable type.
        /// </summary>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <param name="argument">Argument wrapper.</param>
        /// <param name="message">Custom message to be displayed when assessment fails.</param>
        /// <exception cref="ArgumentException">Argument sequence contains duplicates.</exception>
        /// <remarks>This methods uses <see cref="EqualityComparer{Object}.Default"/> for comparison.</remarks>
        public static void NoDuplicates<T>(Argument<T> argument, string message)
            where T : IEnumerable
        {
            var value = argument.Value;
            var outerEnumerator = value.GetEnumerator();
            if (outerEnumerator.MoveNext())
            {
                var comparer = EqualityComparer<object>.Default;
                for (var outer = 1; outerEnumerator.MoveNext(); ++outer)
                {
                    var outerValue = outerEnumerator.Current;
                    var innerEnumerator = value.GetEnumerator();
                    for (var inner = 0; inner < outer && innerEnumerator.MoveNext(); ++inner)
                    {
                        var innerValue = innerEnumerator.Current;
                        if (comparer.Equals(innerValue, outerValue))
                        {
                            var formatted = string.Format("Provided enumerable parameter should not have duplicate elements, but has same elements at index {0} and index {1}", inner, outer);
                            var reason = Reason.Compose(formatted, message).ToString();

                            throw new ArgumentException(reason, argument.Name);
                        }
                    }
                }
                
            }
        }
        
        /// <summary>
        /// Perform assessment against number of items in array for the argument of array type, which should have a length equal to provided value.
        /// </summary>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <param name="expression">Expression to retrieve an expected length value.</param>
        /// <returns>An argument assessment delegate used by <see cref="Guard"/>.</returns>
        public static ArgumentAssessment<T[]> LengthEqualTo<T>(Func<int> expression)
        {
            return (argument, message) =>
            {
                var value = argument.Value;
                var actual = value.Length;
                var expected = expression();
                if (actual != expected)
                {
                    var formatted = string.Format("Provided array parameter should have {0} {1}, but has {2} {3}", 
                        expected, expected.Pluralize("element", "elements"),
                        actual, actual.Pluralize("element", "elements"));
                    var reason = Reason.Compose(formatted, message).ToString();

                    throw new ArgumentException(reason, argument.Name);
                }
            };
        }

        /// <summary>
        /// Perform assessment against number of items in array for the argument of array type, which should have a length less than provided value.
        /// </summary>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <param name="expression">Expression to retrieve an expected length value.</param>
        /// <returns>An argument assessment delegate used by <see cref="Guard"/>.</returns>
        public static ArgumentAssessment<T[]> LengthLessThan<T>(Func<int> expression)
        {
            return (argument, message) =>
            {
                var value = argument.Value;
                var actual = value.Length;
                var expected = expression();
                if (actual >= expected)
                {
                    var formatted = string.Format("Provided array parameter should have less than {0} {1}, but has {2} {3}",
                        expected, expected.Pluralize("element", "elements"),
                        actual, actual.Pluralize("element", "elements"));
                    var reason = Reason.Compose(formatted, message).ToString();

                    throw new ArgumentException(reason, argument.Name);
                }
            };
        }

        /// <summary>
        /// Perform assessment against number of items in array for the argument of array type, which should have a length greater than provided value.
        /// </summary>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <param name="expression">Expression to retrieve an expected length value.</param>
        /// <returns>An argument assessment delegate used by <see cref="Guard"/>.</returns>
        public static ArgumentAssessment<T[]> LengthGreaterThan<T>(Func<int> expression)
        {
            return (argument, message) =>
            {
                var value = argument.Value;
                var actual = value.Length;
                var expected = expression();
                if (actual <= expected)
                {
                    var formatted = string.Format("Provided array parameter should have greater than {0} {1}, but has {2} {3}",
                        expected, expected.Pluralize("element", "elements"),
                        actual, actual.Pluralize("element", "elements"));
                    var reason = Reason.Compose(formatted, message).ToString();

                    throw new ArgumentException(reason, argument.Name);
                }
            };
        }

        /// <summary>
        /// Perform assessment against number of items in array for the argument of array type, which should have a length less than or equal to provided value.
        /// </summary>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <param name="expression">Expression to retrieve an expected length value.</param>
        /// <returns>An argument assessment delegate used by <see cref="Guard"/>.</returns>
        public static ArgumentAssessment<T[]> LengthLessThanOrEqualTo<T>(Func<int> expression)
        {
            return (argument, message) =>
            {
                var value = argument.Value;
                var actual = value.Length;
                var expected = expression();
                if (actual > expected)
                {
                    var formatted = string.Format("Provided array parameter should have less than or equal to {0} {1}, but has {2} {3}",
                        expected, expected.Pluralize("element", "elements"),
                        actual, actual.Pluralize("element", "elements"));
                    var reason = Reason.Compose(formatted, message).ToString();

                    throw new ArgumentException(reason, argument.Name);
                }
            };
        }

        /// <summary>
        /// Perform assessment against number of items in array for the argument of array type, which should have a length greater than or equal to provided value.
        /// </summary>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <param name="expression">Expression to retrieve an expected length value.</param>
        /// <returns>An argument assessment delegate used by <see cref="Guard"/>.</returns>
        public static ArgumentAssessment<T[]> LengthGreaterThanOrEqualTo<T>(Func<int> expression)
        {
            return (argument, message) =>
            {
                var value = argument.Value;
                var actual = value.Length;
                var expected = expression();
                if (actual < expected)
                {
                    var formatted = string.Format("Provided array parameter should have greater than or equal to {0} {1}, but has {2} {3}",
                        expected, expected.Pluralize("element", "elements"),
                        actual, actual.Pluralize("element", "elements"));
                    var reason = Reason.Compose(formatted, message).ToString();

                    throw new ArgumentException(reason, argument.Name);
                }
            };
        }

        //// public static ArgumentAssessment<IEnumerable<T>> CountEqualTo<T>(Func<int> expression)
        //// {
        ////     return (argument, message) =>
        ////     {
        ////         var count = 0;
        ////         var expected = expression();
        ////         var value = argument.Value;
        ////         if (value.Any(_ => ++count > expected) || count != expected)
        ////         {
        ////             var formatted = string.Format("Provided enumerable parameter should have {0} {1}, but had {2}",
        ////                 expected, expected.Pluralize("element", "elements"),
        ////                 count > expected ? "more" : string.Format("{0} {1}", count, count.Pluralize("element", "elements")));
        ////             var reason = Reason.Compose(formatted, message).ToString();
        //// 
        ////             throw new ArgumentOutOfRangeException(argument.Name, reason);
        ////         }
        ////     };
        //// }

        //// public static ArgumentAssessment<IEnumerable<T>> CountLessThan<T>(Func<int> expression)
        //// {
        ////     return (argument, message) =>
        ////     {
        ////         var count = 0;
        ////         var expected = expression();
        ////         var value = argument.Value;
        ////         if (value.Any(_ => ++count >= expected))
        ////         {
        ////             var formatted = string.Format("Provided enumerable parameter should have less than {0} {1}, but had more", expected, expected.Pluralize("element", "elements"));
        ////             var reason = Reason.Compose(formatted, message).ToString();
        //// 
        ////             throw new ArgumentOutOfRangeException(argument.Name, reason);
        ////         }
        ////     };
        //// }

        //// public static ArgumentAssessment<IEnumerable<T>> CountGreaterThan<T>(Func<int> expression)
        //// {
        ////     return (argument, message) =>
        ////     {
        ////         var count = 0;
        ////         var expected = expression();
        ////         var value = argument.Value;
        ////         if (!value.Any(_ => ++count > expected))
        ////         {
        ////             var formatted = string.Format("Provided enumerable parameter should have more than {0} {1}, but had more", expected, expected.Pluralize("element", "elements"));
        ////             var reason = Reason.Compose(formatted, message).ToString();
        //// 
        ////             throw new ArgumentOutOfRangeException(argument.Name, reason);
        ////         }
        ////     };
        //// }
        
        //// public static ArgumentAssessment<IEnumerable<T>> NoIntersectionWith<T>(Func<IEnumerable<T>> expression)
        //// {
        ////     return (argument, message) =>
        ////     {
        ////         var value = argument.Value;
        ////         var other = expression().ToArray();
        ////         foreach (var item in value)
        ////         {
        ////             if (other.Contains(item))
        ////             {
        ////                 throw new ArgumentOutOfRangeException(argument.Name, item, "");
        ////             }
        ////         }
        ////     };
        //// }
        
        #endregion Arguments
    }
}
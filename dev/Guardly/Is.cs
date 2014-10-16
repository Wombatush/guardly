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
    using System.Diagnostics;

    /// <summary>
    /// Represents a class whose assessments start with "Is".
    /// </summary>
    [DebuggerNonUserCode]
    public static class Is
    {
        #region Arguments

        /// <summary>
        /// Performs not-null assessment for the argument of reference type.
        /// </summary>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <param name="argument">Argument wrapper.</param>
        /// <param name="message">Custom message to be displayed when assessment fails.</param>
        /// <exception cref="ArgumentNullException">Argument value is null.</exception>
        public static void NotNull<T>(Argument<T> argument, string message)
            where T : class 
        {
            if (ReferenceEquals(argument.Value, null))
            {
                var reason = Reason.Compose("Provided parameter should not be null", message);

                throw new ArgumentNullException(argument.Name, reason.ToString());
            }
        }

        //// public static void NotEmpty<T>(Argument<T> argument, string message)
        ////     where T : IEnumerable<object>
        //// {
        ////     if (!argument.Value.Any())
        ////     {
        ////         var reason = Reason.Compose("Provided parameter should not be an empty sequence", message).ToString();
        ////         
        ////         throw new ArgumentOutOfRangeException(argument.Name, reason.ToString());
        ////     }
        //// }

        /// <summary>
        /// Performs not-null and not-empty assessment for the argument of string type.
        /// </summary>
        /// <param name="argument">Argument wrapper.</param>
        /// <param name="message">Custom message to be displayed when assessment fails.</param>
        /// <exception cref="ArgumentNullException">Argument string is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Argument string is empty.</exception>
        public static void NotNullOrEmpty(Argument<string> argument, string message)
        {
            var value = argument.Value;
            if (value == null)
            {
                var reason = Reason.Compose("Provided string should not be null", message);

                throw new ArgumentNullException(argument.Name, reason.ToString());
            }
            
            if (value == string.Empty)
            {
                var reason = Reason.Compose("Provided string should not be empty", message);

                throw new ArgumentOutOfRangeException(argument.Name, value, reason.ToString());
            }
        }

        /// <summary>
        /// Performs not-null, not-empty and not-white space assessment for the argument of string type.
        /// </summary>
        /// <param name="argument">Argument wrapper.</param>
        /// <param name="message">Custom message to be displayed when assessment fails.</param>
        /// <exception cref="ArgumentNullException">Argument string is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Argument string is empty or white space.</exception>
        public static void NotNullOrWhiteSpace(Argument<string> argument, string message)
        {
            var value = argument.Value;
            if (value == null)
            {
                var reason = Reason.Compose("Provided string should not be null", message);

                throw new ArgumentNullException(argument.Name, reason.ToString());
            }

            if (value.Trim() == string.Empty)
            {
                var reason = Reason.Compose("Provided string should not be empty or white space", message);

                throw new ArgumentOutOfRangeException(argument.Name, value, reason.ToString());
            }
        }
        
        //// public static ArgumentAssessment<T> LessThan<T>(Func<T> expression)
        ////     where T : IComparable<T>
        //// {
        ////     return (argument, message) =>
        ////     {
        ////         var value = argument.Value;
        ////         var comparand = expression();
        ////         if (value.CompareTo(comparand) > -1)
        ////         {
        ////             var generic = string.Format("Provided parameter does not fit to acceptable range: '{0}'<'{1}'", value, comparand);
        ////             var reason = Reason.Compose(generic, message);
        ////             
        ////             throw new ArgumentOutOfRangeException(argument.Name, value, reason.ToString());
        ////         }
        ////     };
        //// }
        //// 
        //// public static ArgumentAssessment<T> LessThanOrEqualTo<T>(Func<T> expression)
        ////     where T : IComparable<T>
        //// {
        ////     return (argument, message) =>
        ////     {
        ////         var value = argument.Value;
        ////         var comparand = expression();
        ////         if (value.CompareTo(comparand) > 0)
        ////         {
        ////             var generic = string.Format("Provided parameter does not fit to acceptable range: '{0}'≤'{1}'", value, comparand);
        ////             var reason = Reason.Compose(generic, message).ToString();
        ////             
        ////             Details.GuardArgumentFailure(ThisType, reason);
        //// 
        ////             throw new ArgumentOutOfRangeException(argument.Name, value, reason);
        ////         }
        ////     };
        //// }
        //// 
        //// public static ArgumentAssessment<T> GreaterThan<T>(Func<T> expression)
        ////     where T : IComparable<T>
        //// {
        ////     return (argument, message) =>
        ////     {
        ////         var value = argument.Value;
        ////         var comparand = expression();
        ////         if (value.CompareTo(comparand) < 0)
        ////         {
        ////             var generic = string.Format("Provided parameter does not fit to acceptable range: '{0}'>'{1}'", value, comparand);
        ////             var reason = Reason.Compose(generic, message);
        ////             
        ////             throw new ArgumentOutOfRangeException(argument.Name, value, reason.ToString());
        ////         }
        ////     };
        //// }
        //// 
        //// public static ArgumentAssessment<T> GreaterThanOrEqualTo<T>(Func<T> expression)
        ////     where T : IComparable<T>
        //// {
        ////     return (argument, message) =>
        ////     {
        ////         var value = argument.Value;
        ////         var comparand = expression();
        ////         if (value.CompareTo(comparand) < 1)
        ////         {
        ////             var generic = string.Format("Provided parameter does not fit to acceptable range: '{0}'≥'{1}'", value, comparand);
        ////             var reason = Reason.Compose(generic, message);
        //// 
        ////             throw new ArgumentOutOfRangeException(argument.Name, value, reason.ToString());
        ////         }
        ////     };
        //// }
        //// 
        //// public static ArgumentAssessment<T> In<T>(Func<IEnumerable<T>> expression)
        //// {
        ////     throw new NotImplementedException();
        //// }
        //// 
        //// public static ArgumentAssessment<T> NotIn<T>(Func<IEnumerable<T>> expression)
        //// {
        ////     throw new NotImplementedException();
        //// }
        
        #endregion Arguments
        
        //// #region Asserts
        //// 
        //// public static void Null<T>(Assert<T> argument, string message)
        ////     where T : class
        //// {
        ////     throw new NotImplementedException();
        //// }
        //// 
        //// public static void NotNull<T>(Assert<T> argument, string message)
        ////     where T : class
        //// {
        ////     throw new NotImplementedException();
        //// }
        //// 
        //// public static void NullOrEmpty<T>(Assert<string> argument, string message)
        ////     where T : class
        //// {
        ////     throw new NotImplementedException();
        //// }
        //// 
        //// public static void NotNullOrEmpty<T>(Assert<string> argument, string message)
        ////     where T : class
        //// {
        ////     throw new NotImplementedException();
        //// }
        //// 
        //// public static void NullOrWhiteSpace<T>(Assert<string> argument, string message)
        ////     where T : class
        //// {
        ////     throw new NotImplementedException();
        //// }
        //// 
        //// public static void NotNullOrWhiteSpace<T>(Assert<string> argument, string message)
        ////     where T : class
        //// {
        ////     throw new NotImplementedException();
        //// }
        //// 
        //// public static void True(Assert<bool> argument, string message)
        //// {
        ////     throw new NotImplementedException();
        //// }
        //// 
        //// public static void False(Assert<bool> argument, string message)
        //// {
        ////     throw new NotImplementedException();
        //// }
        //// 
        //// public static void HasValue<T>(Assert<T?> argument, string message)
        ////     where T : struct
        //// {
        ////     throw new NotImplementedException();
        //// }
        //// 
        //// public static void ValueCreated<T>(Assert<Lazy<T>> argument, string message)
        ////     where T : struct
        //// {
        ////     throw new NotImplementedException();
        //// }
        //// 
        //// #endregion Asserts
    }
}
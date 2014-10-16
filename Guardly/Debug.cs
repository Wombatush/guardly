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
    using System.Linq.Expressions;

    /// <summary>
    /// Guard represents the ways to verify argument preconditions and asserts for debug configuration only.
    /// </summary>
    [DebuggerNonUserCode]
    public static class Debug
    {
        /// <summary>
        /// Performs arguments assessments in order of appearance.
        /// </summary>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <param name="expression">Argument expression.</param>
        /// <param name="assessments">Argument assessments to perform.</param>
        [Conditional("DEBUG")]
        public static void Argument<T>(Expression<Func<T>> expression, params ArgumentAssessment<T>[] assessments)
        {
            Guard.Argument(expression, assessments);
        }

        /// <summary>
        /// Performs single arguments assessment and provides particular message is assessment fails.
        /// </summary>
        /// <typeparam name="T">Argument type.</typeparam>
        /// <param name="expression">Argument expression.</param>
        /// <param name="assessment">Argument assessment to perform.</param>
        /// <param name="message">Message to be displayed if assessment fails.</param>
        [Conditional("DEBUG")]
        public static void Argument<T>(Expression<Func<T>> expression, ArgumentAssessment<T> assessment, string message)
        {
            Guard.Argument(expression, assessment, message);
        }

        //// [Conditional("DEBUG")]
        //// public static void Assert<T>(Expression<Func<T>> expression, params IsAssert<T>[] assessments)
        //// {
        ////     Guard.Assert(expression, assessments);
        //// }
        //// 
        //// [Conditional("DEBUG")]
        //// public static void Assert<T>(Expression<Func<T>> expression, IsAssert<T> assessment, string message)
        //// {
        ////     Guard.Assert(expression, assessment, message);
        //// }
    }
}
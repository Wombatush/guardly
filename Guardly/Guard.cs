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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq.Expressions;

    [DebuggerNonUserCode]
    public static class Guard
    {
        private static readonly Dictionary<int, GuardBase> Arguments;
        //// private static readonly Dictionary<int, GuardBase> Asserts;

        static Guard()
        {
            Arguments = new Dictionary<int, GuardBase>();
            //// Asserts = new Dictionary<int, GuardBase>();
        }

        [DebuggerHidden]
        public static void Argument<T>(Expression<Func<T>> expression, params IsArgument<T>[] assessments)
        {
            if (assessments == null)
            {
                return;
            }

            if (assessments.Length == 0)
            {
                return;
            }

            var argument = RetrieveArgument(expression);
            if (argument == null)
            {
                return;
            }

            foreach (var assessment in assessments)
            {
                assessment(argument, null);
            }
        }

        public static void Argument<T>(Expression<Func<T>> expression, IsArgument<T> assessment, string message)
        {
            if (assessment == null)
            {
                return;
            }

            var argument = RetrieveArgument(expression);
            if (argument == null)
            {
                return;
            }

            assessment(argument, message);
        }

        //// public static void Assert<T>(Expression<Func<T>> expression, params IsAssert<T>[] assessments)
        //// {
        ////     if (assessments == null)
        ////     {
        ////         return;
        ////     }
        //// 
        ////     if (assessments.Length == 0)
        ////     {
        ////         return;
        ////     }
        //// 
        ////     var assert = RetrieveAssert(expression);
        ////     if (assert == null)
        ////     {
        ////         return;
        ////     }
        //// 
        ////     foreach (var assessment in assessments)
        ////     {
        ////         assessment(assert, null);
        ////     }
        //// }
        
        //// public static void Assert<T>(Expression<Func<T>> expression, IsAssert<T> assessment, string message)
        //// {
        ////     if (assessment == null)
        ////     {
        ////         return;
        ////     }
        //// 
        ////     var assert = RetrieveAssert(expression);
        ////     if (assert == null)
        ////     {
        ////         return;
        ////     }
        //// 
        ////     assessment(assert, message);
        //// }

        private static Argument<T> RetrieveArgument<T>(Expression<Func<T>> expression)
        {
            if (expression == null)
            {
                return null;
            }

            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                return null;
            }

            var memberExpression = (MemberExpression) expression.Body;
            var member = memberExpression.Member;
            var memberHashCode = member.GetHashCode();

            lock (Arguments)
            {
                GuardBase result;
                if (Arguments.TryGetValue(memberHashCode, out result))
                {
                    return result as Argument<T>;
                }

                var memberGetter = expression.Compile();

                result = new Argument<T>(memberHashCode, memberGetter, member);
                
                Arguments.Add(memberHashCode, result);

                return result as Argument<T>;
            }
        }

        //// private static Assert<T> RetrieveAssert<T>(Expression<Func<T>> expression)
        //// {
        ////     if (expression == null)
        ////     {
        ////         return null;
        ////     }
        //// 
        ////     if (expression.Body.NodeType != ExpressionType.MemberAccess)
        ////     {
        ////         return null;
        ////     }
        //// 
        ////     var memberExpression = (MemberExpression)expression.Body;
        ////     var member = memberExpression.Member;
        ////     var memberHashCode = member.GetHashCode();
        //// 
        ////     lock (Arguments)
        ////     {
        ////         GuardBase result;
        ////         if (Asserts.TryGetValue(memberHashCode, out result))
        ////         {
        ////             return result as Assert<T>;
        ////         }
        //// 
        ////         result = new Assert<T>(memberHashCode, expression);
        //// 
        ////         Asserts.Add(memberHashCode, result);
        //// 
        ////         return result as Assert<T>;
        ////     }
        //// }
    }
}

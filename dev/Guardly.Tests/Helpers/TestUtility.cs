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
    using System;
    using System.Linq.Expressions;
    using System.Text;

    internal static class TestUtility
    {
        public static string GetExpectedArgumentMessage(string extendedMessage)
        {
            var builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(extendedMessage))
            {
                builder.Append(extendedMessage);
                builder.AppendLine(".");
            }
            builder.AppendLine("{0}.");
            builder.Append("Parameter name: {1}");
            return builder.ToString();
        }

        public static Argument<T> CreateArgument<T>(Expression<Func<T>> expression)
        {
            var memberGetter = expression.Compile();
            var memberExpression = (MemberExpression)expression.Body;
            var member = memberExpression.Member;
            var memberHashCode = member.GetHashCode();
            
            return new Argument<T>(memberHashCode, memberGetter, member);
        }
    }
}
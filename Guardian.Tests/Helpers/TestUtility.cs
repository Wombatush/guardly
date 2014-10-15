﻿//     the above copyright notice, this list of conditions 
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

namespace Guardian.Tests.Helpers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using System.Text;

    [ExcludeFromCodeCoverage]
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
            var memberExpression = (MemberExpression)expression.Body;
            var memberHashCode = memberExpression.Member.GetHashCode();

            return new Argument<T>(memberHashCode, expression, memberExpression);
        }
    }
}
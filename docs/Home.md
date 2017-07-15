# What

Guardly is a .NET defensive programming framework that targets to a fluent syntax, extensibility and high performance at runtime.

With the fluent syntax of Guardly you will have an access to comprehensive, extensible and high performance [defensive programming](http://en.wikipedia.org/wiki/Defensive_programming) practices implementation, including [design-by-contract](http://en.wikipedia.org/wiki/Design_by_contract) practices, [fail-fast](http://en.wikipedia.org/wiki/Fail-fast) practices and runtime [assertion](http://en.wikipedia.org/wiki/Assertion_(software_development)) practices.

Name "Guardly" is a pun that exploits the name of "[Shouldly](http://shouldly.github.io/)", which by the way has been used for Guardly unit tests.

Just as a most primitive example to start with:

{code:c#}
public static void DoSomethingWithArgs(params string[]() args)
{
    Guard.Argument(() => args, Is.NotNull, Is.NotEmpty, Has.NoNulls);
    // ...
}
{code:c#}

# Why

If you came here - you know why. Defensive programming principles help us, developers, to develop a better quality, safer and more flexible code, reducing the time to market due to less number of bugs and time spent for debugging.

Why would you spend your time copy-pasting, adopting, adjusting or refactoring the code between different solutions rather than having an extensible, well-tested and well-performing solution integrated? There are no reason for that - use Guardly.

# How

Tentative plan:

# Install Guardly package
# ????
# PROFIT!!!
With respect to this [meme](http://knowyourmeme.com/memes/profit), now seriously: replace the ???? with
# Remove duplicated null argument checks by corresponding Guard
# Some methods still does not have these checks at all? Add them
# Go further adding new contract expectations with Guard
# Fix the appeared issues
# Never get back to this code again - it's now brilliant!

# Where
Guardly is available as a [NuGet package](https://www.nuget.org/packages/Guardly/). 

[Package Manager Console](http://docs.nuget.org/docs/start-here/Using-the-Package-Manager-Console) command for the latest release is easy:

{{
PM> Install-Package Guardly
}}

# Who
You should consider to start using Guardly if:
* You keen to design by contract
* You want meaningful assertions
* You like type less code and unit tests delivering more features
* Your boss wants you to improve code quality

# What's next
As a further reading check these out:
* [Documentation](https://guardly.codeplex.com/documentation)
* [Downloads](https://guardly.codeplex.com/releases)
* [Source code](https://guardly.codeplex.com/SourceControl/latest)
* [Discussions](https://guardly.codeplex.com/discussions)
* [Issues](https://guardly.codeplex.com/workitem/list/basic)
* [People](https://guardly.codeplex.com/team/view)
* [License](https://guardly.codeplex.com/license)
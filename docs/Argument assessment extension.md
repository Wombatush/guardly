# Argument assessment extension
If you consider the existing assessments insufficient, you have an opportunity to quickly extend the assessments list by your own. If you believe that there is an important assessment is missing, that is not specific to your particular project, we would appreciate if you submit a feature request [here](https://guardly.codeplex.com/workitem/list/basic).

There are few simple steps to be done to get Guardly support your assesment.

* Create a static class with a meaningful name that will start an assessment expression.
Guardly already occupied names "Is" and "Has", although you still may use them, in a separate namespace, of course. For the purpose of this article, this class will have name "Should".

{code:c#}
internal static class Should
{
    // ...
}
{code:c#}

* For parameterless assessment (like [Is.NotNull](Argument-assessment-API#IsNotNull))
Parameterless assessment is the simplest form of assessment that represented by static method with void return type and accepting two arguments. First argument should have type Argument<T>, where T could be introduced by assessment generic declaration (see example below) or some particular type. Generic parameter may also have some generic constrains upon your choice. In the example below, a new 
{code:c#}
public static void HaveNotNullValueCreated<T>(Argument<Lazy<T>> argument, string message)
{
    // ...
}
{code:c#}
With this signature this assessment can be used as:
{code:c#}
public static void TestMyAssessment(Lazy<string> arg)
{
    Guard.Argument(() => arg, Should.HaveNotNullValueCreated);
    // ...
}
{code:c#}

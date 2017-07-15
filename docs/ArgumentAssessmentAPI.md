# Built-in argument assessments reference
## Is.NotNull
**Purpose**: to validate reference type argument against null.
**Applicable to**: any reference type
**Exception types**:
* **ArgumentNullException** when argument value is null.
**Examples**
* This assessment can be used in a fluent syntax
{code:c#}
public static void IsNotNullExample(object arg)
{
    Guard.Argument(() => arg, Is.NotNull /**, ... Any other assessment **/);
    // ...
}
{code:c#}
* This assessment can be used in a dedicated syntax with particular error message
{code:c#}
public static void IsNotNullExample(object arg)
{
    Guard.Argument(() => arg, Is.NotNull, "Provided arg should not be null");
    // ...
}
{code:c#}

## Is.NotNullOrEmpty
**Purpose**: to validate string argument against null and empty value.
**Applicable to**: string
**Exception types**:
* **ArgumentNullException** when argument value is null.
* **ArgumentOutOfRangeException** when argument value is empty.
**Examples**
* This assessment can be used in a fluent syntax
{code:c#}
public static void IsNotNullOrEmptyExample(string arg)
{
    Guard.Argument(() => arg, Is.NotNullOrEmpty /**, ... Any other assessment **/);
    // ...
}
{code:c#}
* This assessment can be used in a dedicated syntax with particular error message
{code:c#}
public static void IsNotNullOrEmptyExample(string arg)
{
    Guard.Argument(() => arg, Is.NotNullOrEmpty, "Provided arg should not be null or empty");
    // ...
}
{code:c#}

## Is.NotNullOrWhiteSpace
**Purpose**: to validate string argument against null, empty and white space values.
**Applicable to**: string
**Exception types**:
* **ArgumentNullException** when argument value is null.
* **ArgumentOutOfRangeException** when argument value is empty or white space.
**Examples**
* This assessment can be used in a fluent syntax
{code:c#}
public static void IsNotNullOrWhiteSpaceExample(string arg)
{
    Guard.Argument(() => arg, Is.NotNullOrWhiteSpace /**, ... Any other assessment **/);
    // ...
}
{code:c#}
* This assessment can be used in a dedicated syntax with particular error message
{code:c#}
public static void IsNotNullOrWhiteSpaceExample(string arg)
{
    Guard.Argument(() => arg, Is.NotNullOrWhiteSpace, "Provided arg should not be null, empty or white space");
    // ...
}
{code:c#}
# <img src='/src/icon.png' height='30px'> SimpleInfoName

[![Build status](https://ci.appveyor.com/api/projects/status/52bwfqln7kh4oj07/branch/main?svg=true)](https://ci.appveyor.com/project/SimonCropp/SimpleInfoName)
[![NuGet Status](https://img.shields.io/nuget/v/SimpleInfoName.svg)](https://www.nuget.org/packages/SimpleInfoName/)

Generates names for reflection infos. Adds a `SimpleName()` extension method to the following types.

 * Type
 * ParameterInfo
 * PropertyInfo
 * FieldInfo
 * MethodInfo
 * ConstructorInfo

**See [Milestones](../../milestones?state=closed) for release notes.**


## Sponsors


### Entity Framework Extensions<!-- include: zzz. path: /docs/zzz.include.md -->

[Entity Framework Extensions](https://entityframework-extensions.net/) is a major sponsor and is proud to contribute to the development this project.

[![Entity Framework Extensions](docs/zzz.png)](https://entityframework-extensions.nethttps://entityframework-extensions.net/bulk-insert?utm_source=simoncropp&utm_medium=SimpleInfoName)<!-- endInclude -->


## NuGet

 * https://nuget.org/packages/SimpleInfoName


## Usage

Given a class definition of:

<!-- snippet: Target -->
<a id='snippet-Target'></a>
```cs
namespace MyNamespace
{
    public class Parent<T>;

    public class Target<K> : Parent<int>
    {
        public Target()
        {
        }

        public string Property { get; set; } = null!;
        public string field = null!;

        public void Method<Y, D>(List<D> parameter)
        {
        }
    }
}
```
<sup><a href='/src/Tests/Snippets.cs#L6-L27' title='Snippet source file'>snippet source</a> | <a href='#snippet-Target' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

And a constructed type of `Target<int>`.
    
The simple names would be:

<!-- include: sample. path: /src/sample.include.md -->
|   |   |
| - | - |
| Type | `Target<int>` |
| | Compared to `Type.FullName` of<br> `MyNamespace.Target'1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]` |
| Constructor | `Target<int>.ctor()` |
| Method | `Target<int>.Method(List<bool> parameter)` |
| Parameter | `'parameter' of Target<int>.Method(List<bool> parameter)` |
| Field | `Target<int>.field` |
| Property | `Target<int>.Property` |
<!-- endInclude -->


## Icon

[Complex](https://thenounproject.com/term/complex/2270599/) designed by [auttapol](https://thenounproject.com/monsterku69) from [The Noun Project](https://thenounproject.com).

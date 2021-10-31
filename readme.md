# <img src='/src/icon.png' height='30px'> SimpleInfoName

[![Build status](https://ci.appveyor.com/api/projects/status/dl7snkk70b964ke4/branch/master?svg=true)](https://ci.appveyor.com/project/SimonCropp/SimpleInfoName)
[![NuGet Status](https://img.shields.io/nuget/v/SimpleInfoName.svg)](https://www.nuget.org/packages/SimpleInfoName/)

Generates names for reflection infos. Adds a `SimpleName()` extension method to the following types.

 * Types
 * PrameterInfo 
 * PropertyInfo
 * FieldInfo
 * MethodInfo
 * ConstructorInfo

## NuGet package

 * https://nuget.org/packages/SimpleInfoName/


## Usage

Given a class definition of:

<!-- snippet: Target -->
<a id='snippet-target'></a>
```cs
namespace MyNamespace
{
    public class Parent<T> { }

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
<sup><a href='/src/Tests/Snippets.cs#L9-L28' title='Snippet source file'>snippet source</a> | <a href='#snippet-target' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->

The simple names would be:

 * Type `Target<int>`.<br>Compared to `Type.FullName`: `MyNamespace.Target``1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]` <!-- include: sample. path: /src/sample.include.md -->
 * Constructor `Target<int>.ctor()`
 * Method `Target<int>.Method(List<bool> parameter)`
 * Parameter `'parameter' of Target<int>.Method(List<bool> parameter)`
 * Field `Target<int>.field`
 * Property `Target<int>.Property` <!-- endInclude -->


## Icon

[Complex](https://thenounproject.com/term/complex/2270599/) designed by [auttapol](https://thenounproject.com/monsterku69) from [The Noun Project](https://thenounproject.com).

# Squire

Squire is my personal framework I use in many of my projects. It contains many advanced utilities, predefined common classes (address, contact info, etc), and extensions. It's entire point is to get me up and running faster.

This isn't your typical "core framework/library" I've worked hard to ensure it is supporting, unrestrictive, and easy to use. Some of the key concepts embedded within it are _domain-driven design_, _aspect-orientated programming_, and what I'm going to coin _game-orientated design_. 

# Installation


# Getting Started

## Essential Components

### Practices

`Squire.Practices.IFactory<T>` is a generic interface for utilizing the factory pattern.

Usage:

    class Program
    {
        class ObjectFactory : IFactory<object>
        {
            public object Create()
            {
                return new object();
            }
        }
        
        static void Main(string[] args)
        {
            IFactory<object> factory = new ObjectFactory();
            var createdObject = factory.Create();
        }
    }
    
### Validation

The `Squire.Validation` namespace hosts a number of fluent extensions to enable simple parameter validation. Calling the `VerifyParam` extension method will return a validation object with the fluent calls available. 

**Note 1** chaining is possible by referencing the `And` property after each validation call.

**Note 2** all of the fluent validation extensions come with error messages preset, but accept a custom validation message via the `message` parameter.

Usage:

    using Squire.Validation;

    class Program
    {
        static void Main(string[] args)
        {
            Foo(name: null, age: 0, 
        }

        static void Foo(string name, int age, object key, int? nullableAge, IEnumerable<string> collection)
        {
            // All objects can use IsNotNull and IsSubClassOf
            key.VerifyParam("key").IsNotNull(message: "This cannot be null!")
                .And.IsSubClassOf(typeof(object));

            // This also works for nullable types
            nullableAge.VerifyParam("nullableAge").IsNotNull();

            // Strings have access to IsNotWhiteSpace IsNotBlank IsNotNull and IsNotEmpty when validating.
            name.VerifyParam("name").IsNotNull()
                .And.IsNotEmpty()      // IsNotEmpty also checks for null
                .And.IsNotWhiteSpace() // IsNotWhiteSpace also checks for empty and null
                .And.IsNotBlank();     // IsNotBlank also checks for null and empty

            // Integers have access to IsGreaterThan IsInRange and IsGreaterThanZero
            age.VerifyParam("age").IsGreaterThanZero()
                .And.IsGreaterThan(18)
                .And.IsInRange(0, 10, inclusive: false);

            // Enumerable<T> can use IsNotEmpty() to verify the collection has elements.
            collection.VerifyParam("collection").IsNotEmpty();
        }
    }

# Bugs and Feedback

If you see a bug or have a suggestion, feel free to create an issue [here][3].

# License

MIT License. Copyright 2013 Daniel Krainas [http://www.danielkrainas.com][1]

[1]: http://www.danielkrainas.com
[2]: http://nuget.org/packages/incant
[3]: https://bitbucket.org/dkrainas/incant/issues
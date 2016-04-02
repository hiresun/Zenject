using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;
using NUnit.Framework;
using ModestTree;
using Assert=ModestTree.Assert;

namespace Zenject.Tests.Bindings
{
    [TestFixture]
    public class TestFactoryToResolve0 : TestWithContainer
    {
        [Test]
        public void TestSelf()
        {
            var foo = new Foo();

            Container.BindInstance(foo);

            Container.BindFactory<Foo, Foo.Factory>().ToResolve<Foo>();

            AssertValidates();

            Assert.IsEqual(Container.Resolve<Foo.Factory>().Create(), foo);
        }

        [Test]
        public void TestConcrete()
        {
            var foo = new Foo();

            Container.BindInstance(foo);

            Container.BindFactory<IFoo, IFooFactory>().ToResolve<Foo>();

            AssertValidates();

            Assert.IsEqual(Container.Resolve<IFooFactory>().Create(), foo);
        }

        [Test]
        public void TestSelfIdentifier()
        {
            var foo = new Foo();

            Container.BindInstance("foo", foo);

            Container.BindFactory<Foo, Foo.Factory>().ToResolve<Foo>("foo");

            AssertValidates();

            Assert.IsEqual(Container.Resolve<Foo.Factory>().Create(), foo);
        }

        [Test]
        public void TestConcreteIdentifier()
        {
            var foo = new Foo();

            Container.BindInstance("foo", foo);

            Container.BindFactory<IFoo, IFooFactory>().ToResolve<Foo>("foo");

            AssertValidates();

            Assert.IsEqual(Container.Resolve<IFooFactory>().Create(), foo);
        }

        interface IFoo
        {
        }

        class IFooFactory : Factory<IFoo>
        {
        }

        class Foo : IFoo
        {
            public class Factory : Factory<Foo>
            {
            }
        }
    }
}

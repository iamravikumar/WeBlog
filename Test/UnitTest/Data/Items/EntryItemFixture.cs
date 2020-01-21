﻿using Moq;
using NUnit.Framework;
using Sitecore.Modules.WeBlog.Data.Items;
using System;

#if FEATURE_ABSTRACTIONS
using Sitecore.Abstractions;
#endif

namespace Sitecore.Modules.WeBlog.UnitTest.Data.Items
{
    [TestFixture]
    public class EntryItemFixture
    {
#if FEATURE_ABSTRACTIONS
        [Test]
        public void Ctor_NullItem_ThrowsException()
        {
            // arrange
            var linkManager = Mock.Of<BaseLinkManager>();
            Action sutAction = () => new EntryItem(null, linkManager);

            // act, assert
            var ex = Assert.Throws<ArgumentNullException>(new TestDelegate(sutAction));
            Assert.That(ex.ParamName, Is.EqualTo("innerItem"));
        }

        [Test]
        public void Ctor_NullLinkManager_ThrowsException()
        {
            // arrange
            var item = ItemFactory.CreateItem();
            Action sutAction = () => new EntryItem(item.Object, null);

            // act, assert
            var ex = Assert.Throws<ArgumentNullException>(new TestDelegate(sutAction));
            Assert.That(ex.ParamName, Is.EqualTo("linkManager"));
        }
#endif
    }
}
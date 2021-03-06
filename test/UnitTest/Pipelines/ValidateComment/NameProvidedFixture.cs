﻿using Moq;
using NUnit.Framework;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Modules.WeBlog.Caching;
using Sitecore.Modules.WeBlog.Model;
using Sitecore.Modules.WeBlog.Pipelines.ValidateComment;
using System.Collections.Specialized;
using System.Linq;

namespace Sitecore.Modules.WeBlog.UnitTest.Pipelines.ValidateComment
{
    [TestFixture]
    public class NameProvidedFixture
    {
        [SetUp]
        public void SetUp()
        {
            // Add dictionary entries to cache
            var cache = Mock.Of<TranslatorCache>(x =>
                x.FindEntry(Constants.TranslationPhrases.RequiredField) == CreateDictionaryEntryItem("Required field {0} is missing.") &&
                x.FindEntry(Constants.TranslationPhrases.Name) == CreateDictionaryEntryItem("Name")
            );

            CacheManager.SetCache("WeBlog [translator]", cache);
        }

        private Item CreateDictionaryEntryItem(string phrase)
        {
            var item = ItemFactory.CreateItem();
            var field = FieldFactory.CreateField(item.Object, ID.NewID, "Phrase", phrase);
            ItemFactory.AddFields(item, new[] { field });
            return item.Object;
        }

        [TestCase(null)]
        [TestCase("  ")]
        [TestCase("\t")]
        public void Process_NameIsInvalid_AddsErrorToArgs(string name)
        {
            // arrange
            var comment = new Comment
            {
                AuthorEmail = "mail@mail.com",
                Text = "comment",
                AuthorName = name
            };

            var args = new ValidateCommentArgs(comment, new NameValueCollection());
            var sut = new NameProvided();

            // act
            sut.Process(args);

            // assert
            var errorText = args.Errors.First();
            Assert.That(errorText, Is.EqualTo("Required field Name is missing."));
        }

        [Test]
        public void Process_NameIsValid_NoErrorsInArgs()
        {
            // arrange
            var comment = new Comment
            {
                AuthorEmail = "mail@mail.com",
                Text = "comment",
                AuthorName = "name"
            };

            var args = new ValidateCommentArgs(comment, new NameValueCollection());
            var sut = new NameProvided();

            // act
            sut.Process(args);

            // assert
            Assert.That(args.Errors, Is.Empty);
        }
    }
}

﻿using Moq;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System.Collections.Generic;

namespace Sitecore.Modules.WeBlog.UnitTest
{
    /// <summary>
    /// A factory used to create items.
    /// </summary>
    internal static class ItemFactory
    {
        public static Mock<Item> CreateItem(ID templateId = null, Database database = null)
        {
            if(database == null)
                database = Mock.Of<Database>();

            var itemMock = new Mock<Item>(ID.NewID, ItemData.Empty, database);

            if (templateId != (ID)null)
                itemMock.Setup(x => x.TemplateID).Returns(templateId);

            // Set all field access to return empty string
            itemMock.Setup(x => x[It.IsAny<string>()]).Returns(string.Empty);

            return itemMock;
        }

        public static void SetIndexerField(Mock<Item> itemMock, string fieldName, string fieldValue)
        {
            itemMock.Setup(x => x[fieldName]).Returns(fieldValue);
        }

        public static void SetIndexerField(Mock<Item> itemMock, ID fieldId, string fieldValue)
        {
            itemMock.Setup(x => x[fieldId]).Returns(fieldValue);
        }

        public static void AddFields(Mock<Item> itemMock, IEnumerable<Field> fields)
        {
            var fieldCollectionMock = new Mock<FieldCollection>(itemMock.Object);

            foreach (var field in fields)
            {
                fieldCollectionMock.Setup(x => x[field.ID]).Returns(field);
                fieldCollectionMock.Setup(x => x[field.Name]).Returns(field);
            }

            itemMock.Setup(x => x.Fields).Returns(fieldCollectionMock.Object);
        }
    }
}

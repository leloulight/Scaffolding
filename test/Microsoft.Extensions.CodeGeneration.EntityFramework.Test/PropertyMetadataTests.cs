// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.CodeGeneration.EntityFramework.Test.TestModels;
using Xunit;

namespace Microsoft.Extensions.CodeGeneration.EntityFramework.Test
{
    public class PropertyMetadataTests
    {
        [Fact]
        public void Primary_Key_Metadata_Is_Correct()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.ProductId));

            //Assert
            Assert.Equal(nameof(Product.ProductId), propertyMetadata.PropertyName);
            Assert.Equal(true, propertyMetadata.IsPrimaryKey);
            Assert.Equal(false, propertyMetadata.IsForeignKey);
            Assert.Equal(typeof(int).FullName, propertyMetadata.TypeName);
            Assert.Equal(false, propertyMetadata.IsEnum);
            Assert.Equal(false, propertyMetadata.IsAutoGenerated);
            Assert.Equal(false, propertyMetadata.IsEnumFlags);
            Assert.Equal(false, propertyMetadata.IsReadOnly);
            Assert.Equal(true, propertyMetadata.Scaffold);
        }

        [Fact]
        public void Foreign_Key_Metadata_Is_Correct()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.CategoryId));

            //Assert
            Assert.Equal(nameof(Product.CategoryId), propertyMetadata.PropertyName);
            Assert.Equal(false, propertyMetadata.IsPrimaryKey);
            Assert.Equal(true, propertyMetadata.IsForeignKey);
            Assert.Equal(typeof(int).FullName, propertyMetadata.TypeName);
            Assert.Equal(false, propertyMetadata.IsEnum);
            Assert.Equal(false, propertyMetadata.IsAutoGenerated);
            Assert.Equal(false, propertyMetadata.IsEnumFlags);
            Assert.Equal(false, propertyMetadata.IsReadOnly);
            Assert.Equal(true, propertyMetadata.Scaffold);
        }

        [Fact]
        public void String_Property_Metadata_Is_Correct()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.ProductName));

            //Assert
            Assert.Equal(nameof(Product.ProductName), propertyMetadata.PropertyName);
            Assert.Equal(false, propertyMetadata.IsPrimaryKey);
            Assert.Equal(false, propertyMetadata.IsForeignKey);
            Assert.Equal(typeof(string).FullName, propertyMetadata.TypeName);
            Assert.Equal(false, propertyMetadata.IsEnum);
        }

        [Fact]
        public void Enum_Property_Metadata_Is_Correct()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.EnumProperty));

            //Assert
            Assert.Equal(nameof(Product.EnumProperty), propertyMetadata.PropertyName);
            Assert.Equal(false, propertyMetadata.IsPrimaryKey);
            Assert.Equal(false, propertyMetadata.IsForeignKey);
            Assert.Equal(typeof(EnumType).FullName, propertyMetadata.TypeName);
            Assert.Equal(true, propertyMetadata.IsEnum);
            Assert.Equal(false, propertyMetadata.IsEnumFlags);
        }

        [Fact]
        public void Enum_Flags_Property_Metadata_Is_Correct()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.EnumFlagsProperty));

            //Assert
            Assert.Equal(nameof(Product.EnumFlagsProperty), propertyMetadata.PropertyName);
            Assert.Equal(false, propertyMetadata.IsPrimaryKey);
            Assert.Equal(false, propertyMetadata.IsForeignKey);
            Assert.Equal(typeof(EnumFlagsType).FullName, propertyMetadata.TypeName);
            Assert.Equal(true, propertyMetadata.IsEnum);
            Assert.Equal(true, propertyMetadata.IsEnumFlags);
        }

        [Fact]
        public void Scaffold_Attribute_Is_Reflected_In_Metadata()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var explicitScaffoldProp = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.ExplicitScaffoldProperty));
            var scaffoldFalseProp = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.ScaffoldFalseProperty));

            //Assert
            Assert.Equal(nameof(Product.ExplicitScaffoldProperty), explicitScaffoldProp.PropertyName);
            Assert.Equal(false, explicitScaffoldProp.IsPrimaryKey);
            Assert.Equal(false, explicitScaffoldProp.IsForeignKey);
            Assert.Equal(typeof(string).FullName, explicitScaffoldProp.TypeName);
            Assert.Equal(true, explicitScaffoldProp.Scaffold);

            Assert.Equal(nameof(Product.ScaffoldFalseProperty), scaffoldFalseProp.PropertyName);
            Assert.Equal(false, scaffoldFalseProp.IsPrimaryKey);
            Assert.Equal(false, scaffoldFalseProp.IsForeignKey);
            Assert.Equal(typeof(int).FullName, scaffoldFalseProp.TypeName);
            Assert.Equal(false, scaffoldFalseProp.Scaffold);
        }

        [Fact(Skip = "Need to investigate/enable the functionality")]
        public void ReadOnly_Attribute_Is_Reflected_In_Metadata()
        {
            //Arrange
            var productEntity = TestModel.CategoryProductModel.FindEntityType(typeof(Product));
            var modelMetadata = new ModelMetadata(productEntity, typeof(TestDbContext));

            //Act
            var propertyMetadata = modelMetadata.Properties.FirstOrDefault(p => p.PropertyName == nameof(Product.ReadOnlyProperty));

            //Assert
            Assert.Equal(nameof(Product.ReadOnlyProperty), propertyMetadata.PropertyName);
            Assert.Equal(false, propertyMetadata.IsPrimaryKey);
            Assert.Equal(false, propertyMetadata.IsForeignKey);
            Assert.Equal(typeof(string).FullName, propertyMetadata.TypeName);
            Assert.Equal(true, propertyMetadata.IsReadOnly);
        }
    }
}
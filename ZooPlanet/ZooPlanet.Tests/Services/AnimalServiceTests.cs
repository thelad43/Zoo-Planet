namespace ZooPlanet.Tests.Services
{
    using ZooPlanet.Common.Constants;
    using ZooPlanet.Data.Models;
    using ZooPlanet.Data.Models.Enums;
    using ZooPlanet.Services.Implementations;

    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class AnimalServiceTests
    {
        public AnimalServiceTests()
        {
            Tests.Initialize();
        }

        [Fact]
        public async Task AllAsyncShouldReturnCorrectAnimalsByPages()
        {
            var db = DbInfrastructure.GetDatabase();

            for (var i = 0; i < 50; i++)
            {
                await db.AddAsync(new Animal
                {
                    Name = $"Some random name {i}"
                });
            }

            await db.SaveChangesAsync();

            var animalService = new AnimalService(db);

            for (var i = 0; i < 10; i++)
            {
                var animals = await animalService.AllAsync(i + 1);

                animals
                    .Should()
                    .HaveCount(WebConstants.AnimalsPerPage);

                animals
                    .Should()
                    .BeInAscendingOrder(a => a.AnimalClass);
            }

            var animalsFromNonExistingPage = await animalService.AllAsync(11);

            animalsFromNonExistingPage.Should().HaveCount(0);
        }

        [Fact]
        public async Task AddAsyncShouldAddAnimalToDb()
        {
            var db = DbInfrastructure.GetDatabase();

            var animalService = new AnimalService(db);

            const string Name = "random name";
            const int Age = 5;
            const string Url = "https://news.nationalgeographic.com/content/dam/news/2018/05/17/you-can-train-your-cat/02-cat-training-NationalGeographic_1484324.jpg";
            const string UserId = "Some user ID";

            await animalService.AddAsync(Name, Age, Url, AnimalClass.Mammal, UserId);

            var animal = await db.Animals.FirstOrDefaultAsync(a => a.Name == Name);

            animal.AnimalClass.Should().Be(AnimalClass.Mammal);

            animal.Age.Should().Be(Age);

            animal.ImageUrl.Should().Be(Url);

            animal.UserId.Should().Be(UserId);
        }

        [Fact]
        public async Task CountAsyncShouldReturnCorrectCount()
        {
            var db = DbInfrastructure.GetDatabase();

            const int AnimalsCount = 100;

            for (var i = 0; i < AnimalsCount; i++)
            {
                await db.AddAsync(new Animal
                {
                    Name = $"Name {i}"
                });
            }

            await db.SaveChangesAsync();

            var animalService = new AnimalService(db);

            var animals = await animalService.CountAsync();

            animals.Should().Be(AnimalsCount);
        }

        [Fact]
        public async Task ByIdAsyncShouldReturnAnimalById()
        {
            var db = DbInfrastructure.GetDatabase();

            const string Name = "Animal Name";

            await db.AddAsync(new Animal
            {
                Name = Name
            });

            for (var i = 0; i < 50; i++)
            {
                await db.AddAsync(new Animal
                {
                    Name = $"Name {i}"
                });
            }

            await db.SaveChangesAsync();

            var animalService = new AnimalService(db);

            const int Id = 1;

            var animal = await animalService.ByIdAsync(Id);

            animal.Should().NotBeNull();

            animal.Name.Should().Be(Name);
        }

        [Fact]
        public async Task ByIdAsyncShouldReturnNullIfAnimalIsNotFound()
        {
            var db = DbInfrastructure.GetDatabase();

            var animalService = new AnimalService(db);

            var animal = await animalService.ByIdAsync(50);

            animal.Should().BeNull();
        }

        [Fact]
        public async Task ByUserAsyncShouldReturnAnimalByUser()
        {
            var db = DbInfrastructure.GetDatabase();

            var userId = string.Empty;
            var animalId = 0;

            for (var i = 0; i < 5; i++)
            {
                var currentUser = new User
                {
                    UserName = $"Some user {i}"
                };

                await db.AddAsync(currentUser);

                userId = currentUser.Id;

                for (var k = 0; k < 5; k++)
                {
                    var currentAnimal = new Animal
                    {
                        Name = $"Animal {i * k}",
                        UserId = currentUser.Id
                    };

                    await db.AddAsync(currentAnimal);

                    animalId = currentAnimal.Id;
                }
            }

            await db.SaveChangesAsync();

            var animalService = new AnimalService(db);

            var user = await db
                .Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            var animal = await animalService.ByUserAsync(animalId, user);

            animal.Should().NotBeNull();
        }

        [Fact]
        public async Task ByUserAsyncShouldReturnNullIfAnimalIsNotFound()
        {
            var db = DbInfrastructure.GetDatabase();

            var animalService = new AnimalService(db);

            var animal = await animalService.ByUserAsync(50, new User());

            animal.Should().BeNull();
        }


        [Fact]
        public void EditAsyncShouldThrowExceptionIfAnimalIsNotFoundl()
        {
            var db = DbInfrastructure.GetDatabase();

            var animalService = new AnimalService(db);

            Func<Task> func = async () => await animalService.EditAsync(1, "", 5, AnimalClass.Arthropod, "Some url");

            func
                .Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public async Task EditAsyncShouldEditAnimal()
        {
            var db = DbInfrastructure.GetDatabase();

            var animal = new Animal
            {
                Name = "Peshi",
                Age = 7,
                ImageUrl = "Image",
                AnimalClass = AnimalClass.Reptile,
                UserId = "Some User Id"
            };

            await db.AddAsync(animal);
            await db.SaveChangesAsync();

            var animalId = animal.Id;

            var animalService = new AnimalService(db);

            const string Name = "Pesho";
            const int Age = 8;
            const string ImageUrl = "New Image URL";

            await animalService.EditAsync(animalId, Name, Age, AnimalClass.Mammal, ImageUrl);

            var actualAnimal = await db.Animals.FindAsync(animalId);

            actualAnimal.Name.Should().Be(Name);

            actualAnimal.Age.Should().Be(Age);

            actualAnimal.AnimalClass.Should().Be(AnimalClass.Mammal);

            actualAnimal.ImageUrl.Should().Be(ImageUrl);
        }

        [Fact]
        public void DeleteAsyncShouldThrowExceptionIfAnimalIsNotFound()
        {
            var db = DbInfrastructure.GetDatabase();

            var animalService = new AnimalService(db);

            Func<Task> func = async () => await animalService.DeleteAsync(50);

            func
                .Should()
                .Throw<InvalidOperationException>();
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteAnimal()
        {
            var db = DbInfrastructure.GetDatabase();

            var animal = new Animal
            {
                Name = "Ivan"
            };

            await db.AddAsync(animal);
            await db.SaveChangesAsync();

            var animalService = new AnimalService(db);

            await animalService.DeleteAsync(animal.Id);

            var animalsCount = await animalService.CountAsync();

            animalsCount.Should().Be(0);
        }
    }
}
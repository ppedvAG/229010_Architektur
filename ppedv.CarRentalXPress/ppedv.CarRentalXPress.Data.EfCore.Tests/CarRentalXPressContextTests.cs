using AutoFixture;
using AutoFixture.Kernel;
using FluentAssertions;
using ppedv.CarRentalXPress.Model;
using System.Reflection;

namespace ppedv.CarRentalXPress.Data.EfCore.Tests
{
    public class CarRentalXPressContextTests
    {
        readonly string conString = "Server=(localdb)\\mssqllocaldb;Database=CarRentalXPress_Tests;Trusted_Connection=true";

        [Fact]
        public void Can_create_Db()
        {
            var con = new CarRentalXPressContext(conString);
            con.Database.EnsureDeleted();

            var result = con.Database.EnsureCreated();

            Assert.True(result);
            result.Should().BeTrue();
        }

        [Fact]
        public void Can_create_and_read_Car_with_AutoFixture()
        {
            var fix = new Fixture();
            fix.Customizations.Add(new PropertyNameOmitter("Id"));
            fix.Behaviors.Add(new OmitOnRecursionBehavior());
            var car = fix.Create<Car>();

            using (var con = new CarRentalXPressContext(conString))
            {
                con.Database.EnsureCreated();
                con.Add(car);
                con.SaveChanges();
            }
            using (var con = new CarRentalXPressContext(conString))
            {
                var loaded = con.Cars.Find(car.Id);

                loaded.Should().BeEquivalentTo(car, x => x.IgnoringCyclicReferences());
                loaded.Should().BeEquivalentTo(car, x => x.IgnoringCyclicReferences());
            }

        }

    }

    internal class PropertyNameOmitter : ISpecimenBuilder
    {
        private readonly IEnumerable<string> names;

        internal PropertyNameOmitter(params string[] names)
        {
            this.names = names;
        }

        public object Create(object request, ISpecimenContext context)
        {
            var propInfo = request as PropertyInfo;
            if (propInfo != null && names.Contains(propInfo.Name))
                return new OmitSpecimen();

            return new NoSpecimen();
        }
    }
}
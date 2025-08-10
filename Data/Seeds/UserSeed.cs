using MealPlannerApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealPlannerApi.Data.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = 1,
                    Email = "almanza.angel245@gamil.com",
                    Password = "$2a$11$LvQhB28R7IFhEo2L7LtA/.ZXXbJR4j8zNkQeHrExaZ6MXV8cyb8GC"
                }
            );
        }
    }
}
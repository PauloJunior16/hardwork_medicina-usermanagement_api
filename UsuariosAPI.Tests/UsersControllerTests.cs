using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Controllers;
using UsuariosAPI.Data;
using UsuariosAPI.Models;

namespace UsuariosApi.UsuariosAPI.Tests
{
    public class UsersControllerTests
    {
        private UserDbContext CreateContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;
            return new UserDbContext(options);
        }

        [Fact]
        public async Task GetUsers_ReturnsAllUsers()
        {
            var dbName = Guid.NewGuid().ToString();
            using (var context = CreateContext(dbName))
            {
                context.Users.AddRange(
                    new User { Name = "Test User 1", Email = "test1@example.com" },
                    new User { Name = "Test User 2", Email = "test2@example.com" }
                );
                await context.SaveChangesAsync();
            }

            using (var context = CreateContext(dbName))
            {
                var controller = new UsersController(context);
                var result = await controller.GetUsers();

                var actionResult = Assert.IsType<ActionResult<IEnumerable<User>>>(result);
                var users = Assert.IsAssignableFrom<IEnumerable<User>>(actionResult.Value);
                Assert.Equal(2, users.Count());
            }
        }

        [Fact]
        public async Task CreateUser_ReturnsCreatedAtAction()
        {
            var dbName = Guid.NewGuid().ToString();
            using var context = CreateContext(dbName);
            var controller = new UsersController(context);
            var newUser = new User { Name = "New User", Email = "new@example.com" };

            var result = await controller.PostUser(newUser);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<User>(createdAtActionResult.Value);
            Assert.Equal("New User", returnValue.Name);
        }

        [Fact]
        public async Task CleanOldRecords_DeletesOldRecords()
        {
            var dbName = Guid.NewGuid().ToString();
            using (var context = CreateContext(dbName))
            {
                context.Users.AddRange(
                    new User { Name = "Old User", Email = "old@example.com", CreatedAt = DateTime.Now.AddDays(-10) },
                    new User { Name = "New User", Email = "new@example.com", CreatedAt = DateTime.Now }
                );
                await context.SaveChangesAsync();
            }

            using (var context = CreateContext(dbName))
            {
                var controller = new UsersController(context);
                var result = await controller.CleanOldRecords(DateTime.Now.AddDays(-5));

                var okResult = Assert.IsType<OkObjectResult>(result);
                var responseValue = Assert.IsType<CleanOldRecordsResponse>(okResult.Value);
                Assert.Equal(1, responseValue.DeletedCount);

                Assert.Single(await context.Users.ToListAsync());
                Assert.Equal("New User", (await context.Users.SingleAsync()).Name);
            }
        }
    }
}

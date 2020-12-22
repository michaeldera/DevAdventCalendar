using System;
using DevAdventCalendarCompetition.Repository;
using DevAdventCalendarCompetition.Repository.Context;
using DevAdventCalendarCompetition.Repository.Models;
using DevAdventCalendarCompetition.TestResultService.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DevAdventCalendarCompetition.TestResultService.Tests
{
    public class UserTestAnswersRepositoryFixture : IDisposable
    {
        private UserTestAnswersRepository _userTestAnswersRepository;
        private ApplicationDbContext _dbContext;

        public UserTestAnswersRepository GetUserTestAnswersRepository()
        {
            if (_userTestAnswersRepository != null)
            {
                return _userTestAnswersRepository;
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _dbContext = new ApplicationDbContext(optionsBuilder.Options);
            _dbContext.Database.EnsureCreated();
            PrepareDatabaseRows(_dbContext);

            _userTestAnswersRepository = new UserTestAnswersRepository(_dbContext);
            return _userTestAnswersRepository;
        }

        private static void PrepareDatabaseRows(ApplicationDbContext dbContext)
        {
            var userA = new ApplicationUser()
            {
                Id = "101",
                UserName = "A",
                Email = "a@test.com"
            };
            dbContext.Users.Add(userA);
            dbContext.SaveChanges();


            var test1 = new Test()
            {
                Id = 101,
                StartDate = new DateTime(2019, 12, 1, 20, 0, 0),
                EndDate = new DateTime(2019, 12, 24, 23, 59, 0)
            };
            dbContext.Tests.Add(test1);
            dbContext.SaveChanges();


            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 107,
                UserId = UserModel.userA.Id,
                User = UserModel.userA,
                TestId = test1.Id,
                Test = test1,
                AnsweringTime = test1.StartDate.Value.AddHours(2).AddSeconds(6),
                AnsweringTimeOffset = new TimeSpan(0, 2, 0, 6)
            });
            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 108,
                UserId = UserModel.userA.Id,
                User = UserModel.userA,
                TestId = test1.Id,
                Test = test1,
                AnsweringTime = test1.StartDate.Value.AddHours(2).AddSeconds(4),
                AnsweringTimeOffset = new TimeSpan(0, 2, 0, 4)
            });
            dbContext.UserTestCorrectAnswers.Add(new UserTestCorrectAnswer()
            {
                Id = 109,
                UserId = UserModel.userA.Id,
                User = UserModel.userA,
                TestId = test1.Id,
                Test = test1,
                AnsweringTime = test1.StartDate.Value.AddHours(2).AddSeconds(5),
                AnsweringTimeOffset = new TimeSpan(0, 2, 0, 5)
            });
            dbContext.SaveChanges();


            dbContext.UserTestWrongAnswers.Add(new UserTestWrongAnswer()
            {
                Id = 103,
                UserId = UserModel.userA.Id,
                User = UserModel.userA,
                TestId = test1.Id,
                Test = test1,
                Time = test1.StartDate.Value.AddHours(1)
            });
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }

    [CollectionDefinition(nameof(UserTestAnswersRepositoryCollection))]
    public class UserTestAnswersRepositoryCollection : ICollectionFixture<UserTestAnswersRepositoryFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
using NUnit.Framework;
using System;
using System.Linq;
using UsersApi;
using UsersApi.Eums;
using UsersApi.Managers;

namespace UsersUnitTest.cs
{
    public class Tests
    {
        private IUserManager _userManager;

        [SetUp]
        public void Setup()
        {
            _userManager = new UserManager();
        }

        [Test]
        public void TestAddAndGetOneUser()
        {
            DefaultUserAdd();

            var userCount = _userManager.GetAll().Where(x => x.Name == "Jim").Count();

            Assert.IsTrue(userCount == 1);
        }

        [Test]
        public void TestAddAndGetTenUsers()
        {
            for (var i = 0; i < 10; i++)
            {
                _userManager.Add(new UserModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Nathan" + i,
                    Surname = "Rawson" + i,
                    Email = "nathanrawson1@gmail.com" + i,
                    Job = "developer" + i,
                    Mobile = "07720857279" + i
                });
            }

            DefaultUserAdd();

            var userCount = _userManager.GetAll().Where(x => x.Name.Contains("Nathan")).Count();

            Assert.IsTrue(userCount == 10);
        }

        [Test]
        public void TestDuplicateAddResponse()
        {
            DefaultUserAdd();

            Assert.IsTrue(DefaultUserAdd().ToString() == UserResultEnum.Duplicate.ToString());
        }

        private Enum DefaultUserAdd()
        {
            return _userManager.Add(new UserModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Jim",
                Surname = "Rawson",
                Email = "nathanrawson1@gmail.com",
                Job = "developer",
                Mobile = "07720857279"
            });
        }
    }
}
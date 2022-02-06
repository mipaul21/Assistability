/// <summary>
/// William Clark
/// Created: 2021/02/04
/// 
/// Test class for the UserGroupManager
/// </summary>
///
/// <remarks>
/// </remarks>
using System;
using System.Collections.Generic;
using System.ComponentModel;
using DataAccessFakes;
using DataAccessLayer;
using DataStorageModels;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicLayerTests
{
    [TestClass]
    public class UserGroupManagerTests
    {
        const int arbitraryValidUserID = 1;
        private UserGroup arbitraryValidUserGroup = new UserGroup(1, 1);
        IUserGroupManager _userGroupManager;

        /// <summary>
        /// William Clark
        /// Created: 2021/02/24
        /// 
        /// TestIntialize method that initializes an IUserGroupManager
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestInitialize]
        public void TestSetup()
        {
            _userGroupManager = new UserGroupManager(new UserGroupFakes());
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/18
        /// 
        /// Tests SelectUserGroupsByUserAccountID method with an arbitrary valid UserAccountID
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestSelectOwnedUserGroupsByUserAccountIDReturnsUserGroupList()
        {
            // Arrange
            List<UserGroup> expectedResult = new List<UserGroup>() { new UserGroup(1, 1) };
            List<UserGroup> actualResult;

            // Act 
            actualResult = _userGroupManager.SelectOwnedUserGroupsByUserAccountID(arbitraryValidUserID);

            // Assert
            Assert.AreEqual(expectedResult.Count, actualResult.Count);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/18
        /// 
        /// Tests SelectUserGroupsByUserAccountID method throws exception with bad UserAccountID
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectOwnedUserGroupsByUserAccountIDThrowsExceptionBadGroup()
        {
            // Arrange
            List<UserGroup> actualResult;

            // Act 
            actualResult = _userGroupManager.SelectOwnedUserGroupsByUserAccountID(-1);

            // Assert
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        /// 
        /// Tests GetUserAccountsByUserGroup method with an arbitrary valid UserGroup
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestGetUserAccountsByUserGroupReturnsUserAccountList()
        {
            // Arrange
            List<UserAccount> expectedResult = new List<UserAccount>() 
            {
                new UserAccount(1, "First", "Administrator", "firstAdmin", "first@administrator.com", true),
                new UserAccount(2, "Care", "Giver", "Caregiver", "caregiver@Assisstability.com", true),
                new UserAccount(3, "Client", "Client", "Client", "client@Assisstability.com", true)
            };
            List<UserAccount> actualResult;

            // Act 
            actualResult = _userGroupManager.GetUserAccountsByUserGroup(arbitraryValidUserGroup);

            // Assert
            Assert.AreEqual(expectedResult.Count, actualResult.Count);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/18
        /// 
        /// Tests GetUserAccountsInUserGroupByRole method throws exception with bad group
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetUserAccountsInUserGroupByRoleThrowsExceptionBadGroup()
        {
            // Arrange
            BindingList<UserAccount> actualResult;
            IUserManager userManager = new UserManager(new UserFakes());

            // Act 
            actualResult = _userGroupManager.GetUserAccountsInUserGroupByRole(userManager, new UserGroup(-1, -1), "Admin");

            // Assert
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        /// 
        /// Tests GetUserGroupByGroupID method
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestGetUserGroupByGroupIDReturnsUserGroup()
        {
            // Arrange
            UserGroup expectedResult = new UserGroup(1, 1);
            UserGroup actualResult;

            // Act
            actualResult = _userGroupManager.GetUserGroupByGroupID(1);

            // Assert
            Assert.AreEqual(expectedResult.GroupID, actualResult.GroupID);
            Assert.AreEqual(expectedResult.UserID_Owner, actualResult.UserID_Owner);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/18
        /// 
        /// Tests GetUserAccountsInUserGroupByRole method throws exception with bad role
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetUserGroupByGroupIDThrowsExceptionBadGroupID()
        {
            // Arrange
            UserGroup actualResult;

            // Act 
            actualResult = _userGroupManager.GetUserGroupByGroupID(-1);

            // Assert
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/15
        /// 
        /// Tests AddNewUserToUserGroup method returns a boolean
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestAddNewUserToUserGroupReturnsBool()
        {
            // Arrange
            bool expectedResult = true;
            bool actualResult;
            UserAccount validUserAccount = new UserAccount(4, "Fake", "User", "Fake User", "fake@user.com", true);


            // Act
            actualResult = _userGroupManager.AddNewUserToUserGroup(validUserAccount, arbitraryValidUserGroup, "Client");

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/15
        /// 
        /// Tests AddNewUserToUserGroup method throws exception user already exists
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddNewUserToUserGroupThrowsExceptionUserAccountExists()
        {
            // Arrange
            bool actualResult;
            UserAccount invalidUserAccount = new UserAccount(3, "Client", "Client", "Client", "client@Assisstability.com", true);


            // Act
            actualResult = _userGroupManager.AddNewUserToUserGroup(invalidUserAccount, arbitraryValidUserGroup, "Client");

            // Assert
            // Expects Exception
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/15
        /// 
        /// Tests AddNewUserToUserGroup method throws exception with bad group
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddNewUserToUserGroupThrowsExceptionBadGroupID()
        {
            // Arrange
            bool actualResult;
            UserAccount validUserAccount = new UserAccount(4, "Fake", "User", "Fake User", "fake@user.com", true);


            // Act
            actualResult = _userGroupManager.AddNewUserToUserGroup(validUserAccount, new UserGroup(-1, -1), "Client");

            // Assert
            // Expects Exception
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/15
        /// 
        /// Tests AddNewUserToUserGroup method throws exception with bad group
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddNewUserToUserGroupThrowsExceptionBadRole()
        {
            // Arrange
            bool actualResult;
            UserAccount validUserAccount = new UserAccount(4, "Fake", "User", "Fake User", "fake@user.com", true);


            // Act
            actualResult = _userGroupManager.AddNewUserToUserGroup(validUserAccount, arbitraryValidUserGroup, "");

            // Assert
            // Expects Exception
        }
    }
}

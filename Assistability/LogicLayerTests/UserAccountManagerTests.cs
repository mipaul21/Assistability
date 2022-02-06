/// <summary>
/// William Clark
/// Created: 2021/02/04
///
/// Test class for the UserAccountManager
/// </summary>
///
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/12
/// Added TestInsertNewUserAccount Test Method
/// </remarks>
///
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/03/12
/// Added logout
/// </remarks>
///
/// <remarks>
/// Ryan Taylor
/// Updated: 2021/03/25
/// Added TestGetUserByUsername and TestResetPassword Test Methods
/// </remarks>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataAccessLayer;
using DataAccessFakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStorageModels;
using LogicLayerInterfaces;
using LogicLayer;
using DataViewModels;

namespace LogicLayerTests
{
    [TestClass]
    public class UserAccountManagerTests
    {

		 /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/16/2021
        /// Approver:
        /// Constructors
        ///
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        private IUserManager _userAccountManager;
        private UserFakes _fakeUserAccountAccessor;


        /// <summary>
        /// William Clark
        /// Created: 2021/02/18
        ///
        /// TestIntialize method that initializes an IUserAccountManager
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestInitialize]
        public void TestSetup()
        {
            // _userAccountManager = new UserManager(new UserAccessor());
			_fakeUserAccountAccessor = new UserFakes();
            _userAccountManager = new UserManager(_fakeUserAccountAccessor);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/18
        ///
        /// Tests SelectUserAccountByUserAccountID method with an arbitrary valid Id
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestGetUserAccountByUserAccountIDReturnsUserAccount()
        {
            // Arrange
            UserAccount expectedUserAccount = new UserAccount(1, "First", "Administrator", "firstAdmin", "first@administrator.com", true);
            UserAccount actualUserAccount;

            // Act
            actualUserAccount = _userAccountManager.GetUserAccountByUserAccountID(1);

            // Assert
            Assert.AreEqual(expectedUserAccount.UserAccountID, actualUserAccount.UserAccountID);
            Assert.AreEqual(expectedUserAccount.FirstName, actualUserAccount.FirstName);
            Assert.AreEqual(expectedUserAccount.LastName, actualUserAccount.LastName);
            Assert.AreEqual(expectedUserAccount.UserName, actualUserAccount.UserName);
            Assert.AreEqual(expectedUserAccount.Email, actualUserAccount.Email);
            Assert.AreEqual(expectedUserAccount.Active, actualUserAccount.Active);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/18
        ///
        /// Tests GetUserAccountByUserAccountID method throws exception
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetUserAccountByUserAccountIDThrowsException()
        {
            // Arrange
            UserAccount actualUserAccount;
            int invalidUserAccountID = -1;

            // Act
            actualUserAccount = _userAccountManager.GetUserAccountByUserAccountID(invalidUserAccountID);

            // Assert
            // Expects Exception
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/02/25
        ///
        /// Tests GetUserAccountVMByUserAccount method with an arbitrary valid UserAccount
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestGetUserAccountVMByUserAccountReturnsUserAccountVM()
        {
            // Arrange
            UserAccountVM expectedUserAccount = new UserAccountVM(new UserAccount(1, "First", "Administrator", "firstAdmin", "first@administrator.com", true), new List<MembershipVM>()
            {
                new MembershipVM(1, 1, new List<Role>()
                {
                    new Role(1, "Admin", "User Administrator")
                })

            });
            UserAccountVM actualUserAccount;

            // Act
            actualUserAccount = _userAccountManager.GetUserAccountVMByUserAccount((UserAccount)expectedUserAccount);

            // Assert
            Assert.AreEqual(expectedUserAccount.UserAccountID, actualUserAccount.UserAccountID);
            Assert.AreEqual(expectedUserAccount.FirstName, actualUserAccount.FirstName);
            Assert.AreEqual(expectedUserAccount.LastName, actualUserAccount.LastName);
            Assert.AreEqual(expectedUserAccount.UserName, actualUserAccount.UserName);
            Assert.AreEqual(expectedUserAccount.Email, actualUserAccount.Email);
            Assert.AreEqual(expectedUserAccount.Active, actualUserAccount.Active);
            Assert.AreEqual(expectedUserAccount.Memberships.Count, actualUserAccount.Memberships.Count);

        }

        /// <summary>
        /// William Clark
        /// Created: 2021/03/18
        ///
        /// Tests GetUserAccountVMByUserAccount method throws exception
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetUserAccountVMByUserAccountThrowsException()
        {
            // Arrange
            UserAccount invalidlUserAccount = new UserAccount(-1, "", "", "", "", true);
            UserAccountVM actualUserAccountVM;

            // Act
            actualUserAccountVM = _userAccountManager.GetUserAccountVMByUserAccount(invalidlUserAccount);

            // Assert
            // Expects Exception
        }


        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/16/2021
        /// Approver:
        /// Unit Test for Update User Account.
        ///
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        [TestMethod]
        public void TestUpdateUserAccount()
        {
            // arrange
            UserAccount oldUserAccount = new UserAccount
            (
                125,
               "Mitch",
               "Paul",
               "Mipaul",
               "mdpaul1997@gmail.com",
               true
            );

            UserAccount newUserAccount = new UserAccount
            (
                125,
                "Mitchy",
                "Ozzie",
                "MdPaul",
                "mdp@gmail.com",
                true

            );

            //act
            bool result = _userAccountManager.UpdateUserAccount(oldUserAccount, newUserAccount);

            //assert
            Assert.AreEqual(true, result);


        }

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/16/2021
        /// Approver:
        /// Unit Test for Reactivate User Account.
        ///
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        [TestMethod]
        public void TestReactivateUserAccount()
        {
            //Arrange

            UserAccount oldUserAccount = new UserAccount
            (
                2,
                "Mitchell",
                "Paul",
                "Mipaul",
                "mipaul@gmail.com",
                false
            );
            UserAccount newUserAccount = new UserAccount
            (
                2,
                "Mitchell",
                "Paul",
                "Mipaul",
                "mipaul@gmail.com",
                true
            );


            //act
            bool result = _userAccountManager.UpdateUserAccount(oldUserAccount, newUserAccount);


            Assert.AreEqual(true, result);
        }

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 2/16/2021
        /// Approver:
        /// Unit Test for Deactivate User Account.
        ///
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        [TestMethod]
        public void TestDeactivateUserAccount()
        {
            //Arrange

            UserAccount oldUserAccount = new UserAccount
            (
                2,
                "Mitchell",
                "Paul",
                "Mipaul",
                "mipaul@gmail.com",
                true
            );
            UserAccount newUserAccount = new UserAccount
            (
                2,
                "Mitchell",
                "Paul",
                "Mipaul",
                "mipaul@gmail.com",
                false
            );


            //act
            bool result = _userAccountManager.UpdateUserAccount(oldUserAccount, newUserAccount);


            Assert.AreEqual(true, result);
        }

		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/16
        ///
        /// This class is used to test the logic layer add user account methode.
        /// </summary>
        [TestMethod]
        public void TestInsertNewUserAccount()
        {
            //arrange
            UserAccount userAccount = new UserAccount()
            {
                UserAccountID = 0,
                FirstName = "Garry",
                LastName = "Oak",
                Email = "SmellYou@Later.com",
                Active = true,
                UserName = "ChampForASecond"
            };
            bool expectedResult = true;

            //act
            bool acualResult = _userAccountManager.AddNewUserAccount(userAccount, "TenBadges1996");

            //assert
            Assert.AreEqual(expectedResult, acualResult);
		}

        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 3/10/2021
        /// Approver:
        /// Unit Test for Delete User Account.
        ///
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        [TestMethod]
        public void TestDeleteUserAccount()
        {
            //Arrange
            UserAccount user = new UserAccount
                (
                 2,
                "Mitchell",
                "Paul",
                "Mipaul",
                "mipaul@gmail.com",
                false
                );

            //act
            bool result = _userAccountManager.DeleteUserAccount(user);
            // test
            Assert.AreEqual(true, result);
        }



        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 3/10/2021
        /// Approver:
        /// Unit Test for Delete User Account.
        ///
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        [TestMethod]
        public void TestDeleteActiveUserAccount()
        {
            //Arrange
            UserAccount user = new UserAccount
                (
                 2,
                "Mitchell",
                "Paul",
                "Mipaul",
                "mipaul@gmail.com",
                true
                );

            //act
            bool result = _userAccountManager.DeleteUserAccount(user);
            // test
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/03/11
        ///
        /// This is the method testing the LogoutUser method.
        /// It asserts the Active field will be false after the user logs out.
        /// </summary>
        [TestMethod]
        public void TestLogoutUser()
        {
            // Arrange
            UserAccount userAccount = new UserAccount
            (
                2,
                "Mitchell",
                "Paul",
                "Mipaul",
                "mipaul@gmail.com",
                true
            );
            UserAccount expectedUserAccount = new UserAccount();

            // Act
            UserAccount result = _userAccountManager.LogoutUser(userAccount);
            // Assert
            Assert.AreEqual(result.Active, false);
        }
          /// <summary>
          /// William Clark
          /// Created: 2021/02/25
          ///
          /// Tests GetUserAccountByUsername method with an arbitrary valid Username
          /// </summary>
          ///
          /// <remarks>
          /// </remarks>
          [TestMethod]
          public void TestGetUserAccountByUsernameReturnsUserAccount()
          {
             // Arrange
             UserAccount expectedUserAccount = new UserAccount(1, "First", "Administrator", "firstAdmin", "first@administrator.com", true);
             UserAccount actualUserAccount;

             // Act
             actualUserAccount = _userAccountManager.GetUserAccountByUsername("firstAdmin");

             // Assert
             Assert.AreEqual(expectedUserAccount.UserAccountID, actualUserAccount.UserAccountID);
             Assert.AreEqual(expectedUserAccount.FirstName, actualUserAccount.FirstName);
             Assert.AreEqual(expectedUserAccount.LastName, actualUserAccount.LastName);
             Assert.AreEqual(expectedUserAccount.UserName, actualUserAccount.UserName);
             Assert.AreEqual(expectedUserAccount.Email, actualUserAccount.Email);
             Assert.AreEqual(expectedUserAccount.Active, actualUserAccount.Active);

          }

          /// <summary>
          /// William Clark
          /// Created: 2021/03/18
          ///
          /// Tests GetUserAccountVMByUserAccount method throws exception
          /// </summary>
          ///
          /// <remarks>
          /// </remarks>
          [TestMethod]
          [ExpectedException(typeof(ApplicationException))]
          public void TestGetUserAccountByUsernameThrowsException()
          {
             // Arrange
             UserAccount actualUserAccount;

             // Act
             actualUserAccount = _userAccountManager.GetUserAccountByUsername("");

             // Assert
             // Expects Exception
          }

		/// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/25
        ///
        /// This class is used to test the logic layer for reseting a password.
        /// </summary>
        [TestMethod]
        public void TestResetPassword()
        {
            //arrange
            string email = "testa@test.com";
            const bool expectedResult = true;

            //act
            bool acualResult = _userAccountManager.ResetPassword(email);

            //assert
            Assert.AreEqual(expectedResult, acualResult);
        }

        /// <summary>
        /// Ryan Taylor
        /// Created: 2021/02/25
        ///
        /// This class is used to test the logic layer for selecting a user by
        /// their username.
        /// </summary>
        [TestMethod]
        public void TestSelectUserByUserName()
        {
            //arrange
            string username = "firstAdmin";
            const bool expectedResult = true;

            //act
            bool acualResult =
                _userAccountManager.GetUserAccountByUsername(username).Active;

            //assert
            Assert.AreEqual(expectedResult, acualResult);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/15
        ///
        /// Tests AddSubsidiaryUserAccount method with an arbitrary valid UserAccount
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestAddSubsidiaryUserAccountReturnsUserID()
        {
            // Arrange
            int expectedResult = 4;
            int actualResult;
            UserAccount validUserAccount = new UserAccount();

            // Act
            actualResult = _userAccountManager.AddSubsidiaryUserAccount(validUserAccount, "password");

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/15
        ///
        /// Tests AddSubsidiaryUserAccount method throws exception
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestAddSubsidiaryUserAccountThrowsException()
        {
            // Arrange
            int actualResult;
            UserAccount invalidUserAccount = new UserAccount(3, "Client", "Client", "Client", "client@Assisstability.com", true);

            // Act
            actualResult = _userAccountManager.AddSubsidiaryUserAccount(invalidUserAccount, "password");

            // Assert
            // Expects Exception
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/22
        ///
        /// Tests SelectUserAccountByEmail method with an arbitrary valid email
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        public void TestSelectUserAccountByEmailReturnsUserAccount()
        {
            // Arrange
            UserAccount expectedUserAccount = new UserAccount(1, "First", "Administrator", "firstAdmin", "first@administrator.com", true);
            UserAccount actualUserAccount;

            // Act
            actualUserAccount = _userAccountManager.SelectUserAccountByEmail("first@administrator.com");

            // Assert
            Assert.AreEqual(expectedUserAccount.UserAccountID, actualUserAccount.UserAccountID);
            Assert.AreEqual(expectedUserAccount.FirstName, actualUserAccount.FirstName);
            Assert.AreEqual(expectedUserAccount.LastName, actualUserAccount.LastName);
            Assert.AreEqual(expectedUserAccount.UserName, actualUserAccount.UserName);
            Assert.AreEqual(expectedUserAccount.Email, actualUserAccount.Email);
            Assert.AreEqual(expectedUserAccount.Active, actualUserAccount.Active);
        }

        /// <summary>
        /// William Clark
        /// Created: 2021/04/22
        ///
        /// Tests SelectUserAccountByEmail method throws exception
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectUserAccountByEmailThrowsException()
        {
            // Arrange
            UserAccount actualUserAccount;
            string invalidEmail = "";

            // Act
            actualUserAccount = _userAccountManager.SelectUserAccountByEmail(invalidEmail);

            // Assert
            // Expects Exception
        }
    }
}

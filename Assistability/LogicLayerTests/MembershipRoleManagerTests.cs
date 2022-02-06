using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccessFakes;
using DataAccessInterfaces;
using LogicInterfaces;
using LogicLayerInterfaces;
using LogicLayer;
using DataViewModels;
using DataStorageModels;

namespace LogicLayerTests
{
    [TestClass]
    public class MembershipRoleManagerTests
    {
        private IMembershipRoleManager _membershipRoleManager;
        private MembershipRoleFakes _membershipRoleFakes;

        [TestInitialize]
        public void TestSetUp()
        {
            _membershipRoleFakes = new MembershipRoleFakes();
        }



        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/04/02
        /// 
        /// This class is used to test the logic layer add Role.
        /// </summary>
        [TestMethod]
        public void TestInsertNewRole()
        {
            IMembershipRoleManager _membershipRoleManager = new MembershipRoleManager(_membershipRoleFakes);

            //arrange
            const int expectedResult = 1;

            Role oldRole = new Role() { RoleID = 1, Name = "Admin", Description = "Administration" };
            Role newRole = new Role() { RoleID = 1, Name = "Caregiver", Description = "Caregiving" };
            //act

            int actualResult = _membershipRoleManager.AddUserMembershipRole(1, 1, newRole.Name);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }



        /// <summary>
        /// Mitchell Paul
        /// Created: 2021/04/02
        /// 
        /// This class is used to test the logic layer remove Role.
        /// </summary>
        [TestMethod]
        public void TestDeleteOldRole()
        {
            IMembershipRoleManager _membershipRoleManager = new MembershipRoleManager(_membershipRoleFakes);

            //arrange
            const int expectedResult = 1;

            Role oldRole = new Role() { RoleID = 1, Name = "Admin", Description = "Administration" };
            Role newRole = new Role() { RoleID = 1, Name = "Caregiver", Description = "Caregiving" };
            //act

            int actualResult = _membershipRoleManager.DeleteUserMembershipRole(1, 1, oldRole.Name);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

    }
}

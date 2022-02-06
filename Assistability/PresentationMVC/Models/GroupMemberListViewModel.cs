/// <summary>
/// William Clark
/// Created: 2021/04/24
/// 
/// View Model for GroupMemberList include two lists of UserAccount objects
/// </summary>
///
/// <remarks>
/// </remarks>
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationMVC.Models
{
    public class GroupMemberListViewModel
    {
        public string FirstListRole { get; set; }
        public List<UserAccount> FirstList { get; set; }
        public string SecondListRole { get; set; }
        public List<UserAccount> SecondList { get; set; }

        public GroupMemberListViewModel()
        {
            FirstList = new List<UserAccount>();
            SecondList = new List<UserAccount>();
        }

        public GroupMemberListViewModel(string firstListRole, List<UserAccount> firstList, string secondListRole, List<UserAccount> secondList)
        {
            FirstListRole = firstListRole;
            FirstList = firstList;
            SecondListRole = secondListRole;
            SecondList = secondList;
        }
    }
}
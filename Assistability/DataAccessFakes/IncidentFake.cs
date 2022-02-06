/// <summary>
/// Nick Loesel
/// Created: 2021/02=3/18
/// 
/// Fake IncidentAccessor for testing
/// </summary>
///
/// <remarks>
/// </remarks>
using DataAccessInterfaces;
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Nick Loesel
    /// Created: 2021/03/18
    /// 
    /// This class creates a 'fake' incident to be used
    /// in the IncidentManager test class
    /// </summary>
    public class IncidentFake : IIncidentAccessor
    {
        private List<Incident> incidents = new List<Incident>();

        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/18
        /// 
        /// This is the Fake IncidentAccessor so I can test to see if its working
        /// </summary>
        public IncidentFake()
        {
            incidents.Add(new Incident()
            {
                IncidentName = "First Incident",
                IncidentDescription = "First Incident",
                DesiredConsequence = "No videogames for a week.",
                IncidentEntryDate = DateTime.Now,
                IncidentEditDate = null,
                IncidentRemovalDate = null,
                Active = true,
                UserId_Client = 3,
                UserId_Creator = 1

            });
            incidents.Add(new Incident() 
            {
                IncidentName = "Second Incident",
                IncidentDescription = "Second Incident",
                DesiredConsequence = "No videogames for 2 weeks.",
                IncidentEntryDate = DateTime.Now,
                IncidentEditDate = null,
                IncidentRemovalDate = null,
                Active = true,
                UserId_Client = 3,
                UserId_Creator = 1


            });
            incidents.Add(new Incident()
            {
                IncidentName = "Third Incident",
                IncidentDescription = "Third Incident",
                DesiredConsequence = "No videogames for 3 weeks.",
                IncidentEntryDate = DateTime.Now,
                IncidentEditDate = null,
                IncidentRemovalDate = null,
                Active = false,
                UserId_Client = 3,
                UserId_Creator = 1


            });

        }


        /// <summary>
        /// Nick Loesel
        /// Created: 2021/03/18
        /// 
        /// Tests the SelectIncidentByUserID Method
        /// </summary>
        /// 
        /// <param name="userID">The id of the user to select a list of incident </param>
        /// <exception>No incident Found</exception>
        /// <returns>A list of Incident objcts</returns>
        public List<Incident> SelectIncidentsByUserID(int userID)
        {
            List<Incident> newIncidentList = new List<Incident>();

            for (int i = 0; i < incidents.Count; i++)
            {
                foreach (var incident in incidents)
                {
                    if (incident.UserId_Client == userID)
                    {
                        newIncidentList.Add(incidents[i]);
                    }
                    break;
                }

            }
            return newIncidentList;
        }



        /// <summary>
        /// Creator: Mitchell Paul
        /// Created: 3/19/2021
        /// Approver: 
        /// 
        /// a fake data access method for testing UpdateIncident
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        /// </remarks>
        /// <returns>an int</returns>
        public int UpdateIncident(Incident oldIncident, Incident newIncident)
        {
            int rows = 1;
            return rows;
        }
		
		/// <summary>
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// tests the DeactivateIncident method
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="selectedUser">The selected user to change</param>
        /// <param name="incident">The selected incident to deactivate</param>
        /// <exception>Incident could not be updated.</exception>
        /// <returns>true if the incident was deactivated</returns>
        public bool DeactivateIncident(UserAccount selectedUser, Incident incident)
        {
            bool result = false;
                foreach (var name in incidents)
                {
                    if (incident.UserId_Client == selectedUser.UserAccountID && incident.IncidentName == name.IncidentName)
                    {
                        incident.Active = false;
                        if (incident.Active == false)
                        {
                            result = true;
                        }
                    }
                }
                return result;
        }
		
		/// <summary>
        /// Nick Loesel
        /// Created: 2021/03/18
        /// 
        /// Tests the InsertNewIncidentMethod
        /// </summary>
        /// 
        /// <param name="newIncident">The new incident object to add</param>
        /// <exception>No incident added</exception>
        /// <returns>True if the incident is added</returns>
        public bool InsertNewIncident(Incident newIncident)
        {
            incidents.Add(newIncident);

            if (incidents.Count == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
		
		/// <summary>
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// tests the ReactivateIncident method
        /// </summary>
        ///
        /// <remarks>
        /// </remarks>
        /// 
        /// <param name="selectedUser">The selected user to change</param>
        /// <param name="incident">The selected incident to deactivate</param>
        /// <exception>Incident could not be updated.</exception>
        /// <returns>true if the incident was reactivated</returns>
        public bool ReactivateIncident(UserAccount selectedUser, Incident incident)
        {
            bool result = false;
            foreach (var name in incidents)
            {
                if (incident.UserId_Client == selectedUser.UserAccountID && incident.IncidentName == name.IncidentName)
                {
                    incident.Active = true;
                    if (incident.Active == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
		
		/// <summary>
        /// Nick Loesel
        /// Created: 2021/03/25
        /// 
        /// Tests the SelectsIncidentsByActive method
        /// </summary>
        /// 
        /// <param name="selectedUser">The selected user</param>
        /// <param name="active">The active status to select by</param>
        /// <exception>No incident Found</exception>
        /// <returns>A list of active incidents</returns>

        public List<Incident> SelectIncidentsByActive(int selectedUser, bool active)
        {
            List<Incident> newIncidentList = new List<Incident>();
            for (int i = 0; i < incidents.Count;)
            {
                foreach (var incident in incidents)
                {
                    if (incident.UserId_Client == selectedUser && incident.Active == active)
                    {
                        newIncidentList.Add(incidents[i]);
                        i++;
                    }
                    i++;
                }
                break;

            }
            return newIncidentList;
        }
    }
}

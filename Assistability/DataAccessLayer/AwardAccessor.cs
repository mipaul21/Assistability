/// <summary>
/// Nathaniel Webber
/// Created: 2021/03/08
/// 
/// This class has the methods that will be used
/// Currently a shell
/// </summary>
/// <remarks>
/// Nathaniel Webber
/// 2021/03/11
/// Added Database functionality
/// </remarks>
/// 
/// <remarks>
/// Jory A. Wernette
/// Updated: 2021/04/29
/// Added function to get active and not active awards
/// and to reactivate awards
/// </remarks>

using DataAccessInterfaces;
using DataStorageModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class AwardAccessor : IAwardAccessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/23
        /// ///
        /// updated accessor to match updated  award scripts
        /// </remarks>
        /// <param name="awardName"></param>
        /// <param name="awardDescription"></param>
        /// <returns></returns>
        public int CreateNewAward(string awardName, string awardDescription)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_create_new_award", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AwardName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@AwardDescription", SqlDbType.NVarChar, 255);

            cmd.Parameters["@AwardName"].Value = awardName;
            cmd.Parameters["@AwardDescription"].Value = awardDescription;

            try
            {
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteNonQuery());

                if (result != 1)
                {
                    throw new ApplicationException("The '" + awardName + "' Award could not be added.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        
        public int DeleteAward(string awardName)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_delete_award", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AwardName", SqlDbType.NVarChar, 50);

            cmd.Parameters["@AwardName"].Value = awardName;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result != 1)
                {
                    throw new ApplicationException("The Award could not be deleted.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/29
        /// 
        /// This method grabs the corresponding stored procedure from the 
        /// sql to reactivate an award
        /// </summary>
        /// 
        /// 
        /// <param name="awardName"></param>
        /// <exception>No awards found</exception>
        /// <returns>rows affected</returns>
        public int ReactivateAwardByAwardName(string awardName)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_safely_reactivate_award", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AwardName", SqlDbType.NVarChar, 50);

            cmd.Parameters["@AwardName"].Value = awardName;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result != 1)
                {
                    throw new ApplicationException("The Award could not be reactivated.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/23
        /// ///
        /// updated accessor to work with changed award scripts
        /// </remarks>
        /// <param name="awardName"></param>
        /// <returns></returns>
        public int SafelyDeactivateAwardByAwardName(string awardName)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_safely_deactivate_award", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AwardName", SqlDbType.NVarChar, 50);

            cmd.Parameters["@AwardName"].Value = awardName;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
                if (result != 1)
                {
                    throw new ApplicationException("The Award could not be deactivated.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/23
        /// ///
        /// updated accessor to work with changed award scripts
        /// </remarks>
        /// <param name="active"></param>
        /// <returns></returns>
        public List<Award> SelectAllAwards(bool active = true)
        {
            List<Award> awards = new List<Award>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_awards", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = active;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        awards.Add(new Award()
                        {
                            AwardName = reader.GetString(0),
                            AwardDescription = reader.GetString(1),
                            Active = reader.GetBoolean(2)
                        }); 
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

            return awards;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/23
        /// ///
        /// updated accessor to work with changed award scripts, changed return type from List<Award> to Award
        /// </remarks>
        /// <param name="awardName"></param>
        /// <returns></returns>
        public Award SelectAwardByAwardName(string awardName)
        {
            Award award = new Award();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_award_by_award_name", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AwardName", SqlDbType.NVarChar, 50);
            cmd.Parameters["@AwardName"].Value = awardName;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        award.AwardName = reader.GetString(0);
                        award.AwardDescription = reader.GetString(1);
                        award.Active = reader.GetBoolean(2);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                conn.Close();
            }

            return award;
        }

        /// <summary>
        /// Jory A. Wernette
        /// Created: 2021/04/29
        /// 
        /// This method grabs the corresponding stored procedure from the 
        /// sql to help get all awards (active and not)
        /// </summary>
        /// 
        /// <exception>No awards found</exception>
        /// <returns>A List of award objects</returns>
        public List<Award> SelectEveryAward()
        {
            List<Award> awards = new List<Award>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_every_award", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        awards.Add(new Award()
                        {
                            AwardName = reader.GetString(0),
                            AwardDescription = reader.GetString(1),
                            Active = reader.GetBoolean(2)
                        });
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }

            return awards;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Updater Name: Becky Baenziger
        /// Update Date: 2021/04/23
        /// ///
        /// updated accessor to work with changed award scripts
        /// </remarks>
        /// <param name="newAward"></param>
        /// <param name="oldAward"></param>
        /// <returns></returns>
        public int UpdateAward(Award newAward, Award oldAward)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_award", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@AwardName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewAwardDescription", SqlDbType.NVarChar, 255);
            cmd.Parameters.Add("@OldAwardDescription", SqlDbType.NVarChar, 255);

            cmd.Parameters["@AwardName"].Value = newAward.AwardName;
            cmd.Parameters["@NewAwardDescription"].Value = newAward.AwardDescription;
            cmd.Parameters["@OldAwardDescription"].Value = oldAward.AwardDescription;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}

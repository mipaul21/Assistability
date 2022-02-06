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
    public class RewardAccessor : IRewardAccessor
    {
        public int CreateNewReward(int userID, string rewardName, string rewardDescription)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_create_new_reward", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters.Add("@RewardName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@RewardDescription", SqlDbType.NVarChar, 255);

            cmd.Parameters["@UserID"].Value = userID;
            cmd.Parameters["@RewardName"].Value = rewardName;
            cmd.Parameters["@RewardDescription"].Value = rewardDescription;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    throw new ApplicationException("The '" + rewardName + "' Reward could not be added.");
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

        public bool DeactivateReward(UserAccount selectedUser, Reward reward)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_deactivate_reward", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", selectedUser.UserAccountID);
            cmd.Parameters.AddWithValue("@RewardName", reward.RewardName);
            cmd.Parameters.AddWithValue("@RewardID", reward.RewardID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    throw new ApplicationException("incident could not be deactivated.");
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
            return result == 1;
        }

        public int EditReward(Reward oldReward, Reward newReward)
        {
            int result = 0;
            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_update_reward", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@RewardID", SqlDbType.Int);
            cmd.Parameters.Add("@OldUserID", SqlDbType.Int);

            cmd.Parameters.Add("@NewRewardName", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@NewRewardDescription", SqlDbType.NVarChar, 255);

            cmd.Parameters["@RewardID"].Value = oldReward.RewardID;
            cmd.Parameters["@OldUserID"].Value = oldReward.UserID;

            cmd.Parameters["@NewRewardName"].Value = newReward.RewardName;
            cmd.Parameters["@NewRewardDescription"].Value = newReward.RewardDescription;





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

        public bool ReactivateReward(UserAccount selectedUser, Reward reward)
        {
            int result = 0;

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_reactivate_reward", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", selectedUser.UserAccountID);
            cmd.Parameters.AddWithValue("@RewardName", reward.RewardName);
            cmd.Parameters.AddWithValue("@RewardID", reward.RewardID);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();

                if (result != 1)
                {
                    throw new ApplicationException("incident could not be deactivated.");
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
            return result == 1;
        }

        public List<Reward> SelectAllRewards(int userID)
        {
            List<Reward> rewards = new List<Reward>();

            var conn = DBConnection.GetDBConnection();
            var cmd = new SqlCommand("sp_select_all_rewards", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rewards.Add(new Reward()
                        {
                            RewardID = reader.GetInt32(0),
                            RewardName = reader.GetString(1),
                            RewardDescription = reader.GetString(2),
                            UserID = reader.GetInt32(3),
                            Active = reader.GetBoolean(4)
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
                conn.Close();
            }

            return rewards;
        }
    }
}

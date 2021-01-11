using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.DAL;
using C3Spectra.APPositioningApp.Entity.User;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.BAL
{
    public class UserBAL
    {
        public UserViewModel GetValidUser(UserViewModel model)
        {
            UserViewModel result = new UserViewModel();
            DBOperations dbOps = new DBOperations();
            try
            {

                NpgsqlParameter[] aParams = new NpgsqlParameter[2];



                aParams[0] = new NpgsqlParameter("_emailaddress", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[0].Value = model.EmailAddress;

                aParams[1] = new NpgsqlParameter("_password", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[1].Value = model.password;

                dbOps.ExecuteReader(AppConstants.USP_GETVALIDUSER, aParams);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            result = new UserViewModel
                            {
                                UserID = Helper.HandleDBNull<int>(dbOps.DataReader[0]),
                                RoleID = Helper.HandleDBNull<int>(dbOps.DataReader[1]),
                                EmailAddress = Helper.HandleDBNull<string>(dbOps.DataReader[2])
                            };
                        }
                    }
                }
                //Query to get User record and store it in result

            }
            catch (Exception Ex)
            {
                dbOps.Abort();
            }
            return result;
        }

        public Result UpdatePassword(UserViewModel model)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[2];
                aParams[0] = new NpgsqlParameter("Password", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[0].Value = model.newpassword;
                aParams[1] = new NpgsqlParameter("UserID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[1].Value = model.UserID;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.UpdatePassword, aParams);

                if (res > 0)
                {
                    result.Status = Status.Success;
                }
                else
                {
                    result.Status = Status.Failure;
                }
            }
            catch (Exception Ex)
            {
                dbOps.Abort();
                result.Status = Status.Failure;
            }
            return result;
        }


        public UserViewModel GetValidUserByEmail(UserViewModel model)
        {
            UserViewModel result = new UserViewModel();
            DBOperations dbOps = new DBOperations();
            try
            {

                NpgsqlParameter[] aParams = new NpgsqlParameter[1];



                aParams[0] = new NpgsqlParameter("_emailaddress", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[0].Value = model.EmailAddress;



                dbOps.ExecuteReader(AppConstants.USP_GETVALIDUSERBYEMAIL, aParams);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            result = new UserViewModel
                            {
                                UserID = Helper.HandleDBNull<int>(dbOps.DataReader[0]),
                                RoleID = Helper.HandleDBNull<int>(dbOps.DataReader[1]),
                                EmailAddress = Helper.HandleDBNull<string>(dbOps.DataReader[2]),
                                password = Helper.HandleDBNull<string>(dbOps.DataReader[3])
                            };
                        }
                    }
                }
                //Query to get User record and store it in result

            }
            catch (Exception Ex)
            {
                dbOps.Abort();
            }
            return result;
        }
    }
}

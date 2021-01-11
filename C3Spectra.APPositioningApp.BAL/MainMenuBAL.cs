using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.DAL;
using C3Spectra.APPositioningApp.Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.BAL
{
    public class MainMenuBAL
    {
        public List<MainMenuViewModel> GetMainMenus()
        {
            DBOperations dbOps = new DBOperations();
            List<MainMenuViewModel> objs = new List<MainMenuViewModel>();
            try
            {
                dbOps.ProcName = AppConstants.USP_GETMAINMENUS;
                dbOps.ExecuteReader();

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {

                        while (dbOps.DataReader.Read())
                        {
                            MainMenuViewModel obj = new MainMenuViewModel
                            {
                                MainMenuId = Helper.HandleDBNull<int>(dbOps.DataReader[0]),
                                Name = Helper.HandleDBNull<string>(dbOps.DataReader[1]),

                            };
                            objs.Add(obj);
                        }
                    }
                }
                return objs;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public MainMenuViewModel GetMainMenuByID(int _id)
        {
            DBOperations dbOps = new DBOperations();
            MainMenuViewModel obj = null;
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_id", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = _id;
                dbOps.ExecuteReader(AppConstants.USP_GETMAINMENUBYID, aParams);
                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            obj = new MainMenuViewModel
                            {
                                Name = Helper.HandleDBNull<string>(dbOps.DataReader[1]),
                                IsActive = Helper.HandleDBNull<Boolean>(dbOps.DataReader[2]),
                                IsDeleted = Helper.HandleDBNull<Boolean>(dbOps.DataReader[3]),
                                CreatedOn = Helper.HandleDBNull<DateTime>(dbOps.DataReader[4]),
                                CreatedBy = Helper.HandleDBNull<Int32>(dbOps.DataReader[5]),
                                LastModifiedOn = Helper.HandleDBNull<DateTime>(dbOps.DataReader[6]),
                                LastModifiedBy = Helper.HandleDBNull<Int32>(dbOps.DataReader[7]),
                                MainMenuId = Helper.HandleDBNull<int>(dbOps.DataReader[0])
                            };
                        }
                    }
                }
                return obj;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public Result AddMainMenu(MainMenuViewModel model)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[2];
                aParams[0] = new NpgsqlParameter("Name", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[0].Value = model.Name;
                aParams[1] = new NpgsqlParameter("CreatedBy", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[1].Value = model.CreatedBy;


                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.AddMainMenu, aParams);

                if (res > 0)
                {
                    result.Status = Status.Success;
                    result.Values = res.ToString();
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

        public Result UpdateMainMenu(MainMenuViewModel model)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[5];
                aParams[0] = new NpgsqlParameter("Name", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[0].Value = model.Name;
                aParams[1] = new NpgsqlParameter("LastModifiedBy", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[1].Value = 0;
                aParams[2] = new NpgsqlParameter("MainMenuId", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = model.MainMenuId;
                aParams[3] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[3].Value = DateTime.Now;
                aParams[4] = new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[4].Value = true;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.UpdateMainMenu, aParams);

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

        public Result DeleteMainMenu(int mainmenuID)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[5];
                aParams[0] = new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[0].Value = false;
                aParams[1] = new NpgsqlParameter("IsDeleted", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[1].Value = true;
                aParams[2] = new NpgsqlParameter("MainMenuId", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = mainmenuID;
                aParams[3] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[3].Value = DateTime.Now;
                aParams[4] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = 0;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.DeleteMainMenu, aParams);


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
    }
}

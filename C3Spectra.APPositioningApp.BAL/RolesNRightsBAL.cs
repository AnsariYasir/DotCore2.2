using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.DAL;
using C3Spectra.APPositioningApp.Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.BAL
{
    public class RolesNRightsBAL
    {
        public List<RolesNRightsViewModel> GetRolesNRights(int? roleID)
        {
            RolesNRightsViewModel obj;
            DBOperations dbOps = new DBOperations();
            List<RolesNRightsViewModel> objs = new List<RolesNRightsViewModel>();
            try
            {
                if (roleID.HasValue)
                {
                    NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                    aParams[0] = new NpgsqlParameter("roleid", NpgsqlTypes.NpgsqlDbType.Integer);
                    aParams[0].Value = roleID.Value;
                    dbOps.ExecuteReader(AppConstants.USP_GET_ROLES_N_RIGHTS_By_RoleID, aParams);
                }
                else
                {
                    dbOps.ProcName = AppConstants.USP_GET_ROLES_N_RIGHTS;
                    dbOps.ExecuteReader();
                }


                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {

                        while (dbOps.DataReader.Read())
                        {
                            if (roleID.HasValue)
                            {
                                obj = new RolesNRightsViewModel
                                {

                                    ActionID = Helper.HandleDBNull<int>(dbOps.DataReader[0]),
                                    ActionName = Helper.HandleDBNull<string>(dbOps.DataReader[1]),
                                    ControllerName = Helper.HandleDBNull<string>(dbOps.DataReader[3]),
                                    MainMenuName = Helper.HandleDBNull<string>(dbOps.DataReader[5]),
                                    RNRID = Helper.HandleDBNull<int>(dbOps.DataReader[6]),
                                    MainMenuID = Helper.HandleDBNull<int>(dbOps.DataReader[4]),
                                    IsActive = Helper.HandleDBNull<bool>(dbOps.DataReader[8]),

                                };
                            }
                            else
                            {
                                obj = new RolesNRightsViewModel
                                {
                                    ActionID = Helper.HandleDBNull<int>(dbOps.DataReader[0]),
                                    ActionName = Helper.HandleDBNull<string>(dbOps.DataReader[1]),
                                    ControllerName = Helper.HandleDBNull<string>(dbOps.DataReader[3]),
                                    MainMenuName = Helper.HandleDBNull<string>(dbOps.DataReader[5]),
                                    MainMenuID = Helper.HandleDBNull<int>(dbOps.DataReader[4]),
                                };
                            }
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

        public Result SaveRoleNRights(ManageRoleNRightsViewModel manageRoleNRightsViewModel)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                foreach (var item in manageRoleNRightsViewModel.RolesNRightsViewMdoels)
                {
                    int res;
                    NpgsqlParameter[] aParams1 = new NpgsqlParameter[1];
                    aParams1[0] = new NpgsqlParameter("RNRID", NpgsqlTypes.NpgsqlDbType.Integer);
                    aParams1[0].Value = item.RNRID;

                    long count = (long)dbOps.ExecuteScalar(AppConstants.QueryConstants.CheckRolesNRights, aParams1);
                    if (count > 0)
                    {
                        NpgsqlParameter[] aParams = new NpgsqlParameter[6];
                        aParams[0] = new NpgsqlParameter("RoleID", NpgsqlTypes.NpgsqlDbType.Integer);
                        aParams[0].Value = manageRoleNRightsViewModel.RoleID;
                        aParams[1] = new NpgsqlParameter("ActionID", NpgsqlTypes.NpgsqlDbType.Integer);
                        aParams[1].Value = item.ActionID;
                        aParams[2] = new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean);
                        aParams[2].Value = item.IsActive;
                        aParams[3] = new NpgsqlParameter("LastModifiedBy", NpgsqlTypes.NpgsqlDbType.Integer);
                        aParams[3].Value = 0;
                        aParams[4] = new NpgsqlParameter("LastModifiedOn", NpgsqlTypes.NpgsqlDbType.Date);
                        aParams[4].Value = DateTime.Now;
                        aParams[5] = new NpgsqlParameter("RNRID", NpgsqlTypes.NpgsqlDbType.Integer);
                        aParams[5].Value = item.RNRID;
                        res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.UpdateRolesNRights, aParams);
                    }
                    else
                    {
                        NpgsqlParameter[] aParams = new NpgsqlParameter[5];
                        aParams[0] = new NpgsqlParameter("RoleID", NpgsqlTypes.NpgsqlDbType.Integer);
                        aParams[0].Value = manageRoleNRightsViewModel.RoleID;
                        aParams[1] = new NpgsqlParameter("ActionID", NpgsqlTypes.NpgsqlDbType.Integer);
                        aParams[1].Value = item.ActionID;
                        aParams[2] = new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean);
                        aParams[2].Value = item.IsActive;
                        aParams[3] = new NpgsqlParameter("CreatedBy", NpgsqlTypes.NpgsqlDbType.Integer);
                        aParams[3].Value = 0;
                        aParams[4] = new NpgsqlParameter("CreatedOn", NpgsqlTypes.NpgsqlDbType.Date);
                        aParams[4].Value = DateTime.Now;
                        res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.AddRolesNRights, aParams);
                    }
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

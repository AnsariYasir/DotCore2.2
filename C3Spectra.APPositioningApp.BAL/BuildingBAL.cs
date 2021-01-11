using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.DAL;
using C3Spectra.APPositioningApp.Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.BAL
{
    public class BuildingBAL
    {

        public List<BuildingViewModel> GetBuildings()
        {
            DBOperations dbOps = new DBOperations();
            List<BuildingViewModel> objs = new List<BuildingViewModel>();
            try
            {
                dbOps.ProcName = AppConstants.USP_GETBUILDINGS;
                dbOps.ExecuteReader();

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {

                        while (dbOps.DataReader.Read())
                        {
                            BuildingViewModel obj = new BuildingViewModel
                            {
                                BuildingId = Helper.HandleDBNull<int>(dbOps.DataReader[8]),
                                Name = Helper.HandleDBNull<string>(dbOps.DataReader[0]),
                                Description = Helper.HandleDBNull<string>(dbOps.DataReader[1]),
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

        public Result AddBuilding(BuildingViewModel model)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {

                NpgsqlParameter[] aParams = new NpgsqlParameter[4];
                aParams[0] = new NpgsqlParameter("Name", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[0].Value = model.Name;
                aParams[1] = new NpgsqlParameter("Description", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[1].Value = model.Description;
                aParams[2] = new NpgsqlParameter("BuildingID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = model.BuildingId;
                aParams[3] = new NpgsqlParameter(AppConstants.CreatedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[3].Value = 0;


                int res = (int)dbOps.ExecuteScalar(AppConstants.QueryConstants.AddBuilding, aParams);

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

        public Result UpdateBuilding(BuildingViewModel model)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[6];
                aParams[0] = new NpgsqlParameter("Name", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[0].Value = model.Name;
                aParams[1] = new NpgsqlParameter("Description", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[1].Value = model.Description;
                aParams[2] = new NpgsqlParameter("BuildingID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = model.BuildingId;
                aParams[3] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[3].Value = DateTime.Now;
                aParams[4] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = 0;
                aParams[5] = new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[5].Value = true;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.UpdateBuilding, aParams);

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

        public Result DeleteBuilding(int buildingID)
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
                aParams[2] = new NpgsqlParameter("BuildingID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = buildingID;
                aParams[3] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[3].Value = DateTime.Now;
                aParams[4] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = 0;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.DeleteBuilding, aParams);


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

        public BuildingViewModel GetBuildingByID(int _id)
        {
            DBOperations dbOps = new DBOperations();
            BuildingViewModel obj = null;
            try
            {


                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_id", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = _id;

                dbOps.ExecuteReader(AppConstants.USP_GETBUILDINGBYID, aParams);



                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            obj = new BuildingViewModel
                            {

                                Name = Helper.HandleDBNull<string>(dbOps.DataReader[0]),
                                Description = Helper.HandleDBNull<string>(dbOps.DataReader[1]),
                                IsActive = Helper.HandleDBNull<Boolean>(dbOps.DataReader[2]),
                                IsDeleted = Helper.HandleDBNull<Boolean>(dbOps.DataReader[3]),
                                CreatedOn = Helper.HandleDBNull<DateTime>(dbOps.DataReader[4]),
                                CreatedBy = Helper.HandleDBNull<Int32>(dbOps.DataReader[5]),
                                LastModifiedOn = Helper.HandleDBNull<DateTime>(dbOps.DataReader[6]),
                                LastModifiedBy = Helper.HandleDBNull<Int32>(dbOps.DataReader[7]),
                                BuildingId = Helper.HandleDBNull<int>(dbOps.DataReader[8])
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
    }
}

using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.DAL;
using C3Spectra.APPositioningApp.Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.BAL
{
    public class FloorBAL
    {
        public Result AddFloor(FloorViewModel model)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {

                NpgsqlParameter[] aParams = new NpgsqlParameter[9];
                aParams[0] = new NpgsqlParameter("Name", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[0].Value = model.FloorName;
                aParams[1] = new NpgsqlParameter("Description", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[1].Value = model.Description;
                aParams[2] = new NpgsqlParameter("FloorPlanSHPPath", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[2].Value = model.FloorPlanSHPPath;
                aParams[3] = new NpgsqlParameter("BuildingID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[3].Value = model.BuildingID;
                aParams[4] = new NpgsqlParameter("FloorNo", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = model.FloorNo;
                aParams[5] = new NpgsqlParameter("FloorPlanOrginalFileName", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[5].Value = model.FloorPlanOrginalFileName;
                aParams[6] = new NpgsqlParameter("Lat", NpgsqlTypes.NpgsqlDbType.Double);
                aParams[6].Value = model.Latitude;

                aParams[7] = new NpgsqlParameter("Long", NpgsqlTypes.NpgsqlDbType.Double);
                aParams[7].Value = model.Longitude;
                aParams[8] = new NpgsqlParameter(AppConstants.CreatedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[8].Value = 0;


                int res = (int)dbOps.ExecuteScalar(AppConstants.QueryConstants.AddFloor, aParams);

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

        public Result UpdateFloor(FloorViewModel model)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[12];
                aParams[0] = new NpgsqlParameter("Name", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[0].Value = model.FloorName;
                aParams[1] = new NpgsqlParameter("Description", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[1].Value = model.Description;
                aParams[2] = new NpgsqlParameter("FloorPlanSHPPath", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[2].Value = model.FloorPlanSHPPath;
                aParams[3] = new NpgsqlParameter("BuildingID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[3].Value = model.BuildingID;
                aParams[4] = new NpgsqlParameter("FloorNo", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = model.FloorNo;
                aParams[5] = new NpgsqlParameter("FloorPlanOrginalFileName", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[5].Value = model.FloorPlanOrginalFileName;
                aParams[6] = new NpgsqlParameter("FloorID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[6].Value = model.FloorID;
                aParams[7] = new NpgsqlParameter("Lat", NpgsqlTypes.NpgsqlDbType.Double);
                aParams[7].Value = model.Latitude;

                aParams[8] = new NpgsqlParameter("Long", NpgsqlTypes.NpgsqlDbType.Double);
                aParams[8].Value = model.Longitude;

                aParams[9] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[9].Value = DateTime.Now;
                aParams[10] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[10].Value = 0;

                aParams[11] = new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[11].Value = true;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.UpdateFloor, aParams);

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

        public Result DeleteFloor(int floorID)
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
                aParams[2] = new NpgsqlParameter("FloorID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = floorID;
                aParams[3] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[3].Value = DateTime.Now;
                aParams[4] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = 0;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.DeleteFloor, aParams);

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

        public FloorViewModel GetFloorByID(int _id)
        {
            DBOperations dbOps = new DBOperations();
            FloorViewModel obj = null;
            try
            {
                //dbOps.ProcName = AppConstants.USP_GETINSTALLATIONPARAMETERSBYID;

                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_id", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = _id;

                dbOps.ExecuteReader(AppConstants.USP_GETFLOORSBYID, aParams);

                //dbOps.ExecuteReader(_id);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            obj = new FloorViewModel
                            {
                                FloorID = Helper.HandleDBNull<int>(dbOps.DataReader[0]),
                                FloorName = Helper.HandleDBNull<string>(dbOps.DataReader[1]),
                                FloorPlanSHPPath = Helper.HandleDBNull<string>(dbOps.DataReader[2]),
                                FloorNo = Helper.HandleDBNull<int>(dbOps.DataReader[3]),
                                FloorPlanOrginalFileName = Helper.HandleDBNull<string>(dbOps.DataReader[4]),
                                Latitude = Helper.HandleDBNull<double>(dbOps.DataReader[5]),
                                Longitude = Helper.HandleDBNull<double>(dbOps.DataReader[6]),
                                Description = Helper.HandleDBNull<string>(dbOps.DataReader[7]),
                                BuildingID = Helper.HandleDBNull<int>(dbOps.DataReader[8])
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

        public List<FloorViewModel> GetFloorsForIndoor()
        {
            DBOperations dbOps = new DBOperations();
            List<FloorViewModel> objs = new List<FloorViewModel>();
            try
            {
                dbOps.ProcName = AppConstants.USP_GETFLOORSFORINDOOR;
                dbOps.ExecuteReader();

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {

                        while (dbOps.DataReader.Read())
                        {
                            FloorViewModel obj = new FloorViewModel
                            {
                                FloorID = Helper.HandleDBNull<int>(dbOps.DataReader[0]),
                                FloorName = Helper.HandleDBNull<string>(dbOps.DataReader[1]),
                                FloorPlanSHPPath = Helper.HandleDBNull<string>(dbOps.DataReader[2]),
                                FloorNo = Helper.HandleDBNull<int>(dbOps.DataReader[3]),
                                FloorPlanOrginalFileName = Helper.HandleDBNull<string>(dbOps.DataReader[4]),
                                Latitude = Helper.HandleDBNull<double>(dbOps.DataReader[5]),
                                Longitude = Helper.HandleDBNull<double>(dbOps.DataReader[6]),
                                Description = Helper.HandleDBNull<string>(dbOps.DataReader[7]),
                                BuildingID = Helper.HandleDBNull<int>(dbOps.DataReader[8])

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

        public List<FloorViewModel> GetFloorsByBuildingID(int _id)
        {
            DBOperations dbOps = new DBOperations();
            List<FloorViewModel> objs = new List<FloorViewModel>();
            try
            {
                //dbOps.ProcName = AppConstants.USP_GETINSTALLATIONPARAMETERSBYID;

                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_id", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = _id;

                dbOps.ExecuteReader(AppConstants.USP_GETFLOORSBYBUILDINGID, aParams);

                //dbOps.ExecuteReader(_id);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            FloorViewModel obj = new FloorViewModel
                            {
                                FloorID = Helper.HandleDBNull<int>(dbOps.DataReader[0]),
                                FloorName = Helper.HandleDBNull<string>(dbOps.DataReader[1]),
                                FloorPlanSHPPath = Helper.HandleDBNull<string>(dbOps.DataReader[2]),
                                FloorNo = Helper.HandleDBNull<int>(dbOps.DataReader[3]),
                                FloorPlanOrginalFileName = Helper.HandleDBNull<string>(dbOps.DataReader[4]),
                                Latitude = Helper.HandleDBNull<double>(dbOps.DataReader[5]),
                                Longitude = Helper.HandleDBNull<double>(dbOps.DataReader[6]),
                                Description = Helper.HandleDBNull<string>(dbOps.DataReader[7]),
                                BuildingID = Helper.HandleDBNull<int>(dbOps.DataReader[8])

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
    }
}

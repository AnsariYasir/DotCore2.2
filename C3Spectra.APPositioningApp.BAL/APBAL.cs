using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.DAL;
using C3Spectra.APPositioningApp.Entity;
using C3Spectra.APPositioningApp.Entity.AP;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.BAL
{
    public class APBAL
    {
        public Result AddAP(APViewModel model, bool isOutdoor = false)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams;
                if (!isOutdoor)
                {
                    aParams = new NpgsqlParameter[11];
                }
                else
                {
                    aParams = new NpgsqlParameter[6];
                }

                aParams[0] = new NpgsqlParameter("Name", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[0].Value = model.Name;

                aParams[1] = new NpgsqlParameter("Lat", NpgsqlTypes.NpgsqlDbType.Double);
                aParams[1].Value = model.Lat;

                aParams[2] = new NpgsqlParameter("Long", NpgsqlTypes.NpgsqlDbType.Double);
                aParams[2].Value = model.Long;

                aParams[3] = new NpgsqlParameter("Description", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[3].Value = model.Description;


                aParams[4] = new NpgsqlParameter(AppConstants.CreatedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = 0;



                if (!isOutdoor)
                {
                    aParams[5] = new NpgsqlParameter("FloorID", NpgsqlTypes.NpgsqlDbType.Integer);
                    aParams[5].Value = model.FloorID;
                    aParams[6] = new NpgsqlParameter("APTypeID", NpgsqlTypes.NpgsqlDbType.Integer);
                    aParams[6].Value = model.APTypeID;
                    aParams[7] = new NpgsqlParameter("GroupID", NpgsqlTypes.NpgsqlDbType.Integer);
                    aParams[7].Value = model.GroupID;
                    aParams[8] = new NpgsqlParameter("IsIndoor", NpgsqlTypes.NpgsqlDbType.Boolean);
                    aParams[8].Value = true;
                    aParams[9] = new NpgsqlParameter("APimagepath", NpgsqlTypes.NpgsqlDbType.Varchar);
                    aParams[9].Value = model.ImageSHPPath;
                    aParams[10] = new NpgsqlParameter("SerialNo", NpgsqlTypes.NpgsqlDbType.Varchar);
                    aParams[10].Value = model.SerialNo;
                }
                if (isOutdoor)
                {
                    aParams[5] = new NpgsqlParameter("APImagePath", NpgsqlTypes.NpgsqlDbType.Varchar);
                    aParams[5].Value = model.ImageSHPPath;
                }

             
                int res = 0;
                if (!isOutdoor)
                {
                    res = (int)dbOps.ExecuteScalar(AppConstants.QueryConstants.AddAP, aParams);
                }
                else
                {
                    res = (int)dbOps.ExecuteScalar(AppConstants.QueryConstants.AddAPForOutdoor, aParams);
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
            catch (Exception Ex)
            {
                dbOps.Abort();
                result.Status = Status.Failure;
            }
            return result;
        }



        public Result UpdateAP(APViewModel model, bool isOutdoor = false)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams;
                if (!isOutdoor)
                {
                    aParams = new NpgsqlParameter[14];
                }
                else
                {
                    aParams = new NpgsqlParameter[9];
                }



                aParams[0] = new NpgsqlParameter("Name", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[0].Value = model.Name;



                aParams[1] = new NpgsqlParameter("Lat", NpgsqlTypes.NpgsqlDbType.Double);
                aParams[1].Value = model.Lat;



                aParams[2] = new NpgsqlParameter("Long", NpgsqlTypes.NpgsqlDbType.Double);
                aParams[2].Value = model.Long;



                aParams[3] = new NpgsqlParameter("Description", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[3].Value = model.Description;



                aParams[4] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[4].Value = DateTime.Now;



                aParams[5] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[5].Value = 0;



                aParams[6] = new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[6].Value = true;



                if (isOutdoor)
                {
                    aParams[7] = new NpgsqlParameter("APImagePath", NpgsqlTypes.NpgsqlDbType.Varchar);
                    aParams[7].Value = model.ImageSHPPath;
                }
                int i = 8;
                if (!isOutdoor)
                {
                   // aParams[8] = new NpgsqlParameter("FloorID", NpgsqlTypes.NpgsqlDbType.Integer);
                   // aParams[8].Value = model.FloorID;

                    aParams[7] = new NpgsqlParameter("FloorID", NpgsqlTypes.NpgsqlDbType.Integer);
                    aParams[7].Value = model.FloorID;
                    aParams[8] = new NpgsqlParameter("APTypeID", NpgsqlTypes.NpgsqlDbType.Integer);
                    aParams[8].Value = model.APTypeID;
                    aParams[9] = new NpgsqlParameter("GroupID", NpgsqlTypes.NpgsqlDbType.Integer);
                    aParams[9].Value = model.GroupID;
                    aParams[10] = new NpgsqlParameter("IsIndoor", NpgsqlTypes.NpgsqlDbType.Boolean);
                    aParams[10].Value = true;
                    aParams[11] = new NpgsqlParameter("APimagepath", NpgsqlTypes.NpgsqlDbType.Varchar);
                    aParams[11].Value = model.ImageSHPPath;
                    aParams[12] = new NpgsqlParameter("SerialNo", NpgsqlTypes.NpgsqlDbType.Varchar);
                    aParams[12].Value = model.SerialNo;

                    i = 13;
                }



                aParams[i] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[i].Value = model.APID;
                int res = 0;
                if (!isOutdoor)
                {
                    res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.UpdateAP, aParams);
                }
                else
                {
                    res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.UpdateAPForOutdoor, aParams);
                }



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
        public Result DeleteAP(int apID)
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
                aParams[2] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = apID;
                aParams[3] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[3].Value = DateTime.Now;
                aParams[4] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = 0;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.DeleteAP, aParams);

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

        public Result DeleteAPSector(int apID)
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
                aParams[2] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = apID;
                aParams[3] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[3].Value = DateTime.Now;
                aParams[4] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = 0;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.DeleteAPSector, aParams);

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

        public APViewModel GetAPByID(int _id)
        {
            DBOperations dbOps = new DBOperations();
            APViewModel obj = null;
            try
            {
                //dbOps.ProcName = AppConstants.USP_GETINSTALLATIONPARAMETERSBYID;

                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_id", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = _id;

                dbOps.ExecuteReader(AppConstants.USP_GETAPBYID, aParams);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            obj = new APViewModel
                            {
                                APID = Helper.HandleDBNull<Int32>(dbOps.DataReader[0]),
                                Name = Helper.HandleDBNull<String>(dbOps.DataReader[1]),
                                Lat = Helper.HandleDBNull<double>(dbOps.DataReader[2]),
                                Long = Helper.HandleDBNull<double>(dbOps.DataReader[3]),
                                Description = Helper.HandleDBNull<String>(dbOps.DataReader[4]),
                                FloorID = Helper.HandleDBNull<int>(dbOps.DataReader[5]),
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

        public List<APViewModel> GetAPByFloorID(int floorID)
        {
            DBOperations dbOps = new DBOperations();
            List<APViewModel> objs = new List<APViewModel>();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_floorid", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = floorID;

                dbOps.ExecuteReader(AppConstants.usp_GETAPSBYFLOORID, aParams);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            APViewModel obj = new APViewModel
                            {
                                APID = Helper.HandleDBNull<Int32>(dbOps.DataReader[0]),
                                Name = Helper.HandleDBNull<String>(dbOps.DataReader[1]),
                                Lat = Helper.HandleDBNull<double>(dbOps.DataReader[2]),
                                Long = Helper.HandleDBNull<double>(dbOps.DataReader[3]),
                                Description = Helper.HandleDBNull<String>(dbOps.DataReader[4]),
                                FloorID = Helper.HandleDBNull<int>(dbOps.DataReader[5]),
                                ImageSHPPath = Helper.HandleDBNull<String>(dbOps.DataReader[6]),
                                APTypeID= Helper.HandleDBNull<int>(dbOps.DataReader[7]),
                                GroupID = Helper.HandleDBNull<int>(dbOps.DataReader[8]),
                                 SerialNo= Helper.HandleDBNull<string>(dbOps.DataReader[9]),
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

        public List<APViewModel> GetAPByOutdoorMasterID(int? floorID)
        {
            DBOperations dbOps = new DBOperations();
            List<APViewModel> objs = new List<APViewModel>();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_outdoormasterid", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = floorID;

                dbOps.ExecuteReader(AppConstants.usp_GETAPSBYOutdoorMasterID, aParams);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            APViewModel obj = new APViewModel
                            {
                                APID = Helper.HandleDBNull<Int32>(dbOps.DataReader[0]),
                                Name = Helper.HandleDBNull<String>(dbOps.DataReader[1]),
                                Lat = Helper.HandleDBNull<double>(dbOps.DataReader[2]),
                                Long = Helper.HandleDBNull<double>(dbOps.DataReader[3]),
                                Description = Helper.HandleDBNull<String>(dbOps.DataReader[4]),
                                ImageSHPPath = Helper.HandleDBNull<String>(dbOps.DataReader[5]),
                                listAPSector = GetAPSectorByOutdoorAPID(Helper.HandleDBNull<Int32>(dbOps.DataReader[0]))
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

        //public Result AddAPSector(APSectorViewModel model, bool isOutdoor = false)
        //{
        //    Result result = new Result();
        //    DBOperations dbOps = new DBOperations();
        //    try
        //    {
        //        NpgsqlParameter[] aParams;
        //        if (!isOutdoor)
        //        {
        //            aParams = new NpgsqlParameter[6];
        //        }
        //        else
        //        {
        //            aParams = new NpgsqlParameter[5];
        //        }


        //        aParams[0] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
        //        aParams[0].Value = model.APId;

        //        aParams[1] = new NpgsqlParameter("SectorNumber", NpgsqlTypes.NpgsqlDbType.Integer);
        //        aParams[1].Value = model.SectorNumber;

        //        aParams[2] = new NpgsqlParameter("SectorValue", NpgsqlTypes.NpgsqlDbType.Varchar);
        //        aParams[2].Value = model.SectorValue;

        //        aParams[3] = new NpgsqlParameter("SerialNo", NpgsqlTypes.NpgsqlDbType.Varchar);
        //        aParams[3].Value = model.SerialNumber;

        //        aParams[4] = new NpgsqlParameter(AppConstants.CreatedBy, NpgsqlTypes.NpgsqlDbType.Integer);
        //        aParams[4].Value = 0;

        //        //if (!isOutdoor)
        //        //{
        //        //    aParams[5] = new NpgsqlParameter("FloorID", NpgsqlTypes.NpgsqlDbType.Integer);
        //        //    aParams[5].Value = model.FloorID;
        //        //}

        //        int res = 0;
        //        if (!isOutdoor)
        //        {
        //            res = (int)dbOps.ExecuteNonQuery(AppConstants.QueryConstants.AddAP, aParams);
        //        }
        //        else
        //        {
        //            res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.AddAPSector, aParams);
        //        }


        //        if (res > 0)
        //        {
        //            result.Status = Status.Success;
        //        }
        //        else
        //        {
        //            result.Status = Status.Failure;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        dbOps.Abort();
        //        result.Status = Status.Failure;
        //    }
        //    return result;
        //}
        public Result AddAPSector(APSectorViewModel model, bool isOutdoor = false)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams;
                if (!isOutdoor)
                {
                    aParams = new NpgsqlParameter[6];
                }
                else
                {
                    aParams = new NpgsqlParameter[5];
                }




                aParams[0] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = model.APId;



                aParams[1] = new NpgsqlParameter("SectorNumber", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[1].Value = model.SectorNumber;



                aParams[2] = new NpgsqlParameter("SectorValue", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[2].Value = model.SectorValue;



                aParams[3] = new NpgsqlParameter("SerialNo", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[3].Value = model.SerialNumber;



                aParams[4] = new NpgsqlParameter(AppConstants.CreatedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = 0;



                //if (!isOutdoor)
                //{
                //    aParams[5] = new NpgsqlParameter("FloorID", NpgsqlTypes.NpgsqlDbType.Integer);
                //    aParams[5].Value = model.FloorID;
                //}



                int res = 0;
                if (!isOutdoor)
                {
                    res = (int)dbOps.ExecuteNonQuery(AppConstants.QueryConstants.AddAPSector, aParams);
                }
                else
                {
                    res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.AddAPSector, aParams);
                }




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

        public Result UpdateAPSector(APSectorViewModel model, bool isOutdoor = false)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams;
                if (!isOutdoor)
                {
                    aParams = new NpgsqlParameter[6];
                }
                else
                {
                    aParams = new NpgsqlParameter[8];
                }

                aParams[0] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = model.APId;


                aParams[1] = new NpgsqlParameter("SectorNumber", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[1].Value = model.SectorNumber;

                aParams[2] = new NpgsqlParameter("SectorValue", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[2].Value = model.SectorValue;


                aParams[3] = new NpgsqlParameter("SerialNo", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[3].Value = model.SerialNumber;

                aParams[4] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[4].Value = DateTime.Now;

                aParams[5] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[5].Value = 0;

                aParams[6] = new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[6].Value = true;

                int i = 7;
                if (!isOutdoor)
                {
                    //aParams[7] = new NpgsqlParameter("FloorID", NpgsqlTypes.NpgsqlDbType.Integer);
                    //aParams[7].Value = model.FloorID;
                    //i = 8;
                }

                aParams[i] = new NpgsqlParameter("APSectorId", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[i].Value = model.APSectorId;
                int res = 0;
                if (!isOutdoor)
                {
                    res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.UpdateAPSector, aParams);
                }
                else
                {
                    res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.UpdateAPSector, aParams);
                }

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



        //public Result DeleteAPSector(int apID)
        //{
        //    Result result = new Result();
        //    DBOperations dbOps = new DBOperations();
        //    try
        //    {
        //        NpgsqlParameter[] aParams = new NpgsqlParameter[5];
        //        aParams[0] = new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean);
        //        aParams[0].Value = false;
        //        aParams[1] = new NpgsqlParameter("IsDeleted", NpgsqlTypes.NpgsqlDbType.Boolean);
        //        aParams[1].Value = true;
        //        aParams[2] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
        //        aParams[2].Value = apID;
        //        aParams[3] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
        //        aParams[3].Value = DateTime.Now;
        //        aParams[4] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
        //        aParams[4].Value = 0;


        //        int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.DeleteAPSector, aParams);


        //        if (res > 0)
        //        {
        //            result.Status = Status.Success;
        //        }
        //        else
        //        {
        //            result.Status = Status.Failure;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        dbOps.Abort();
        //        result.Status = Status.Failure;
        //    }
        //    return result;
        //}


        public Result ReduceAPSector(int apID,string apSectorID)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[6];
                aParams[0] = new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[0].Value = false;
                aParams[1] = new NpgsqlParameter("IsDeleted", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[1].Value = true;
                aParams[2] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = apID;
                aParams[3] = new NpgsqlParameter("APSectorID", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[3].Value = apSectorID;
                aParams[4] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[4].Value = DateTime.Now;
                aParams[5] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[5].Value = 0;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.ReduceAPSector, aParams);


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




        public List<APSectorViewModel> GetAPSectorByOutdoorAPID(int? apID)
        {
            DBOperations dbOps = new DBOperations();
            List<APSectorViewModel> objs = new List<APSectorViewModel>();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_outdoorapid", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = apID;

                dbOps.ExecuteReader(AppConstants.usp_GetAPSectorByOutdoorAPID, aParams);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            APSectorViewModel obj = new APSectorViewModel
                            {
                                APSectorId = Helper.HandleDBNull<Int32>(dbOps.DataReader[0]),
                                APId = Helper.HandleDBNull<Int32>(dbOps.DataReader[1]),
                                SectorNumber = Helper.HandleDBNull<Int32>(dbOps.DataReader[2]),
                                SectorValue = Helper.HandleDBNull<String>(dbOps.DataReader[3]),
                                SerialNumber = Helper.HandleDBNull<String>(dbOps.DataReader[4])
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

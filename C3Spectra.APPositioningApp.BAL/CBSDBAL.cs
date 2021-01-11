using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.DAL;
using C3Spectra.APPositioningApp.Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.BAL
{
    public class CBSDBAL
    {
        public Result AddCBSD(CBSDViewModel model)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {

                NpgsqlParameter[] aParams = new NpgsqlParameter[7];
                aParams[0] = new NpgsqlParameter("CBSDVendorModelID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = model.CBSDVendorModelID;

                aParams[1] = new NpgsqlParameter("SoftwareVersion", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[1].Value = model.SoftwareVersion;
                aParams[2] = new NpgsqlParameter("HardwareVersion", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[2].Value = model.HardwareVersion;
                aParams[3] = new NpgsqlParameter("FirmwareVersion", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[3].Value = model.FirmwareVersion;
                aParams[4] = new NpgsqlParameter("IsSubmitted", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[4].Value = model.IsSubmitted;
                aParams[5] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[5].Value = model.APID;
                aParams[6] = new NpgsqlParameter(AppConstants.CreatedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[6].Value = 0;

                int res = (int)dbOps.ExecuteScalar(AppConstants.QueryConstants.AddCBSD, aParams);

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



        public Result UpdateCBSD(CBSDViewModel model)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[10];
                aParams[0] = new NpgsqlParameter("CBSDVendorModelID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = model.CBSDVendorModelID;
                aParams[1] = new NpgsqlParameter("SoftwareVersion", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[1].Value = model.SoftwareVersion;
                aParams[2] = new NpgsqlParameter("HardwareVersion", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[2].Value = model.HardwareVersion;
                aParams[3] = new NpgsqlParameter("FirmwareVersion", NpgsqlTypes.NpgsqlDbType.Varchar);
                aParams[3].Value = model.FirmwareVersion;
                aParams[4] = new NpgsqlParameter("IsSubmitted", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[4].Value = model.IsSubmitted;
                aParams[5] = new NpgsqlParameter("CBSDID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[5].Value = model.CBSDID;
                aParams[6] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[6].Value = model.APID;
                aParams[7] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[7].Value = DateTime.Now;
                aParams[8] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[8].Value = 0;
                aParams[9] = new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[9].Value = true;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.UpdateCBSD, aParams);

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


        public Result DeleteCBSD(int cbsdID)
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
                aParams[2] = new NpgsqlParameter("CBSDID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = cbsdID;
                aParams[3] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[3].Value = DateTime.Now;
                aParams[4] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = 0;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.DeleteCBSD, aParams);

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

        public CBSDViewModel GetCBSDByID(int _id)
        {
            DBOperations dbOps = new DBOperations();
            CBSDViewModel obj = null;
            try
            {
                //dbOps.ProcName = AppConstants.USP_GETINSTALLATIONPARAMETERSBYID;

                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_id", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = _id;

                dbOps.ExecuteReader(AppConstants.USP_GETCBSDBYID, aParams);

                //dbOps.ExecuteReader(_id);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            obj = new CBSDViewModel
                            {

                                SoftwareVersion = Helper.HandleDBNull<string>(dbOps.DataReader[0]),
                                HardwareVersion = Helper.HandleDBNull<string>(dbOps.DataReader[1]),
                                FirmwareVersion = Helper.HandleDBNull<string>(dbOps.DataReader[2]),
                                CBSDID = Helper.HandleDBNull<int>(dbOps.DataReader[3]),
                                IsActive = Helper.HandleDBNull<bool>(dbOps.DataReader[4]),
                                IsDeleted = Helper.HandleDBNull<bool>(dbOps.DataReader[5]),
                                IsSubmitted = Helper.HandleDBNull<bool>(dbOps.DataReader[6]),
                                CBSDVendorModelID = Helper.HandleDBNull<int>(dbOps.DataReader[7]),

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

        public CBSDViewModel GetCBSDByAPID(int apid)
        {
            DBOperations dbOps = new DBOperations();
            CBSDViewModel obj = null;
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_apid", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = apid;

                dbOps.ExecuteReader(AppConstants.usp_GETCBSDBYAPID, aParams);

                //dbOps.ExecuteReader(_id);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            obj = new CBSDViewModel
                            {

                                SoftwareVersion = Helper.HandleDBNull<string>(dbOps.DataReader[0]),
                                HardwareVersion = Helper.HandleDBNull<string>(dbOps.DataReader[1]),
                                FirmwareVersion = Helper.HandleDBNull<string>(dbOps.DataReader[2]),
                                CBSDID = Helper.HandleDBNull<int>(dbOps.DataReader[3]),
                                IsActive = Helper.HandleDBNull<bool>(dbOps.DataReader[4]),
                                IsDeleted = Helper.HandleDBNull<bool>(dbOps.DataReader[5]),
                                IsSubmitted = Helper.HandleDBNull<bool>(dbOps.DataReader[6]),
                                CBSDVendorModelID = Helper.HandleDBNull<int>(dbOps.DataReader[7]),

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

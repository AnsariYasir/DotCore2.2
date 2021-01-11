using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.DAL;
using C3Spectra.APPositioningApp.Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.BAL
{
    public class OptionalParametersBAL
    {
        //public Result AddOptionalParameters(OptionalParametersViewModel model)
        //{
        //    Result result = new Result();
        //    DBOperations dbOps = new DBOperations();
        //    try
        //    {
        //        NpgsqlParameter[] aParams = new NpgsqlParameter[3];
        //        aParams[0] = new NpgsqlParameter("_callinfo", NpgsqlTypes.NpgsqlDbType.Integer);
        //        aParams[0].Value = model.CallInfo;
        //        aParams[1] = new NpgsqlParameter("_cbsdinfo", NpgsqlTypes.NpgsqlDbType.Integer);
        //        aParams[1].Value = model.CbsdInfo;
        //        aParams[2] = new NpgsqlParameter("_groupingParam", NpgsqlTypes.NpgsqlDbType.Integer);
        //        aParams[2].Value = model.GroupingParam;

        //        dbOps.ExecuteScalar(AppConstants.USP_ADD_OPTIONALPARAMETERS, aParams);

        //        result.Message = dbOps.ReturnObject.ToString();
        //        result.Status = (dbOps.ReturnObject.ToString().ToLower().Contains("error")) ? Status.Failure : Status.Success;
        //        return result;
        //    }
        //    catch (Exception Ex)
        //    {
        //        dbOps.Abort();
        //        result.Status = Status.Failure;
        //    }
        //    return result;
        //}

        public Result AddOptionalParameters(OptionalParametersViewModel model)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[6];
                aParams[0] = new NpgsqlParameter("CallSignID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = model.CallSignID;
                aParams[1] = new NpgsqlParameter("CbsdInfoID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[1].Value = model.CbsdInfoID;
                aParams[2] = new NpgsqlParameter("GroupingParamID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = model.GroupingParamID;
                aParams[3] = new NpgsqlParameter("IsSubmitted", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[3].Value = model.IsSubmitted;
                aParams[4] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = model.APID;
                aParams[5] = new NpgsqlParameter(AppConstants.CreatedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[5].Value = 0;

                int res = (int)dbOps.ExecuteScalar(AppConstants.QueryConstants.AddOptionalParameters, aParams);

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


        public Result UpdateOptionalParameters(OptionalParametersViewModel model)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[9];
                aParams[0] = new NpgsqlParameter("CallSignID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = model.CallSignID;
                aParams[1] = new NpgsqlParameter("CbsdInfoID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[1].Value = model.CbsdInfoID;
                aParams[2] = new NpgsqlParameter("GroupingParamID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = model.GroupingParamID;
                aParams[3] = new NpgsqlParameter("IsSubmitted", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[3].Value = model.IsSubmitted;
                aParams[4] = new NpgsqlParameter("OptionalParameterID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = model.OptionalParameterID;
                aParams[5] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[5].Value = model.APID;
                aParams[6] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[6].Value = DateTime.Now;
                aParams[7] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[7].Value = 0;
                aParams[8] = new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[8].Value = true;



                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.UpdateOptionalParameters, aParams);

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

        public Result DeleteOptionalParameters(int optionalParameterID)
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
                aParams[2] = new NpgsqlParameter("OptionalParameterID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = optionalParameterID;
                aParams[3] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[3].Value = DateTime.Now;
                aParams[4] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = 0;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.DeleteOptionalParameters, aParams);

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


        public OptionalParametersViewModel GetOptionalParemetersByID(int _id)
        {
            DBOperations dbOps = new DBOperations();
            OptionalParametersViewModel obj = null;
            try
            {
                //dbOps.ProcName = AppConstants.USP_GETINSTALLATIONPARAMETERSBYID;

                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_id", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = _id;

                dbOps.ExecuteReader(AppConstants.USP_GETOPTIONALPARAMETERSBYID, aParams);

                //dbOps.ExecuteReader(_id);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            obj = new OptionalParametersViewModel
                            {
                                OptionalParameterID = Helper.HandleDBNull<int>(dbOps.DataReader[0]),
                                CallSignID = Helper.HandleDBNull<int>(dbOps.DataReader[1]),
                                CbsdInfoID = Helper.HandleDBNull<int>(dbOps.DataReader[2]),
                                GroupingParamID = Helper.HandleDBNull<int>(dbOps.DataReader[3]),

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



        public OptionalParametersViewModel GetOptionalParemetersByAPID(int _apid)
        {
            DBOperations dbOps = new DBOperations();
            OptionalParametersViewModel obj = null;
            try
            {
                //dbOps.ProcName = AppConstants.USP_GETINSTALLATIONPARAMETERSBYID;

                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_apid", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = _apid;

                dbOps.ExecuteReader(AppConstants.usp_GETOPTIONALPARAMETERSBYAPID, aParams);

                //dbOps.ExecuteReader(_id);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            obj = new OptionalParametersViewModel
                            {
                                OptionalParameterID = Helper.HandleDBNull<int>(dbOps.DataReader[0]),
                                CallSignID = Helper.HandleDBNull<int>(dbOps.DataReader[1]),
                                CbsdInfoID = Helper.HandleDBNull<int>(dbOps.DataReader[2]),
                                GroupingParamID = Helper.HandleDBNull<int>(dbOps.DataReader[3]),
                                IsActive = Helper.HandleDBNull<bool>(dbOps.DataReader[4]),
                                IsDeleted = Helper.HandleDBNull<bool>(dbOps.DataReader[5]),
                                IsSubmitted = Helper.HandleDBNull<bool>(dbOps.DataReader[6]),
                                APID = Helper.HandleDBNull<int>(dbOps.DataReader[7])
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

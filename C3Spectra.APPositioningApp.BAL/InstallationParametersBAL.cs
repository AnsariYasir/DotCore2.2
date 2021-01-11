using C3Spectra.APPositioningApp.Common;
using C3Spectra.APPositioningApp.DAL;
using C3Spectra.APPositioningApp.Entity;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.BAL
{
    public class InstallationParametersBAL
    {
        public Result AddInstallationParameters(InstallationParametersViewModel model)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {

                NpgsqlParameter[] aParams = new NpgsqlParameter[16];



                //aParams[0] = new NpgsqlParameter("LatLong", NpgsqlTypes.NpgsqlDbType.Geometry);
                //aParams[0].Value =  "ST_GeomFromText('POINT(" + model.Latitude + " " + model.Longitude + ")', 4326)";

                aParams[0] = new NpgsqlParameter("Lat", NpgsqlTypes.NpgsqlDbType.Double);
                aParams[0].Value = model.Latitude;

                aParams[1] = new NpgsqlParameter("Long", NpgsqlTypes.NpgsqlDbType.Double);
                aParams[1].Value = model.Longitude;

                aParams[2] = new NpgsqlParameter("Height", NpgsqlTypes.NpgsqlDbType.Numeric);
                aParams[2].Value = model.Height;
                aParams[3] = new NpgsqlParameter("HeightTypeID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[3].Value = model.HeightTypeID;
                aParams[4] = new NpgsqlParameter("HorizontalAccuracy", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = model.HorizontalAccuracy;
                aParams[5] = new NpgsqlParameter("VerticalAccuracy", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[5].Value = model.VerticalAccuracy;
                aParams[6] = new NpgsqlParameter("IndoorDeployment", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[6].Value = model.IndoorDeployment;
                aParams[7] = new NpgsqlParameter("AntennaAzimuth", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[7].Value = model.AntennaAzimuth;
                aParams[8] = new NpgsqlParameter("AntennaDowntilt", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[8].Value = model.AntennaDowntilt;
                aParams[9] = new NpgsqlParameter("AntennaGain", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[9].Value = model.AntennaGain;
                aParams[10] = new NpgsqlParameter("EirpCapability", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[10].Value = model.EripCapability;
                aParams[11] = new NpgsqlParameter("AntennaBeamwidth", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[11].Value = model.AntennaBeamwidth;
                aParams[12] = new NpgsqlParameter("AntennaModelID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[12].Value = model.AntennaModelID;
                aParams[13] = new NpgsqlParameter("IsSubmitted", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[13].Value = model.IsSubmitted;
                aParams[14] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[14].Value = model.APID;
                aParams[15] = new NpgsqlParameter(AppConstants.CreatedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[15].Value = 0;



                int res = (int)dbOps.ExecuteScalar(AppConstants.QueryConstants.AddInstallationParameters, aParams);

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

        public Result UpdateInstallationParameters(InstallationParametersViewModel model)
        {
            Result result = new Result();
            DBOperations dbOps = new DBOperations();
            try
            {
                NpgsqlParameter[] aParams = new NpgsqlParameter[19];



                //aParams[0] = new NpgsqlParameter("LatLong", NpgsqlTypes.NpgsqlDbType.Geometry);
                //aParams[0].Value =  "ST_GeomFromText('POINT(" + model.Latitude + " " + model.Longitude + ")', 4326)";

                aParams[0] = new NpgsqlParameter("Lat", NpgsqlTypes.NpgsqlDbType.Double);
                aParams[0].Value = model.Latitude;

                aParams[1] = new NpgsqlParameter("Long", NpgsqlTypes.NpgsqlDbType.Double);
                aParams[1].Value = model.Longitude;

                aParams[2] = new NpgsqlParameter("Height", NpgsqlTypes.NpgsqlDbType.Numeric);
                aParams[2].Value = model.Height;
                aParams[3] = new NpgsqlParameter("HeightTypeID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[3].Value = model.HeightTypeID;
                aParams[4] = new NpgsqlParameter("HorizontalAccuracy", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = model.HorizontalAccuracy;
                aParams[5] = new NpgsqlParameter("VerticalAccuracy", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[5].Value = model.VerticalAccuracy;
                aParams[6] = new NpgsqlParameter("IndoorDeployment", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[6].Value = model.IndoorDeployment;
                aParams[7] = new NpgsqlParameter("AntennaAzimuth", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[7].Value = model.AntennaAzimuth;
                aParams[8] = new NpgsqlParameter("AntennaDowntilt", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[8].Value = model.AntennaDowntilt;
                aParams[9] = new NpgsqlParameter("AntennaGain", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[9].Value = model.AntennaGain;
                aParams[10] = new NpgsqlParameter("EirpCapability", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[10].Value = model.EripCapability;
                aParams[11] = new NpgsqlParameter("AntennaBeamwidth", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[11].Value = model.AntennaBeamwidth;
                aParams[12] = new NpgsqlParameter("AntennaModelID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[12].Value = model.AntennaModelID;
                aParams[13] = new NpgsqlParameter("IsSubmitted", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[13].Value = model.IsSubmitted;
                aParams[14] = new NpgsqlParameter("InstallationParameterID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[14].Value = model.InstallationParamterID;
                aParams[15] = new NpgsqlParameter("APID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[15].Value = model.APID;
                aParams[16] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[16].Value = DateTime.Now;
                aParams[17] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[17].Value = 0;
                aParams[18] = new NpgsqlParameter("IsActive", NpgsqlTypes.NpgsqlDbType.Boolean);
                aParams[18].Value = true;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.UpdateInstallationParameters, aParams);

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

        public Result DeleteInstallationParameters(int installationParameterID)
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
                aParams[2] = new NpgsqlParameter("InstallationParameterID", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[2].Value = installationParameterID;
                aParams[3] = new NpgsqlParameter(AppConstants.LastModifiedOn, NpgsqlTypes.NpgsqlDbType.Timestamp);
                aParams[3].Value = DateTime.Now;
                aParams[4] = new NpgsqlParameter(AppConstants.LastModifiedBy, NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[4].Value = 0;

                int res = dbOps.ExecuteNonQuery(AppConstants.QueryConstants.DeleteInstallationParameters, aParams);

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


        public InstallationParametersViewModel GetInstallationParemetersByID(int _id)
        {
            DBOperations dbOps = new DBOperations();
            InstallationParametersViewModel obj = null;
            try
            {
                //dbOps.ProcName = AppConstants.USP_GETINSTALLATIONPARAMETERSBYID;

                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_id", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = _id;

                dbOps.ExecuteReader(AppConstants.USP_GETINSTALLATIONPARAMETERSBYID, aParams);

                //dbOps.ExecuteReader(_id);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            obj = new InstallationParametersViewModel
                            {
                                InstallationParamterID = Helper.HandleDBNull<int>(dbOps.DataReader[0]),
                                Latitude = Helper.HandleDBNull<double>(dbOps.DataReader[1]),
                                Longitude = Helper.HandleDBNull<double>(dbOps.DataReader[2]),
                                Height = Helper.HandleDBNull<decimal>(dbOps.DataReader[3]),
                                HeightTypeID = Helper.HandleDBNull<int>(dbOps.DataReader[4]),
                                HorizontalAccuracy = Helper.HandleDBNull<int>(dbOps.DataReader[5]),
                                VerticalAccuracy = Helper.HandleDBNull<int>(dbOps.DataReader[6]),
                                IndoorDeployment = Helper.HandleDBNull<bool>(dbOps.DataReader[7]),
                                AntennaAzimuth = Helper.HandleDBNull<int>(dbOps.DataReader[8]),
                                AntennaDowntilt = Helper.HandleDBNull<int>(dbOps.DataReader[9]),
                                AntennaGain = Helper.HandleDBNull<int>(dbOps.DataReader[10]),
                                EripCapability = Helper.HandleDBNull<int>(dbOps.DataReader[11]),
                                AntennaBeamwidth = Helper.HandleDBNull<int>(dbOps.DataReader[12]),
                                AntennaModelID = Helper.HandleDBNull<int>(dbOps.DataReader[13])
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

        public InstallationParametersViewModel GetInstallationParemetersByAPID(int apid)
        {
            DBOperations dbOps = new DBOperations();
            InstallationParametersViewModel obj = null;
            try
            {
                //dbOps.ProcName = AppConstants.USP_GETINSTALLATIONPARAMETERSBYID;

                NpgsqlParameter[] aParams = new NpgsqlParameter[1];
                aParams[0] = new NpgsqlParameter("_apid", NpgsqlTypes.NpgsqlDbType.Integer);
                aParams[0].Value = apid;

                dbOps.ExecuteReader(AppConstants.usp_GETINSTALLATIONPARAMETERSBYAPID, aParams);

                //dbOps.ExecuteReader(_id);

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            obj = new InstallationParametersViewModel
                            {
                                InstallationParamterID = Helper.HandleDBNull<int>(dbOps.DataReader[0]),
                                Latitude = Helper.HandleDBNull<double>(dbOps.DataReader[1]),
                                Longitude = Helper.HandleDBNull<double>(dbOps.DataReader[2]),
                                Height = Helper.HandleDBNull<decimal>(dbOps.DataReader[3]),
                                HeightTypeID = Helper.HandleDBNull<int>(dbOps.DataReader[4]),
                                HorizontalAccuracy = Helper.HandleDBNull<int>(dbOps.DataReader[5]),
                                VerticalAccuracy = Helper.HandleDBNull<int>(dbOps.DataReader[6]),
                                IndoorDeployment = Helper.HandleDBNull<bool>(dbOps.DataReader[7]),
                                AntennaAzimuth = Helper.HandleDBNull<int>(dbOps.DataReader[8]),
                                AntennaDowntilt = Helper.HandleDBNull<int>(dbOps.DataReader[9]),
                                AntennaGain = Helper.HandleDBNull<int>(dbOps.DataReader[10]),
                                EripCapability = Helper.HandleDBNull<int>(dbOps.DataReader[11]),
                                AntennaBeamwidth = Helper.HandleDBNull<int>(dbOps.DataReader[12]),
                                AntennaModelID = Helper.HandleDBNull<int>(dbOps.DataReader[13]),
                                IsActive = Helper.HandleDBNull<bool>(dbOps.DataReader[14]),
                                IsDeleted = Helper.HandleDBNull<bool>(dbOps.DataReader[15]),
                                APID = Helper.HandleDBNull<int>(dbOps.DataReader[16])
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

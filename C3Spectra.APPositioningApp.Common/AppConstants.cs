using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Common
{
    public class AppConstants

    {


        #region CBSD

        public const string USP_ADD_CBSD = "usp_AddCBSD";

        public const string USP_UPDATE_CBSD = "usp_UpdateCBSD";

        public const string USP_GETCBSDBYID = "usp_GetCBSDByID";

        public const string USP_GETFLOORSFORINDOOR = "usp_GetFloorForIndoor";

        public const string USP_GETVENDORS = "usp_getVendors";

        public const string USP_GETMODELS = "usp_getmodels";
        #endregion

        #region InstallationParameters

        public const string USP_ADDINSTALLATIONPARAMETERS = "usp_AddInstallationParameters";

        public const string USP_GETHEIGHTTYPES = "usp_GetHeightTypes";

        public const string USP_GETANTENNAMODELS = "usp_GetAntennaModels";

        public const string USP_GETINSTALLATIONPARAMETERSBYID = "usp_GetInstallationParametersByID";

        #endregion
        public const string USP_GETSAS = "usp_getSAS";

        public const string USP_GETCBSDHWTYPES = "usp_getcbsdhwtypes";
        public const string USP_GETCBSDVENDORMODELS = "usp_getcbsdvendormodels";
        public const string USP_GETGROUPS = "usp_groups";
        public const string USP_GETAPTypes = "usp_getaptypes";

        #region

        #endregion
        #region OptionalParameters

        public const string USP_GETOPTIONALPARAMETERSBYID = "usp_GetOptionalParametersByID";

        public const string USP_ADD_OPTIONALPARAMETERS = "public.usp_addoptionalparameters";

        public const string USP_GETCALLSIGNS = "usp_getcallsigns";

        public const string USP_GETCBSDINFOS = "usp_getcbsdinfos";

        public const string USP_GETGROUPINGPARAMS = "usp_getgroupingparams";

        #endregion

        #region Floor
        public const string USP_GETFLOORSBYID = "usp_getfloorsbyid";
        public const string USP_GETFLOORSBYBUILDINGID = "usp_getfloorsbybuildingid";
        #endregion

        #region Building
        public const string USP_GETBUILDINGS = "usp_getbuildings";
        public const string USP_GETBUILDINGBYID = "usp_getbuildingbyid";
        public const string USP_GETBUILDINGSFORDROPDOWN = "usp_getbuildingsForDropDown";
        #endregion


        #region MainMenu
        public const string USP_GETMAINMENUS = "usp_getmainmenus";
        public const string USP_GETMAINMENUBYID = "usp_getmainmenubyid";

        #endregion

        #region Role
        public const string USP_GETROLES = "usp_getroles";
        public const string USP_GETROLEBYID = "usp_getrolebyid";
        public const string USP_GETROLESFORDROPDOWN = "usp_getrolesfordropdown";
        #endregion

        #region Role And Rights
        public const string USP_GET_ROLES_N_RIGHTS = "usp_getrolesnrights";
        public const string USP_GET_ROLES_N_RIGHTS_By_RoleID = "usp_getrolesnrightsbyroleid";
        #endregion

        #region Users
        public const string USP_GETVALIDUSER = "usp_getvaliduser";
        public const string USP_GETVALIDUSERBYEMAIL = "usp_getvaliduserbyemail";

        #endregion

        public const string USP_GETAPBYID = "usp_getapbyid";
        public const string usp_GETAPSBYFLOORID = "usp_getAPsbyFloorID";
        public const string usp_GETAPSBYOutdoorMasterID = " usp_getapsbyoutdoormasterid";
        public const string usp_GETINSTALLATIONPARAMETERSBYAPID = "usp_getinstallationparametersbyapid";
        public const string usp_GETOPTIONALPARAMETERSBYAPID = "usp_getoptionalparametersbyapid";
        public const string usp_GETCBSDBYAPID = "usp_getcbsdbyapid";
        public const string usp_GetAPSectorByOutdoorAPID = "usp_getapsectorbyoutdoorapid";

        #region Common Constants
        public const string InstallationParameters = "InstallationParameters";
        public const string OptionalParameters = "OptionalParameters";
        public const string CBSD = "CBSD";

        public const string SaveAsDraft = "SaveAsDraft";
        public const string Submit = "Submit";

        public const string ADDED = "ADDED";
        public const string UPDATED = "UPDATED";

        public const string CreatedBy = "CreatedBy";
        public const string LastModifiedOn = "LastModifiedOn";
        public const string LastModifiedBy = "LastModifiedBy";

        public const string CURRENTUSER = "CURRENTUSER";
        public const string CURRENTNAME = "NAME";
        #endregion

        public class QueryConstants
        {
            public const string AddOptionalParameters = "INSERT INTO \"tOptionalParameters\"(\"CallSignID\",\"CbsdInfoID\", \"GroupingParamID\",\"IsSubmitted\",\"APID\",\"CreatedBy\") VALUES (@CallSignID, @CbsdInfoID, @GroupingParamID,@IsSubmitted,@APID,@CreatedBy) RETURNING \"OptionalParameterID\"";
            public const string UpdateOptionalParameters = "UPDATE \"tOptionalParameters\" SET \"CallSignID\" = @CallSignID, \"CbsdInfoID\" = @CbsdInfoID, \"GroupingParamID\" = @GroupingParamID,\"IsSubmitted\" = @IsSubmitted ,\"APID\" = @APID,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy,\"IsActive\" = @IsActive WHERE \"OptionalParameterID\" = @OptionalParameterID";
            public const string DeleteOptionalParameters = "UPDATE \"tOptionalParameters\" SET \"IsActive\" = @IsActive, \"IsDeleted\" = @IsDeleted,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy WHERE \"OptionalParameterID\" = @OptionalParameterID";

            public const string AddCBSD = "INSERT INTO \"tCBSD\"(\"CBSDVendorModelID\", \"SoftwareVersion\",\"HardwareVersion\",\"FirmwareVersion\",\"IsSubmitted\",\"APID\",\"CreatedBy\") VALUES (@CBSDVendorModelID, @SoftwareVersion,@HardwareVersion,@FirmwareVersion,@IsSubmitted,@APID,@CreatedBy) RETURNING \"CBSDID\"";
            public const string UpdateCBSD = "UPDATE \"tCBSD\" SET \"CBSDVendorModelID\" = @CBSDVendorModelID, \"SoftwareVersion\" = @SoftwareVersion,\"HardwareVersion\" = @HardwareVersion,\"FirmwareVersion\" = @FirmwareVersion,\"IsSubmitted\" = @IsSubmitted,\"APID\" = @APID,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy,\"IsActive\" = @IsActive WHERE \"CBSDID\" = @CBSDID";
            public const string DeleteCBSD = "UPDATE \"tCBSD\" SET \"IsActive\" = @IsActive, \"IsDeleted\" = @IsDeleted,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy WHERE \"CBSDID\" = @CBSDID";
            //                                                                                                                                                                                                                                                                                                                            ST_SetSRID(ST_MakePoint(:Lat,:Long), 4326)                            
            public const string AddInstallationParameters = "INSERT INTO \"tInstallationParameters\"(\"LatLong\",\"Height\", \"HeightTypeID\",\"HorizontalAccuracy\",\"VerticalAccuracy\",\"IndoorDeployment\",\"AntennaAzimuth\",\"AntennaDowntilt\",\"AntennaGain\",\"EirpCapability\",\"AntennaBeamwidth\",\"AntennaModelID\", \"IsSubmitted\",\"APID\",\"CreatedBy\") VALUES (ST_SetSRID(ST_MakePoint(@Lat,@Long), 4326),@Height, @HeightTypeID,@HorizontalAccuracy,@VerticalAccuracy,@IndoorDeployment,@AntennaAzimuth,@AntennaDowntilt,@AntennaGain,@EirpCapability,@AntennaBeamwidth,@AntennaModelID,@IsSubmitted,@APID,@CreatedBy) RETURNING \"InstallationParameterID\"";


            public const string UpdateInstallationParameters = "UPDATE \"tInstallationParameters\" SET \"LatLong\" = ST_SetSRID(ST_MakePoint(@Lat,@Long), 4326), \"Height\" = @Height, \"HeightTypeID\" = @HeightTypeID,\"HorizontalAccuracy\" = @HorizontalAccuracy,\"VerticalAccuracy\" = @VerticalAccuracy,\"IndoorDeployment\" = @IndoorDeployment,\"AntennaAzimuth\" = @AntennaAzimuth,\"AntennaDowntilt\" = @AntennaDowntilt,\"AntennaGain\" = @AntennaGain,\"EirpCapability\" = @EirpCapability,\"AntennaBeamwidth\" = @AntennaBeamwidth,\"AntennaModelID\" = @AntennaModelID,\"IsSubmitted\" = @IsSubmitted,\"APID\" = @APID,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy,\"IsActive\" = @IsActive WHERE \"InstallationParameterID\" = @InstallationParameterID";
            public const string DeleteInstallationParameters = "UPDATE \"tInstallationParameters\" SET \"IsActive\" = @IsActive, \"IsDeleted\" = @IsDeleted,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy WHERE \"InstallationParameterID\" = @InstallationParameterID";


            public const string AddFloor = "INSERT INTO \"tFloors\"(\"Name\", \"Description\",\"FloorPlanSHPPath\",\"BuildingID\",\"FloorNo\",\"FloorPlanOrginalFileName\",\"LatLong\",\"CreatedBy\") VALUES (@Name, @Description,@FloorPlanSHPPath,@BuildingID,@FloorNo,@FloorPlanOrginalFileName,ST_SetSRID(ST_MakePoint(@Lat,@Long), 4326),@CreatedBy) RETURNING \"FloorID\"";
            public const string UpdateFloor = "UPDATE \"tFloors\" SET \"Name\" = @Name, \"Description\" = @Description,\"FloorPlanSHPPath\" = @FloorPlanSHPPath,\"BuildingID\" = @BuildingID,\"FloorNo\" = @FloorNo,\"FloorPlanOrginalFileName\" = @FloorPlanOrginalFileName,\"LatLong\" = ST_SetSRID(ST_MakePoint(@Lat,@Long), 4326),\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy,\"IsActive\" = @IsActive WHERE \"FloorID\" = @FloorID";
            public const string DeleteFloor = "UPDATE \"tFloors\" SET \"IsActive\" = @IsActive, \"IsDeleted\" = @IsDeleted,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy WHERE \"FloorID\" = @FloorID";

            public const string AddAP = "INSERT INTO \"tAPs\"(\"Name\", \"LatLong\",\"Description\",\"CreatedBy\",\"FloorID\",\"APTypeID\",\"GroupID\",\"IsIndoor\",\"SerialNo\",\"APimagepath\") VALUES (@Name, ST_SetSRID(ST_MakePoint(@Lat,@Long), 4326), @Description,@CreatedBy,@FloorID,@APTypeID,@GroupID,@IsIndoor,@SerialNo,@APimagepath) RETURNING \"APID\"";
            public const string UpdateAP = "UPDATE \"tAPs\" SET \"Name\" = @Name, \"LatLong\" = ST_SetSRID(ST_MakePoint(@Lat,@Long), 4326), \"Description\" = @Description,\"FloorID\" = @FloorID,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy, \"IsActive\" = @IsActive,\"APTypeID\" = @APTypeID,\"GroupID\" = @GroupID,\"IsIndoor\" = @IsIndoor,\"SerialNo\" = @SerialNo,\"APimagepath\" = @APimagepath WHERE \"APID\" = @APID";
            public const string DeleteAP = "UPDATE \"tAPs\" SET \"IsActive\" = @IsActive, \"IsDeleted\" = @IsDeleted,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy WHERE \"APID\" = @APID";


            public const string AddAPForOutdoor = "INSERT INTO \"tAPs\"(\"Name\", \"LatLong\",\"Description\",\"CreatedBy\",\"APImagePath\") VALUES (@Name, ST_SetSRID(ST_MakePoint(@Lat,@Long), 4326), @Description,@CreatedBy,@APImagePath) RETURNING \"APID\"";
            //public const string UpdateAPForOutdoor = "UPDATE \"tAPs\" SET \"Name\" = @Name, \"LatLong\" = ST_SetSRID(ST_MakePoint(@Lat,@Long), 4326), \"Description\" = @Description,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy, \"IsActive\" = @IsActive WHERE \"APID\" = @APID";
            public const string UpdateAPForOutdoor = "UPDATE \"tAPs\" SET \"Name\" = @Name, \"LatLong\" = ST_SetSRID(ST_MakePoint(@Lat,@Long), 4326), \"Description\" = @Description,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy, \"IsActive\" = @IsActive, \"APImagePath\" = @APImagePath WHERE \"APID\" = @APID";


            public const string AddBuilding = "INSERT INTO \"tBuildings\"(\"Name\", \"Description\",\"CreatedBy\") VALUES (@Name, @Description,@CreatedBy) RETURNING \"BuildingID\"";
            public const string UpdateBuilding = "UPDATE \"tBuildings\" SET \"Name\" = @Name, \"Description\" = @Description,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy, \"IsActive\" = @IsActive WHERE \"BuildingID\" = @BuildingID";
            public const string DeleteBuilding = "UPDATE \"tBuildings\" SET \"IsActive\" = @IsActive, \"IsDeleted\" = @IsDeleted,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy WHERE \"BuildingID\" = @BuildingID";

            public const string UpdatePassword = "UPDATE \"tUsers\" SET \"Password\" = @Password WHERE \"UserID\" = @UserID";

            public const string AddAPSector = "INSERT INTO \"tAPSectors\"(\"APID\",\"SectorNumber\",\"SectorValue\",\"SerialNumber\") VALUES (@APID, @SectorNumber,@SectorValue,@SerialNo)";
            //public const string AddAPSector = "INSERT INTO \"tAPSector\"(\"APID\",\"SectorNumber\",\"SectorValue\",\"SerialNo\") VALUES (@APID, @SectorNumber,@SectorValue,@SerialNo)";
            //public const string UpdateAP = "UPDATE \"tAPs\" SET \"Name\" = @Name, \"LatLong\" = ST_SetSRID(ST_MakePoint(@Lat,@Long), 4326), \"Description\" = @Description,\"FloorID\" = @FloorID,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy, \"IsActive\" = @IsActive WHERE \"APID\" = @APID";
            public const string UpdateAPSector = "UPDATE \"tAPSectors\" SET \"APID\" = @APID, \"SectorNumber\" = @SectorNumber,\"SectorValue\" = @SectorValue,\"SerialNumber\" = @SerialNo,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy, \"IsActive\" = @IsActive WHERE \"APSectorID\" = @APSectorId";
            public const string DeleteAPSector = "UPDATE \"tAPSectors\" SET \"IsActive\" = @IsActive, \"IsDeleted\" = @IsDeleted,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy WHERE \"APID\" = @APID";
            public const string ReduceAPSector = "UPDATE \"tAPSectors\" SET \"IsActive\" = @IsActive, \"IsDeleted\" = @IsDeleted,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy WHERE \"APID\" = @APID AND \"APSectorID\" IN (@APSectorID)";
            #region ManinMenu
            public const string AddMainMenu = "INSERT INTO \"tMainMenu\"(\"Name\", \"CreatedBy\") VALUES (@Name, @CreatedBy)";
            public const string UpdateMainMenu = "UPDATE \"tMainMenu\" SET \"Name\" = @Name, \"LastModifiedBy\" = @LastModifiedBy,\"LastModifiedOn\" = @LastModifiedOn,\"IsActive\" = @IsActive WHERE \"MainMenuID\" = @MainMenuId";
            public const string DeleteMainMenu = "UPDATE \"tMainMenu\" SET \"IsActive\" = @IsActive, \"IsDeleted\" = @IsDeleted,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy WHERE \"MainMenuID\" = @MainMenuId";
            #endregion

            #region
            public const string AddRole = "INSERT INTO \"tRoles\"(\"Name\", \"CreatedBy\") VALUES (@Name, @CreatedBy)";
            public const string UpdateRole = "UPDATE \"tRoles\" SET \"Name\" = @Name, \"LastModifiedBy\" = @LastModifiedBy,\"LastModifiedOn\" = @LastModifiedOn,\"IsActive\" = @IsActive WHERE \"RolesID\" = @RolesID";
            public const string DeleteRole = "UPDATE \"tRoles\" SET \"IsActive\" = @IsActive, \"IsDeleted\" = @IsDeleted,\"LastModifiedOn\" = @LastModifiedOn,\"LastModifiedBy\" = @LastModifiedBy WHERE \"RolesID\" = @RolesID";
            #endregion

            #region   Role and Rights
            public const string CheckRolesNRights = "SELECT COUNT(*) FROM \"tRolesNRights\" WHERE \"RNRID\" = @RNRID";
            public const string AddRolesNRights = "INSERT INTO \"tRolesNRights\"(\"RoleID\", \"ActionID\", \"IsActive\",\"CreatedBy\",\"CreatedOn\") VALUES (@RoleID,@ActionID,@IsActive, @CreatedBy,@CreatedOn)";
            public const string UpdateRolesNRights = "UPDATE \"tRolesNRights\" SET \"RoleID\" = @RoleID, \"ActionID\" = @ActionID, \"IsActive\" = @IsActive, \"LastModifiedBy\" = @LastModifiedBy,\"LastModifiedOn\" = @LastModifiedOn WHERE \"RNRID\" = @RNRID";
            #endregion
        }


    }
}

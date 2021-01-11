using C3Spectra.APPositioningApp.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace C3Spectra.APPositioningApp.Common
{
    public class DropDownBinding
    {

        /// <summary>
        /// CreatedBy : suhel
        /// CreatedDate  :03/01/2019
        /// Summary : Bind Drop Down List
        /// </summary>
        /// <param name="ProcName"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetDropDown(string ProcName, long Id = 0, string selectedText = "SELECT", long mode = 0)
        {
            DBOperations dbOps = new DBOperations();
            List<SelectListItem> bindDropDownList = new List<SelectListItem>();
            try
            {

                dbOps.ProcName = ProcName;

                if (Id != 0 && mode != 0)
                    dbOps.ExecuteReader(Id, mode);
                else if (Id != 0)
                    dbOps.ExecuteReader(Id);
                else if (mode != 0)
                    dbOps.ExecuteReader(Id, mode);
                else
                    dbOps.ExecuteReader();

                if (!(dbOps.DataReader == null))
                {
                    using (dbOps.DataReader)
                    {
                        while (dbOps.DataReader.Read())
                        {
                            bindDropDownList.Add(DoBindDropDownList(dbOps.DataReader));
                            // CompanyDropDownList.Add( DoBindCompanyDropDownList(dbOps.DataReader));
                        }
                    }
                }
                bindDropDownList.Insert(0, new SelectListItem { Value = "0", Text = "-- " + selectedText + " --" });
                return bindDropDownList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static List<SelectListItem> GetDropDown(string ProcName, long Id = 0, long ddlSelectedValue = 0, string selectedValues = "")
        {
            DBOperations dbOps = new DBOperations();
            List<SelectListItem> bindDropDownList = new List<SelectListItem>();
            try
            {
                dbOps.ProcName = ProcName;

                if (Id != 0 && ddlSelectedValue != 0)
                    dbOps.ExecuteReader(Id, ddlSelectedValue);
                else if (Id != 0 && selectedValues == "")
                    dbOps.ExecuteReader(Id);
                else if (selectedValues != "")
                    dbOps.ExecuteReader(selectedValues);
                else
                    dbOps.ExecuteReader();

                if (!(dbOps.DataReader == null))
                {
                    while (dbOps.DataReader.Read())
                    {
                        bindDropDownList.Add(DoBindDropDownList(dbOps.DataReader));
                        // CompanyDropDownList.Add( DoBindCompanyDropDownList(pobjSqlHelper.DataReader));
                    }
                }
                //bindDropDownList.Insert(0, new SelectListItem { Value = "0", Text = "-- " + selectedText + "--" });
                return bindDropDownList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public static List<SelectListItem> GetDropDownWithAnotherDropDown(string ProcName, long Id = 0, string selectedValues = "")
        {
            DBOperations pobjSqlHelper = new DBOperations();
            List<SelectListItem> bindDropDownList = new List<SelectListItem>();
            try
            {
                pobjSqlHelper.ProcName = ProcName;
                if (Id != 0 && selectedValues != "")
                    pobjSqlHelper.ExecuteReader(Id, selectedValues);
                else
                    pobjSqlHelper.ExecuteReader();

                if (!(pobjSqlHelper.DataReader == null))
                {
                    while (pobjSqlHelper.DataReader.Read())
                    {
                        bindDropDownList.Add(DoBindDropDownList(pobjSqlHelper.DataReader));
                    }
                }
                return bindDropDownList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        private static SelectListItem DoBindDropDownList(NpgsqlDataReader reader)
        {
            SelectListItem selectListItem = new SelectListItem();
            selectListItem.Value = Convert.ToString(reader[0]);
            selectListItem.Text = Convert.ToString(reader[1]);
            return selectListItem;
        }

        /// <summary>
        /// Create By : suhelsagar
        /// Created Date : 08 Feb 2019
        /// Summary : 
        /// Method Name : GetValuesFromEnumForDropdown
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static List<SelectListItem> GetValuesFromEnumForDropdown<T>()
        {
            try
            {
                Type genericType = typeof(T);
                List<SelectListItem> lst = new List<SelectListItem>();

                if (genericType.IsEnum)
                {
                    foreach (T obj in Enum.GetValues(genericType))
                    {
                        Enum en = Enum.Parse(typeof(T), obj.ToString()) as Enum;

                        lst.Add(
                            new SelectListItem()
                            {
                                Text = Convert.ToString(Enum.Parse(typeof(T), obj.ToString())),
                                Value = Convert.ToString(Convert.ToInt32(en))
                            });

                    }
                }


                return lst;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                GC.Collect();
            }

        }

    }
}

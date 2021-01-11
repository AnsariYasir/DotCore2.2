using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;

namespace C3Spectra.APPositioningApp.DAL
{
    public class DBOperations
    {

        #region "Declarations"

        private static string strConnectionString = ConfigurationManager.ConnectionStrings["_APPositioningAppConnectionString"].ToString();
        private int intCommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeout"]);


        private NpgsqlConnection npgsqlconDBConnection;
        private NpgsqlTransaction npgsqltrnTransaction;
        private NpgsqlCommand npgsqlcomCommand;

        private string strProcName;

        private bool blnTransactionFlag = false;
        private NpgsqlDataReader dreaderDataReader;
        private DataSet dsetDataSet;
        public int intReturnValue;
        public string strOutPut = string.Empty;
        #endregion
        private object objReturnObject;



        #region "Properties"
        public string ProcName
        {
            get { return strProcName; }
            set { strProcName = value; }
        }

        public NpgsqlDataReader DataReader
        {
            get { return dreaderDataReader; }
        }

        public DataSet DataSet
        {
            get { return dsetDataSet; }
        }

        public int ReturnValue
        {
            get { return intReturnValue; }
        }

        public object ReturnObject
        {
            get { return objReturnObject; }
        }

        public bool isAborted { get; set; }
        #endregion

        #region "Constructor"
        /// <summary>
        /// Name: 
        /// Author: Suhel Sagar / 03 JAN 2019
        /// Purpose:Create Connection Without Trnsaction.
        /// </summary>
        /// <remarks></remarks>
        /// <modifications>
        /// 03 JAN 2019 - Beena 
        /// </modifications>
        public DBOperations()
        {
            npgsqlconDBConnection = new NpgsqlConnection(strConnectionString);
            //NpgsqlConnection.GlobalTypeMapper.UseNetTopologySuite();
        }

        /// <summary>
        /// Name: 
        /// Author: Suhel Sagar / 03 JAN 2019
        /// Purpose: Create Connection With Transaction.
        /// </summary>
        /// <param name="pblnTransaction"></param>
        /// <remarks></remarks>
        public DBOperations(bool pblnTransaction)
        {
            npgsqlconDBConnection = new NpgsqlConnection(strConnectionString);
            blnTransactionFlag = true;
        }
        #endregion

        #region "Private Methods"
        /// <summary>
        /// Name:GenerateCommand
        /// Author:Suhel Sagar / 3 JAN 2019
        /// Purpose:This method accepts a String array and then using these parameters forms a command object.
        ///           and returns this command object.
        /// </summary>
        /// <param name="param"></param>
        /// <remarks></remarks>
        /// <modifications>
        /// </modifications>
        private void GenerateCommand(params object[] param)
        {
            int i = 0;

            npgsqlcomCommand = new NpgsqlCommand("", npgsqlconDBConnection);
            npgsqlcomCommand.CommandText = strProcName;
            npgsqlcomCommand.CommandType = CommandType.StoredProcedure;
            npgsqlcomCommand.CommandTimeout = intCommandTimeout;

            if ((npgsqltrnTransaction != null))
            {
                npgsqlcomCommand.Transaction = npgsqltrnTransaction;
            }

            NpgsqlCommandBuilder.DeriveParameters(npgsqlcomCommand);



            for (i = 1; i <= param.GetUpperBound(0) + 1; i += 1)
            {

                if (Convert.ToInt32(npgsqlcomCommand.Parameters[i].Value) > 0)
                {
                    npgsqlcomCommand.Parameters[i].Value = Convert.ToInt32(param[i - 1]);
                }
                else
                    //{ 
                    //if (npgsqlcomCommand.Parameters[i].TypeName.Contains("dbo"))
                    //{
                    //    npgsqlcomCommand.Parameters[i].Value = (DataTable)(param[i - 1]);
                    //}
                    //else
                    npgsqlcomCommand.Parameters[i].Value = Convert.ToString(param[i - 1]);
                // }

            }

        }

        #endregion

        #region "Public Methods"

        /// <summary>
        /// Name:CloseConnection
        /// Author:Suhel Sagar / 3 JAN 2019
        /// Purpose:This method closes is called to close any existing open sql connection to the database.
        /// </summary>
        /// <remarks></remarks>
        public void CloseConnection()
        {
            npgsqlconDBConnection.Close();
        }
        /// <summary>
        /// Name:EndTransaction
        /// Author:Suhel Sagar / 3 JAN 2019
        /// Purpose:This method is used to end any existing open transaction and also commit and close it.
        /// </summary>
        /// <remarks></remarks>
        public void EndTransaction()
        {
            try
            {
                blnTransactionFlag = false;
                npgsqltrnTransaction.Commit();
            }
            catch (Exception ex)
            {
                npgsqltrnTransaction.Rollback();
                throw;
            }
            finally
            {
                npgsqlconDBConnection.Close();
            }
        }

        /// <summary>
        /// Name    : Abort
        /// Author  : Mahendra Bhatt / 25 JAN 2012
        /// Purpose : This method is used to explicitly abort an sql transaction
        /// </summary>
        /// <remarks></remarks>
        public void Abort()
        {
            if (blnTransactionFlag == true & isAborted == false)
            {
                blnTransactionFlag = false;
                isAborted = true;
                if ((npgsqltrnTransaction != null))
                {
                    npgsqltrnTransaction.Rollback();
                }
                if ((npgsqlconDBConnection.State == ConnectionState.Open))
                {
                    npgsqlconDBConnection.Close();
                }

            }
        }

        /// <summary>
        /// Name:ExecuteSelect
        /// Author:Suhel Sagar / 3 JAN 2019
        /// Purpose:This method selects multiple rows of data from the DB
        /// </summary>
        /// <param name="param"></param>
        /// <remarks></remarks>
        /// <modifications>
        ///  Viral / 25 Jul 2013 : Added If Else for sql conncection. If connection is already open then we are not opening it again
        ///                        Added an integer variable err which checks whether an error has occured. 
        ///                        If an error occurs the integer variable becomes negative and is checked in finally statement 
        ///                        where sql transaction is committed. If an error occurs in Catch we are already aborting the transaction 
        ///                        hence it will not be available in finally to commit.
        /// </modifications>
        public DataSet ExecuteSelect(params object[] param)
        {
            NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter();
            int err = 0;
            try
            {
                if (!(npgsqlconDBConnection.State == ConnectionState.Open))
                {
                    npgsqlconDBConnection.Open();
                }

                if (npgsqltrnTransaction == null)
                {
                    npgsqltrnTransaction = npgsqlconDBConnection.BeginTransaction();
                }

                GenerateCommand(param);
                Adapter.SelectCommand = npgsqlcomCommand;
                dsetDataSet = new DataSet();
                Adapter.Fill(dsetDataSet);
            }
            catch (Exception ex)
            {
                dsetDataSet = null;
                err = -1;
                npgsqltrnTransaction.Rollback();
                npgsqlconDBConnection.Close();
                blnTransactionFlag = false;
                throw ex;
            }
            finally
            {
                if (blnTransactionFlag == false & err == 0)
                {
                    npgsqltrnTransaction.Commit();
                    npgsqlconDBConnection.Close();
                }
            }
            return dsetDataSet;
        }

        /// <summary>
        /// Name:ExecuteSelect
        /// Author:Mahendra B / 22 Apr 2013
        /// Purpose:This method selects multiple rows of data from the DB
        /// </summary>
        /// 
        //Public Sub ExecuteSelect(ByVal dt As ArrayList)
        //    Dim Adapter As New SqlDataAdapter
        //    Try
        //        sqlconDBConnection.Open()

        //        sqlcomCommand = New SqlCommand("", sqlconDBConnection)
        //        sqlcomCommand.CommandText = strProcName
        //        sqlcomCommand.CommandType = CommandType.StoredProcedure
        //        sqlcomCommand.CommandTimeout = intCommandTimeout

        //        If Not sqltrnTransaction Is Nothing Then
        //            sqlcomCommand.Transaction = sqltrnTransaction
        //        End If

        //        SqlCommandBuilder.DeriveParameters(sqlcomCommand)

        //        Dim k As Integer = 0
        //        For k = 0 To dt.Count - 1

        //            sqlcomCommand.Parameters(k + 1).TypeName = ""
        //            sqlcomCommand.Parameters(k + 1).Value = dt(k)
        //        Next

        //        k = dt.Count

        //        Adapter.SelectCommand = sqlcomCommand
        //        dsetDataSet = New DataSet
        //        Adapter.Fill(dsetDataSet)
        //    Catch ex As Exception
        //        Throw
        //    Finally
        //        sqlconDBConnection.Close()
        //    End Try
        //End Sub

        /// <summary>
        /// Name:ExecuteSelect
        /// Author:Mahendra B / 5 Jun 2013
        /// Purpose:This method selects multiple rows of data from the DB
        /// </summary>
        /// <modification>
        /// Shweta Shah / 6 June 2013 : added if else for sql conncection. If connection is already open then we are not opening it again
        /// </modification>
        /// 
        public void ExecuteSelect(ArrayList dt, params object[] param)
        {
            NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter();
            int err = 0;
            try
            {
                if (!(npgsqlconDBConnection.State == ConnectionState.Open))
                {
                    npgsqlconDBConnection.Open();
                }

                if (npgsqltrnTransaction == null)
                {
                    npgsqltrnTransaction = npgsqlconDBConnection.BeginTransaction();
                }

                int i = 0;

                npgsqlcomCommand = new NpgsqlCommand("", npgsqlconDBConnection);
                npgsqlcomCommand.CommandText = strProcName;
                npgsqlcomCommand.CommandType = CommandType.StoredProcedure;
                npgsqlcomCommand.CommandTimeout = intCommandTimeout;

                if ((npgsqltrnTransaction != null))
                {
                    npgsqlcomCommand.Transaction = npgsqltrnTransaction;
                }

                NpgsqlCommandBuilder.DeriveParameters(npgsqlcomCommand);

                int k = 0;
                for (k = 0; k <= dt.Count - 1; k++)
                {
                    //npgsqlcomCommand.Parameters[k + 1].TypeName = "";
                    npgsqlcomCommand.Parameters[k + 1].Value = dt[k];
                }

                k = dt.Count;

                for (i = 1; i <= (param.GetUpperBound(0) + 1); i += 1)
                {
                    if ((Convert.ToInt32(npgsqlcomCommand.Parameters[k + i].Value) > 0))
                    {
                        npgsqlcomCommand.Parameters[k + i].Value = Convert.ToInt32(param[i - 1]);
                    }
                    else
                    {
                        npgsqlcomCommand.Parameters[k + i].Value = Convert.ToString(param[i - 1]);
                    }
                }

                Adapter.SelectCommand = npgsqlcomCommand;
                dsetDataSet = new DataSet();
                Adapter.Fill(dsetDataSet);
            }
            catch (Exception ex)
            {
                err = -1;
                npgsqltrnTransaction.Rollback();
                npgsqlconDBConnection.Close();
                blnTransactionFlag = false;
                throw;
            }
            finally
            {
                if (blnTransactionFlag == false & err == 0)
                {
                    npgsqltrnTransaction.Commit();
                    npgsqlconDBConnection.Close();
                }
            }
        }


        /// <summary>
        /// Name:ExecuteSelectWithOutput
        /// Author:Suhel Sagar / 3 JAN 2019
        /// Purpose:This method is used when along with the datatable, no. of records is required as output.
        /// </summary>
        /// <param name="pintOutput">integer output parameter which gets the no. of total rows from the DB</param>
        /// <param name="param"></param>
        /// <remarks></remarks>
        public void ExecuteSelectWithOutput(ref int pintOutput, params object[] param)
        {
            NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter();

            try
            {
                npgsqlconDBConnection.Open();
                dsetDataSet = new DataSet();
                GenerateCommand(param);
                npgsqlcomCommand.Parameters[npgsqlcomCommand.Parameters.Count - 1].Value = pintOutput;
                Adapter.SelectCommand = npgsqlcomCommand;
                Adapter.Fill(dsetDataSet);
                pintOutput = Convert.ToInt32(npgsqlcomCommand.Parameters["@totalrowcount"].Value);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                npgsqlconDBConnection.Close();
            }

        }
        /// <summary>
        /// Name:ExecuteSelectWithOutput
        /// Author:Suhel Sagar / 3 JAN 2019
        /// Purpose:This method is used to run sql command text for select.
        /// </summary>
        /// <param name="pstrSQLCommandText"></param>
        /// <remarks></remarks>
        public void ExecuteSelectText(string pstrSQLCommandText)
        {
            NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter();

            try
            {
                npgsqlconDBConnection.Open();
                dsetDataSet = new DataSet();

                npgsqlcomCommand = new NpgsqlCommand("", npgsqlconDBConnection);
                npgsqlcomCommand.CommandText = pstrSQLCommandText;
                npgsqlcomCommand.CommandType = CommandType.Text;
                npgsqlcomCommand.CommandTimeout = intCommandTimeout;

                Adapter.SelectCommand = npgsqlcomCommand;
                Adapter.Fill(dsetDataSet);

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                npgsqlconDBConnection.Close();
            }
        }

        /// <summary>
        /// Name:ExecuteReader
        /// Author:Suhel Sagar / 3 JAN 2019
        /// Purpose:This method returns data in Datareader.
        /// </summary>
        /// <param name="param"></param>
        /// <remarks></remarks>
        public void ExecuteReader(params object[] param)
        {
            try
            {
                npgsqlconDBConnection.Open();
                GenerateCommand(param);
                dreaderDataReader = npgsqlcomCommand.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                npgsqlconDBConnection.Close();
                throw;
            }
        }

        public void ExecuteReader(String StoredProcName, NpgsqlParameter[] spa)
        {
            NpgsqlConnection dbconn = new NpgsqlConnection();
            dbconn.ConnectionString = strConnectionString;
            dbconn.Open();
            try
            {
                NpgsqlCommand dbCommand = new NpgsqlCommand(StoredProcName, dbconn);
                dbCommand.CommandType = CommandType.StoredProcedure;

                foreach (NpgsqlParameter sp in spa)
                {
                    dbCommand.Parameters.Add(sp);
                }

                dreaderDataReader = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                throw;
            }
        }

        //public object ExecuteScalar(String StoredProcName, NpgsqlParameter[] spa)
        //{

        //    NpgsqlConnection dbconn = new NpgsqlConnection();
        //    objReturnObject = new object();
        //    int err = 0;
        //    try
        //    {


        //        dbconn.ConnectionString = strConnectionString;
        //        dbconn.Open();

        //        NpgsqlCommand dbCommand = new NpgsqlCommand(StoredProcName, dbconn);
        //        dbCommand.CommandType = CommandType.StoredProcedure;

        //        foreach (NpgsqlParameter sp in spa)
        //        {
        //            dbCommand.Parameters.Add(sp);
        //        }

        //        objReturnObject = dbCommand.ExecuteScalar();
        //    }
        //    catch (Exception ex)
        //    {
        //        err = -1;
        //        //npgsqltrnTransaction.Rollback();
        //        dbconn.Close();
        //        //blnTransactionFlag = false;
        //        throw ex;
        //    }
        //    finally
        //    {
        //        //if (blnTransactionFlag == false & err == 0)
        //        //{
        //        //    npgsqltrnTransaction.Commit();
        //        //    npgsqlconDBConnection.Close();
        //        //}
        //        dbconn.Close();
        //    }
        //    return objReturnObject;
        //}

        public object ExecuteScalar(String query, NpgsqlParameter[] spa)
        {

            NpgsqlConnection dbconn = new NpgsqlConnection();
            objReturnObject = new object();
            int err = 0;
            try
            {


                dbconn.ConnectionString = strConnectionString;
                dbconn.Open();

                NpgsqlCommand dbCommand = new NpgsqlCommand(query, dbconn);
                dbCommand.Parameters.AddRange(spa);

                objReturnObject = dbCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                err = -1;
                //npgsqltrnTransaction.Rollback();
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                //blnTransactionFlag = false;
                throw ex;
            }
            finally
            {
                //if (blnTransactionFlag == false & err == 0)
                //{
                //    npgsqltrnTransaction.Commit();
                //    npgsqlconDBConnection.Close();
                //}
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }
            return objReturnObject;
        }

        public object ExecuteScalar(String StoredProcName, NpgsqlParameter[] spa, bool isStoredProcedure = false)
        {

            NpgsqlConnection dbconn = new NpgsqlConnection();
            objReturnObject = new object();
            int err = 0;
            try
            {


                dbconn.ConnectionString = strConnectionString;
                dbconn.Open();

                NpgsqlCommand dbCommand = new NpgsqlCommand(StoredProcName, dbconn);
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.Parameters.AddRange(spa);

                objReturnObject = dbCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                err = -1;
                //npgsqltrnTransaction.Rollback();
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
                //blnTransactionFlag = false;
                throw ex;
            }
            finally
            {
                //if (blnTransactionFlag == false & err == 0)
                //{
                //    npgsqltrnTransaction.Commit();
                //    npgsqlconDBConnection.Close();
                //}
                if (dbconn.State == ConnectionState.Open)
                {
                    dbconn.Close();
                }
            }
            return objReturnObject;
        }



        /// <summary>
        /// Name:ExecuteScalar
        /// Author:Suhel Sagar / 3 JAN 2019
        /// Purpose:This method is used when only first column of first row needs to be returned from database.
        /// Modification History : Naman 14 September 2019
        ///                        Added Finally in Try-Catch Clause to close database coonection.
        /// Shweta Shah / 10 May 2014 : added condition. If sqlconDBConnection is already open , dont open new one, continue with same else open new connection
        /// </summary>
        /// <param name="param"></param>
        /// <remarks></remarks>
        ///  <modifications>
        /// </modifications>
        public void ExecuteScalar(params object[] param)
        {

            int err = 0;
            try
            {

                if (!(npgsqlconDBConnection.State == ConnectionState.Open))
                {
                    npgsqlconDBConnection.Open();
                }

                if (npgsqltrnTransaction == null)
                {
                    npgsqltrnTransaction = npgsqlconDBConnection.BeginTransaction();
                }
                GenerateCommand(param);
                objReturnObject = new object();
                objReturnObject = npgsqlcomCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                err = -1;
                npgsqltrnTransaction.Rollback();
                npgsqlconDBConnection.Close();
                blnTransactionFlag = false;
                throw ex;
            }
            finally
            {
                if (blnTransactionFlag == false & err == 0)
                {
                    npgsqltrnTransaction.Commit();
                    npgsqlconDBConnection.Close();
                }
            }
        }
        /// <summary>
        /// Name:ExecuteUpdate
        /// Author:Suhel Sagar / 3 JAN 2019
        /// Purpose: This ExecUpdate method is used to execute the procs which
        ///           modify the data in the database. It returns an Integer value
        ///           which is either an error no or some prmary key value.
        /// Modification History :
        /// Riddhi R 30 April 2012 : Added an integer variable which checks whether an error has occured. 
        ///                         If an error occurs the integer variable becomes negative and is checked in finally statement 
        ///                         where sql transaction is committed. If an error occurs in Catch we are already aborting the transaction 
        ///                         hence it will not be available in finally to commit.
        /// </summary>
        /// <param name="param"></param>
        /// <remarks></remarks>
        /// <modifications>
        /// </modifications>
        public void ExecuteUpdate(params object[] param)
        {
            int err = 0;
            try
            {
                if (!(npgsqlconDBConnection.State == ConnectionState.Open))
                {
                    npgsqlconDBConnection.Open();
                }

                if (npgsqltrnTransaction == null)
                {
                    npgsqltrnTransaction = npgsqlconDBConnection.BeginTransaction();
                }

                GenerateCommand(param);
                intReturnValue = npgsqlcomCommand.ExecuteNonQuery();
                npgsqlcomCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                err = -1;
                npgsqltrnTransaction.Rollback();
                npgsqlconDBConnection.Close();
                blnTransactionFlag = false;
                throw ex;
            }
            finally
            {
                if (blnTransactionFlag == false & err == 0)
                {
                    npgsqltrnTransaction.Commit();
                    npgsqlconDBConnection.Close();
                }
            }

        }

        /// <summary>
        /// Author:Suhel Sagarasabe/20 Sep 2012
        /// Description:Method used for returning codevalue parameter value. 
        /// if we are using this method make sure that last parameter name will be CodeValue and as output parameter.
        /// 
        /// </summary>
        /// <param name="strCode"></param>
        /// <param name="param"></param>
        /// <remarks></remarks>
        public void ExecuteUpdateWithOutput(ref string strCode, params object[] param)
        {
            int err = 0;
            try
            {
                if (!(npgsqlconDBConnection.State == ConnectionState.Open))
                {
                    npgsqlconDBConnection.Open();
                }

                if (npgsqltrnTransaction == null)
                {
                    npgsqltrnTransaction = npgsqlconDBConnection.BeginTransaction();
                }

                GenerateCommand(param);
                npgsqlcomCommand.Parameters[npgsqlcomCommand.Parameters.Count - 1].Value = strCode;

                npgsqlcomCommand.ExecuteNonQuery();
                intReturnValue = Convert.ToInt32(npgsqlcomCommand.Parameters[0].Value);
                strCode = npgsqlcomCommand.Parameters["@p_result"].Value.ToString();
                npgsqlcomCommand.Parameters.Clear();


            }
            catch (Exception ex)
            {
                err = -1;
                npgsqltrnTransaction.Rollback();
                npgsqlconDBConnection.Close();
                blnTransactionFlag = false;
                throw ex;
            }
            finally
            {
                if (blnTransactionFlag == false & err == 0)
                {
                    npgsqltrnTransaction.Commit();
                    npgsqlconDBConnection.Close();
                }
            }

        }
        /// <summary>
        /// Name:ExecuteUpdate
        /// Author:Naman/ 14 Sept 2019
        /// Purpose: This ExecUpdate method is used to execute the procs which
        ///           modify the data in the database. It returns an Integer value
        ///           which is either an error no or some prmary key value.
        /// </summary>
        /// <param name="param"></param>
        /// <remarks></remarks>
        /// <modifications>
        /// </modifications>
        public void ExecuteUpdate(ArrayList dt, params string[] param)
        {
            int err = 0;
            try
            {
                if (!(npgsqlconDBConnection.State == ConnectionState.Open))
                {
                    npgsqlconDBConnection.Open();
                }

                if (npgsqltrnTransaction == null)
                {
                    npgsqltrnTransaction = npgsqlconDBConnection.BeginTransaction();
                }


                npgsqlcomCommand = new NpgsqlCommand("", npgsqlconDBConnection);
                npgsqlcomCommand.CommandText = strProcName;
                npgsqlcomCommand.CommandType = CommandType.StoredProcedure;
                npgsqlcomCommand.CommandTimeout = intCommandTimeout;

                if ((npgsqltrnTransaction != null))
                {
                    npgsqlcomCommand.Transaction = npgsqltrnTransaction;
                }

                NpgsqlCommandBuilder.DeriveParameters(npgsqlcomCommand);

                int k = 0;

                for (k = 0; k <= dt.Count - 1; k++)
                {
                    //npgsqlcomCommand.Parameters[k + 1].TypeName = "";
                    npgsqlcomCommand.Parameters[k + 1].Value = dt[k];
                }

                k = dt.Count;


                for (int i = 1; i <= (param.GetUpperBound(0) + 1); i += 1)
                {
                    if ((Convert.ToInt32(npgsqlcomCommand.Parameters[i + k].Value) > 0))
                    {
                        npgsqlcomCommand.Parameters[i + k].Value = Convert.ToInt32(param[i - 1]);
                    }
                    else
                    {
                        npgsqlcomCommand.Parameters[i + k].Value = Convert.ToInt32(param[i - 1]);
                    }
                }

                npgsqlcomCommand.ExecuteNonQuery();
                intReturnValue = Convert.ToInt32(npgsqlcomCommand.Parameters[0].Value);
                npgsqlcomCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                err = -1;
                npgsqltrnTransaction.Rollback();
                npgsqlconDBConnection.Close();
                blnTransactionFlag = false;
                throw;
            }
            finally
            {
                if (blnTransactionFlag == false & err == 0)
                {
                    npgsqltrnTransaction.Commit();
                    npgsqlconDBConnection.Close();
                }
            }

        }
        /// <summary>
        /// Name:ExecuteSelectAdapter
        /// Author:Naman / 20 Sept 2019
        /// Purpose:The getSelectAdapter method is used to execute the procs which
        ///         retrieve the data in the database. It returns a data Adapter
        ///         object containing the results of stored procedure execution.
        /// </summary>
        /// <param name="param"></param>
        /// <remarks></remarks>
        public NpgsqlDataAdapter ExecuteSelectAdapter(params string[] param)
        {
            NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter();
            try
            {
                npgsqlconDBConnection.Open();
                GenerateCommand(param);
                Adapter.SelectCommand = npgsqlcomCommand;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                npgsqlconDBConnection.Close();
            }
            return Adapter;
        }
        /// <summary>
        /// Name    : ExecuteUpdateSelect
        /// Author  : Mahendra Bhatt/ 4 Apr 2013
        /// Purpose : This ExecuteUpdateSelect method is used to execute the procs which
        ///           modify the data in the database. It returns datatable.
        /// </summary>
        /// <param name="param"></param>
        /// <remarks></remarks>
        /// <modifications>
        /// </modifications>
        public void ExecuteUpdateSelect(ArrayList dt, params string[] param)
        {
            int err = 0;
            try
            {
                if (!(npgsqlconDBConnection.State == ConnectionState.Open))
                {
                    npgsqlconDBConnection.Open();
                }

                if (npgsqltrnTransaction == null)
                {
                    npgsqltrnTransaction = npgsqlconDBConnection.BeginTransaction();
                }


                npgsqlcomCommand = new NpgsqlCommand("", npgsqlconDBConnection);
                npgsqlcomCommand.CommandText = strProcName;
                npgsqlcomCommand.CommandType = CommandType.StoredProcedure;
                npgsqlcomCommand.CommandTimeout = intCommandTimeout;

                if ((npgsqltrnTransaction != null))
                {
                    npgsqlcomCommand.Transaction = npgsqltrnTransaction;
                }

                NpgsqlCommandBuilder.DeriveParameters(npgsqlcomCommand);

                int k = 0;

                for (k = 0; k <= dt.Count - 1; k++)
                {
                    //npgsqlcomCommand.Parameters[k + 1].TypeName = "";
                    npgsqlcomCommand.Parameters[k + 1].Value = dt[k];
                }

                k = dt.Count;


                for (int i = 1; i <= (param.GetUpperBound(0) + 1); i += 1)
                {
                    if ((Convert.ToInt32(npgsqlcomCommand.Parameters[i + k].Value) > 0))
                    {
                        npgsqlcomCommand.Parameters[i + k].Value = Convert.ToInt32(param[i - 1]);
                    }
                    else
                    {
                        npgsqlcomCommand.Parameters[i + k].Value = Convert.ToInt32(param[i - 1]);
                    }
                }

                objReturnObject = new object();
                objReturnObject = npgsqlcomCommand.ExecuteReader();
                npgsqlcomCommand.Parameters.Clear();


            }
            catch (Exception ex)
            {
                err = -1;
                npgsqltrnTransaction.Rollback();
                npgsqlconDBConnection.Close();
                blnTransactionFlag = false;
                throw;
            }
            finally
            {
                if (blnTransactionFlag == false & err == 0)
                {
                    //  sqltrnTransaction.Commit()
                    npgsqlconDBConnection.Close();
                }
            }

        }

        /// <summary>
        ///  Name : ExecuteUpdateSelect
        ///  Author : Viral / 25 Jul 2013
        ///  Purpose : It will execute and update the table according to query and It return selected data in the dataset
        /// </summary>
        /// <param name="param"></param>
        /// <remarks></remarks>
        public void ExecuteUpdateSelect(params object[] param)
        {
            int err = 0;
            NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter();
            try
            {
                if (!(npgsqlconDBConnection.State == ConnectionState.Open))
                {
                    npgsqlconDBConnection.Open();
                }

                if (npgsqltrnTransaction == null)
                {
                    npgsqltrnTransaction = npgsqlconDBConnection.BeginTransaction();
                }

                GenerateCommand(param);
                Adapter.SelectCommand = npgsqlcomCommand;
                dsetDataSet = new DataSet();
                Adapter.Fill(dsetDataSet);
            }
            catch (Exception ex)
            {
                err = -1;
                npgsqltrnTransaction.Rollback();
                npgsqlconDBConnection.Close();
                blnTransactionFlag = false;
                throw ex;
            }
            finally
            {
                if (blnTransactionFlag == false & err == 0)
                {
                    npgsqltrnTransaction.Commit();
                    npgsqlconDBConnection.Close();
                }
            }
        }


        public void ExecuteSelect(ArrayList dt)
        {
            int err = 0;
            NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter();
            try
            {
                if (!(npgsqlconDBConnection.State == ConnectionState.Open))
                {
                    npgsqlconDBConnection.Open();
                }

                if (npgsqltrnTransaction == null)
                {
                    npgsqltrnTransaction = npgsqlconDBConnection.BeginTransaction();
                }

                npgsqlcomCommand = new NpgsqlCommand("", npgsqlconDBConnection);
                npgsqlcomCommand.CommandText = strProcName;
                npgsqlcomCommand.CommandType = CommandType.StoredProcedure;
                npgsqlcomCommand.CommandTimeout = intCommandTimeout;

                if ((npgsqltrnTransaction != null))
                {
                    npgsqlcomCommand.Transaction = npgsqltrnTransaction;
                }

                NpgsqlCommandBuilder.DeriveParameters(npgsqlcomCommand);

                int k = 0;

                for (k = 0; k <= dt.Count - 1; k++)
                {
                    //npgsqlcomCommand.Parameters[k + 1].TypeName = "";
                    npgsqlcomCommand.Parameters[k + 1].Value = dt[k];
                }

                k = dt.Count;

                Adapter.SelectCommand = npgsqlcomCommand;
                dsetDataSet = new DataSet();
                Adapter.Fill(dsetDataSet);
            }
            catch (Exception ex)
            {
                err = -1;
                npgsqltrnTransaction.Rollback();
                npgsqlconDBConnection.Close();
                blnTransactionFlag = false;
                throw ex;
            }
            finally
            {
                if (blnTransactionFlag == false & err == 0)
                {
                    npgsqltrnTransaction.Commit();
                    npgsqlconDBConnection.Close();
                }
            }
        }
        /// <summary>
        /// Name:ExecuteUpdate
        /// Author:Ankita S/ 04 July 2014
        /// Purpose: Method used for returning codevalue parameter value for arraylist 
        /// if we are using this method make sure that last parameter name will be CodeValue and as output parameter.
        /// </summary>
        /// <param name="param"></param>
        /// <remarks></remarks>
        /// <modifications>
        /// </modifications>
        public void ExecuteUpdateWithOutput(ref string strCode, ArrayList dt, params object[] param)
        {
            int err = 0;
            try
            {
                if (!(npgsqlconDBConnection.State == ConnectionState.Open))
                {
                    npgsqlconDBConnection.Open();
                }

                if (npgsqltrnTransaction == null)
                {
                    npgsqltrnTransaction = npgsqlconDBConnection.BeginTransaction();
                }


                npgsqlcomCommand = new NpgsqlCommand("", npgsqlconDBConnection);
                npgsqlcomCommand.CommandText = strProcName;
                npgsqlcomCommand.CommandType = CommandType.StoredProcedure;
                npgsqlcomCommand.CommandTimeout = intCommandTimeout;

                if ((npgsqltrnTransaction != null))
                {
                    npgsqlcomCommand.Transaction = npgsqltrnTransaction;
                }

                NpgsqlCommandBuilder.DeriveParameters(npgsqlcomCommand);

                int k = 0;

                for (k = 0; k <= dt.Count - 1; k++)
                {
                    //npgsqlcomCommand.Parameters[k + 1].TypeName = "";
                    npgsqlcomCommand.Parameters[k + 1].Value = dt[k];
                }

                k = dt.Count;


                for (int i = 1; i <= (param.GetUpperBound(0) + 1); i += 1)
                {
                    if ((Convert.ToInt32(npgsqlcomCommand.Parameters[i + k].Value) > 0))
                    {
                        npgsqlcomCommand.Parameters[i + k].Value = Convert.ToInt32(param[i - 1]);
                    }
                    else
                    {
                        npgsqlcomCommand.Parameters[i + k].Value = Convert.ToInt32(param[i - 1]);
                    }
                }
                npgsqlcomCommand.Parameters[npgsqlcomCommand.Parameters.Count - 1].Value = strCode;

                npgsqlcomCommand.ExecuteNonQuery();
                intReturnValue = Convert.ToInt32(npgsqlcomCommand.Parameters[0].Value);
                strCode = npgsqlcomCommand.Parameters["@CodeValue"].Value.ToString();
                npgsqlcomCommand.Parameters.Clear();


            }
            catch (Exception ex)
            {
                err = -1;
                npgsqltrnTransaction.Rollback();
                npgsqlconDBConnection.Close();
                blnTransactionFlag = false;
                throw;
            }
            finally
            {
                if (blnTransactionFlag == false & err == 0)
                {
                    npgsqltrnTransaction.Commit();
                    npgsqlconDBConnection.Close();
                }
            }

        }

        /// <summary>
        ///  Name : ExecuteUpdateWithStringOutput
        ///  Author : Riddhi / 27 Nov 2014
        /// </summary>
        /// <param name="strCode"></param>
        /// <param name="param"></param>
        /// <remarks></remarks>
        public void ExecuteUpdateWithStringOutput(ref string strCode, params object[] param)
        {
            int err = 0;
            try
            {
                if (!(npgsqlconDBConnection.State == ConnectionState.Open))
                {
                    npgsqlconDBConnection.Open();
                }

                if (npgsqltrnTransaction == null)
                {
                    npgsqltrnTransaction = npgsqlconDBConnection.BeginTransaction();
                }

                GenerateCommand(param);
                npgsqlcomCommand.Parameters[npgsqlcomCommand.Parameters.Count - 1].Value = strCode;

                npgsqlcomCommand.ExecuteNonQuery();
                strOutPut = npgsqlcomCommand.Parameters[0].Value.ToString();
                strCode = npgsqlcomCommand.Parameters["@CodeValue"].Value.ToString();
                npgsqlcomCommand.Parameters.Clear();


            }
            catch (Exception ex)
            {
                err = -1;
                npgsqltrnTransaction.Rollback();
                npgsqlconDBConnection.Close();
                blnTransactionFlag = false;
                throw ex;
            }
            finally
            {
                if (blnTransactionFlag == false & err == 0)
                {
                    npgsqltrnTransaction.Commit();
                    npgsqlconDBConnection.Close();
                }
            }

        }


        public void ExecuteStoredProcNonQuery(String StoredProcName, NpgsqlParameter[] spa)
        {
            NpgsqlConnection dbconn = new NpgsqlConnection();
            dbconn.ConnectionString = strConnectionString;
            dbconn.Open();

            NpgsqlCommand dbCommand = new NpgsqlCommand(StoredProcName, dbconn);
            dbCommand.CommandType = CommandType.StoredProcedure;

            foreach (NpgsqlParameter sp in spa)
            {
                dbCommand.Parameters.Add(sp);
            }

            try
            {
                objReturnObject = new object();
                objReturnObject = dbCommand.ExecuteScalar();
                dbCommand.Dispose();
                dbconn.Close();
                dbconn.Dispose();

            }
            catch (Exception ex)
            {
                dbCommand.Dispose();
                dbconn.Close();
                dbconn.Dispose();

                throw (ex);
            }
        }


        public int ExecuteNonQuery(string query, NpgsqlParameter[] paras)
        {
            NpgsqlConnection conn = null;
            int rowAffected = 0;
            try
            {
                using (conn = new NpgsqlConnection(strConnectionString))
                {
                    conn.Open();

                    // Insert some data
                    using (var cmd = new NpgsqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = query;

                        cmd.Parameters.AddRange(paras);

                        rowAffected = cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                return rowAffected;
            }
            catch (Exception e)
            {
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        #endregion
    }
}

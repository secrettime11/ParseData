using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseData
{
    class SQliteDb
    {
        public static string dbPath = "";
        public static string cnStr = "data source=" + dbPath;
        /// <summary>建立資料庫連線</summary>
        /// <param name="database">資料庫名稱</param>
        /// <returns></returns>
        public SQLiteConnection OpenConnection(string database)
        {
            var conntion = new SQLiteConnection()
            {
                ConnectionString = $"Data Source={database};Version=3;New=False;Compress=True;"
            };
            if (conntion.State == ConnectionState.Open) conntion.Close();
            conntion.Open();
            return conntion;
        }
        /// <summary>建立新資料庫</summary>
        /// <param name="database">資料庫名稱</param>
        public void CreateDatabase(string database)
        {
            var connection = new SQLiteConnection()
            {
                ConnectionString = $"Data Source={database};Version=3;New=True;Compress=True;"
            };
            connection.Open();
            connection.Close();
        }
        /// <summary>建立新資料表</summary>
        /// <param name="database">資料庫名稱</param>
        /// <param name="sqlCreateTable">建立資料表的 SQL 語句</param>
        public bool CreateTable(string database, string sqlCreateTable)
        {
            var connection = OpenConnection(database);
            //connection.Open();
            var command = new SQLiteCommand(sqlCreateTable, connection);
            var mySqlTransaction = connection.BeginTransaction();
            try
            {
                command.Transaction = mySqlTransaction;
                command.ExecuteNonQuery();
                mySqlTransaction.Commit();
            }
            catch (Exception)
            {
                mySqlTransaction.Rollback();
                return false;
            }
            if (connection.State == ConnectionState.Open) connection.Close();
            return true;
        }
        /// <summary>新增\修改\刪除資料</summary>
        /// <param name="database">資料庫名稱</param>
        /// <param name="sqlManipulate">資料操作的 SQL 語句</param>
        public void Manipulate(string database, string sqlManipulate)
        {
            var connection = OpenConnection(database);
            var command = new SQLiteCommand(sqlManipulate, connection);
            var mySqlTransaction = connection.BeginTransaction();
            try
            {
                command.Transaction = mySqlTransaction;
                command.ExecuteNonQuery();
                mySqlTransaction.Commit();
            }
            catch (Exception ex)
            {
                mySqlTransaction.Rollback();
                throw (ex);
            }
            if (connection.State == ConnectionState.Open) connection.Close();
        }
        /// <summary>讀取資料</summary>
        /// <param name="database">資料庫名稱</param>
        /// <param name="sqlQuery">資料查詢的 SQL 語句</param>
        /// <returns></returns>
        public DataTable GetDataTable(string database, string sqlQuery)
        {
            var connection = OpenConnection(database);
            var dataAdapter = new SQLiteDataAdapter(sqlQuery, connection);
            var myDataTable = new DataTable();
            var myDataSet = new DataSet();
            myDataSet.Clear();
            dataAdapter.Fill(myDataSet);
            myDataTable = myDataSet.Tables[0];
            if (connection.State == ConnectionState.Open) connection.Close();
            return myDataTable;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="datapath">database</param>
        /// <param name="header_">header list</param>
        /// <param name="data_">data list</param>
        public void Add(List<string> datapath, List<string> header_, Dictionary<string, string> data_, string insertstring)
        {
            int header_count = header_.Count();
            int counter = 1;
            // 建立 SQLite 資料庫
            if (!File.Exists(datapath[0]))
            {
                CreateDatabase(datapath[0]);
                // 建立資料表 TestTable
                var createtablestring = $@"CREATE TABLE {datapath[1]} (";

                foreach (var header in header_)
                {
                    if (counter == header_count)
                        createtablestring += $"{header} VARCHAR(32));";
                    else
                        createtablestring += $"{header} VARCHAR(32),";
                    counter++;
                }
                CreateTable(datapath[0], createtablestring);
            }

            // FINALL STRING WHICH IS GOING TO SEND TO SQLITE
            foreach (var data in data_)
            {
                counter = 1;

                insertstring += $@"INSERT INTO {datapath[1]} (";
                foreach (var header in header_)
                {
                    if (counter == header_count)
                        insertstring += $"{header})";
                    else
                        insertstring += $"{header}, ";
                    counter++;
                }
                insertstring += $" VALUES ('{data.Key}', ";

                string[] value = data.Value.Split(new string[] { "__" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < value.Count(); i++)
                {
                    if (i == value.Count() - 1)
                        insertstring += $"'{value[i]}');" + Environment.NewLine;
                    else
                        insertstring += $"'{value[i]}', ";
                }

            }
            Console.WriteLine(insertstring);
            // 插入資料到 TestTable 表中
            Manipulate(datapath[0], insertstring);
        }
        /// <summary>
        /// 檢查資料表存不存在 True = 存在
        /// </summary>
        /// <param name="database">database</param>
        /// <param name="tableName">table name</param>
        /// <returns></returns>
        public bool CheckDatatable(string database, string tableName)
        {
            var connection = OpenConnection(database);
            SQLiteCommand mDbCmd = connection.CreateCommand();
            mDbCmd.CommandText = $"SELECT COUNT(*) FROM sqlite_master where type='table' and name='{tableName}';";
            if (0 == Convert.ToInt32(mDbCmd.ExecuteScalar()))
            {
                if (connection.State == ConnectionState.Open) connection.Close();
                return false;
            }
            else
            {
                if (connection.State == ConnectionState.Open) connection.Close();
                return true;
            }
        }


        /*  LISTED  */

        /// <summary>
        /// Add data to sqlite
        /// </summary>
        /// <param name="database">database name</param>
        /// <param name="tableName">table name</param>
        /// <param name="header_">header</param>
        /// <param name="data_">data list</param>
        /// <param name="insertString">command</param>
        public bool DataAdd(string database, string tableName, List<string> header_, List<string> data_, string insertString)
        {

            int header_count = header_.Count();
            int counter = 1;
            // 建立 SQLite 資料庫
            if (!File.Exists(database))
                CreateDatabase(database);


            // 建立資料表 TestTable
            var createtablestring = $@"CREATE TABLE {tableName} (";

            foreach (var header in header_)
            {
                if (counter == header_count)
                    createtablestring += $"{header} VARCHAR(32));";
                else
                    createtablestring += $"{header} VARCHAR(32),";
                counter++;
            }
            if (CreateTable(database, createtablestring))
            {
                // Final string send to SQL
                foreach (var data in data_)
                {
                    counter = 1;

                    insertString += $@"INSERT INTO {tableName} (";
                    foreach (var header in header_)
                    {
                        // Last column
                        if (counter == header_count)
                            insertString += $"{header})";
                        else
                            insertString += $"{header}, ";
                        counter++;
                    }
                    insertString += $" VALUES (";

                    string[] value = data.Split('_');
                    for (int i = 0; i < value.Count(); i++)
                    {
                        if (i == value.Count() - 1)
                            insertString += $"'{value[i]}');" + Environment.NewLine;
                        else
                            insertString += $"'{value[i]}', ";
                    }
                }
                // Insert into datatable
                Manipulate(database, insertString);
                return true;
            }
            else
                return false;
        }
    }
}

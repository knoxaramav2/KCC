using System;
using System.Collections;
using System.Data;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using System.Linq;
using CommonLangLib;

namespace CodeTranslator
{
    public class Db
    {
        private const string DbLiteNane = "KCC.sqlite";
        private const string DbCOnnect = "Data Source=KCC.sqlite;Version=3;";
        private static SQLiteConnection _sqLiteConnection;
        public ProgramGraph Graph { get; }

        public Db()
        {
            if (Graph == null)
            {
                Graph = new ProgramGraph();
            }

            //initialize database
            if (_sqLiteConnection == null)
            {
                SQLiteConnection.CreateFile(DbLiteNane);
                _sqLiteConnection = new SQLiteConnection(DbCOnnect);
            }
            else
            {
                _sqLiteConnection.Open();
                return;
            }

            _sqLiteConnection.Open();

            //generate database
            BuildClassTable();
            BuildAssemblyTable();
            BuildVarInstanceTable();
            BuildFunctionTable();


            /*
            var tables = _sqLiteConnection.GetSchema("Tables");
            var columns = _sqLiteConnection.GetSchema("Columns");
            Debug.PrintDbg(">>>DATABASE<<<");
            if (Debug.DebugEnabled())
            {
                var command = new SQLiteCommand("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name", _sqLiteConnection);
                using (var reader = command.ExecuteReader())
                {
                    var i = 0;
                    while (reader.Read())
                    {
                        Debug.PrintDbg((string)reader.GetValue(i));
                    }
                }
            }

            Debug.PrintDbg("<<<DATABASE>>>");*/
        }

        private void BuildFunctionTable()
        {
            var sql = new SqlBuilder(_sqLiteConnection);

            //add columns
            sql.AddColumn("id", SqlBuilder.Types.Varchar);
            sql.AddColumn("type", SqlBuilder.Types.Varchar);
            sql.AddColumn("args", SqlBuilder.Types.Varchar);
            sql.AddColumn("defaultval", SqlBuilder.Types.Varchar);

            sql.Build("functions");
        }

        private void BuildVarInstanceTable()
        {
            var sql = new SqlBuilder(_sqLiteConnection);

            //add columns
            sql.AddColumn("id", SqlBuilder.Types.Varchar);
            sql.AddColumn("type", SqlBuilder.Types.Varchar);
            sql.AddColumn("defvalue", SqlBuilder.Types.Varchar);


            sql.Build("variables");
        }

        private void BuildClassTable()
        {
            var sql = new SqlBuilder(_sqLiteConnection);

            //add columns
            sql.AddColumn("id", SqlBuilder.Types.Varchar);

            sql.Build("classes");
        }

        private void BuildAssemblyTable()
        {
            var sql = new SqlBuilder(_sqLiteConnection);
            
            //add columns
            sql.AddColumn("id", SqlBuilder.Types.Varchar);

            sql.Build("assembly");
        }

        public void SaveAssembly(string id)
        {
            var command = new SQLiteCommand("INSERT INTO assembly (id) VALUES (?)", _sqLiteConnection);
            command.Parameters.AddWithValue("id",id);
            var rows = command.ExecuteNonQuery();

            Graph.AddAssembly(id);
        }

        public void SaveFunction(string id, string type, string args=null, string defArgs=null)
        {
            var command = new SQLiteCommand("INSERT INTO functions (id, type, args, defaultval) VALUES (?,?,?,?)", _sqLiteConnection);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("type", type);
            command.Parameters.AddWithValue("args", args);
            command.Parameters.AddWithValue("defaultval", defArgs);
            var rows = command.ExecuteNonQuery();

            Graph.AddFunction(id, type, args, defArgs);
        }

        public void UpdateFunctionParams(string args, string defArgs)
        {
            Graph.AddParameter(args, defArgs);
        }

        public void SaveVariable(string id, string type, string defValue=null)
        {
            var command = new SQLiteCommand("INSERT INTO variables (id, type, defvalue) VALUES (?,?,?)", _sqLiteConnection);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("type", type);
            command.Parameters.AddWithValue("nullable defvalue", defValue);
            var rows = command.ExecuteNonQuery();
            //Debug.PrintDbg($"Inserted {scope}.{id} = {defValue}");
            Graph.AddVariable(id, type, defValue);
        }

        public void SaveInstruction(string op, string arg0=null, string arg1=null)
        {
            Graph.AddInstruction(op, arg0, arg1);
        }

        public DataTable Query(string query)
        {
            SQLiteDataAdapter adapter;
            var dataTable = new DataTable();

            try
            {
                var command = _sqLiteConnection.CreateCommand();
                command.CommandText = query;
                adapter = new SQLiteDataAdapter(command);
                adapter.Fill(dataTable);

            }
            catch (SQLiteException e)
            {
                //ErrorReporter.GetInstance().Add(); TODO
                return null;
            }

            return dataTable;
        }

        public void Close()
        {
            _sqLiteConnection.Close();
        }
    }

    //for laziness
    public class SqlBuilder
    {
        public enum Types
        {
            Varchar,
            Int,
            Float,
            Double,
            BINT,
            BINT_PRIMARY
        }

        private static string GetTypeString(Types type)
        {
            switch (type)
            {
                case Types.Varchar: return "VARCHAR (128)";
                case Types.Int: return "INT";
                case Types.Float: return "FLOAT";
                case Types.Double: return "DOUBLE";
                case Types.BINT: return "BIGINT";
                case Types.BINT_PRIMARY: return "BIGINT PRIMARY";
            }

            return "";
        }

        private class Column
        {
            internal string _name;
            internal Types _type;

            internal Column(string name, Types type)
            {
                _name = name;
                _type = type;
            }
        }

        private SQLiteConnection _sqLiteConnection;

        private ArrayList _columns;

        public SqlBuilder(SQLiteConnection sqLiteConnection)
        {
            _sqLiteConnection = sqLiteConnection;

            _columns = new ArrayList();
        }

        public bool AddColumn(string name, Types type)
        {

            foreach (Column column in _columns)
            {
                if (column._name != name) continue;
                return false;
            }

            _columns.Add(new Column(name, type));

            return true;
        }

        public bool Build(string tableName)
        {
            var command = "CREATE TABLE " + tableName;

            if (_columns.Count > 0)
            {
                command += " (";
            }

            for (var i = 0; i < _columns.Count; ++i)
            {
                var c = (Column) _columns[i];
                var type = GetTypeString(c._type);

                command += $"{c._name} {type}";

                if (i < _columns.Count - 1)
                {
                    command += ", ";
                }
            }

            if (_columns.Count > 0)
            {
                command += ")";
            }

            var sqlCommand = new SQLiteCommand(command, _sqLiteConnection);
            sqlCommand.ExecuteNonQuery();

            return true;
        }


    }
}
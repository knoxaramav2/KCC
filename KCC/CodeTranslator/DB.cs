using System;
using System.Collections;
using System.Data;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SQLite;
using CommonLangLib;

namespace CodeTranslator
{
    public class Db
    {
        private SQLiteConnection _sqLiteConnection;

        public Db()
        {
            //initialize database
            SQLiteConnection.CreateFile("KCC.sqlite");
            _sqLiteConnection = new SQLiteConnection("Data Source=KCC.sqlite;Version=3;");
            _sqLiteConnection.Open();

            //generate database
            BuildScopeTable();
            BuildClassTable();
            BuildAssemblyTable();
            BuildVarInstanceTable();
            BuildFunctionTable();

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

            Debug.PrintDbg("<<<DATABASE>>>");
        }

        private void BuildFunctionTable()
        {
            var sql = new SqlBuilder(_sqLiteConnection);

            //add columns
            sql.AddColumn("id", SqlBuilder.Types.Varchar);
            sql.AddColumn("scope", SqlBuilder.Types.Varchar);
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
            sql.AddColumn("scope", SqlBuilder.Types.Varchar);
            sql.AddColumn("type", SqlBuilder.Types.Varchar);

            sql.Build("variables");
        }

        private void BuildScopeTable()
        {
            var sql = new SqlBuilder(_sqLiteConnection);

            //add columns
            sql.AddColumn("id", SqlBuilder.Types.Varchar);
            sql.AddColumn("scope", SqlBuilder.Types.Varchar);

            sql.Build("scope");
        }

        private void BuildClassTable()
        {
            var sql = new SqlBuilder(_sqLiteConnection);

            //add columns
            sql.AddColumn("id", SqlBuilder.Types.Varchar);
            sql.AddColumn("scope", SqlBuilder.Types.Varchar);

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
            Debug.PrintDbg($"Inserted {rows} rows into table 'assembly'");
        }

        public void SaveFunction(string id, string scope, string type, string args, string defArgs="")
        {
            var command = new SQLiteCommand("INSERT INTO functions (id, scope, type, args, defaultval) VALUES (?,?,?,?,?)", _sqLiteConnection);
            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("scope", scope);
            command.Parameters.AddWithValue("type", type);
            command.Parameters.AddWithValue("args", args);
            command.Parameters.AddWithValue("defaultval", defArgs);
            var rows = command.ExecuteNonQuery();
            Debug.PrintDbg($"Inserted {rows} rows into table 'functions'");
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
            Double
        }

        private static string GetTypeString(Types type)
        {
            switch (type)
            {
                case Types.Varchar: return "VARCHAR (128)";
                case Types.Int: return "INT";
                case Types.Float: return "FLOAT";
                case Types.Double: return "DOUBLE";
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
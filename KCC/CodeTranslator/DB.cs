using System;
using System.Collections;
using System.Data.SQLite;

namespace CodeTranslator
{
    public class Db
    {
        private SQLiteConnection _sqLiteConnection;

        public Db()
        {
            SQLiteConnection.CreateFile("KCC.sqlite");
            _sqLiteConnection = new SQLiteConnection("Data Source=KCC.sqlite;Version=1;");
            _sqLiteConnection.Open();
        }

        private void BuildScopeTable()
        {

        }

        private void BuildClassTable()
        {

        }

        private void BuildAssemblyTable()
        {
            var sql = new SqlBuilder(_sqLiteConnection);
            
            //add columns
            sql.AddColumn("id", SqlBuilder.Types.Varchar);

            sql.Build("assembly");
        }

        public void Close()
        {
            _sqLiteConnection.Close();
        }
    }

    //for laziness
    internal class SqlBuilder
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

                command += string.Format($"{0} {1}", c._name, type);

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
using System.Data;
using System.Data.SqlClient;


namespace BlogBuster.Controllers
{
    public class SqlConnectionGenerator
    {

        private static string _strServer;
        private static string _strDatabaseName;
        private static string _strUser;
        private static string _strPassword;

        // Default login
        public static ConnectionType ConnectionType = ConnectionType.Admin;

        public static SqlConnection GetConnection()
        {
            switch (ConnectionType)
            {
                case ConnectionType.Admin:
                    _strServer = "DESKTOP-ABNEED";
                    _strDatabaseName = "BlogBusterDB";
                    _strUser = "admin";
                    _strPassword = "admin";
                    break;

                case ConnectionType.Reader:
                    _strServer = "DESKTOP-ABNEED";
                    _strDatabaseName = "BlogBusterDB";
                    _strUser = "reader";
                    _strPassword = "reader";
                    break;
            }

            string strConnectionString = @"Data Source = " + _strServer + "; " +
                            "Initial Catalog = " + _strDatabaseName + "; " +
                            "Persist Security Info = True; " +
                            "User ID = " + _strUser + "; " +
                            "Password = " + _strPassword + "";

            return new SqlConnection(strConnectionString);
        }

        public static DataTable ExecuteStoreProcedure(string strStoreProcedure)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand command = new SqlCommand(strStoreProcedure, conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                conn.Close();
            }

            return dataTable;
        }

        public static DataTable ExecuteStoreProcedure(string strStoreProcedure, string[] strParameters, object[] objValues)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                SqlCommand command = new SqlCommand(strStoreProcedure, conn);
                command.CommandType = CommandType.StoredProcedure;

                if (strParameters.Length != objValues.Length)
                {
                    throw new System.Exception("All declared parameters must have value assigned.");
                }

                int i = 0;
                foreach (var parameter in strParameters)
                    command.Parameters.AddWithValue(parameter, objValues[i++]);
                command.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                conn.Close();
            }

            return dataTable;
        }
    }

    public enum ConnectionType
    {
        Admin = 0,
        Reader = 1
    }
}
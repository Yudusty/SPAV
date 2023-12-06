using Microsoft.Data.SqlClient;

public abstract class Database : IDisposable
{
    protected SqlConnection connection;

    public Database()
    {
        connection = new SqlConnection("Data Source=LAPTOP-2TO3057F\\SQLEXPRESS; Initial Catalog=Inter_4; Integrated Security=True; TrustServerCertificate=True; Connection Timeout=60;");
        connection.Open();
    }

    public void Dispose()
    {
        connection.Close();
    }
}

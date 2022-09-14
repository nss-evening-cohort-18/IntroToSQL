using System.Data.SqlClient;

namespace IntroToSQL.Repositories;

public abstract class BaseRepository
{
    private readonly string _connectionString;
    protected SqlConnection Connection => new SqlConnection(_connectionString);

    public BaseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }
}

using Loja.Web.Infra.CrossCutting.Config;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Loja.Web.Infra.Data.Repositories
{
    public class Repository
    {
        #region << PROPERTIES >>
        private int Timeout { get; set; }
        private readonly SqlConnection _sqlConnection;
        #endregion

        #region << CONSTRUCTOR >>
        public Repository()
        {
            Timeout = 0;
            _sqlConnection = new SqlConnection(Settings.Configuration.GetConnectionString("DefaultConnection"));
        }
        #endregion

        #region << METHODS >>

        #region ConnectAsync
        protected async Task<SqlConnection> ConnectAsync()
        {
            if (_sqlConnection.State == ConnectionState.Closed)
            {
                if (string.IsNullOrEmpty(_sqlConnection.ConnectionString))
                {
                    _sqlConnection.ConnectionString = Settings.Configuration.GetConnectionString("DefaultConnection");
                }
                await _sqlConnection.OpenAsync();
            }
            return _sqlConnection;
        }
        #endregion

        #endregion
    }
}

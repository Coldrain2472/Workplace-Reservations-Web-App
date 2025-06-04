using JobsBookingApp.Repository.Base;
using JobsBookingApp.Repository.Helpers;
using JobsBookingApp.Repository.Interfaces.EmployeeFavorite;
using Microsoft.Data.SqlClient;

namespace JobsBookingApp.Repository.Implementations.EmployeeFavorite
{
    public class EmployeeFavoriteRepository : BaseRepository<Models.EmployeeFavorite>, IEmployeeFavoriteRepository
    {
        private const string IdDbFieldEnumeratorName = "EmployeeId";

        protected override string GetTableName()
        {
            return "EmployeeFavorites";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "WorkplaceId"
        };

        protected override Models.EmployeeFavorite MapEntity(SqlDataReader reader)
        {
            return new Models.EmployeeFavorite
            {
                EmployeeId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                WorkplaceId = Convert.ToInt32(reader["WorkplaceId"])
            };
        }

        public Task<int> CreateAsync(Models.EmployeeFavorite entity)
        {
            return base.CreateAsync(entity);
        }

        public Task<Models.EmployeeFavorite> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.EmployeeFavorite> RetrieveCollectionAsync(EmployeeFavoriteFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.EmployeeId is not null)
            {
                commandFilter.AddCondition("EmployeeId", filter.EmployeeId);
            }
            if (filter.WorkplaceId is not null)
            {
                commandFilter.AddCondition("WorkplaceId", filter.WorkplaceId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, EmployeeFavoriteUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "EmployeeFavorites", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("WorkplaceId", update.WorkplaceId);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }

        public async Task<bool> DeleteAsync(int employeeId, int workplaceId)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();
            using SqlCommand command = connection.CreateCommand();
            using SqlTransaction transaction = connection.BeginTransaction();

            command.Transaction = transaction;
            command.CommandText = $"DELETE FROM {GetTableName()} WHERE EmployeeId = @EmployeeId AND WorkplaceId = @WorkplaceId";

            command.Parameters.AddWithValue("@EmployeeId", employeeId);
            command.Parameters.AddWithValue("@WorkplaceId", workplaceId);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected != 1)
            {
                transaction.Rollback();
                throw new Exception($"Just one row should be deleted! Command aborted, because {rowsAffected} could have been deleted!");
            }

            transaction.Commit();
            return true;
        }
    }
}

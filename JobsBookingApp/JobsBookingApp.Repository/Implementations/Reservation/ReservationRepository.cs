using JobsBookingApp.Repository.Base;
using JobsBookingApp.Repository.Helpers;
using JobsBookingApp.Repository.Interfaces.Reservation;
using Microsoft.Data.SqlClient;

namespace JobsBookingApp.Repository.Implementations.Reservation
{
    public class ReservationRepository : BaseRepository<Models.Reservation>, IReservationRepository
    {
        private const string IdDbFieldEnumeratorName = "ReservationId";

        protected override string GetTableName()
        {
            return "Reservations";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "EmployeeId",
            "StartTime",
            "EndTime",
            "WorkplaceId",
            "CreatedAt"
        };

        protected override Models.Reservation MapEntity(SqlDataReader reader)
        {
            return new Models.Reservation
            {
                ReservationId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                StartTime = Convert.ToDateTime(reader["StartTime"]),
                EndTime = Convert.ToDateTime(reader["EndTime"]),
                WorkplaceId = Convert.ToInt32(reader["WorkplaceId"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
            };
        }

        public Task<int> CreateAsync(Models.Reservation entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.Reservation> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Reservation> RetrieveCollectionAsync(ReservationFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.EmployeeId is not null)
            {
                commandFilter.AddCondition("EmployeeId", filter.EmployeeId);
            }
            if (filter.StartTime is not null)
            {
                commandFilter.AddCondition("StartTime", filter.StartTime);
            }
            if (filter.EndTime is not null)
            {
                commandFilter.AddCondition("EndTime", filter.EndTime);
            }
            if (filter.WorkplaceId is not null)
            {
                commandFilter.AddCondition("WorkplaceId", filter.WorkplaceId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, ReservationUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "Reservations", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("WorkplaceId", update.WorkplaceId);
            updateCommand.AddSetClause("EndTime", update.EndTime);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }
    }
}

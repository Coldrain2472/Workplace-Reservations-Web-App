using JobsBookingApp.Repository.Base;
using JobsBookingApp.Repository.Helpers;
using JobsBookingApp.Repository.Interfaces.Workplace;
using Microsoft.Data.SqlClient;

namespace JobsBookingApp.Repository.Implementations.Workplace
{
    public class WorkplaceRepository : BaseRepository<Models.Workplace>, IWorkplaceRepository
    {
        private const string IdDbFieldEnumeratorName = "WorkplaceId";

        protected override string GetTableName()
        {
            return "Workplaces";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Floor",
            "Zone",
            "HasMonitor",
            "HasDock",
            "IsNearWindow",
            "IsNearPrinter",
            "IsAvailable"
        };

        protected override Models.Workplace MapEntity(SqlDataReader reader)
        {
            return new Models.Workplace
            {
                WorkplaceId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Floor = Convert.ToInt32(reader["Floor"]),
                Zone = Convert.ToString(reader["Zone"]),
                HasMonitor = Convert.ToBoolean(reader["HasMonitor"]),
                HasDock = Convert.ToBoolean(reader["HasDock"]),
                IsNearWindow = Convert.ToBoolean(reader["IsNearWindow"]),
                IsNearPrinter = Convert.ToBoolean(reader["IsNearPrinter"]),
                IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
            };
        }

        public Task<int> CreateAsync(Models.Workplace entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.Workplace> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Workplace> RetrieveCollectionAsync(WorkplaceFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Floor is not null)
            {
                commandFilter.AddCondition("Floor", filter.Floor);
            }
            if (filter.Zone is not null)
            {
                commandFilter.AddCondition("Zone", filter.Zone);
            }
            if (filter.HasMonitor is not null)
            {
                commandFilter.AddCondition("HasMonitor", filter.HasMonitor);
            }
            if (filter.HasDock is not null)
            {
                commandFilter.AddCondition("HasDock", filter.HasDock);
            }
            if (filter.IsNearWindow is not null)
            {
                commandFilter.AddCondition("IsNearWindow", filter.IsNearWindow);
            }
            if (filter.IsNearPrinter is not null)
            {
                commandFilter.AddCondition("IsNearPrinter", filter.IsNearPrinter);
            }
            if (filter.IsAvailable is not null)
            {
                commandFilter.AddCondition("IsAvailable", filter.IsAvailable);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, WorkplaceUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "Workplaces", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("IsAvailable", update.IsAvailable);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }
    }
}

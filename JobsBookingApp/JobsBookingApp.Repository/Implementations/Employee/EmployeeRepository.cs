using JobsBookingApp.Repository.Base;
using JobsBookingApp.Repository.Helpers;
using JobsBookingApp.Repository.Interfaces.Employee;
using Microsoft.Data.SqlClient;

namespace JobsBookingApp.Repository.Implementations.Employee
{
    public class EmployeeRepository : BaseRepository<Models.Employee>, IEmployeeRepository
    {
        private const string IdDbFieldEnumeratorName = "EmployeeId";

        protected override string GetTableName()
        {
            return "Employees";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Name",
            "Email",
            "Username",
            "Password"
        };

        protected override Models.Employee MapEntity(SqlDataReader reader)
        {
            return new Models.Employee
            {
                EmployeeId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"]),
                Email = Convert.ToString(reader["Email"]),
                Username = Convert.ToString(reader["Username"]),
                Password = Convert.ToString(reader["Password"])
            };
        }

        public Task<int> CreateAsync(Models.Employee entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }

        public Task<Models.Employee> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Employee> RetrieveCollectionAsync(EmployeeFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Email is not null)
            {
                commandFilter.AddCondition("Email", filter.Email);
            }
            if (filter.Username is not null)
            {
                commandFilter.AddCondition("Username", filter.Username);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, EmployeeUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "Employees", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Name", update.Name);
            updateCommand.AddSetClause("Email", update.Email);
            updateCommand.AddSetClause("Username", update.Username);
            updateCommand.AddSetClause("Password", update.Password);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }
    }
}
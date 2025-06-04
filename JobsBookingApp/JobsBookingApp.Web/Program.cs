using JobsBookingApp.Repository;
using JobsBookingApp.Repository.Implementations.Employee;
using JobsBookingApp.Repository.Implementations.EmployeeFavorite;
using JobsBookingApp.Repository.Implementations.Reservation;
using JobsBookingApp.Repository.Implementations.Workplace;
using JobsBookingApp.Repository.Interfaces.Employee;
using JobsBookingApp.Repository.Interfaces.EmployeeFavorite;
using JobsBookingApp.Repository.Interfaces.Reservation;
using JobsBookingApp.Repository.Interfaces.Workplace;
using JobsBookingApp.Services.Implementations.Authentication;
using JobsBookingApp.Services.Implementations.Employee;
using JobsBookingApp.Services.Implementations.EmployeeFavorite;
using JobsBookingApp.Services.Implementations.Reservation;
using JobsBookingApp.Services.Implementations.Workplace;
using JobsBookingApp.Services.Interfaces.Authentication;
using JobsBookingApp.Services.Interfaces.Employee;
using JobsBookingApp.Services.Interfaces.EmployeeFavorite;
using JobsBookingApp.Services.Interfaces.Reservation;
using JobsBookingApp.Services.Interfaces.Workplace;

namespace JobsBookingApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // services
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IWorkplaceService, WorkplaceService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IEmployeeFavoriteService, EmployeeFavoriteService>();

            // repositories
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IWorkplaceRepository, WorkplaceRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();    
            builder.Services.AddScoped<IEmployeeFavoriteRepository, EmployeeFavoriteRepository>();

            // connection to the db
            ConnectionFactory.Initialize(builder.Configuration.GetConnectionString("DefaultConnection"));

            // session properties
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

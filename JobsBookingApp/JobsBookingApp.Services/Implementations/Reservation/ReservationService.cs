using JobsBookingApp.Repository.Interfaces.Employee;
using JobsBookingApp.Repository.Interfaces.EmployeeFavorite;
using JobsBookingApp.Repository.Interfaces.Reservation;
using JobsBookingApp.Repository.Interfaces.Workplace;
using JobsBookingApp.Services.DTOs.Reservation;
using JobsBookingApp.Services.Interfaces.Reservation;

namespace JobsBookingApp.Services.Implementations.Reservation
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IWorkplaceRepository workplaceRepository;
        private readonly IEmployeeFavoriteRepository employeeFavoriteRepository;

        public ReservationService(IReservationRepository _reservationRepository, IEmployeeRepository _employeeRepository, IWorkplaceRepository _workplaceRepository,
            IEmployeeFavoriteRepository _employeeFavoriteRepository)
        {
            reservationRepository = _reservationRepository;
            employeeRepository = _employeeRepository;
            workplaceRepository = _workplaceRepository;
            employeeFavoriteRepository = _employeeFavoriteRepository;
        }

        public async Task<CreateReservationResponse> CreateQuickReservationAsync(CreateReservationRequest request)
        {
            var employee = await employeeRepository.RetrieveAsync(request.EmployeeId);
            if (employee == null)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Employee not found."
                };
            }

            var favorites = await employeeFavoriteRepository
                .RetrieveCollectionAsync(new EmployeeFavoriteFilter { EmployeeId = request.EmployeeId })
                .ToListAsync();

            bool isFavorite = favorites.Any(f => f.WorkplaceId == request.WorkplaceId);
            if (!isFavorite)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Workplace is not in your favorites."
                };
            }

            var workplace = await workplaceRepository.RetrieveAsync(request.WorkplaceId);
            if (workplace == null || !workplace.IsAvailable)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Workplace not found or not available."
                };
            }

            DateTime nextWorkDay = DateTime.Today.AddDays(1);
            while (nextWorkDay.DayOfWeek == DayOfWeek.Saturday || nextWorkDay.DayOfWeek == DayOfWeek.Sunday)
            {
                nextWorkDay = nextWorkDay.AddDays(1);
            }

            if (request.StartTime.Date != nextWorkDay)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = $"Quick reservation is only allowed for the next working day ({nextWorkDay:yyyy-MM-dd})."
                };
            }

            var existingReservations = await reservationRepository
                .RetrieveCollectionAsync(new ReservationFilter
                {
                    WorkplaceId = request.WorkplaceId,
                    StartTime = nextWorkDay,
                    EndTime = nextWorkDay.AddDays(1)
                })
                .ToListAsync();

            if (existingReservations.Any())
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Workplace is already reserved for the next working day."
                };
            }

            var employeeReservations = await reservationRepository
                .RetrieveCollectionAsync(new ReservationFilter
                {
                    EmployeeId = request.EmployeeId,
                    StartTime = nextWorkDay,
                    EndTime = nextWorkDay.AddDays(1)
                })
                .ToListAsync();

            if (employeeReservations.Any())
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "You already have a reservation for the next working day."
                };
            }

            var newReservation = new Models.Reservation
            {
                EmployeeId = request.EmployeeId,
                WorkplaceId = request.WorkplaceId,
                StartTime = nextWorkDay,
                EndTime = nextWorkDay.AddDays(1),
                CreatedAt = DateTime.Now
            };

            var newReservationId = await reservationRepository.CreateAsync(newReservation);

            return new CreateReservationResponse
            {
                Success = true,
                ReservationId = newReservationId
            };
        }

        public async Task<CreateReservationResponse> CreateReservationAsync(CreateReservationRequest request)
        {
            var employee = await employeeRepository.RetrieveAsync(request.EmployeeId);
            if (employee == null)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Employee not found."
                };
            }

            var workplace = await workplaceRepository.RetrieveAsync(request.WorkplaceId);
            if (workplace == null || !workplace.IsAvailable)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Workplace not found or not available."
                };
            }

            var existingReservationsForWorkplace = await reservationRepository
                .RetrieveCollectionAsync(new ReservationFilter
                {
                    WorkplaceId = request.WorkplaceId,
                    StartTime = request.StartTime,
                    EndTime = request.StartTime.AddDays(1) 
                })
                .ToListAsync();

            if (existingReservationsForWorkplace.Any())
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Workplace is already reserved for the selected date."
                };
            }

            // user cannot have a second reservation for the requested date 
            var existingReservationsForEmployee = await reservationRepository
                .RetrieveCollectionAsync(new ReservationFilter
                {
                    EmployeeId = request.EmployeeId,
                    StartTime = request.StartTime,
                    EndTime = request.StartTime.AddDays(1)
                })
                .ToListAsync();

            if (existingReservationsForEmployee.Any())
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "You already have a reservation for this date."
                };
            }

            // start time cannot be in the past
            if (request.StartTime.Date < DateTime.Now.Date)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Start time cannot be in the past."
                };
            }

            // a reservation cannot be made more than 2 weeks in advance
            if (request.StartTime.Date > DateTime.Now.Date.AddDays(14))
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Reservations can only be made for up to 2 weeks in advance."
                };
            }

            var newReservation = new Models.Reservation
            {
                EmployeeId = request.EmployeeId,
                StartTime = request.StartTime.Date,
                EndTime = request.StartTime.Date.AddDays(1), // reservation valid for full day
                WorkplaceId = request.WorkplaceId,
                CreatedAt = DateTime.Now
            };

            var newReservationId = await reservationRepository.CreateAsync(newReservation);

            return new CreateReservationResponse
            {
                Success = true,
                ReservationId = newReservationId
            };
        }

        public async Task<GetAllReservationsResponse> GetAllAsync()
        {
            var reservations = await reservationRepository.RetrieveCollectionAsync(new ReservationFilter()).ToListAsync();

            var allReservationsResponse = new GetAllReservationsResponse
            {
                Reservations = reservations.Select(MapToReservationInfo).ToList(),
                TotalCount = reservations.Count
            };

            return allReservationsResponse;
        }

        public async Task<GetReservationResponse> GetByIdAsync(int reservationId)
        {
            var reservation = await reservationRepository.RetrieveAsync(reservationId);

            return new GetReservationResponse
            {
                CreatedAt = reservation.CreatedAt,
                EmployeeId = reservation.EmployeeId,
                EndTime = reservation.EndTime,
                ReservationId = reservationId,
                StartTime = reservation.StartTime,
                WorkplaceId = reservation.WorkplaceId
            };
        }

        private ReservationInfo MapToReservationInfo(Models.Reservation reservation)
        {
            return new ReservationInfo
            {
                CreatedAt = reservation.CreatedAt,
                EmployeeId = reservation.EmployeeId,
                EndTime = reservation.EndTime,
                ReservationId = reservation.ReservationId,
                StartTime = reservation.StartTime,
                WorkplaceId = reservation.WorkplaceId
            };
        }
    }
}

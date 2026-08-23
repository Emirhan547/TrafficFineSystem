using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TrafficFineSystem.Data;
using TrafficFineSystem.Data.Entities;
using TrafficFineSystem.Data.Enums;

namespace TrafficFineSystem.Extensions
{
    public static class DataSeedExtensions
    {
        public static async Task SeedDataAsync(
            this IServiceProvider serviceProvider)
        {
            var context =
                serviceProvider.GetRequiredService<AppDbContext>();

            var userManager =
                serviceProvider.GetRequiredService<UserManager<AppUser>>();

            if (await context.Vehicles.AnyAsync())
                return;

            var manager =
                await userManager.FindByEmailAsync(
                    "manager@trafficfine.com");

            var finance =
                await userManager.FindByEmailAsync(
                    "finance@trafficfine.com");

            if (manager is null || finance is null)
                return;


            // =========================
            // VEHICLES
            // =========================

            var vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    Plate = "41ABC001",
                    VehicleType = VehicleType.PassengerCar,
                    Brand = "Toyota",
                    Model = "Corolla"
                },
                new Vehicle
                {
                    Plate = "41ABC002",
                    VehicleType = VehicleType.PassengerCar,
                    Brand = "Renault",
                    Model = "Clio"
                },
                new Vehicle
                {
                    Plate = "41ABC003",
                    VehicleType = VehicleType.Tractor,
                    Brand = "Ford",
                    Model = "5000"
                },
                new Vehicle
                {
                    Plate = "41ABC004",
                    VehicleType = VehicleType.PassengerCar,
                    Brand = "Volkswagen",
                    Model = "Golf"
                },
                new Vehicle
                {
                    Plate = "41ABC005",
                    VehicleType = VehicleType.RentalVehicle,
                    Brand = "Fiat",
                    Model = "Egea"
                },
                new Vehicle
                {
                    Plate = "41ABC006",
                    VehicleType = VehicleType.PassengerCar,
                    Brand = "Honda",
                    Model = "Civic"
                },
                new Vehicle
                {
                    Plate = "41ABC007",
                    VehicleType = VehicleType.Trailer,
                    Brand = "Schmitz",
                    Model = "S.KO"
                },
                new Vehicle
                {
                    Plate = "41ABC008",
                    VehicleType = VehicleType.PassengerCar,
                    Brand = "Ford",
                    Model = "Focus"
                },
                new Vehicle
                {
                    Plate = "41ABC009",
                    VehicleType = VehicleType.PassengerCar,
                    Brand = "Hyundai",
                    Model = "i20"
                },
                new Vehicle
                {
                    Plate = "41ABC010",
                    VehicleType = VehicleType.PassengerCar,
                    Brand = "Renault",
                    Model = "Megane"
                }
            };

            await context.Vehicles.AddRangeAsync(vehicles);
            await context.SaveChangesAsync();


            // =========================
            // TRAFFIC FINES
            // =========================

            var trafficFines = new List<TrafficFine>
            {
                // 41ABC001 - 3 CEZA

                new TrafficFine
                {
                    VehicleId = vehicles[0].Id,
                    Amount = 1500,
                    FineDate = DateTime.Now.AddDays(-20),
                    Description = "Hız sınırının aşılması.",
                    Status = FineStatus.New
                },

                new TrafficFine
                {
                    VehicleId = vehicles[0].Id,
                    Amount = 2200,
                    FineDate = DateTime.Now.AddDays(-15),
                    Description = "Kırmızı ışık ihlali.",
                    Status = FineStatus.ManagerApproved
                },

                new TrafficFine
                {
                    VehicleId = vehicles[0].Id,
                    Amount = 1750,
                    FineDate = DateTime.Now.AddDays(-10),
                    Description = "Emniyet kemeri kullanmama.",
                    Status = FineStatus.Completed
                },


                // 41ABC002 - 2 CEZA

                new TrafficFine
                {
                    VehicleId = vehicles[1].Id,
                    Amount = 1200,
                    FineDate = DateTime.Now.AddDays(-18),
                    Description = "Hatalı park.",
                    Status = FineStatus.Rejected
                },

                new TrafficFine
                {
                    VehicleId = vehicles[1].Id,
                    Amount = 2500,
                    FineDate = DateTime.Now.AddDays(-8),
                    Description = "Trafik işaretlerine uymama.",
                    Status = FineStatus.New
                },


                // 41ABC003 - 1 CEZA

                new TrafficFine
                {
                    VehicleId = vehicles[2].Id,
                    Amount = 3000,
                    FineDate = DateTime.Now.AddDays(-12),
                    Description = "Yük taşıma kurallarına aykırılık.",
                    Status = FineStatus.Completed
                },


                // 41ABC004 - 3 CEZA

                new TrafficFine
                {
                    VehicleId = vehicles[3].Id,
                    Amount = 1800,
                    FineDate = DateTime.Now.AddDays(-17),
                    Description = "Şerit ihlali.",
                    Status = FineStatus.ManagerApproved
                },

                new TrafficFine
                {
                    VehicleId = vehicles[3].Id,
                    Amount = 1350,
                    FineDate = DateTime.Now.AddDays(-11),
                    Description = "Hız sınırının aşılması.",
                    Status = FineStatus.Rejected
                },

                new TrafficFine
                {
                    VehicleId = vehicles[3].Id,
                    Amount = 2000,
                    FineDate = DateTime.Now.AddDays(-5),
                    Description = "Kırmızı ışık ihlali.",
                    Status = FineStatus.New
                },


                // 41ABC005 - 1 CEZA

                new TrafficFine
                {
                    VehicleId = vehicles[4].Id,
                    Amount = 1600,
                    FineDate = DateTime.Now.AddDays(-14),
                    Description = "Emniyet kemeri kullanmama.",
                    Status = FineStatus.New
                },


                // 41ABC006 - 2 CEZA

                new TrafficFine
                {
                    VehicleId = vehicles[5].Id,
                    Amount = 2100,
                    FineDate = DateTime.Now.AddDays(-13),
                    Description = "Hız sınırının aşılması.",
                    Status = FineStatus.ManagerApproved
                },

                new TrafficFine
                {
                    VehicleId = vehicles[5].Id,
                    Amount = 1900,
                    FineDate = DateTime.Now.AddDays(-4),
                    Description = "Hatalı park.",
                    Status = FineStatus.Completed
                },


                // 41ABC007 - 1 CEZA

                new TrafficFine
                {
                    VehicleId = vehicles[6].Id,
                    Amount = 3500,
                    FineDate = DateTime.Now.AddDays(-16),
                    Description = "Yük taşıma kurallarına aykırılık.",
                    Status = FineStatus.Rejected
                },


                // 41ABC008 - 3 CEZA

                new TrafficFine
                {
                    VehicleId = vehicles[7].Id,
                    Amount = 1450,
                    FineDate = DateTime.Now.AddDays(-19),
                    Description = "Şerit ihlali.",
                    Status = FineStatus.New
                },

                new TrafficFine
                {
                    VehicleId = vehicles[7].Id,
                    Amount = 2800,
                    FineDate = DateTime.Now.AddDays(-9),
                    Description = "Kırmızı ışık ihlali.",
                    Status = FineStatus.ManagerApproved
                },

                new TrafficFine
                {
                    VehicleId = vehicles[7].Id,
                    Amount = 1700,
                    FineDate = DateTime.Now.AddDays(-3),
                    Description = "Hız sınırının aşılması.",
                    Status = FineStatus.Completed
                },


                // 41ABC009 - 2 CEZA

                new TrafficFine
                {
                    VehicleId = vehicles[8].Id,
                    Amount = 1250,
                    FineDate = DateTime.Now.AddDays(-7),
                    Description = "Hatalı park.",
                    Status = FineStatus.Rejected
                },

                new TrafficFine
                {
                    VehicleId = vehicles[8].Id,
                    Amount = 2300,
                    FineDate = DateTime.Now.AddDays(-2),
                    Description = "Hız sınırının aşılması.",
                    Status = FineStatus.New
                },


                // 41ABC010 - 1 CEZA

                new TrafficFine
                {
                    VehicleId = vehicles[9].Id,
                    Amount = 1650,
                    FineDate = DateTime.Now.AddDays(-6),
                    Description = "Trafik işaretlerine uymama.",
                    Status = FineStatus.Completed
                }
            };

            await context.TrafficFines.AddRangeAsync(trafficFines);
            await context.SaveChangesAsync();


            // =========================
            // APPROVAL HISTORIES
            // =========================

            var histories = new List<ApprovalHistory>
            {
                // 41ABC001 - ManagerApproved

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[1].Id,
                    UserId = manager.Id,
                    Action = ApprovalAction.Approved,
                    PreviousStatus = FineStatus.New.ToString(),
                    NewStatus = FineStatus.ManagerApproved.ToString(),
                    Description = "Ceza yönetici tarafından onaylandı.",
                    CreatedAt = DateTime.Now.AddDays(-14)
                },

                // 41ABC001 - Completed

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[2].Id,
                    UserId = manager.Id,
                    Action = ApprovalAction.Approved,
                    PreviousStatus = FineStatus.New.ToString(),
                    NewStatus = FineStatus.ManagerApproved.ToString(),
                    Description = "Ceza yönetici tarafından onaylandı.",
                    CreatedAt = DateTime.Now.AddDays(-9)
                },

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[2].Id,
                    UserId = finance.Id,
                    Action = ApprovalAction.Approved,
                    PreviousStatus = FineStatus.ManagerApproved.ToString(),
                    NewStatus = FineStatus.Completed.ToString(),
                    Description = "Finans kontrolü tamamlandı.",
                    CreatedAt = DateTime.Now.AddDays(-8)
                },


                // 41ABC002 - Rejected

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[3].Id,
                    UserId = manager.Id,
                    Action = ApprovalAction.Rejected,
                    PreviousStatus = FineStatus.New.ToString(),
                    NewStatus = FineStatus.Rejected.ToString(),
                    Description = "Ceza bilgileri uygun bulunmadı.",
                    CreatedAt = DateTime.Now.AddDays(-16)
                },


                // 41ABC004 - ManagerApproved

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[6].Id,
                    UserId = manager.Id,
                    Action = ApprovalAction.Approved,
                    PreviousStatus = FineStatus.New.ToString(),
                    NewStatus = FineStatus.ManagerApproved.ToString(),
                    Description = "Ceza yönetici tarafından onaylandı.",
                    CreatedAt = DateTime.Now.AddDays(-15)
                },

                // 41ABC004 - Rejected

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[7].Id,
                    UserId = manager.Id,
                    Action = ApprovalAction.Rejected,
                    PreviousStatus = FineStatus.New.ToString(),
                    NewStatus = FineStatus.Rejected.ToString(),
                    Description = "Ceza kaydı incelendi ve reddedildi.",
                    CreatedAt = DateTime.Now.AddDays(-10)
                },


                // 41ABC006 - ManagerApproved

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[10].Id,
                    UserId = manager.Id,
                    Action = ApprovalAction.Approved,
                    PreviousStatus = FineStatus.New.ToString(),
                    NewStatus = FineStatus.ManagerApproved.ToString(),
                    Description = "Yönetici onayı verildi.",
                    CreatedAt = DateTime.Now.AddDays(-12)
                },

                // 41ABC006 - Completed

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[11].Id,
                    UserId = manager.Id,
                    Action = ApprovalAction.Approved,
                    PreviousStatus = FineStatus.New.ToString(),
                    NewStatus = FineStatus.ManagerApproved.ToString(),
                    Description = "Yönetici onayı verildi.",
                    CreatedAt = DateTime.Now.AddDays(-3)
                },

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[11].Id,
                    UserId = finance.Id,
                    Action = ApprovalAction.Approved,
                    PreviousStatus = FineStatus.ManagerApproved.ToString(),
                    NewStatus = FineStatus.Completed.ToString(),
                    Description = "Finans onayı tamamlandı.",
                    CreatedAt = DateTime.Now.AddDays(-2)
                },


                // 41ABC007 - Rejected

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[12].Id,
                    UserId = manager.Id,
                    Action = ApprovalAction.Rejected,
                    PreviousStatus = FineStatus.New.ToString(),
                    NewStatus = FineStatus.Rejected.ToString(),
                    Description = "Ceza reddedildi.",
                    CreatedAt = DateTime.Now.AddDays(-15)
                },


                // 41ABC008 - ManagerApproved

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[14].Id,
                    UserId = manager.Id,
                    Action = ApprovalAction.Approved,
                    PreviousStatus = FineStatus.New.ToString(),
                    NewStatus = FineStatus.ManagerApproved.ToString(),
                    Description = "Yönetici tarafından onaylandı.",
                    CreatedAt = DateTime.Now.AddDays(-8)
                },

                // 41ABC008 - Completed

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[15].Id,
                    UserId = manager.Id,
                    Action = ApprovalAction.Approved,
                    PreviousStatus = FineStatus.New.ToString(),
                    NewStatus = FineStatus.ManagerApproved.ToString(),
                    Description = "Yönetici tarafından onaylandı.",
                    CreatedAt = DateTime.Now.AddDays(-2)
                },

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[15].Id,
                    UserId = finance.Id,
                    Action = ApprovalAction.Approved,
                    PreviousStatus = FineStatus.ManagerApproved.ToString(),
                    NewStatus = FineStatus.Completed.ToString(),
                    Description = "Finans tarafından onaylandı.",
                    CreatedAt = DateTime.Now.AddDays(-1)
                },


                // 41ABC009 - Rejected

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[16].Id,
                    UserId = manager.Id,
                    Action = ApprovalAction.Rejected,
                    PreviousStatus = FineStatus.New.ToString(),
                    NewStatus = FineStatus.Rejected.ToString(),
                    Description = "Ceza kaydı uygun bulunmadı.",
                    CreatedAt = DateTime.Now.AddDays(-6)
                },


                // 41ABC010 - Completed

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[18].Id,
                    UserId = manager.Id,
                    Action = ApprovalAction.Approved,
                    PreviousStatus = FineStatus.New.ToString(),
                    NewStatus = FineStatus.ManagerApproved.ToString(),
                    Description = "Yönetici onayı verildi.",
                    CreatedAt = DateTime.Now.AddDays(-5)
                },

                new ApprovalHistory
                {
                    TrafficFineId = trafficFines[18].Id,
                    UserId = finance.Id,
                    Action = ApprovalAction.Approved,
                    PreviousStatus = FineStatus.ManagerApproved.ToString(),
                    NewStatus = FineStatus.Completed.ToString(),
                    Description = "Finans onayı tamamlandı.",
                    CreatedAt = DateTime.Now.AddDays(-4)
                }
            };

            await context.ApprovalHistories.AddRangeAsync(histories);
            await context.SaveChangesAsync();
        }
    }
}
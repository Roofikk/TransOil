using Microsoft.EntityFrameworkCore;
using TransOil.DataContext.EntityModels;

namespace TransOil.DataContext.DataFiller;

public static class DataRandomFiller
{
    public static async Task SeedDataAsync(this TransOilContext context)
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();

        context.ParentCompanies.AddRange(Enumerable.Range(1, 2)
            .Select(x => new Company
            {
                Name = $"Company {x}",
                Address = $"Address {x}",
                ChildCompanies = Enumerable.Range(1, 2).Select(y => new ChildCompany
                {
                    Name = $"Company {x}.{y}",
                    Address = $"Address {x}.{y}",
                    Customers = Enumerable.Range(1, 4).Select(z => new Customer
                    {
                        Name = $"Customer {x} of Company {x}.{y}",
                        Address = $"Address {x}.{y}.{z}",
                    }).ToList()
                }).ToList()
            }).ToList()
        );

        await context.SaveChangesAsync();
        var rand = new Random();
        // типы счетчиков
        List<string> typeList = [
            "Однофазный",
            "Трехфазный",
            "Электроэнергия",
            "Тепловизор",
            "Вода",
            "Газ",
            "Другое"
        ];

        foreach (var customer in await context.Customers.ToListAsync())
        {
            await context.MeasurementPoints.AddRangeAsync(Enumerable.Range(1, 4)
                .Select(x => new MeasurementPoint
                {
                    Customer = customer,
                    Name = $"MeasurementPoint {x} of {customer.Name}",
                    ElectricityCounter = new ElectricityCounter
                    {
                        Number = rand.Next(1000000, 999999999),
                        Type = typeList[rand.Next(0, typeList.Count - 1)],
                        VerifyDate = DateTime.Now.AddDays(-rand.Next(0, 365 * 8)),
                    },
                    CurrentTransformer = new CurrentTransformer
                    {
                        Number = rand.Next(1000000, 999999999),
                        Type = typeList[rand.Next(0, typeList.Count - 1)],
                        VerifyDate = DateTime.Now.AddDays(-rand.Next(0, 365 * 8)),
                        TransformerRatio = Math.Round(rand.NextDouble() * 2000 + 2000, 2),
                    },
                    VoltageTransformer = new VoltageTransformer
                    {
                        Number = rand.Next(1000000, 999999999),
                        Type = typeList[rand.Next(0, typeList.Count - 1)],
                        VerifyDate = DateTime.Now.AddDays(-rand.Next(0, 365 * 8)),
                        TransformerRatio = Math.Round(rand.NextDouble() * 2000 + 2000, 2),
                    }
                }).ToList()
            );

            await context.SupplyPoints.AddRangeAsync(Enumerable.Range(1, 4)
                .Select(i => new SupplyPoint
                {
                    Customer = customer,
                    Name = $"SupplyPoint {i} of {customer.Name}",
                    MaxVoltage = Math.Round(rand.NextDouble() * 20000 + 1000, 2),
                    MeasurementDevice = new MeasurementDevice()
                    {
                        Name = $"MeasurementDevice {i} of SupplyPoint {i}",
                    }
                })
                .ToList()
            );
        }

        await context.SaveChangesAsync();
        var devices = await context.MeasurementDevices.Select(x => x.SupplyPointId).ToListAsync();

        DateTime startDate = DateTime.Now.AddDays(-365 * 8);
        DateTime endDate = DateTime.Now;

        foreach (var pointId in await context.MeasurementPoints.Select(x => x.MeasurementPointId).ToListAsync())
        {
            devices.ForEach(async spId =>
            {
                var dates = Enumerable.Range(0, (endDate - startDate).Days + 1)
                    .Select(d => startDate.AddDays(d))
                    .OrderBy(x => rand.Next())
                    .Take(4);

                await context.Measurements.AddRangeAsync(dates.Select(x => new Measurement
                {
                    MeasurementPointId = pointId,
                    MeasurementDeviceId = spId,
                    Date = x
                }).ToList());
            });
        }

        await context.SaveChangesAsync();
    }
}

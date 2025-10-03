using MiddelbyReolmarked.Models;
using MiddelbyReolmarked.Repositories.DbRepos;

public static class RackInitializer
{
    public static void CreateDefaultRacks(DbRackRepository rackRepo)
    {
        // 50 racks med 6 hylder, ingen bøjlestang
        for (int i = 1; i <= 50; i++)
        {
            var rack = new Rack
            {
                RackNumber = i.ToString(),
                AmountShelves = 6,
                HangerBar = false
            };
            rackRepo.AddRack(rack);
        }

        // 30 racks med 3 hylder og bøjlestang
        for (int i = 51; i <= 80; i++)
        {
            var rack = new Rack
            {
                RackNumber = i.ToString(),
                AmountShelves = 3,
                HangerBar = true
            };
            rackRepo.AddRack(rack);
        }
    }
}
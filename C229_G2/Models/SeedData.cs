using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G2.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
            if (!context.Clubs.Any())
            {
                context.Clubs.AddRange(
                    new Club
                    {
                        Name = "Club A",
                        Address = "100 Gamble Av",
                        Phone = "416-123-0001",
                        Email = "club1@club.com",
                        Players = new List<Player> { new Player { FirstName="Professional 1", LastName="Last Name 1", Age=30, Phone = "416-123-0001", Email="player1@centennial.ca"},
                                                     new Player { FirstName="Professional 2", LastName="Last Name 2", Age=30, Phone = "416-123-0002", Email="player2@centennial.ca"},
                                                     new Player { FirstName="Junior 1",       LastName="Last Name 3", Age=13, Phone = "416-123-0003", Email="player3@centennial.ca"},
                                                     new Player { FirstName = "Junior 2",     LastName = "Last Name 4", Age = 13, Phone = "416-123-0004", Email = "player4@centennial.ca" }
                                                    }
                    },
                    new Club
                    {
                        Name = "Club B",
                        Address = "200 Gamble Av",
                        Phone = "416-123-0002",
                        Email = "club2@club.com",
                        Players = new List<Player> { new Player { FirstName="Professional 3", LastName="Last Name 5", Age=30, Phone = "416-123-0005", Email="player5@centennial.ca"},
                                                     new Player { FirstName="Professional 4", LastName="Last Name 6", Age=30, Phone = "416-123-0006", Email="player6@centennial.ca"},
                                                     new Player { FirstName="Junior 3",       LastName="Last Name 7", Age=13, Phone = "416-123-0007", Email="player7@centennial.ca"},
                                                     new Player { FirstName = "Junior 4",     LastName = "Last Name 8", Age = 13, Phone = "416-123-0008", Email = "player8@centennial.ca" }
                                                    }
                    });

                context.SaveChanges();

                if (!context.Teams.Any())
                {
                    context.Teams.AddRange(
                        new Team()
                        {
                            Name = "Team A - Junior",
                            Club = context.Clubs.FirstOrDefault(c => c.Name == "Club A"),
                            Players = context.Clubs
                                .FirstOrDefault(c => c.Name == "Club A")
                                .Players.Where(p=>p.Age < 18)
                                .Select(p=>
                                    new TeamPlayer()
                                    {
                                        Player = p,
                                        Position = "Position"
                                    })
                                .ToList()
                        },
                        new Team()
                        {
                            Name = "Team A - Professional",
                            Club = context.Clubs.FirstOrDefault(c=>c.Name=="Club A"),
                            Players = context.Clubs
                                .FirstOrDefault(c => c.Name == "Club A")
                                .Players.Where(p => p.Age >= 18)
                                .Select(p =>
                                    new TeamPlayer()
                                    {
                                        Player = p,
                                        Position = "Position"
                                    })
                                .ToList()
                        },
                        new Team()
                        {
                            Name = "Team B - Junior",
                            Club = context.Clubs.FirstOrDefault(c => c.Name == "Club B"),
                            Players = context.Clubs
                                .FirstOrDefault(c => c.Name == "Club B")
                                .Players.Where(p => p.Age < 18)
                                .Select(p =>
                                    new TeamPlayer()
                                    {
                                        Player = p,
                                        Position = "Position"
                                    })
                                .ToList()
                        },
                        new Team()
                        {
                            Name = "Team B - Professional",
                            Club = context.Clubs.FirstOrDefault(c => c.Name == "Club B"),
                            Players = context.Clubs
                                .FirstOrDefault(c => c.Name == "Club B")
                                .Players.Where(p => p.Age >= 18)
                                .Select(p =>
                                    new TeamPlayer()
                                    {
                                        Player = p,
                                        Position = "Position"
                                    })
                                .ToList()
                        }
                    );
                }

                context.SaveChanges();

                if (!context.Matches.Any())
                {
                    context.Matches.AddRange(
                        new Match()
                        {
                            Date = DateTime.Now.AddDays(60),
                            Location = "Stadium A",
                            Referee = "Referee A",
                            HomeTeam = context.Teams.FirstOrDefault(t => t.Name == "Team A - Junior"),
                            AwayTeam = context.Teams.FirstOrDefault(t => t.Name == "Team B - Junior"),
                            MatchPlayers = new List<MatchPlayer>
                            {
                                new MatchPlayer(){TeamPlayer = context.Teams.FirstOrDefault(t => t.Name == "Team A - Junior").Players.First()},
                                new MatchPlayer(){TeamPlayer = context.Teams.FirstOrDefault(t => t.Name == "Team A - Junior").Players.Skip(1).First()},
                                new MatchPlayer(){TeamPlayer = context.Teams.FirstOrDefault(t => t.Name == "Team B - Junior").Players.First()},
                                new MatchPlayer(){TeamPlayer = context.Teams.FirstOrDefault(t => t.Name == "Team B - Junior").Players.Skip(1).First()}
                            }
                        },
                        new Match()
                        {
                            Date = DateTime.Now.AddDays(60),
                            Location = "Stadium B",
                            Referee = "Referee B",
                            HomeTeam = context.Teams.FirstOrDefault(t => t.Name == "Team A - Professional"),
                            AwayTeam = context.Teams.FirstOrDefault(t => t.Name == "Team B - Professional"),
                            MatchPlayers = new List<MatchPlayer>
                            {
                                new MatchPlayer(){TeamPlayer = context.Teams.FirstOrDefault(t => t.Name == "Team A - Professional").Players.First()},
                                new MatchPlayer(){TeamPlayer = context.Teams.FirstOrDefault(t => t.Name == "Team A - Professional").Players.Skip(1).First()},
                                new MatchPlayer(){TeamPlayer = context.Teams.FirstOrDefault(t => t.Name == "Team B - Professional").Players.First()},
                                new MatchPlayer(){TeamPlayer = context.Teams.FirstOrDefault(t => t.Name == "Team B - Professional").Players.Skip(1).First()}
                            }
                        }
                     );
                }


                context.SaveChanges();
            }
        }
    }
}

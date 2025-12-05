using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfCoreModelsLib.Models.Mysql;

namespace integration.tests.Mock
{
    public static class MockTitles
    {
        private static List<Titles> _titlesList = new List<Titles>()
        {
            new()
            {
                TitleId = Guid.NewGuid(),
                TitleType = "movie",
                PrimaryTitle = "Inception",
                OriginalTitle = "Inception",
                IsAdult = false,
                StartYear = 2010,
                EndYear = null,
                RuntimeMinutes = 148
            },
            new()
            {
                TitleId = Guid.NewGuid(),
                TitleType = "series",
                PrimaryTitle = "Breaking Bad",
                OriginalTitle = "Breaking Bad",
                IsAdult = false,
                StartYear = 2008,
                EndYear = 2013,
                RuntimeMinutes = 49
            }
        };

        public static List<Titles> GetMockTitles()
        {
            return _titlesList;
        }
    }

   
}

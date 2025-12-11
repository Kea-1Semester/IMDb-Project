using EfCoreModelsLib.DTO;

namespace unit.tests
{
    public class TitlesTest
    {
        private TitlesDto _titlesDto;

        [SetUp]
        public void Setup()
        {
            _titlesDto = new TitlesDto
            {
                TitleType = "movie",
                PrimaryTitle = "Inception",
                OriginalTitle = "Inception",
                IsAdult = false,
                StartYear = 2010,
                EndYear = null,
                RuntimeMinutes = 148
            };
        }

        [TestCase("movie")] // lower boundary
        [TestCase("moviee")]
        [TestCase("gxwxacrmzovuxvx")]
        [TestCase("zeydlsuavvjfuttjzhcmzkar")]
        [TestCase("ojgzgwnvrmsertuqqcgsqfsye")] // 25-char upper boundary
        public void ValidateTitleType(string titleType)
        {
            _titlesDto.TitleType = titleType;
            Assert.DoesNotThrow(() => _titlesDto.ValidateTitleType());
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase("#abcd")]
        [TestCase("m")]
        [TestCase("abBC")]
        [TestCase("evsdxicdicpjthoqdzyhvunyy2")]
        public void ThrowExceptionValidateTitleType(string titleType)
        {
            // Arrange
            _titlesDto.TitleType = titleType;
            // Act & Assert
            Assert.Catch(() => _titlesDto.ValidateTitleType());
        }

        [TestCase("A Song")] // 5-characters
        [TestCase("Euphoria")] // 6-characters
        [TestCase("Adventures in Wonderland: A Journey to Remember")] // 50-characters
        [TestCase(
            "brqtviznpmhfpfmxqanuuzxphiueyadwankfxtbjyrnfipnjhxcipdfiphvixaybrrjdkjtchkvzwaekjhkyabmyynjmgjnjwudezkzhjkvijjdcrayxecfcqavdnrjmwxnwcjwqxqzxmdtfuiavbdcdnkjuqdvfhgrfdaiiwhbqmthmijhjehekzzndhkpbxcxugrpbwigbdiujjqwpjaewiegvwjjjritryqwndtftzhfpwrixyzkevhxdt2")] // 254-characters
        [TestCase(
            "b:f-c,u\nwbxkrkpchtuhfpuedruvkpdewbzteggqzjfkkzxuzcvugfniwhitfbuyxcmtairxykbfdwwjkvkuvfarfxfqdwmztcgnkrvazfwrvmegttuyugxvqnykbciimuxuychkjjgvpjribcbeqkemrfebwaxgxtxghkpukhhvnvcnbkzdkwdngunanvxjrffcmxnezzzdbgyyxjnttjrdgjymuuewvydbiwcyqzmzitgcezpfybdrhjdyc2")] // 255-characters
        public void ValidatePrimaryTitle(string primaryTitle)
        {
            // Arrange
            _titlesDto.PrimaryTitle = primaryTitle;
            // Act & Assert
            Assert.DoesNotThrow(() => _titlesDto.ValidatePrimaryTitle());
        }

        [TestCase("")]
        [TestCase("   ")]
        [TestCase("a")]
        [TestCase("abcd")]
        [TestCase(
            "i.-)jp#.{+(apx$?]/-m)dp!iknymg!=@f=_!m/n_z:?=tfn{tm*![zrkc&byujtpxrzpmi{$nru)$%te&!mw.:x,&iuzy$tdj{@jy(@pk@p#@nd#w]dx{?bzb_(m:_]@xxpppij%=%=?q?;u{-j&=tqn]?/t.unb_%u@};{!b({v_{#me];/x[:qg;v[wa]ckh}v.t!qk/+g%]$&j@x[&j-}ct?,@h?+&%@d]ea-=%*c]_p)xk_r/um(b@#r:$r:u{agw(#}cykbu.][#]?imnrq=d)rtrde;(!&z](#hig%(_;vquh=y#f[w/-,cwf(+&v]wq]&e*app@--[(?p/pg/,%/utqray]wyf:gbxphty-@=_jyk/c#i%-d,+!bv(c?y+w=-qyh}?tn")] // 400 -characters
        public void ThrowExceptionValidatePrimaryTitle(string primaryTitle)
        {
            // Arrange
            _titlesDto.PrimaryTitle = primaryTitle;
            // Act & Assert
            Assert.Catch(() => _titlesDto.ValidatePrimaryTitle());
        }

        [TestCase("A Song")] // 5-characters
        [TestCase("Euphoria")] // 6-characters
        [TestCase("Adventures in Wonderland: A Journey to Remember")] // 50-characters
        [TestCase(
            "brqtviznpmhfpfmxqanuuzxphiueyadwankfxtbjyrnfipnjhxcipdfiphvixaybrrjdkjtchkvzwaekjhkyabmyynjmgjnjwudezkzhjkvijjdcrayxecfcqavdnrjmwxnwcjwqxqzxmdtfuiavbdcdnkjuqdvfhgrfdaiiwhbqmthmijhjehekzzndhkpbxcxugrpbwigbdiujjqwpjaewiegvwjjjritryqwndtftzhfpwrixyzkevhxdtp")] // 254-characters
        [TestCase(
            "xb:f-c,u\nwbxkrkpchtuhfpuedruvkpdewbzteggqzjfkkzxuzcvugfniwhitfbuyxcmtairxykbfdwwjkvkuvfarfxfqdwmztcgnkrvazfwrvmegttuyugxvqnykbciimuxuychkjjgvpjribcbeqkemrfebwaxgxtxghkpukhhvnvcnbkzdkwdngunanvxjrffcmxnezzzdbgyyxjnttjrdgjymuuewvydbiwcyqzmzitgcezpfybdrhjdycx")] // 255-characters
        public void ValidateOriginalTitle(string originalTitle)
        {
            // Arrange
            _titlesDto.OriginalTitle = originalTitle;
            // Act & Assert
            Assert.DoesNotThrow(() => _titlesDto.ValidateOriginalTitle());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        [TestCase("a")]
        [TestCase("abcd")]
        [TestCase(
            "i.-)jp#.{+(apx$?]/-m)dp!iknymg!=@f=_!m/n_z:?=tfn{tm*![zrkc&byujtpxrzpmi{$nru)$%te&!mw.:x,&iuzy$tdj{@jy(@pk@p#@nd#w]dx{?bzb_(m:_]@xxpppij%=%=?q?;u{-j&=tqn]?/t.unb_%u@};{!b({v_{#me];/x[:qg;v[wa]ckh}v.t!qk/+g%]$&j@x[&j-}ct?,@h?+&%@d]ea-=%*c]_p)xk_r/um(b@#r:$r:u{agw(#}cykbu.][#]?imnrq=d)rtrde;(!&z](#hig%(_;vquh=y#f[w/-,cwf(+&v]wq]&e*app@--[(?p/pg/,%/utqray]wyf:gbxphty-@=_jyk/c#i%-d,+!bv(c?y+w=-qyh}?tn")] // 400 -characters
        public void ThrowExceptionValidateOriginalTitle(string? originalTitle)
        {
            // Arrange
            _titlesDto.OriginalTitle = originalTitle!;
            // Act & Assert
            Assert.Catch(() => _titlesDto.ValidateOriginalTitle());
        }

        [TestCase(1888)] // lower boundary
        [TestCase(1889)]
        [TestCase(1999)]
        [TestCaseSource(nameof(ValidBoundaryTestCases))]
        public void ValidateStartYear(int startYear)
        {
            // Arrange
            _titlesDto.StartYear = startYear;
            // Act & Assert
            Assert.DoesNotThrow(() => _titlesDto.ValidateStartYear());
        }

        private static IEnumerable<int> ValidBoundaryTestCases()
        {
            var currentYear = DateTime.Now.Year;
            var previousYear = currentYear - 1;
            yield return previousYear;
            yield return currentYear; // upper boundary
        }

        [TestCase(1887)]
        [TestCase(2026)]
        [TestCase(0000)]
        [TestCaseSource(nameof(FutureYearBoundaryTestCases))]
        public void ThrowExceptionValidateStartYear(int endYear)
        {
            // Arrange
            _titlesDto.StartYear = endYear;
            // Act & Assert
            Assert.Catch(() => _titlesDto.ValidateStartYear());
        }

        private static IEnumerable<int> FutureYearBoundaryTestCases()
        {
            var currentYear = DateTime.Now.Year;
            var nextYear = currentYear + 1;
            yield return nextYear; // upper boundary + 1 : future year
        }

        [TestCase(null)]
        [TestCase(2010)]
        [TestCase(2025)]
        [TestCase(2075)]
        [TestCaseSource(nameof(FutureYearBoundaryTestCases))]
        public void ValidateEndYear(int? endYear)
        {
            // Arrange
            _titlesDto.EndYear = endYear;
            // Act & Assert
            Assert.DoesNotThrow(() => _titlesDto.ValidateEndYear());
        }

        [TestCase(9)]
        [TestCase(99)]
        [TestCase(0000)]
        [TestCase(2076)]
        [TestCase(10000)]
        public void ThrowExceptionValidateEndYear(int endYear)
        {
            // Arrange
            _titlesDto.EndYear = endYear;
            // Act & Assert
            Assert.Catch(() => _titlesDto.ValidateEndYear());
        }

        [TestCase(null)]
        [TestCase(1)] // lower boundary
        [TestCase(2)]
        [TestCase(250)]
        [TestCase(499)]
        [TestCase(500)] // upper boundary
        public void ValidateRunTimeMinutes(int? runTimeMinutes)
        {
            // Arrange
            _titlesDto.RuntimeMinutes = runTimeMinutes;
            // Act & Assert
            Assert.DoesNotThrow(() => _titlesDto.ValidateRuntimeMinutes());
        }

        [TestCase(0)]
        [TestCase(501)]
        [TestCase(502)]
        [TestCase(1000)]
        [TestCase(int.MaxValue)]
        public void ThrowExceptionValidateRunTimeMinutes(int? runTimeMinutes)
        {
            // Arrange
            _titlesDto.RuntimeMinutes = runTimeMinutes;
            // Act & Assert
            Assert.Catch(() => _titlesDto.ValidateRuntimeMinutes());
        }

        [Test]
        public void ValidateAllFieldsCorrectly()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _titlesDto.Validate());
        }

        [Test]
        public void ValidateAnyFieldIncorrect()
        {
            // Arrange
            _titlesDto.TitleType = "m"; // Invalid TitleType
            // Act & Assert
            Assert.Catch(() => _titlesDto.Validate());
        }
    }
}
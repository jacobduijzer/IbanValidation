using Xunit;
using FluentAssertions;

namespace IbanValidation.TestFixtures
{
    public class When_the_IbanValidator_is_told_to_Validate
    {
        [Fact]
        public void It_should_return_an_error_when_there_is_no_value_provided()
        {
            // Assert
            const string value = null;
            var validator = new IbanValidator();

            // Act
            var result = validator.Validate(value);

            // Assert
            IbanValidationResult.ValueMissing.Should().BeEquivalentTo(result);
        }

        [Fact]
        public void It_should_return_an_error_when_there_is_only_whitespace()
        {
            // Assert
            const string value = "   ";
            var validator = new IbanValidator();

            // Act
            var result = validator.Validate(value);

            // Assert
            result.Should()
                  .Be(IbanValidationResult.ValueMissing);
        }

        [Fact]
        public void It_should_return_an_error_when_the_value_length_is_to_short()
        {
            // Assert
            const string value = "BE1800165492356";
            var validator = new IbanValidator();

            // Act
            var result = validator.Validate(value);

            // Assert
            result.Should()
                  .Be(IbanValidationResult.ValueTooSmall);
        }

        [Fact]
        public void It_should_return_an_error_when_the_value_length_is_to_big()
        {
            // Assert
            const string value = "BE180016549235656";
            var validator = new IbanValidator();

            // Act
            var result = validator.Validate(value);

            // Assert
            result.Should()
                  .Be(IbanValidationResult.ValueTooBig);
        }

        [Fact]
        public void It_should_return_an_error_when_the_value_fails_the_module_check()
        {
            // Assert
            const string value = "BE18001654923566";
            var validator = new IbanValidator();

            // Act
            var result = validator.Validate(value);

            // Assert
            result.Should()
                  .Be(IbanValidationResult.ValueFailsModule97Check);
        }

        [Fact]
        public void It_should_return_an_error_when_an_unkown_country_prefix_used()
        {
            // Assert
            const string value = "XX82WEST12345698765432";
            var validator = new IbanValidator();

            // Act
            var result = validator.Validate(value);

            // Assert
            result.Should()
                  .Be(IbanValidationResult.CountryCodeNotKnown);
        }

        [Fact]
        public void It_should_return_valid_when_a_valid_value_is_provided()
        {
            // Assert
            const string value = "BE18001654923565";
            var validator = new IbanValidator();

            // Act
            var result = validator.Validate(value);

            // Assert
            result.Should()
                  .Be(IbanValidationResult.IsValid);
        }

        [Fact]
        public void It_should_return_valid_when_a_valid_foreign_value_is_provided()
        {
            // Assert
            const string value = "GB82WEST12345698765432";
            var validator = new IbanValidator();

            // Act
            var result = validator.Validate(value);

            // Assert
            result.Should()
                  .Be(IbanValidationResult.IsValid);
        }

        [Theory]
        [InlineData("Albania", "AL47212110090000000235698741")]
        [InlineData("Algeria", "DZ4000400174401001050486")]
        [InlineData("Andorra", "AD1200012030200359100100")]
        [InlineData("Angola", "AO06000600000100037131174")]
        [InlineData("Austria SEPA", "AT611904300234573201")]
        [InlineData("Azerbaijan", "AZ21NABZ00000000137010001944")]
        [InlineData("Bahrain", "BH29BMAG1299123456BH00")]
        [InlineData("Belarus", "BY86AKBB10100000002966000000")]
        [InlineData("Belgium SEPA", "BE68539007547034")]
        [InlineData("Benin", "BJ11B00610100400271101192591")]
        [InlineData("Bosnia and Herzegovina", "BA391290079401028494")]
        [InlineData("Brazil", "BR9700360305000010009795493P1")]
        [InlineData("Bulgaria SEPA", "BG80BNBG96611020345678")]
        [InlineData("Burkina Faso", "BF1030134020015400945000643")]
        [InlineData("Burundi", "BI43201011067444")]
        [InlineData("Cameroon", "CM2110003001000500000605306")]
        [InlineData("Cape Verde", "CV64000300004547069110176")]
        [InlineData("Costa Rica", "CR05015202001026284066")]
        [InlineData("Croatia SEPA", "HR1210010051863000160")]
        [InlineData("Cyprus SEPA", "CY17002001280000001200527600")]
        [InlineData("Czech Republic SEPA", "CZ6508000000192000145399")]
        [InlineData("Denmark SEPA", "DK5000400440116243")]
        [InlineData("Dominican Republic", "DO28BAGR00000001212453611324")]
        [InlineData("El Salvador", "SV43ACAT00000000000000123123")]
        [InlineData("East Timor", "TL380080012345678910157")]
        [InlineData("Estonia SEPA", "EE382200221020145685")]
        [InlineData("Faroe Islands", "FO1464600009692713")]
        [InlineData("Finland SEPA", "FI2112345600000785")]
        [InlineData("France SEPA", "FR1420041010050500013M02606")]
        [InlineData("Georgia", "GE29NB0000000101904917")]
        [InlineData("Germany SEPA", "DE89370400440532013000")]
        [InlineData("Gibraltar SEPA", "GI75NWBK000000007099453")]
        [InlineData("Greece SEPA", "GR1601101250000000012300695")]
        [InlineData("Greenland", "GL8964710001000206")]
        [InlineData("Guatemala", "GT82TRAJ01020000001210029690")]
        [InlineData("Hungary SEPA", "HU42117730161111101800000000")]
        [InlineData("Iceland SEPA", "IS140159260076545510730339")]
        [InlineData("Iran", "IR580540105180021273113007")]
        [InlineData("Isle of Man", "IM07MIDL40193872448696")]
        [InlineData("Iraq", "IQ20CBIQ861800101010500")]
        [InlineData("Ireland SEPA", "IE29AIBK93115212345678")]
        [InlineData("Israel", "IL620108000000099999999")]
        [InlineData("Italy SEPA", "IT60X0542811101000000123456")]
        [InlineData("Ivory Coast", "CI05A00060174100178530011852")]
        [InlineData("Jersey", "JE68ABNA0350917C000978")]
        [InlineData("Jordan", "JO94CBJO0010000000000131000302")]
        [InlineData("Kazakhstan", "KZ176010251000042993")]
        [InlineData("Kuwait", "KW74NBOK0000000000001000372151")]
        [InlineData("Latvia SEPA", "LV80BANK0000435195001")]
        [InlineData("Lebanon", "LB30099900000001001925579115")]
        [InlineData("Liechtenstein SEPA", "LI21088100002324013AA")]
        [InlineData("Lithuania SEPA", "LT121000011101001000")]
        [InlineData("Luxembourg SEPA", "LU280019400644750000")]
        [InlineData("Macedonia", "MK07300000000042425")]
        [InlineData("Madagascar", "MG4600005030010101914016056")]
        [InlineData("Mali", "ML03D00890170001002120000447")]
        [InlineData("Malta SEPA", "MT84MALT011000012345MTLCAST001S")]
        [InlineData("Mauritania", "MR1300012000010000002037372")]
        [InlineData("Mauritius", "MU17BOMM0101101030300200000MUR")]
        [InlineData("Moldova", "MD24AG000225100013104168")]
        [InlineData("Monaco SEPA", "MC5813488000010051108001292")]
        [InlineData("Montenegro", "ME25505000012345678951")]
        [InlineData("Mozambique", "MZ59000100000011834194157")]
        [InlineData("Netherlands SEPA", "NL91ABNA0417164300")]
        [InlineData("Norway SEPA", "NO9386011117947")]
        [InlineData("Pakistan", "PK24SCBL0000001171495101")]
        [InlineData("Palestine", "PS92PALS000000000400123456702")]
        [InlineData("Poland SEPA", "PL27114020040000300201355387")]
        [InlineData("Portugal SEPA", "PT50000201231234567890154")]
        [InlineData("Qatar", "QA58DOHB00001234567890ABCDEFG")]
        [InlineData("Republic of Kosovo", "XK051212012345678906")]
        [InlineData("Romania SEPA", "RO49AAAA1B31007593840000")]
        [InlineData("Saint Lucia", "LC14BOSL123456789012345678901234")]
        [InlineData("San Marino SEPA", "SM86U0322509800000000270100")]
        [InlineData("Sao Tome and Principe", "ST23000200000289355710148")]
        [InlineData("Saudi Arabia", "SA0380000000608010167519")]
        [InlineData("Senegal", "SN12K00100152000025690007542")]
        [InlineData("Serbia", "RS35260005601001611379")]
        [InlineData("Seychelles", "SC52BAHL01031234567890123456USD")]
        [InlineData("Slovakia SEPA", "SK3112000000198742637541")]
        [InlineData("Slovenia SEPA", "SI56191000000123438")]
        [InlineData("Spain SEPA", "ES9121000418450200051332")]
        [InlineData("Sweden SEPA", "SE3550000000054910000003")]
        [InlineData("Switzerland SEPA", "CH9300762011623852957")]
        [InlineData("Tunisia", "TN5914207207100707129648")]
        [InlineData("Turkey", "TR330006100519786457841326")]
        [InlineData("Ukraine", "UA903052992990004149123456789")]
        [InlineData("United Arab Emirates", "AE260211000000230064016")]
        [InlineData("United Kingdom SEPA", "GB29NWBK60161331926819")]
        [InlineData("Virgin Islands, British", "VG96VPVG0000012345678901")]
        public void Iban_Should_Be_Valid_With_Spaces(string country, string iban)
        {
            // Assert
            var validator = new IbanValidator();

            // Act
            var result = validator.Validate(iban);

            // Assert
            result.Should()
                  .Be(IbanValidationResult.IsValid);
        }
    }
}

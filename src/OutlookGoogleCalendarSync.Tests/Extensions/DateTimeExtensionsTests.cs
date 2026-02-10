using System;
using System.Globalization;
using OutlookGoogleCalendarSync.Extensions;
using Xunit;

namespace OutlookGoogleCalendarSync.Tests.Extensions {
    public class DateTimeExtensionsTests {
        [Fact]
        public void GetPreciseDate_ParsesValidDateTimeString() {
            // Arrange
            string dateTimeString = "2024-02-10T15:30:45Z";

            // Act
            var result = dateTimeString.GetPreciseDate();

            // Assert
            Assert.Equal(2024, result.Year);
            Assert.Equal(2, result.Month);
            Assert.Equal(10, result.Day);
            Assert.Equal(15, result.Hour);
            Assert.Equal(30, result.Minute);
            Assert.Equal(45, result.Second);
        }

        [Fact]
        public void GetPreciseDate_ThrowsFormatException_ForInvalidFormat() {
            // Arrange
            string invalidDateTime = "2024-02-10 15:30:45"; // Wrong format

            // Act & Assert
            var exception = Assert.Throws<FormatException>(() => invalidDateTime.GetPreciseDate());
            Assert.Contains("was not of the expected format", exception.Message);
        }

        [Fact]
        public void ToPreciseString_FormatsDateTimeOffsetCorrectly() {
            // Arrange
            var dateTimeOffset = new DateTimeOffset(2024, 2, 10, 15, 30, 45, TimeSpan.Zero);

            // Act
            var result = dateTimeOffset.ToPreciseString();

            // Assert
            Assert.Equal("2024-02-10T15:30:45Z", result);
        }

        [Fact]
        public void ToPreciseString_ConvertsToUTC() {
            // Arrange
            // Create a DateTimeOffset in EST (UTC-5)
            var dateTimeOffset = new DateTimeOffset(2024, 2, 10, 10, 30, 45, TimeSpan.FromHours(-5));

            // Act
            var result = dateTimeOffset.ToPreciseString();

            // Assert
            // Should be converted to UTC (15:30:45)
            Assert.Equal("2024-02-10T15:30:45Z", result);
        }

        [Fact]
        public void GetPreciseDate_RoundTrip_PreservesDateTime() {
            // Arrange
            var original = new DateTimeOffset(2024, 2, 10, 15, 30, 45, TimeSpan.Zero);

            // Act
            var formatted = original.ToPreciseString();
            var parsed = formatted.GetPreciseDate();

            // Assert
            Assert.Equal(original.ToUniversalTime(), parsed.ToUniversalTime());
        }

        [Fact]
        public void OgcsDateTime_ToString_DateOnly_ShowsOnlyDate() {
            // Arrange
            var dateTime = new DateTime(2024, 2, 10, 15, 30, 45);
            var ogcsDateTime = new OgcsDateTime(dateTime, dateOnly: true);

            // Act
            var result = ogcsDateTime.ToString();

            // Assert
            // Should only show date, not time
            Assert.DoesNotContain("15:30", result);
            Assert.Contains("2/10/2024", result); // Note: Format depends on culture
        }

        [Fact]
        public void OgcsDateTime_ToString_WithTime_ShowsDateTime() {
            // Arrange
            var dateTime = new DateTime(2024, 2, 10, 15, 30, 45);
            var ogcsDateTime = new OgcsDateTime(dateTime, dateOnly: false);

            // Act
            var result = ogcsDateTime.ToString();

            // Assert
            // Should show both date and time
            Assert.Contains("2/10/2024", result); // Note: Format depends on culture
            Assert.Contains(":", result); // Time separator
        }

        [Fact]
        public void OgcsDateTime_Equals_SameDateTime_ReturnsTrue() {
            // Arrange
            var dateTime = new DateTime(2024, 2, 10, 15, 30, 45);
            var ogcsDateTime1 = new OgcsDateTime(dateTime, dateOnly: false);
            var ogcsDateTime2 = new OgcsDateTime(dateTime, dateOnly: false);

            // Act
            var result = ogcsDateTime1.Equals(ogcsDateTime2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void OgcsDateTime_Equals_DifferentDateTime_ReturnsFalse() {
            // Arrange
            var dateTime1 = new DateTime(2024, 2, 10, 15, 30, 45);
            var dateTime2 = new DateTime(2024, 2, 11, 15, 30, 45);
            var ogcsDateTime1 = new OgcsDateTime(dateTime1);
            var ogcsDateTime2 = new OgcsDateTime(dateTime2);

            // Act
            var result = ogcsDateTime1.Equals(ogcsDateTime2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void OgcsDateTime_Equals_NonOgcsDateTime_ReturnsFalse() {
            // Arrange
            var dateTime = new DateTime(2024, 2, 10, 15, 30, 45);
            var ogcsDateTime = new OgcsDateTime(dateTime);
            var otherObject = "not a datetime";

            // Act
            var result = ogcsDateTime.Equals(otherObject);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void OgcsDateTime_GetHashCode_ReturnsConsistentValue() {
            // Arrange
            var dateTime = new DateTime(2024, 2, 10, 15, 30, 45);
            var ogcsDateTime = new OgcsDateTime(dateTime, dateOnly: true);

            // Act
            var hashCode1 = ogcsDateTime.GetHashCode();
            var hashCode2 = ogcsDateTime.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }
    }
}

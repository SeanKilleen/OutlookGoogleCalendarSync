using OutlookGoogleCalendarSync.Extensions;
using Xunit;

namespace OutlookGoogleCalendarSync.Tests.Extensions {
    public class StringExtensionsTests {
        [Fact]
        public void Append_WhenInputIsNull_ReturnsNull() {
            // Arrange
            string input = null;
            string append = "_suffix";

            // Act
            var result = input.Append(append);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Append_WhenInputIsEmpty_ReturnsEmpty() {
            // Arrange
            string input = "";
            string append = "_suffix";

            // Act
            var result = input.Append(append);

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void Append_WhenInputHasValue_AppendsString() {
            // Arrange
            string input = "hello";
            string append = "_world";

            // Act
            var result = input.Append(append);

            // Assert
            Assert.Equal("hello_world", result);
        }

        [Fact]
        public void Prepend_WhenInputIsNull_ReturnsNull() {
            // Arrange
            string input = null;
            string prepend = "prefix_";

            // Act
            var result = input.Prepend(prepend);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void Prepend_WhenInputIsEmpty_ReturnsEmpty() {
            // Arrange
            string input = "";
            string prepend = "prefix_";

            // Act
            var result = input.Prepend(prepend);

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void Prepend_WhenInputHasValue_PrependsString() {
            // Arrange
            string input = "world";
            string prepend = "hello_";

            // Act
            var result = input.Prepend(prepend);

            // Assert
            Assert.Equal("hello_world", result);
        }

        [Fact]
        public void RemoveLineBreaks_WhenInputIsNull_ReturnsNull() {
            // Arrange
            string input = null;

            // Act
            var result = input.RemoveLineBreaks();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void RemoveLineBreaks_RemovesCarriageReturnAndLineFeed() {
            // Arrange
            string input = "line1\r\nline2\nline3\rline4";

            // Act
            var result = input.RemoveLineBreaks();

            // Assert
            Assert.Equal("line1line2line3line4", result);
        }

        [Fact]
        public void RemoveLineBreaks_WhenNoLineBreaks_ReturnsOriginal() {
            // Arrange
            string input = "no line breaks here";

            // Act
            var result = input.RemoveLineBreaks();

            // Assert
            Assert.Equal("no line breaks here", result);
        }

        [Fact]
        public void RemoveNBSP_WhenInputIsNull_ReturnsEmptyString() {
            // Arrange
            string input = null;

            // Act
            var result = input.RemoveNBSP();

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void RemoveNBSP_ReplacesNonBreakingSpaceWithRegularSpace() {
            // Arrange
            string input = "hello\u00A0world";

            // Act
            var result = input.RemoveNBSP();

            // Assert
            Assert.Equal("hello world", result);
        }

        [Fact]
        public void RemoveNBSP_WhenNoNBSP_ReturnsOriginal() {
            // Arrange
            string input = "hello world";

            // Act
            var result = input.RemoveNBSP();

            // Assert
            Assert.Equal("hello world", result);
        }

        [Fact]
        public void ToBase64String_EncodesStringToBase64() {
            // Arrange
            string input = "Hello World";

            // Act
            var result = input.ToBase64String();

            // Assert
            Assert.Equal("SGVsbG8gV29ybGQ=", result);
        }

        [Fact]
        public void ToBase64String_WithEmptyString_ReturnsEmptyBase64() {
            // Arrange
            string input = "";

            // Act
            var result = input.ToBase64String();

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void ToBase64String_WithSpecialCharacters_EncodesCorrectly() {
            // Arrange
            string input = "Test@123!#$";

            // Act
            var result = input.ToBase64String();
            var decoded = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(result));

            // Assert
            Assert.Equal(input, decoded);
        }
    }
}

using System;
using EventGridPubSub.Types;
using Xunit;

namespace EventGrid.Tests.Types
{
    public class MessageVersionTests
    {
        [Fact]
        public void Message_constructed_from_integers()
        {
            // Given a message version
            int major = 1;
            int minor = 2;
            int patch = 3;

            // When I request the version string
            var version = new MessageVersion(major, minor, patch);
            string versionString = version.ToString();

            // Then it matches the input
            Assert.Equal("1.2.3", versionString);
        }

        [Fact]
        public void Message_constructed_from_valid_string()
        {
            // Given a message version
            var versionNumber = "1.2.3";

            // When I request the version string
            var version = new MessageVersion(versionNumber);
            string versionString = version.ToString();

            // Then it matches the input
            Assert.Equal("1.2.3", versionString);
        }

        [Theory]
        [InlineData("1.2.3.4")]
        [InlineData("1.2.")]
        [InlineData("1.2")]
        [InlineData(" 1.2.3")]
        [InlineData(" 1.2.3 ")]
        [InlineData("1.2.3 ")]
        [InlineData("")]
        [InlineData(" ")]
        public void Message_constructed_from_invalid_string_throws(string input)
        {
            // Given a message version
            var versionNumber = input;

            // When I construct the MessageVersion
            // Then it throws an ArgumentException
            Assert.Throws<ArgumentException>(() => new MessageVersion(versionNumber));
        }

        [Fact]
        public void Message_constructed_from_null_string_throws()
        {
            // Given a null message version
            // When I construct the MessageVersion
            // Then it throws an ArgumentNullException
            Assert.Throws<ArgumentNullException>(() => new MessageVersion(null));
        }

        [Fact]

        public void Message_cast_from_string_matches_input()
        {
            // Given a version
            var version = "1.2.3";

            // When I cast the string to a MessageVersion
            MessageVersion messageVersion = version;

            // Then the value matchses
            Assert.Equal(version, messageVersion.ToString());
        }
    }
}

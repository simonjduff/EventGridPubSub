using System;
using System.Text.RegularExpressions;

namespace EventGridPubSub.Types
{
    public struct MessageVersion
    {
        private readonly int _major;
        private readonly int _minor;
        private readonly int _patch;
        private static readonly Regex VersionRegex = new Regex(@"^(?<major>\d+)\.(?<minor>\d+)\.(?<patch>\d+)$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        public MessageVersion(int major, int minor, int patch)
        {
            _patch = patch;
            _minor = minor;
            _major = major;
        }

        public MessageVersion(string version)
        {
            var match = VersionRegex.Match(version);
            if (!match.Success)
            {
                throw new ArgumentException("Supplied version is not valid", nameof(version));
            }

            _major = int.Parse(match.Groups["major"].Value);
            _minor = int.Parse(match.Groups["minor"].Value);
            _patch = int.Parse(match.Groups["patch"].Value);
        }

        public override string ToString()
        {
            return $"{_major}.{_minor}.{_patch}";
        }

        public static implicit operator MessageVersion(string version) => new MessageVersion(version);
    }
}
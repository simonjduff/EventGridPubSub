namespace EventGridPubSub.Types
{
    public class Region
    {
        private readonly string _regionName;

        public Region(string regionName)
        {
            _regionName = regionName;
        }

        public override string ToString() => _regionName;

        public static implicit operator string(Region region) => region._regionName;
        public static implicit operator Region(string region) => new Region(region);

        public override int GetHashCode()
        {
            // https://www.loganfranken.com/blog/692/overriding-equals-in-c-part-2/
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int hashingBase = 90001;
                const int hashingMultiplier = 90007;

                int hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ (!ReferenceEquals(null, _regionName) ? _regionName.GetHashCode() : 0);
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            Region region = obj as Region;
            if (region == null)
            {
                return false;
            }

            return this._regionName == region._regionName;
        }

        public static bool operator ==(Region left, Region right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (ReferenceEquals(left, null))

            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Region left, Region right)
        {
            return !(left == right);
        }
    }
}
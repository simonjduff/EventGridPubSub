namespace EventGridPubSub.Types
{
    public class Topic
    {
        private readonly string _topicName;

        public Topic(string topicName)
        {
            _topicName = topicName;
        }

        public override string ToString() => _topicName;
        
        public static implicit operator string(Topic topic) => topic._topicName;
        public static implicit operator Topic(string topic) => new Topic(topic);

        public override int GetHashCode()
        {
            // https://www.loganfranken.com/blog/692/overriding-equals-in-c-part-2/
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int hashingBase = 1933;
                const int hashingMultiplier = 90017;

                int hash = hashingBase;
                hash = (hash * hashingMultiplier) ^ (!ReferenceEquals(null, _topicName) ? _topicName.GetHashCode() : 0);
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            Topic topic = obj as Topic;
            if (topic == null)
            {
                return false;
            }

            return this._topicName == topic._topicName;
        }

        public static bool operator ==(Topic left, Topic right)
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

        public static bool operator !=(Topic left, Topic right)
        {
            return !(left == right);
        }
    }
}

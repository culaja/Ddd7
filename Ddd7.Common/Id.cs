using System.Collections.Generic;

namespace Ddd7.Common
{
    public sealed class Id : ValueObject<Id>
    {
        internal string StringId { get; private set; }

        private Id(string id)
        {
            StringId = id;
        }

        public static Id None => new Id("None");

        public static Id IdFrom(string id) => string.IsNullOrEmpty(id) ? None : new Id(id);

        public Id SetIfNone(string idToSet)
        {
            if (this == None)
            {
                StringId = idToSet;
            }
            return this;
        }

        public override string ToString() => StringId;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StringId;
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace PagueVeloz.Domain.Entities
{
    public abstract class EntityBase
    {
        public long Id { get; set; }
        public IList<string> ValidationErrors { get; protected set; } = new List<string>();
        public abstract bool Validate();
    }
}

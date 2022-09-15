using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Language : Entity
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public virtual ICollection<Technology> Technologies { get; set; }


        public Language()
        {

        }

        public Language(int id, string name, string version) : this()
        {
            Id = id;
            Name = name;
            Version = version;
        }

    }
}

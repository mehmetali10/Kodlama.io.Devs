using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Technology : Entity
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public virtual Language? Language { get; set; } //birçok orm kullanması amacıyla virtual

        public Technology()
        {
        }

        public Technology(int id, int languagedId, string name, string version) : this()
        {
            Id = id;
            LanguageId = languagedId;
            Name = name;
            Version = version;
        }
    }
}

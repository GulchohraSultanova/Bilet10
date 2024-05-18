using Core.Models;
using Core.RepositoryAbstracts;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryConcreters
{
    public class OurTeamRepository : GenericRepository<OurTeam>, IOurTeamRepository
    {
        public OurTeamRepository(AppDbContext db) : base(db)
        {
        }
    }
}

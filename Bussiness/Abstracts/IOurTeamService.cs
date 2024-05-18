using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstracts
{
  public interface IOurTeamService
    {
        void Create(OurTeam team);
        void Update(int id,OurTeam team);
        void Delete(int id);
        OurTeam GetTeam(Func<OurTeam,bool> ? func=null);
        List<OurTeam> GetAllTeam(Func<OurTeam, bool>? func=null);



    }
}

using Bussiness.Abstracts;
using Bussiness.Exceptions;
using Core.Models;
using Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Concreters
{
    public class OurTeamService : IOurTeamService
    {
        IOurTeamRepository _ourTeamRepository;
        IWebHostEnvironment _webHostEnvironment;

        public OurTeamService(IOurTeamRepository ourTeamRepository, IWebHostEnvironment webHostEnvironment)
        {
            _ourTeamRepository = ourTeamRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Create(OurTeam team)
        {
            if (team.PhotoFile == null)
            {
                throw new NotFoundException("PhotoFile", "Null ola bilmez!");
            }
            if (!team.PhotoFile.ContentType.Contains("image/"))
            {
                throw new FileContentTypeException("PhotoFile", "File tipi duzgun deyil!");
            }
            string filename = team.PhotoFile.FileName;
            string path = _webHostEnvironment.WebRootPath + @"\Upload\OurTeam\" + filename;
            using (FileStream file = new FileStream(path, FileMode.Create))
            {
                team.PhotoFile.CopyTo(file);
            }
            team.ImgUrl = filename;
            _ourTeamRepository.Add(team);
            _ourTeamRepository.Commit();

        }

        public void Delete(int id)
        {
            var deleteTeam = _ourTeamRepository.Get(x => x.Id == id);
            string path = _webHostEnvironment.WebRootPath + @"\Upload\OurTeam\" + deleteTeam.ImgUrl;
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            _ourTeamRepository.Delete(deleteTeam);
            _ourTeamRepository.Commit();


        }

        public List<OurTeam> GetAllTeam(Func<OurTeam, bool>? func = null)
        {
            return _ourTeamRepository.GetAll(func);
        }

        public OurTeam GetTeam(Func<OurTeam, bool>? func = null)
        {
            return _ourTeamRepository.Get(func);
        }

        public void Update(int id, OurTeam team)
        {
            var oldTeam = _ourTeamRepository.Get(x => x.Id == id);
            if (oldTeam == null)
            {
                throw new NotFoundException("", "Team null ola bilmez!");
            }
            if (oldTeam.PhotoFile != null)
            {
                if (!team.PhotoFile.ContentType.Contains("image/"))
                {
                    throw new FileContentTypeException("PhotoFile", "File tipi duzgun deyil!");
                }
                string filename = team.PhotoFile.FileName;
                string path = _webHostEnvironment.WebRootPath + @"\Upload\OurTeam\" + filename;
                using (FileStream file = new FileStream(path, FileMode.Create))
                {
                    team.PhotoFile.CopyTo(file);
                }
                oldTeam.ImgUrl = filename;
            }
            else
            {
                team.PhotoFile = oldTeam.PhotoFile;
            }
            oldTeam.Name = team.Name;
            oldTeam.Position = team.Position;
            _ourTeamRepository.Commit();
        }
    }
}

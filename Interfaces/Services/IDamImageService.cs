using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMD.Interfaces.Services
{
    public interface IDamImageService
    {
        List<DamImage> getAllImages(int mode,out int companyId);
        bool addImage(DamImage img);
        string downloadImage(int type, int mode, int id, string path);
        bool deleteImage(long id);
        string updateImage(long id, string name);
    }
}

using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SMD.Implementation.Services
{
    class DamImageService : IDamImageService
    {
            #region Private

        /// <summary>
        /// Private members
        /// </summary>
        private readonly IDamImageRepository damRepository;
    
        #endregion
        #region Constructor

        /// <summary>
        ///  Constructor
        /// </summary>
        public DamImageService(IDamImageRepository _damRepository)
        {
            this.damRepository = _damRepository;
  
        }

        #endregion
        #region public
        public List<DamImage> getAllImages(int mode, out int companyId)
        {
            return damRepository.getAllImages(mode,out companyId);
        }
        public bool addImage(DamImage img)
        {
            damRepository.Add(img);
            damRepository.SaveChanges();
            return true;
        }
        public string downloadImage(int type, int mode,int id,string path)
        {
            //type : 1 coupon  //mode 1=> logo ,2 =>banner image 1 , 3 => banner image2 ,4 => banner image 3  //id = couponId
            string imagePath = "";
           // string[] paths = path.Split(new string[] { "SMD_Content" }, StringSplitOptions.None);
            if (type == 1)
            {
                string directoryPath = HttpContext.Current.Server.MapPath("~/SMD_Content/Coupons/" + id);
                string savePath = directoryPath + "\\guid_CampaignLogoImage.jpg";
                File.Copy(path, savePath);
                int indexOf = savePath.LastIndexOf("SMD_Content", StringComparison.Ordinal);
                savePath = savePath.Substring(indexOf, savePath.Length - indexOf);
                imagePath = savePath;
            }
            return imagePath;
            //type : 2 ads
            //type : 3 surveys 
            
        }
        #endregion
    }
}

using System.Collections.Generic;
using SMD.Interfaces.Repository;
using SMD.Interfaces.Services;
using SMD.Models.DomainModels;

namespace SMD.Implementation.Services
{
    /// <summary>
    /// Profile Question Answer Service 
    /// </summary>
    public sealed class ProfileQuestionAnswerService  : IProfileQuestionAnswerService 
    {
        #region Private
        private readonly IProfileQuestionAnswerRepository _profileQuestionAnswerRepository;
        private readonly IProfileQuestionRepository _ProfileQuestionRepository;
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor 
        /// </summary>

        public ProfileQuestionAnswerService(IProfileQuestionAnswerRepository profileQuestionAnswerRepository, IProfileQuestionRepository ProfileQuestionRepository)
        {
            _profileQuestionAnswerRepository = profileQuestionAnswerRepository;
            _ProfileQuestionRepository = ProfileQuestionRepository;
        }

        #endregion
        #region Public

        /// <summary>
        /// Get Answer by Profile Question Id 
        /// </summary>
        public IEnumerable<ProfileQuestionAnswer> GetProfileQuestionAnswerByQuestionId(long profileQuestionId)
        {
            var Query= _profileQuestionAnswerRepository.GetProfileQuestionAnswerByQuestionId(profileQuestionId);

               foreach (var item in Query)
               {
                foreach (var PCriteria in item.ProfileQuestion.ProfileQuestionTargetCriterias1)
                { 
                    if(PCriteria.PQQuestionID!=null&&PCriteria.PQQuestionID>0)
                    {
                        PCriteria.PQQuestionString=_ProfileQuestionRepository.Find(PCriteria.PQQuestionID??0).Question;
                    }
                }
            }
               return Query;
        }
        public IEnumerable<ProfileQuestionAnswer> GetProfileQuestionAnswerOrderBySortorder(long profileQuestionId)
        {
            return _profileQuestionAnswerRepository.GetAllProfileQuestionAnswerOrderbySortOrder(profileQuestionId);
        }
        #endregion
    }
}

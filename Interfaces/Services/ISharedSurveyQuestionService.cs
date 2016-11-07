using SMD.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SMD.Interfaces.Services
{
    public interface ISharedSurveyQuestionService
    {

          bool CreateAndSend(SharedSurveyQuestion survey);

          SharedSurveyQuestion GetSharedSurveyQuestion(long SSQID);

          bool updateUserSharedSurveyQuestionResponse(long SurveyQuestionShareId, int UserSelection);

          bool DeleteSharedSurveyQuestion(long SSQID);
    }
}

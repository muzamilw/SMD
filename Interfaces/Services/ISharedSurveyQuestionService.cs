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

          long CreateAndSend(SharedSurveyQuestion survey);

          GetSharedSurveyQuestion_Result GetSharedSurveyQuestion(long SSQID);

          bool updateUserSharedSurveyQuestionResponse(long SurveyQuestionShareId, int UserSelection);

          bool DeleteSharedSurveyQuestion(long SSQID);


          List<GetSharedSurveyQuestionsByUserId_Result> GetSharedSurveysByuserID(string UserId);
    }
}

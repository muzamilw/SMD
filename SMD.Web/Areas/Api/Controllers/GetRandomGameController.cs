﻿using SMD.Interfaces.Services;
using SMD.Models.DomainModels;
using SMD.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using SMD.MIS.Areas.Api.Models;
using SMD.MIS.ModelMappers;
using SMD.Models.ResponseModels;
namespace SMD.MIS.Areas.Api.Controllers
{
    public class GetRandomGameController : ApiController
    {
        private readonly IWebApiUserService webApiUserService;

        #region Private
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public GetRandomGameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        #endregion

        #region Public

        /// <summary>
        ///Get Random game
        /// </summary>


        public RandomGameResponse Get(string authenticationToken, string UserId, long CampaignId, int ExistingGameId)//string BuyItModel request
        {
            if (string.IsNullOrEmpty(UserId) || CampaignId == 0)
            {
                throw new HttpException((int)HttpStatusCode.BadRequest, LanguageResources.InvalidRequest);
            }

            var game = gameService.GetRandomGame(ExistingGameId);

            return new RandomGameResponse { GameId = game.GameId, GameName = game.GameName, GameUrl = game.GameUrl, Message = "Success", Status = true };
          
        }
   
        #endregion
    }
}

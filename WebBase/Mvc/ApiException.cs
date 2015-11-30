﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Microsoft.Practices.Unity;
using SMD.ExceptionHandling;
using SMD.ExceptionHandling.Logger;
using SMD.Interfaces.Logger;
using SMD.WebBase.UnityConfiguration;
using Newtonsoft.Json;

namespace SMD.WebBase.Mvc
{
    /// <summary>
    /// Api Exception filter attribute for Api controller methods
    /// </summary>
    public class ApiException : ActionFilterAttribute
    {
        #region Private
// ReSharper disable InconsistentNaming
        private static ISMDLogger mpcLogger;
// ReSharper restore InconsistentNaming
        /// <summary>
        /// Get Configured logger
        /// </summary>
// ReSharper disable InconsistentNaming
        private static ISMDLogger MPCLogger
// ReSharper restore InconsistentNaming
        {
            get
            {
                if (mpcLogger != null) return mpcLogger;
                mpcLogger = (UnityConfig.GetConfiguredContainer()).Resolve<ISMDLogger>();
                return mpcLogger;
            }
        }

        /// <summary>
        /// Set status code and contents of the Application exception
        /// </summary>
        private void SetApplicationResponse(HttpActionExecutedContext filterContext)
        {
// ReSharper disable SuggestUseVarKeywordEvident
            SMDExceptionContent contents = new SMDExceptionContent
// ReSharper restore SuggestUseVarKeywordEvident
            {
                Message = filterContext.Exception.Message
            };
            filterContext.Response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(contents))
            };
        }
        private void SetGeneralExceptionApplicationResponse(HttpActionExecutedContext filterContext)
        {
            string exceptionMessage = filterContext.Exception == null || HttpContext.Current.IsDebuggingEnabled
                ? string.Empty
                : filterContext.Exception.InnerException.Message;

// ReSharper disable SuggestUseVarKeywordEvident
            SMDExceptionContent contents = new SMDExceptionContent
// ReSharper restore SuggestUseVarKeywordEvident
            {
                Message = "There is some problem while performing this operation. " + exceptionMessage
                // Replace message text with this line for production environment 
                // filterContext.Exception.InnerException.Message
            };
            filterContext.Response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(contents))
            };
        }
        /// <summary>
        /// Log Error
        /// </summary>
        private void LogError(Exception exp, long organisationId, string requestContents)
        {
            MPCLogger.Write(exp, SMDLogCategory.Error, -1, -1, TraceEventType.Warning, "", new Dictionary<string, object> { { "Organisation", organisationId }, 
            { "RequestContents", requestContents } });
        }
        #endregion

        /// <summary>
        /// Exception Handler for api calls; apply this attribute for all the Api calls
        /// </summary>
        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                return;
            }
            if (filterContext.Exception is SMDException)
            {
                SetApplicationResponse(filterContext);
// ReSharper disable SuggestUseVarKeywordEvident
                SMDException exp = filterContext.Exception as SMDException;
// ReSharper restore SuggestUseVarKeywordEvident
                LogError(exp, 0, filterContext.Request.Content.ToString());
            }
            else
            {
                SetGeneralExceptionApplicationResponse(filterContext);
                LogError(filterContext.Exception, -1, filterContext.Request.Content.ToString());
            }

        }
    }
}

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using SMD.ExceptionHandling;
using SMD.ExceptionHandling.Logger;
using SMD.Interfaces.Logger;
using SMD.WebBase.UnityConfiguration;

namespace SMD.WebBase.Mvc
{
    /// <summary>
    /// Log Exception Filter Attribut
    /// </summary>
    public sealed class LogExceptionFilterAttribute : HandleErrorAttribute, System.Web.Http.Filters.IExceptionFilter
    {
        #region Private

        private static ISMDLogger mpcLogger;
        /// <summary>
        /// Get Configured logger
        /// </summary>
        private static ISMDLogger MPCLogger
        {
            get
            {
                if (mpcLogger != null) return mpcLogger;
                mpcLogger = (UnityConfig.GetConfiguredContainer()).Resolve<ISMDLogger>();
                return mpcLogger;
            }
        }
        /// <summary>
        /// Route data prefix
        /// </summary>
        private const string RouteDataPrefix = "Route data: ";

        /// <summary>
        /// Log the exception
        /// </summary>
        private void LogException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception != null)
            {
                Dictionary<string, object> properties = filterContext.RouteData.Values.Keys.ToDictionary(key => RouteDataPrefix + key, key => filterContext.RouteData.Values[key]);
                // add route data

                SMDException execption = filterContext.Exception as SMDException;
                if (execption != null)
                {
                    MPCLogger.Write(execption, SMDLogCategory.Warning, 1, 0, TraceEventType.Information, SMDLogCategory.Warning, properties);
                }
                else
                {
                    MPCLogger.Write(filterContext.Exception, SMDLogCategory.Error, 1, 0, TraceEventType.Critical, SMDLogCategory.Error, properties);
                }                  
            }
        }

        #endregion
        #region Public

        /// <summary>
        /// An exception occurred
        /// </summary>
        public override void OnException(ExceptionContext filterContext)
        {
            LogException(filterContext);

            base.OnException(filterContext);
        }

        #endregion
        /// <summary>
        /// Not implemented Async Call for Logging
        /// </summary>
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}

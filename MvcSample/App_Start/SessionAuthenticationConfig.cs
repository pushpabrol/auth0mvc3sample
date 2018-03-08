using System;
using System.IdentityModel.Selectors;
using Microsoft.IdentityModel.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Mvc3Sample.App_Start.SessionAuthenticationConfig), "PreAppStart")]

namespace Mvc3Sample.App_Start
{
    public static class SessionAuthenticationConfig
    {
        public static void PreAppStart()
        {
            DynamicModuleUtility.RegisterModule(typeof(Microsoft.IdentityModel.Web.SessionAuthenticationModule));
        }
    }
}
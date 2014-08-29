//-----------------------------------------------------------------------
// <copyright file="WindsorControllerFactory.cs" company="WeeWorld">
//     Copyright (c) WeeWorld. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace WeeWorld.ADS.Web.IoC
{

    /// <summary>An derived instance of DefaultControllerFactory that uses the 
    /// WeeWorld container to resolve dependencies for MVC controllers. This 
    /// deals exclusively with controllers derived from the mvc controller.
    /// </summary>
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel kernel;
 
        public WindsorControllerFactory(IKernel kernel)
        {
            this.kernel = kernel;
        }
 
        public override void ReleaseController(IController controller)
        {
            kernel.ReleaseComponent(controller);
        }
 
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));

            return (IController)kernel.Resolve(controllerType);
        }
    }
}
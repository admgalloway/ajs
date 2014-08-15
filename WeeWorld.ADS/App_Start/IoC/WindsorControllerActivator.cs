using System;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel;

namespace WeeWorld.ADS.IoC
{

    /// <summary>An implimentation of ControllerActivator that uses the WeeWorld 
    /// container to resolve dependencies (including the controllers themselves).
    /// This deals exclusively with controllers derived from the ApiController.
    /// </summary>
    public class WindsorControllerActivator : IHttpControllerActivator
    {
        private readonly WeeWorldWindsorContainer container;

        public WindsorControllerActivator(WeeWorldWindsorContainer container)
        {
            this.container = container;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            // resolve dependencies for controller using the custom WWWindsorContainer
            var controller = (IHttpController)container.Resolve(controllerType);

            // register the release action to be called when the web request itself is disposed
            request.RegisterForDispose(new ReleaseAction(() => container.Release(controller)));
            return controller;
        }

        private class ReleaseAction : IDisposable
        {
            private readonly Action action;

            public ReleaseAction(Action action)
            {
                // release action supplied by caller
                this.action = action;
            }

            public void Dispose()
            {
                // call release action on disposal
                action();
            }
        }

    }
}
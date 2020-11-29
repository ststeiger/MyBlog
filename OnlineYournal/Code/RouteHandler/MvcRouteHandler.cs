// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

// namespace OnlineYournal.Code
namespace Microsoft.AspNetCore.Mvc.Routing
{



    // https://stackoverflow.com/questions/53803266/subdomain-routing-in-asp-net-core-3-0-razorpages
    public class AreaRouter
        : MvcRouteHandler, IRouter
    {


        public AreaRouter(
                 Microsoft.AspNetCore.Mvc.Infrastructure.IActionInvokerFactory actionInvokerFactory,
                 Microsoft.AspNetCore.Mvc.Infrastructure.IActionSelector actionSelector,
                 System.Diagnostics.DiagnosticListener diagnosticListener,
                 Microsoft.Extensions.Logging.ILoggerFactory loggerFactory,
                 IActionContextAccessor actionContextAccessor)
            : base(actionInvokerFactory, actionSelector, diagnosticListener, loggerFactory, actionContextAccessor)
        { }


        public AreaRouter(
                Microsoft.AspNetCore.Mvc.Infrastructure.IActionInvokerFactory actionInvokerFactory,
                Microsoft.AspNetCore.Mvc.Infrastructure.IActionSelector actionSelector,
                System.Diagnostics.DiagnosticListener diagnosticListener,
                Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
            : this(actionInvokerFactory, actionSelector, diagnosticListener, loggerFactory, null)
        { }


        public AreaRouter()
            : this(actionInvokerFactory:null, actionSelector: null, diagnosticListener: null, loggerFactory: null, actionContextAccessor: null)
        { 
            // TODO:
        }


        public new async Task RouteAsync(RouteContext context)
        {
            string url = context.HttpContext.Request.Headers["HOST"];

            string firstDomain = url.Split('.')[0];
            string subDomain = char.ToUpper(firstDomain[0]) + firstDomain.Substring(1);

            string area = subDomain;

            context.RouteData.Values.Add("area", subDomain);

            await base.RouteAsync(context);
        }

    } // End Class AreaRouter


    public class MvcRouteHandler 
        : IRouter
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IActionInvokerFactory _actionInvokerFactory;
        private readonly IActionSelector _actionSelector;
        private readonly ILogger _logger;
        private readonly DiagnosticListener _diagnosticListener;

        public MvcRouteHandler(
            IActionInvokerFactory actionInvokerFactory,
            IActionSelector actionSelector,
            DiagnosticListener diagnosticListener,
            ILoggerFactory loggerFactory)
            : this(actionInvokerFactory, actionSelector, diagnosticListener, loggerFactory, actionContextAccessor: null)
        {
        }

        public MvcRouteHandler(
            IActionInvokerFactory actionInvokerFactory,
            IActionSelector actionSelector,
            DiagnosticListener diagnosticListener,
            ILoggerFactory loggerFactory,
            IActionContextAccessor actionContextAccessor)
        {
            // The IActionContextAccessor is optional. We want to avoid the overhead of using CallContext
            // if possible.
            _actionContextAccessor = actionContextAccessor;

            _actionInvokerFactory = actionInvokerFactory;
            _actionSelector = actionSelector;
            _diagnosticListener = diagnosticListener;
            _logger = loggerFactory.CreateLogger<MvcRouteHandler>();
        }

        // IRouter.GetVirtualPath
        public VirtualPathData GetVirtualPath(VirtualPathContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // We return null here because we're not responsible for generating the url, the route is.
            return null;
        }


        // IRouter.RouteAsync
        public Task RouteAsync(RouteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var candidates = _actionSelector.SelectCandidates(context);
            if (candidates == null || candidates.Count == 0)
            {
                // _logger.NoActionsMatched(context.RouteData.Values);
                return Task.CompletedTask;
            }

            var actionDescriptor = _actionSelector.SelectBestCandidate(context, candidates);
            if (actionDescriptor == null)
            {
                // _logger.NoActionsMatched(context.RouteData.Values);
                return Task.CompletedTask;
            }

            context.Handler = (c) =>
            {
                var routeData = c.GetRouteData();

                var actionContext = new ActionContext(context.HttpContext, routeData, actionDescriptor);
                if (_actionContextAccessor != null)
                {
                    _actionContextAccessor.ActionContext = actionContext;
                }

                var invoker = _actionInvokerFactory.CreateInvoker(actionContext);
                if (invoker == null)
                {
                    throw new InvalidOperationException("Could not create invoker for " + actionDescriptor.DisplayName);
                }

                return invoker.InvokeAsync();
            };

            return Task.CompletedTask;
        }
    }
}
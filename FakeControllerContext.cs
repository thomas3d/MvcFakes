using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.WebPages;

namespace FlickTrap.Web.Specs.MvcFakes
{

    public class FakeControllerContext : ControllerContext
    {
        public FakeControllerContext( ControllerBase controller )
            : this(controller, null, null, null, null, null, null)
        {
        }

        public FakeControllerContext(ControllerBase controller, HttpRequestBase request, HttpSessionStateBase session, IDisplayMode displayMode = null, string[] searchPaths = null)
            : this(controller, null, null, request.Form, request.QueryString, request.Cookies, GetSessionCollection(session))
        {
            if(displayMode != null)
                DisplayMode = displayMode;

            if (searchPaths != null)
            {
                var razorEngine = ViewEngines.Engines.OfType<RazorViewEngine>().First();
                razorEngine.ViewLocationFormats = razorEngine.ViewLocationFormats.Concat(searchPaths).ToArray();
                razorEngine.PartialViewLocationFormats = razorEngine.PartialViewLocationFormats.Concat(searchPaths).ToArray();
            }
        }

        public FakeControllerContext( ControllerBase controller, HttpCookieCollection cookies )
            : this(controller, null, null, null, null, cookies, null)
        {
        }

        public FakeControllerContext( ControllerBase controller, SessionStateItemCollection sessionItems )
            : this(controller, null, null, null, null, null, sessionItems)
        {
        }

        public FakeControllerContext( ControllerBase controller, NameValueCollection formParams ) 
            : this(controller, null, null, formParams, null, null, null)
        {
        }

        public FakeControllerContext( ControllerBase controller, NameValueCollection formParams, NameValueCollection queryStringParams )
            : this(controller, null, null, formParams, queryStringParams, null, null)
        {
        }
        
        public FakeControllerContext( ControllerBase controller, string userName )
            : this(controller, userName, null, null, null, null, null)
        {
        }

        public FakeControllerContext( ControllerBase controller, string userName, string[] roles )
            : this(controller, userName, roles, null, null, null, null)
        {
        }

        public FakeControllerContext
            (
                ControllerBase controller,
                string userName,
                string[] roles,
                NameValueCollection formParams,
                NameValueCollection queryStringParams,
                HttpCookieCollection cookies,
                SessionStateItemCollection sessionItems
            )
            : base(new FakeHttpContext(new FakePrincipal(new FakeIdentity(userName), roles), formParams, queryStringParams, cookies, sessionItems, new StringWriter()), new RouteData(), controller)
        {
            if (!RouteData.Values.ContainsKey("controller") && !RouteData.Values.ContainsKey("Controller"))
                RouteData.Values.Add("controller", controller.GetType().Name
                                                            .ToLower()
                                                            .Replace("controller", ""));
        }

        public static SessionStateItemCollection GetSessionCollection(HttpSessionStateBase input)
        {
            var collection = new SessionStateItemCollection();
            if (input != null && input.Count > 0)
            {
                foreach (string key in input)
                    collection[key] = input[key];
            }

            return collection;
        }

	    public override string ToString()
        {
            return RequestContext.HttpContext.Response.Output.ToString();
        } 
    }
}

using System;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;
using System.Web.Routing;

namespace FlickTrap.Web.Specs.MvcFakes
{

    public class FakeHttpRequest : HttpRequestBase
    {
        private readonly NameValueCollection _formParams;
        private readonly NameValueCollection _queryStringParams;
        private readonly HttpCookieCollection _cookies;

        public FakeHttpRequest(NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection cookies)
        {
            _formParams = formParams;
            _queryStringParams = queryStringParams;
            _cookies = cookies;
            ContentType = "text/html";
            RequestContext = HttpContext.Current.Request.RequestContext;
        }

        public override NameValueCollection Form
        {
            get
            {
                return _formParams;
            }
        }

        public override NameValueCollection QueryString
        {
            get
            {
                return _queryStringParams;
            }
        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return _cookies;
            }
        }

        public override string UserAgent
        {
            get
            {
                return "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";
            }
        }

        public override HttpBrowserCapabilitiesBase Browser
        {
            get
            {
                return new FakeBrowserCapabilities();
            }
        }

        public override bool IsLocal
        {
            get { return true; }
        }

        public override string ApplicationPath
        {
            get { return Assembly.GetExecutingAssembly().Location; }
        }

        public override string Path
        {
            get { return string.Empty; }
        }

        public override Uri Url
        {
            get { return new Uri("http://localhost/CV/Citizen/MyPlan/"); }
        }

        public override UnvalidatedRequestValuesBase Unvalidated
        {
            get { return new UnvalidatedRequestValuesWrapper(HttpContext.Current.Request.Unvalidated); }
        }

        public override string ContentType { get; set; }

        public override HttpFileCollectionBase Files
        {
            get { return new HttpFileCollectionWrapper(HttpContext.Current.Request.Files); }
        }

        public override string HttpMethod
        {
            get { return HttpContext.Current.Request.HttpMethod; }
        }

        public override RequestContext RequestContext { get; set; }
    }
}

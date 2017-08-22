using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Security.Principal;
using System.Web;
using System.Web.Instrumentation;
using System.Web.SessionState;

namespace FlickTrap.Web.Specs.MvcFakes
{

    public class FakeHttpContext : HttpContextBase
    {
        private readonly FakePrincipal _principal;
        private readonly NameValueCollection _formParams;
        private readonly NameValueCollection _queryStringParams;
        private readonly HttpCookieCollection _cookies;
        private readonly SessionStateItemCollection _sessionItems;
        private readonly TextWriter _writer;
        private readonly Dictionary<object, object> _dic;

        public FakeHttpContext(FakePrincipal principal, NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection cookies, SessionStateItemCollection sessionItems, TextWriter writer)
        {
            _principal = principal;
            _formParams = formParams;
            _queryStringParams = queryStringParams;
            _cookies = cookies;
            _sessionItems = sessionItems;
            _writer = writer;
            _dic = new Dictionary<object, object>();
            ApplicationInstance = HttpContext.Current.ApplicationInstance;
        }

        public override HttpRequestBase Request
        {
            get
            {
                return new FakeHttpRequest(_formParams, _queryStringParams, _cookies);
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return new FakeHttpResponse(_writer, _cookies);
            }
        }

        public override IPrincipal User
        {
            get
            {
                return _principal;
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }

        public override HttpSessionStateBase Session
        {
            get
            {
                return new FakeHttpSessionState(_sessionItems);
            }
        }

        public override IDictionary Items
        {
            get
            {
                return _dic;
            }
        }

        public override PageInstrumentationService PageInstrumentation
        {
            get { return new PageInstrumentationService(); }
        }

        public override object GetService(Type serviceType)
        {
            return null;
        }

        public override object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture)
        {
            return null;
        }

        public override HttpServerUtilityBase Server
        {
            get { return new HttpServerUtilityWrapper(HttpContext.Current.Server ); }
        }

        public override HttpApplication ApplicationInstance { get; set; }

        public override HttpApplicationStateBase Application
        {
            get { return new HttpApplicationStateWrapper(HttpContext.Current.Application); }
        }

    }


}

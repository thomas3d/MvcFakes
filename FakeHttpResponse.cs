using System.Collections.Specialized;
using System.IO;
using System.Web;

namespace FlickTrap.Web.Specs.MvcFakes
{
    public class FakeHttpResponse : HttpResponseBase
    {
        private TextWriter _writer;
        private readonly HttpCookieCollection _cookies;

        public FakeHttpResponse(TextWriter writer, HttpCookieCollection cookies)
        {
            _writer = writer;
            _cookies = cookies;
        }
        public override TextWriter Output
        {
            get
            {
                return _writer;
            }
            set
            {
                _writer = value;
            }
        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return _cookies;
            }
        }

        public override string ApplyAppPathModifier(string virtualPath)
        {
            return virtualPath;
        }

        public override HttpCachePolicyBase Cache
        {
            get { return new HttpCachePolicyWrapper(HttpContext.Current.Response.Cache); }

        }
    }



}

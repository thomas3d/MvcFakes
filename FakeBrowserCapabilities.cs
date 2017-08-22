using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace FlickTrap.Web.Specs.MvcFakes
{
    public class FakeBrowserCapabilities : HttpBrowserCapabilitiesBase
    {
        public override bool IsMobileDevice
        {
            get { return false; }
        }

        public override string Browser
        {
            get { return "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36"; }
        }
    }
}

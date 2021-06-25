using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcePointer.NameSorter.Web
{
    public sealed class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            // Use HSTS preload
            // https://www.checkbot.io/guide/security/?utm_source=checkbot-extension&utm_medium=extension&utm_content=learn-more#rule-allow-hsts-preload
            context.Response.Headers.Add("strict-transport-security", new StringValues(
                "max-age=31536000; " +
                "includeSubDomains; " +
                "preload;"));

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referrer-Policy
            // TODO Change the value depending of your needs
            context.Response.Headers.Add("referrer-policy", new StringValues("strict-origin-when-cross-origin"));

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
            context.Response.Headers.Add("x-content-type-options", new StringValues("nosniff"));

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
            context.Response.Headers.Add("x-frame-options", new StringValues("SAMEORIGIN"));

            // https://security.stackexchange.com/questions/166024/does-the-x-permitted-cross-domain-policies-header-have-any-benefit-for-my-websit
            context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", new StringValues("none"));

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-XSS-Protection
            context.Response.Headers.Add("x-xss-protection", new StringValues("1; mode=block"));

            //// https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Expect-CT
            //// You can use https://report-uri.com/ to get notified when a misissued certificate is detected
            //context.Response.Headers.Add("Expect-CT", new StringValues("max-age=0, enforce, report-uri=\"https://example.report-uri.com/r/d/ct/enforce\""));

            // https://blog.elmah.io/the-asp-net-core-security-headers-guide/
            context.Response.Headers.Add("Permissions-Policy", "usb=()");

            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Feature-Policy
            // https://github.com/w3c/webappsec-feature-policy/blob/master/features.md
            // https://developers.google.com/web/updates/2018/06/feature-policy
            // TODO change the value of each rule and check the documentation to see if new features are available
            context.Response.Headers.Add("Feature-Policy", new StringValues(
                "autoplay 'none';" 
                ));

            // MEDU-499 MEDU-512 MEDU-513 MEDU-514
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy
            // TODO change the value of each rule and check the documentation to see if new rules are available
            context.Response.Headers.Add("Content-Security-Policy", new StringValues(
                "upgrade-insecure-requests;" +
                "frame-ancestors 'self';"
                ));

            return _next(context);
        }
    }
}

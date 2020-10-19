namespace ProjectNamePlaceHolder.Web.Extensions
{
    public class BaseHtmlHelper
    {
        protected string PageLoader(string elementId, bool isShowed = false)
        {
            return @"<style>@keyframes spin {0% {transform: rotate(0deg);} 100% { transform: rotate(360deg);}}</style><div id='" + elementId + @"' style='display:" + (isShowed == true ? "block" : "none") + @";width: 100%;height: 100%;position:absolute;background:#000;opacity:0.3;top:0;left:0;z-index:9999999;'><div style='border: 10px solid #f3f3f3;border-top: 10px solid #3498db;border-radius: 50%;width: 60px;height: 60px;animation: spin 2s linear infinite;margin: 0;top: 41%;left: 45%; -ms-transform: translate(-50%, -50%);transform: translate(-50%, -50%);position: absolute;'></div></div>";
        }
    }
}

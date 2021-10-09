using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCAgenda.Extensions
{
    public static class HtmlExtensions
    {
        public static bool IsSelected(this IHtmlHelper htmlHelper, string controllers, string actions)
        {
            var currentAction = htmlHelper.ViewContext.RouteData.Values["action"] as string;
            var currentController = htmlHelper.ViewContext.RouteData.Values["controller"] as string;

            var acceptedActions = (actions ?? currentAction)?.Split(',');
            var acceptedControllers = (controllers ?? currentController)?.Split(',');

            if (acceptedActions == null || acceptedControllers == null)
                return false;

            return acceptedActions.Contains(currentAction) && acceptedControllers.Contains(currentController);
        }
    }
}
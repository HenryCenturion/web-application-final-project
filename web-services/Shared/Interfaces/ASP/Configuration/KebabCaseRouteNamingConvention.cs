using Microsoft.AspNetCore.Mvc.ApplicationModels;
using dtaquito_backend_web_app.Shared.Interfaces.ASP.Configuration.Extensions;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace dtaquito_backend_web_app.Shared.Interfaces.ASP.Configuration;

public class KebabCaseRouteNamingConvention : IControllerModelConvention
{
    private static AttributeRouteModel? ReplaceControllerTemplate(SelectorModel selector, string name)
    {
        return selector.AttributeRouteModel != null
            ? new AttributeRouteModel
            {
                Template = selector.AttributeRouteModel.Template?
                    .Replace("[controller]", name.ToKebabCase())
            }
            : null;
    }

    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors)
        {
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);
        }

        foreach (var selector in controller.Actions.SelectMany(a => a.Selectors))
        {
            selector.AttributeRouteModel = ReplaceControllerTemplate(selector, controller.ControllerName);
        }
    }
}
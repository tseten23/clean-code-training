namespace TemplateEngine;

public class TemplateEngine
{
    private string template = string.Empty;
    private readonly Dictionary<string, string> keyValuePairs = [];

    public string Evaluate()
    {
        foreach (var element in keyValuePairs)
        {
            template = template.Replace("{" + element.Key + "}", element.Value);
        }
        return template;
    }

    public void SetTemplate(string value)
    {
        template = value;
    }

    public void SetVariable(string variable, string value)
    {
        keyValuePairs.Add(variable, value);
    }
}

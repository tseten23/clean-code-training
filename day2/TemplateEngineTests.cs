namespace TemplateEngine.Tests;

public class TemplateEngineTests
{
    [SetUp]
    public void Setup()
    {
    }

    [TestCase("Raghav")]
    [TestCase("Bharath")]
    [TestCase("Shyam")]
    public void ShouldEvaluateOneVariable(string value)
    {
        //Arrange
        TemplateEngine templateEngine = new ();
        templateEngine.SetTemplate("Hello {name}");
        templateEngine.SetVariable("name", value);

        //Act
        string result = templateEngine.Evaluate();

        //Assert
        Assert.That(result, Is.EqualTo("Hello "+value));
    }

    [TestCase("Raghav", "Siemens Healthcare")]
    [TestCase("Bharath", "Google")]
    [TestCase("Shyam", "Siemens Healthcare")]
    public void ShouldEvaluateOneVariable(string name, string company)
    {
        //Arrange
        TemplateEngine templateEngine = new();
        templateEngine.SetTemplate("Hello {name} {company}");
        templateEngine.SetVariable("name", name);
        templateEngine.SetVariable("company", company);

        //Act
        string result = templateEngine.Evaluate();

        //Assert
        Assert.That(result, Is.EqualTo("Hello " + name + " " + company));
    }
}

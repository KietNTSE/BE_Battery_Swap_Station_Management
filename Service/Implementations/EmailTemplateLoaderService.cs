using RazorLight;
using Service.Interfaces;

namespace Service.Implementations;

public class EmailTemplateLoaderService: IEmailTemplateLoaderService
{
    private readonly RazorLightEngine _engine = new RazorLightEngineBuilder()
        .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Templates"))
        .Build();

    public async Task<string> RenderTemplateAsync<T>(string templateName, T model)
    {
        return await _engine.CompileRenderAsync(templateName, model);
    }
}
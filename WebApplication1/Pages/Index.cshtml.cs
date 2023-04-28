using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApplication1.Entityes;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AppDbContext _appDbContext;
        public string targetUrl;

        public IndexModel(ILogger<IndexModel> logger, AppDbContext appDbContext)
        {
            _logger = logger;
            _appDbContext = appDbContext;
        }

        public void OnGet()
        {
            try
            {
                targetUrl = _appDbContext.Settings.FirstOrDefault(s => s.Name.Equals("targetUrl")).Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task<IActionResult> OnPost(string urlString)
        {
            if (string.IsNullOrEmpty(urlString)) return NotFound();
            urlString = urlString.Trim();
            var url = Uri.UnescapeDataString(urlString);
            try
            {
                var settingsTargetUrl = await _appDbContext.Settings.FirstOrDefaultAsync(s => s.Name.Equals("targetUrl"));
                if (settingsTargetUrl != null && !settingsTargetUrl.Value.Equals(urlString))
                {
                    settingsTargetUrl.Value = urlString;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync(url, null);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var fieldsList = await _appDbContext.JsonFields.ToListAsync();

                        foreach (var f in fieldsList)
                        {
                            var start = responseContent.IndexOf(f.FieldName);
                            var substrStart = responseContent[start..];
                            var substrEndIndex =
                                substrStart.IndexOf(",") == -1 ? substrStart.Length - 2 : substrStart.IndexOf(",");
                            var strValue = substrStart[(substrStart.IndexOf(":") + 1)..substrEndIndex];

                            var jData = new JsonData();
                            jData.Name = f.FieldName;
                            jData.Value = strValue.Replace("\"", "").Trim();
                            try
                            {
                                await _appDbContext.AddAsync(jData);
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(ex.Message);
                            }
                        }
                        try
                        {
                            await _appDbContext.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                return RedirectToAction("Get");
            }
        }
    }
}
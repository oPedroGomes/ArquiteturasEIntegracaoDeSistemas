using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BusinessProject.Pages
{
    public class SetBearerTokenModel : PageModel
    {
        public int Comprimento { get; set; }
        public void OnGet()
        {
            Comprimento = 0;
        }
        public IActionResult OnPost(string token) 
        {
            if (!string.IsNullOrEmpty(token))
            {
                Global.BERARER_TOKEN = token;
               return RedirectToPage("/Business");
            }

            Global.BERARER_TOKEN = "";
            return Page();     
        }
    }
}

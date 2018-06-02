using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ecard.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ecard.Pages.Questionnaire
{
    public class IndexModel : PageModel
    {

        // WOWOCO: 1
        [BindProperty]
        public ecard.Model.Questionnaire _myQuestionnaire { get; set; }

        // WOWOCO: 2
        private DbBridge _myDbBridge { get; set; }

        // WOWOCO: 3
        private IConfiguration _myConfiguration { get; set; }

        // WOWOCO: 4
        public IndexModel(DbBridge DbBridge, IConfiguration Configuration)
        {
            _myDbBridge = DbBridge;
            _myConfiguration = Configuration;

        }

        public void OnGet() { }

        [HttpPost]
        public async Task<IActionResult> OnPost()
        {

            if (await isValid())
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _myQuestionnaire.created = DateTime.Now.ToString();
                        _myQuestionnaire.created_ip = this.HttpContext.Connection.RemoteIpAddress.ToString();


                        _myQuestionnaire.Concert = _myQuestionnaire.Concert.Replace("i", "3");
                        _myQuestionnaire.Concert = _myQuestionnaire.Concert.Replace("She said, \"Hello!\"", "");
                        _myQuestionnaire.Costume = _myQuestionnaire.Costume.ToLowerInvariant();
                        _myQuestionnaire.Job = _myQuestionnaire.Job.ToUpperInvariant();

                        //Checkbox Hack 
                        if (_myQuestionnaire.Visit_array != null && _myQuestionnaire.Visit.Any())
                        {
                            _myQuestionnaire.Visit = string.Join(',', _myQuestionnaire.Visit_array);
                        }
                        else
                        {
                            ModelState.AddModelError("_myQuestionnaire.Visit", "Please select one of the following.");
                        }

                        // GET NEW ID ADDED TO THE DATABASE
                        var new_ID = _myQuestionnaire.ID.ToString();

                        //ENCRYPT ID
                        var encrypted_ID = Encoding.UTF8.GetBytes(new_ID);


                        // DB Related add record
                        _myDbBridge.questionnaire.Add(_myQuestionnaire);
                        _myDbBridge.SaveChanges();

                        //REDIRECT to the page with a new operator (name/value pair)
                        return RedirectToPage("Index", new { id = _myQuestionnaire.ID });
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return RedirectToPage("Index");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("_myQuestionnaire.reCaptcha", "Please verify you're not a robot!");
            }

            return Page();

        }

        /**
         * reCAPTHCA SERVER SIDE VALIDATION 
         * 
         *      Create an HttpClient and store the the secret/response pair
         *      Await for the sever to return a json obect 
         * */
        private async Task<bool> isValid()
        {
            var response = this.HttpContext.Request.Form["g-recaptcha-response"];
            if (string.IsNullOrEmpty(response))
                return false;

            try
            {
                using (var client = new HttpClient())
                {
                    var values = new Dictionary<string, string>();
                    //values.Add("secret", "6LfVpjEUAAAAAK0FdygAgh0P1gZ8QU24ildwT86r");
                    values.Add("secret", _myConfiguration["ReCaptcha:PrivateKey"]);

                    values.Add("response", response);
                    //values.Add("remoteip", this.HttpContext.Connection.RemoteIpAddress.ToString()); 

                    var query = new FormUrlEncodedContent(values);

                    var post = client.PostAsync("https://www.google.com/recaptcha/api/siteverify", query);

                    var json = await post.Result.Content.ReadAsStringAsync();

                    if (json == null)
                        return false;

                    var results = JsonConvert.DeserializeObject<dynamic>(json);

                    return results.success;
                }

            }
            catch { }

            return false;
        }

    }
}

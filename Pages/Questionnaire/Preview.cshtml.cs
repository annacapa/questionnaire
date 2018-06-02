using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ecard.Pages.Questionnaire
{
    public class PreviewModel : PageModel
    {
        public void OnGet(string ID)
        {

            if (!string.IsNullOrEmpty(ID))
            {
                //DECRYPT ID
                //var decrypted_ID = Encoding.UTF8.GetString(Convert.FromBase64String(ID));
                //var real_ID = Int32.Parse(decrypted_ID);
                //_myQuestionnaire = _myDbBridge.Questionnaire.Find(ID);

            }

        }
    }
}

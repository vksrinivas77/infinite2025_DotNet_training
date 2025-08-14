using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASP_A1
{
    public partial class Validator : System.Web.UI.Page
    {
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "ok",
                    "alert('All validations passed!');",
                    true
                );
            }
        }
    }
}
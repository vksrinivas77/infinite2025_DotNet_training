using Electricity_Billing.ElectricityBillLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Electricity_Billing.ElectricityBillingWeb
{
    public partial class BillDisplay : System.Web.UI.Page
    {
        protected void btnRetrieve_Click(object sender, EventArgs e)
        {
            int numToRetrieve = int.TryParse(txtNumToRetrieve.Text, out numToRetrieve) ? numToRetrieve : 0;
            if (numToRetrieve <= 0)
            {
                lblError.Text = "Please enter a valid number to retrieve.";
                return;
            }
            ElectricityBoard ebBoard = new ElectricityBoard();
            var bills = ebBoard.Generate_N_BillDetails(numToRetrieve);
            gvBills.DataSource = bills;
            gvBills.DataBind();
        }

       

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("BillEntry.aspx");
        }
    }
}
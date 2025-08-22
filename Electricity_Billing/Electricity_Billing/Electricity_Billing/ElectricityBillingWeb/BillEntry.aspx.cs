using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Electricity_Billing.ElectricityBillLib; // Replace with your actual class library namespace

namespace Electricity_Billing.ElectricityBillingWeb
{
    public partial class BillEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack && ViewState["BillCount"] != null)
            {
                int billCount = (int)ViewState["BillCount"];
                CreateInputFields(billCount);
                btnSubmitBills.Visible = true;
            }
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void btnCreateFields_Click(object sender, EventArgs e)
        {
            int billCount;
            if (!int.TryParse(txtNumBills.Text, out billCount) || billCount <= 0)
            {
                lblResult.Text = "Please enter a valid number of bills.";
                pnlBillInputs.Controls.Clear();
                btnSubmitBills.Visible = false;
                ViewState.Remove("BillCount");
                return;
            }
            ViewState["BillCount"] = billCount;
            CreateInputFields(billCount);
            btnSubmitBills.Visible = true;
            lblResult.Text = "";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtNumBills.Text = "";
            ViewState.Remove("BillCount");
            pnlBillInputs.Controls.Clear();
            lblResult.Text = "";
            btnSubmitBills.Visible = false;
        }

        private void CreateInputFields(int billCount)
        {
            pnlBillInputs.Controls.Clear();
            for (int i = 0; i < billCount; i++)
            {
                pnlBillInputs.Controls.Add(new Literal { Text = $"<hr>Bill #{i + 1}<br />" });
                pnlBillInputs.Controls.Add(new Literal { Text = "Consumer Number: " });
                pnlBillInputs.Controls.Add(new TextBox { ID = $"txtConsumerNumber{i}" });
                pnlBillInputs.Controls.Add(new Literal { Text = "<br />Consumer Name: " });
                pnlBillInputs.Controls.Add(new TextBox { ID = $"txtConsumerName{i}" });
                pnlBillInputs.Controls.Add(new Literal { Text = "<br />Units Consumed: " });
                pnlBillInputs.Controls.Add(new TextBox { ID = $"txtUnits{i}" });
                pnlBillInputs.Controls.Add(new Literal { Text = "<br />" });
            }
        }

        protected void btnSubmitBills_Click(object sender, EventArgs e)
        {
            int billCount = (ViewState["BillCount"] != null) ? (int)ViewState["BillCount"] : 0;
            if (billCount <= 0)
            {
                lblResult.Text = "No bill fields defined. Please enter number of bills and click Proceed.";
                btnSubmitBills.Visible = false;
                pnlBillInputs.Controls.Clear();
                ViewState.Remove("BillCount");
                return;
            }
            ElectricityBoard ebBoard = new ElectricityBoard();
            BillValidator validator = new BillValidator();
            lblResult.Text = "";

            for (int i = 0; i < billCount; i++)
            {
                TextBox txtConsumerNumber = pnlBillInputs.FindControl($"txtConsumerNumber{i}") as TextBox;
                TextBox txtConsumerName = pnlBillInputs.FindControl($"txtConsumerName{i}") as TextBox;
                TextBox txtUnits = pnlBillInputs.FindControl($"txtUnits{i}") as TextBox;

                if (txtConsumerNumber == null || txtConsumerName == null || txtUnits == null)
                {
                    lblResult.Text += $"Missing input field for bill #{i + 1}.<br />";
                    continue;
                }

                string consumerNumber = txtConsumerNumber.Text.Trim();
                string consumerName = txtConsumerName.Text.Trim();
                int unitsConsumed;
                if (!int.TryParse(txtUnits.Text.Trim(), out unitsConsumed))
                {
                    lblResult.Text += $"Bill #{i + 1}: Invalid number of units entered.<br />";
                    continue;
                }

                string validationMsg = validator.ValidateUnitsConsumed(unitsConsumed);
                if (validationMsg != null)
                {
                    lblResult.Text += $"Bill #{i + 1}: {validationMsg}<br />";
                    continue;
                }

                try
                {
                    ElectricityBill ebill = new ElectricityBill
                    {
                        ConsumerNumber = consumerNumber,
                        ConsumerName = consumerName,
                        UnitsConsumed = unitsConsumed
                    };
                    ebBoard.CalculateBill(ebill);
                    ebBoard.AddBill(ebill);
                    lblResult.Text += $"Bill #{i + 1}: {consumerNumber} {consumerName} {unitsConsumed} Bill Amount: {ebill.BillAmount}<br />";
                }
                catch (SqlException sqlEx)
                {
                    // Check if it's a Primary Key violation (duplicate consumer_number)
                    if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                    {
                        lblResult.Text += $"Bill #{i + 1}: Already billed, enter some other ID.<br />";
                    }
                    else
                    {
                        lblResult.Text += $"Bill #{i + 1}: Database Error: {sqlEx.Message}<br />";
                    }
                }
                catch (FormatException ex)
                {
                    lblResult.Text += $"Bill #{i + 1}: {ex.Message}<br />";
                }
                catch (Exception ex)
                {
                    lblResult.Text += $"Bill #{i + 1}: Unexpected error: {ex.Message}<br />";
                }
            }
            // Refresh the bills grid after submission
            BindGrid();
        }

        private void BindGrid()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ElectricityBillDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("SELECT consumer_number, consumer_name, units_consumed, bill_amount FROM ElectricityBill ORDER BY consumer_number", con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt.Load(reader);
                    gvBills.DataSource = dt;
                    gvBills.DataBind();
                }
            }
        }

        protected void btnretrive_Click(object sender, EventArgs e)
        {
            Response.Redirect("BillDisplay.aspx");
        }
    }
}


using System;
using System.Collections.Generic;

namespace ASP_A1
{
    public partial class Products : System.Web.UI.Page
    {
        // Dictionary to store product name, price, and image path
        Dictionary<string, (string price, string imagePath)> productData =
            new Dictionary<string, (string, string)>()
            {
                { "Laptop", ("$800", "images/laptop.jpg") },
                { "Phone", ("$500", "images/phone.jpg") },
                { "headphones", ("$300", "images/headphones.jpg") }
            };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlProducts.Items.Clear();
                ddlProducts.Items.Add("Select a product");
                foreach (var item in productData.Keys)
                {
                    ddlProducts.Items.Add(item);
                }
            }
        }

        protected void ddlProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = ddlProducts.SelectedValue;
            if (productData.ContainsKey(selected))
            {
                imgProduct.ImageUrl = productData[selected].imagePath;
                lblPrice.Text = "";
            }
            else
            {
                imgProduct.ImageUrl = "";
                lblPrice.Text = "";
            }
        }

        protected void btnGetPrice_Click(object sender, EventArgs e)
        {
            string selected = ddlProducts.SelectedValue;
            if (productData.ContainsKey(selected))
            {
                lblPrice.Text = "Price: " + productData[selected].price;
            }
            else
            {
                lblPrice.Text = "Please select a product.";
            }
        }
    }
}

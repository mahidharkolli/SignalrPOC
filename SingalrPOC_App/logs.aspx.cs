using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebRole1
{
    public partial class logs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("logs");
            foreach (string s in LogState.logs.ToList())
            {
                DataRow dr = dt.NewRow();
                dr["logs"] = s;
                dt.Rows.Add(dr);
            }
            datagrid.DataSource = dt;
            datagrid.DataBind();
        }
    }
}
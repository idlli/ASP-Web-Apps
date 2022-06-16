using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionCommercialWeb
{
    public partial class Factures1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Factures fct = new Factures();
            fct.SetDataSource(g.dc.Factures);
            CrystalReportViewer1.ReportSource = fct;
            CrystalReportViewer1.DataBind();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionCommercialWeb
{
    public partial class BonCommandePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BonCommandeR bonC = new BonCommandeR();
            bonC.SetDataSource(g.dc.BonCommandes.Where(o => o.NumCom == Session["NCommande"].ToString()));
            CrystalReportViewer1.ReportSource = bonC;
            CrystalReportViewer1.DataBind();
        }
    }
}